namespace BikeCommander.MotorBike.Dash
{
    partial class MainDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.revCounter1 = new BikeCommander.Framework.Controls.RevCounter();
            this.SuspendLayout();
            // 
            // revCounter1
            // 
            this.revCounter1.BackColor = System.Drawing.SystemColors.Control;
            this.revCounter1.BarColor1 = System.Drawing.Color.Orange;
            this.revCounter1.BarColor2 = System.Drawing.Color.Orange;
            this.revCounter1.BarWidth = 14F;
            this.revCounter1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.revCounter1.ForeColor = System.Drawing.Color.DimGray;
            this.revCounter1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.revCounter1.LineColor = System.Drawing.Color.DimGray;
            this.revCounter1.LineWidth = 1;
            this.revCounter1.Location = new System.Drawing.Point(586, 12);
            this.revCounter1.Maximum = ((long)(100));
            this.revCounter1.MinimumSize = new System.Drawing.Size(100, 100);
            this.revCounter1.Name = "revCounter1";
            this.revCounter1.ProgressShape = BikeCommander.Framework.Controls.RevCounter.ProgessShape.Flat;
            this.revCounter1.Size = new System.Drawing.Size(186, 186);
            this.revCounter1.TabIndex = 0;
            this.revCounter1.Text = null;
            this.revCounter1.TextMode = BikeCommander.Framework.Controls.RevCounter.Textmode.Percentage;
            this.revCounter1.Value = ((long)(57));
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.revCounter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainDashboard";
            this.Text = "MainDashboard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainDashboard_FormClosed);
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.RevCounter revCounter1;
    }
}