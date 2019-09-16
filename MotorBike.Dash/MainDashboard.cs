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
            MotorBike.Core.Diagnostic.Electronics.Functions.ElectronicHealth++;

            mainDashboard.Location = new Point(
                DashboardConstructor.DashboardParams["LOCATION_X"], 
                DashboardConstructor.DashboardParams["LOCATION_X"]
            );
        }

        private void MainDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            MotorBike.Core.CoreFunctions.ConsoleUpdate("dashboard closed");
        }

        internal static void UpdateLabel(string label, string text)
        {
            var control = mainDashboard.Controls.OfType<Label>().FirstOrDefault(c => c.Name == label);

            if (control != null)
            {
                control.Text = text;
            }
        }
    }
}
