using System;

namespace BikeCommander.MotorBike.Core.Management.Engine
{
    class Functions
    {

        internal static bool AllowTurnOver = false;

        internal static bool Warm {
            get
            {
                return Warm;
            }
            private set
            {
                Warm = (Core.Management.Sensors.Temperature.EngineTemperature() >= 70) ? true : false;
            }
        }

        #region ENGINE_REMOTE
        public static bool EngineStartPower()
        {
            return true;
        }
        public static bool EngineStopPower()
        {
            return true;
        }

        internal static void StartEngine()
        {
            Console.WriteLine("Engine Started");
        }
        #endregion
    }
}
