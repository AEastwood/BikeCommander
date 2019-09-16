using System;
using System.Threading;
using BikeCommander.Framework.DataTypes;

namespace BikeCommander.MotorBike.Core.Diagnostic.Electronics
{
    class Functions
    {
        internal static int ElectronicHealth = 0;
        internal static int ElectronicCheck()
        {
            MotorBike.Dash.MainDashboard mainDashboardForm = new MotorBike.Dash.MainDashboard();
            Console.WriteLine(string.Format("Mode selected: ", Core.MainConstructor.CoreParams["BikeSelectedPowerMode"]));
            Thread DashThread = new Thread(() => mainDashboardForm.ShowDialog());
            DashThread.Start();

            Console.WriteLine("Dashboard loaded");
            ElectronicHealth++;
            return ElectronicHealth;
        }

    }
}
