using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BikeCommander.MotorBike.Core
{
    class MainConstructor
    {
        // Core Params
        internal readonly static Dictionary<string, dynamic> CoreParams = new Dictionary<string, dynamic>()
        {
            { "AuthKey", MotorBike.Core.Security.Authentication.AuthKey() },
            { "AuthKeyLocation", @"D:\ebr2r\key.eb" },
            { "BikeName", "EastwoodMotorBikeCore" },
            { "BikeCoreVersion", Assembly.GetEntryAssembly().GetName().Version },
            { "BikeRegistration", "PN17 VAO" },
            { "BikeSecret", "XIuKj7jU/voX+oVyL4PgiFR8wzYWfQwG6Q20ZWhXsVXWn/pfqorlwzvb4ssX66aZhkD0x8jt+RA0UzcWsEfL4sEYXsNvubGU5rMD4H8QCHEXxS998KYlGiBh1ax+IZcc" },
            { "BikeSelectedPowerMode", 2 },
            { "BikeVIN", null }
        };

        // Default Params
        internal readonly static Dictionary<string, bool> DefaultParams = new Dictionary<string, bool>()
        {
            { "AUTH_OVERRIDE", FileExistsInBaseDir("auth.eb") },
            { "DEBUG_MODE", FileExistsInBaseDir("debug.eb") },
            { "PROCESS_COMMANDS", true },
            { "GPS_ENABLED", true }
        };


        private static bool FileExistsInBaseDir(string filename) => File.Exists(Assembly.GetEntryAssembly().Location.Replace("BikeCommand.exe", filename));
    }
}
