using System.Collections.Generic;
using System.Reflection;

namespace BikeCommander.MotorBike.Core
{
    class MainConstructor
    {
        internal static string bikeKey = null;

        public static Dictionary<string, dynamic> CoreParams = new Dictionary<string, dynamic>()
        {
            { "AuthKey", MotorBike.Core.Security.Authentication.AuthKey() },
            { "BikeName", "EastwoodMotorBikeCore" },
            { "BikeVersion", Assembly.GetEntryAssembly().GetName().Version },
            { "BikeRegistration", "PN17 VAO" },
            { "BikeSecret", "XIuKj7jU/voX+oVyL4PgiFR8wzYWfQwG6Q20ZWhXsVXWn/pfqorlwzvb4ssX66aZhkD0x8jt+RA0UzcWsEfL4sEYXsNvubGU5rMD4H8QCHEXxS998KYlGiBh1ax+IZcc" },
            { "BikeSelectedPowerMode", 2 },
            { "BikeVIN", null },
        };

        public static readonly Dictionary<string, bool> DefaultParams = new Dictionary<string, bool>()
        {
            { "AUTH_OVERRIDE", false },
            { "PROCESS_COMMANDS", true },
            { "GPS_ENABLED", true }
        };

    }
}
