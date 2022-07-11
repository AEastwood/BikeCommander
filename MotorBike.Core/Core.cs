using BikeCommander.MotorBike.Core.Security;
using System;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;

namespace BikeCommander.MotorBike.Core
{
    class MotorBikeCore : MainConstructor
    {
        private static bool AllowEngineStart = false;
        private static SerialPort Arduino;
        private static bool Connected = false;
        private static bool ECUAuthGiven = false;
        internal static int EngineHealth = 0;
        internal static string key;
        private static string[] Ports;
        public static bool ProcessCommands = false;
        private static dynamic SerialCommand = null;

        private static void Authenticate()
        {
            Authentication.bikePath = MotorBike.Core.MainConstructor.CoreParams["AuthKeyLocation"];

            Console.WriteLine("Authenticating with key..");

            while (!MotorBike.Core.Security.Authentication.keyPresent)
            {
                MotorBike.Core.Security.Authentication.KeyPresent();
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

        private static string[] AvailablePorts() => SerialPort.GetPortNames();

        private static void ConnectToArduino(string Port)
        {
            Arduino = new SerialPort(Port, 9600, Parity.None, 8, StopBits.One);
            Arduino.DataReceived += new SerialDataReceivedEventHandler(ArduinoCommandHandler);
            Arduino.Open();

            Connected = true;

            Console.WriteLine(string.Format("Connected to: {0}", Port));
        }

        public static void CoreStart()
        {
            Console.WriteLine(Assembly.GetEntryAssembly().Location + "\\debug.eb");
            
            if (MotorBike.Core.MainConstructor.DefaultParams["DEBUG_MODE"])
                Console.WriteLine("DEBUG MODE ENABLED!!!");

            StartUpProcedure();

            Console.WriteLine("Awaiting Commands..");

            while (MotorBike.Core.MainConstructor.DefaultParams["PROCESS_COMMANDS"])
            {
                CommandManager.ProcessCommand(null);
                Thread.Sleep(25);
            }
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

        private static void StartEngine()
        {
            if (Management.Engine.EngineManagement.AllowTurnOver && AllowEngineStart)
                Management.Engine.EngineManagement.StartEngine();
        }

        private static void StartUpProcedure()
        {
            Console.Title = "EastwoodMotorBikeCore";
            Console.WriteLine("Core Started Successfully!");

            Ports = AvailablePorts();

            if (Ports.Length > 0 && !MotorBike.Core.MainConstructor.DefaultParams["DEBUG_MODE"])
            {
                ConnectToArduino(Ports[0]);
                
                if(!MotorBike.Core.MainConstructor.DefaultParams["AUTH_OVERRIDE"]) Authenticate();
            }

            EngineHealth += Diagnostic.Electronics.ElectronicDiagnostics.ElectronicCheck();
            EngineHealth += Diagnostic.Engine.EngineDiagnostics.EngineCheck();

            if (EngineHealth < 2)
            {
                Console.WriteLine("System has failed health check");
                return;
            }

            Management.Engine.EngineManagement.EngineStartPower();
            Management.Engine.EngineManagement.AllowTurnOver = true;
            SendMessage("  Ready 2 Ride   Systems are GO");
        }
    }
}
