using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace BikeCommander.MotorBike.Core
{
    class MotorBikeCore : MainConstructor
    {
        private static readonly Dash.MainDashboard mainDashboardForm = new Dash.MainDashboard();
        public static bool ProcessCommands = false;
        public static void CoreStart()
        {
            StartUpProcedure();
            while (DefaultParams["PROCESS_COMMANDS"])
            {
                CommandManager.ProcessCommand(null);
                Thread.Sleep(25);
            }
        }

        private static int EngineHealth = 0;
        private static SerialPort Arduino;
        private static string[] Ports;
        private static bool ECUAuthGiven = false;
        private static void StartUpProcedure()
        {
            Console.Title = "EastwoodMotorBikeCore.CORE_STATUS: OK";
            Console.WriteLine("Core Started Successfully!");

            Ports = AvailablePorts();

            if (Ports.Length > 0)
            {
                ConnectToArduino(Ports[0]);
            }

            while (!ECUAuthGiven)
            {
                Thread.Sleep(250);
            }

            Console.WriteLine("ECU Requested Boot Process");

            SendMessage("Checking        Electronics");
            EngineHealth += Diagnostic.Electronics.Functions.ElectronicCheck();
            SendMessage("Electronics OK!");
            Thread.Sleep(1000);

            SendMessage("Checking Engine");
            EngineHealth += Diagnostic.Engine.Functions.EngineCheck();
            SendMessage("Engine OK!");
            Thread.Sleep(1000);

            if (EngineHealth == 2)
            {
                Management.Engine.Functions.EngineStartPower();
                Management.Engine.Functions.AllowTurnOver = true;
                SendMessage("  Ready 2 Ride   Systems are GO");
            }
            else
            {
                Console.WriteLine("System has failed health check");
            }

            mainDashboardForm.Dispose();
        }

        private static dynamic SerialCommand = null;
        private static void ArduinoCommandHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Arduino.DtrEnable = true;
            Arduino.RtsEnable = true;

            int CommandBuffer = Arduino.BytesToRead;
            byte[] CommandByteBuffer = new byte[CommandBuffer];

            SerialCommand = Arduino.Read(CommandByteBuffer, 0, CommandBuffer);
            SerialCommand = Encoding.UTF8.GetString(CommandByteBuffer, 0, CommandBuffer);

            MotorBike.Dash.MainDashboard.UpdateLabel("speedLabel", SerialCommand);

            switch (SerialCommand)
            {
                case "BOOT":
                    ECUAuthGiven = true;
                    return;

                case "START":
                    StartEngine();
                    return;
            }
        }

        private static string[] AvailablePorts()
        {
            Ports = SerialPort.GetPortNames();
            return Ports;
        }

        private static bool Connected = false;
        private static void ConnectToArduino(string Port)
        {
            Connected = true;
            Arduino = new SerialPort(Port, 9600, Parity.None, 8, StopBits.One);
            Arduino.DataReceived += new SerialDataReceivedEventHandler(ArduinoCommandHandler);
            Arduino.Open();
            Console.WriteLine(string.Format("Connected to: {0}", Ports[0]));
        }

        private static void SendMessage(string Message)
        {
            try
            {
                if (Connected)
                {
                    Arduino.Write("#STAR\n");
                    Thread.Sleep(1000);
                    Arduino.Write(string.Format("#TEXT{0}#\n", Message));
                }
            }
            catch (Exception)
            {
                
            }
        }

        #pragma warning disable IDE0044
        private static bool AllowEngineStart = false;
        #pragma warning restore IDE0044
        private static void StartEngine()
        {
            if (Management.Engine.Functions.AllowTurnOver && AllowEngineStart)
            {
                Management.Engine.Functions.StartEngine();
            }
        }
    }
}
