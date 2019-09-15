using System.Collections.Generic;

namespace BikeCommander.MotorBike.Core.Management.Engine
{
    class PowerModes
    {

        internal static int SelectedPowerMode = Core.MainConstructor.CoreParams["BikeSelectedPowerMode"];
        internal static object PowerModeSelector(int PowerMode = 2)
        {
            Dash.MainDashboard mainDashboard = new Dash.MainDashboard();

            if (!Core.Management.Engine.Functions.Warm && PowerMode == 4)
            {
                PowerMode = 1;
                mainDashboard.speedLabel.Text = "not available";
            }

            Dictionary<string, dynamic> BeastMode = new Dictionary<string, dynamic>()
            {
                {"MaxThrottle", 100 },
                {"MaxRPM", 16000 },
                {"MaxHP", 200 },
                {"TCS", true },
                {"AWC", true },
                {"EV", 1 }
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
    }
}
