using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace BikeCommander.MotorBike.Core
{
    class MotorBikeCore : MainConstructor
    {
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

        internal static string key;
        internal static int EngineHealth = 0;
        private static SerialPort Arduino;
        private static string[] Ports;
        private static bool ECUAuthGiven = false;
        private static void StartUpProcedure()
        {
            Console.Title = "EastwoodMotorBikeCore";
            Console.WriteLine("Core Started Successfully!");
            Ports = AvailablePorts();

            if (Ports.Length > 0 && !MotorBike.Core.MainConstructor.DefaultParams["DEBUG_MODE"])
            {
                ConnectToArduino(Ports[0]);
                Authenticate();
            }

            EngineHealth += Diagnostic.Electronics.ElectronicDiagnostics.ElectronicCheck();
            EngineHealth += Diagnostic.Engine.EngineDiagnostics.EngineCheck();
         
            if (EngineHealth == 2)
            {
                Management.Engine.EngineManagement.EngineStartPower();
                Management.Engine.EngineManagement.AllowTurnOver = true;
                SendMessage("  Ready 2 Ride   Systems are GO");
            }
            else
            {
                Console.WriteLine("System has failed health check");
            }
        }

        private static void Authenticate()
        {
            while (!MotorBike.Core.Security.Authentication.keyPresent)
            {
                MotorBike.Core.Security.Authentication.CheckKey();
                Thread.Sleep(250);
            }

            Console.WriteLine("sending authentication signal");

            while (!ECUAuthGiven)
            {
                Arduino.Write(key);
                Thread.Sleep(1000);
            }
            Console.WriteLine("ECU Requested Boot Process");
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

        internal static void SendMessage(string Message)
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
            if (Management.Engine.EngineManagement.AllowTurnOver && AllowEngineStart)
            {
                Management.Engine.EngineManagement.StartEngine();
            }
        }
    }
}
