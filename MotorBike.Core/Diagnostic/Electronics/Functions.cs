using System;
using System.Threading;

namespace BikeCommander.MotorBike.Core.Diagnostic.Electronics
{
    class ElectronicDiagnostics
    {
        internal static int ElectronicHealth = 0;
#pragma warning disable IDE0044 // Add readonly modifier
        private static MotorBike.Dash.MainDashboard mainDashboardForm = new MotorBike.Dash.MainDashboard();
#pragma warning restore IDE0044 // Add readonly modifier
        internal static int ElectronicCheck()
        {
            Console.WriteLine(string.Format("Mode selected: {0}", Core.MainConstructor.CoreParams["BikeSelectedPowerMode"]));
            Thread DashThread = new Thread(() => mainDashboardForm.ShowDialog());
            DashThread.Start();
            
            MotorBike.Core.MotorBikeCore.SendMessage("Checking        Electronics");
            MotorBike.Core.MotorBikeCore.SendMessage("Electronics OK!");

            Console.WriteLine("Dashboard loaded");
            ElectronicHealth++;
            return ElectronicHealth;
        }

    }
}
