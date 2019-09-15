using System;
using System.Threading;

namespace BikeCommander
{
    class Program
    {
        static void Main(string[] args)
        {

            if (MotorBike.Core.Security.Authentication.KeyPresent(MotorBike.Core.Security.Authentication.AuthKey()))
            {
                try
                {
                    Thread CommandThread = new Thread(() => MotorBike.Core.MotorBikeCore.CoreStart());
                    CommandThread.Start();
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unable to start motorcycle core");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("INVALID_KEY");
                Console.Read();
            }

        }
    }
}
