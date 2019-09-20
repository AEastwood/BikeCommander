using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BikeCommander.MotorBike.Dash
{
    public partial class MainDashboard : Form
    {
        internal static MainDashboard mainDashboard = new MainDashboard();
        public MainDashboard()
        {
            InitializeComponent();
        }

        private void MainDashboard_Load(object sender, System.EventArgs e)
        {
            MotorBike.Core.Diagnostic.Electronics.ElectronicDiagnostics.ElectronicHealth++;

            mainDashboard.Location = new Point(
                DashboardConstructor.DashboardParams["LOCATION_X"],
                DashboardConstructor.DashboardParams["LOCATION_X"]
            );

            DashboardSetup();
        }

        private delegate void BackColorInvoke(Color color);
        private void DashboardSetup()
        {
            this.revCounter1.Value = 88;
        }

        private void MainDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            MotorBike.Core.CoreFunctions.ConsoleUpdate("dashboard closed");
        }
        
    }
}
