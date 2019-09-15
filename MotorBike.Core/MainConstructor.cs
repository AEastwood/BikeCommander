using System.Collections.Generic;
using System.Reflection;
namespace BikeCommander.MotorBike.Core
{
    class MainConstructor
    {
        public static Dictionary<string, dynamic> CoreParams = new Dictionary<string, dynamic>()
        {
            { "AuthKey", MotorBike.Core.Security.Authentication.AuthKey() },
            { "BikeName", "EastwoodMotorBikeCore" },
            { "BikeVersion", Assembly.GetEntryAssembly().GetName().Version },
            { "BikeRegistration", "PN17 VAO" },
            { "BikeSecret", "CA56B39755AF7BFFDC84E4DDEB1AD60543EB9BB367A5E33E69F87654810B0F76" },
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
