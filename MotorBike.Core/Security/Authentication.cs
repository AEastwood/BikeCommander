using System;

namespace BikeCommander.MotorBike.Core.Security
{
    class Authentication
    {

        internal static string AuthKey()
        {
            return "CA56B39755AF7BFFDC84E4DDEB1AD60543EB9BB367A5E33E69F87654810B0F76";
        }

        public static bool KeyPresent(string AuthKey)
        {
            if (AuthKey == MotorBike.Core.MainConstructor.CoreParams["BikeSecret"])
            {
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            return false;
        }
    }
}
