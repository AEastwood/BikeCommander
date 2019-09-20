using System;
using System.Collections.Generic;
using System.Threading;

namespace BikeCommander.MotorBike.Core.Management.Engine
{
    class PowerModes
    {

        internal static int SelectedPowerMode = Core.MainConstructor.CoreParams["BikeSelectedPowerMode"];
        private static bool BeastModeSelected = false;
        internal static object PowerModeSelector(int PowerMode = 2)
        {
            SelectedPowerMode = PowerMode;
            Dash.MainDashboard mainDashboard = new Dash.MainDashboard();

            if (!Core.Management.Engine.EngineManagement.Warm && PowerMode == 4)
            {
                BeastModeSelected = true;
                PowerMode = 1;
                Thread AwaitBeastMode = new Thread(() => AwaitBeastModeThread());
                AwaitBeastMode.Start();
            }

            Dictionary<string, dynamic> BeastMode = new Dictionary<string, dynamic>()
            {
                {"MaxThrottle", 100 },
                {"MaxRPM", 16000 },
                {"MaxHP", 200 },
                {"TCS", true },
                {"AWC", true },
                {"EV", 60.0f }
            };

            Dictionary<string, dynamic> PowerMode1 = new Dictionary<string, dynamic>()
            {
                {"MaxThrottle", 95 },
                {"MaxRPM", 15000 },
                {"MaxHP", 190 },
                {"TCS", true },
                {"AWC", true },
                {"EV", 0 }
            };

            Dictionary<string, dynamic> PowerMode2 = new Dictionary<string, dynamic>()
            {
                {"MaxThrottle", 90 },
                {"MaxRPM", 14000 },
                {"MaxHP", 175 },
                {"TCS", true },
                {"AWC", true },
                {"EV", 0 }

            };

            Dictionary<string, dynamic> PowerMode3 = new Dictionary<string, dynamic>()
            {
                {"MaxThrottle", 80 },
                {"MaxRPM", 12500 },
                {"MaxHP", 130 },
                {"TCS", 5 },
                {"AWC", true },
                {"EV", 0 }
            };

            switch (PowerMode)
            {
                case 1:
                    return PowerMode1;

                case 2:
                    return PowerMode2;

                case 3:
                    return PowerMode3;

                case 4:
                    return BeastMode;


                default:
                    return PowerMode2;
            }
        }

        private static void AwaitBeastModeThread()
        {
            while (BeastModeSelected)
            {
                if (Core.Management.Engine.EngineManagement.Warm && BeastModeSelected)
                {
                    SelectedPowerMode = 4;
                    MotorBike.Core.Management.Exhaust.ExhaustFunctions.ExhaustPositionModify(60.0f);
                }
                Thread.Sleep(500);
            }

            Thread.CurrentThread.Abort();
        }
    }
}
