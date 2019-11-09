using System;
using System.Threading;

namespace BikeCommander
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
