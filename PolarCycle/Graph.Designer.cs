namespace PolarCycle {
    partial class Graph {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zoomButton = new MetroFramework.Controls.MetroButton();
            this.chbx_HR = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_SPD = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_CAD = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_ALT = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_PWR = new MetroFramework.Controls.MetroCheckBox();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(50, 159);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1226, 527);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // zoomButton
            // 
            this.zoomButton.Location = new System.Drawing.Point(305, 86);
            this.zoomButton.Name = "zoomButton";
            this.zoomButton.Size = new System.Drawing.Size(75, 23);
            this.zoomButton.TabIndex = 1;
            this.zoomButton.Text = "Zoom";
            this.zoomButton.UseSelectable = true;
            this.zoomButton.Click += new System.EventHandler(this.zoomButton_Click);
            this.zoomButton.MouseLeave += new System.EventHandler(this.zoomButton_MouseLeave);
            this.zoomButton.MouseHover += new System.EventHandler(this.zoomButton_MouseHover);
            // 
            // chbx_HR
            // 
            this.chbx_HR.AutoSize = true;
            this.chbx_HR.Location = new System.Drawing.Point(50, 121);
            this.chbx_HR.Name = "chbx_HR";
            this.chbx_HR.Size = new System.Drawing.Size(75, 15);
            this.chbx_HR.TabIndex = 2;
            this.chbx_HR.Text = "HeartRate";
            this.chbx_HR.UseSelectable = true;
            this.chbx_HR.CheckedChanged += new System.EventHandler(this.chbx_HR_CheckedChanged);
            // 
            // chbx_SPD
            // 
            this.chbx_SPD.AutoSize = true;
            this.chbx_SPD.Location = new System.Drawing.Point(169, 121);
            this.chbx_SPD.Name = "chbx_SPD";
            this.chbx_SPD.Size = new System.Drawing.Size(55, 15);
            this.chbx_SPD.TabIndex = 2;
            this.chbx_SPD.Text = "Speed";
            this.chbx_SPD.UseSelectable = true;
            this.chbx_SPD.CheckedChanged += new System.EventHandler(this.chbx_SPD_CheckedChanged);
            // 
            // chbx_CAD
            // 
            this.chbx_CAD.AutoSize = true;
            this.chbx_CAD.Location = new System.Drawing.Point(288, 121);
            this.chbx_CAD.Name = "chbx_CAD";
            this.chbx_CAD.Size = new System.Drawing.Size(69, 15);
            this.chbx_CAD.TabIndex = 2;
            this.chbx_CAD.Text = "Cadence";
            this.chbx_CAD.UseSelectable = true;
            this.chbx_CAD.CheckedChanged += new System.EventHandler(this.chbx_CAD_CheckedChanged);
            // 
            // chbx_ALT
            // 
            this.chbx_ALT.AutoSize = true;
            this.chbx_ALT.Location = new System.Drawing.Point(407, 121);
            this.chbx_ALT.Name = "chbx_ALT";
            this.chbx_ALT.Size = new System.Drawing.Size(65, 15);
            this.chbx_ALT.TabIndex = 2;
            this.chbx_ALT.Text = "Altitude";
            this.chbx_ALT.UseSelectable = true;
            this.chbx_ALT.CheckedChanged += new System.EventHandler(this.chbx_ALT_CheckedChanged);
            // 
            // chbx_PWR
            // 
            this.chbx_PWR.AutoSize = true;
            this.chbx_PWR.Location = new System.Drawing.Point(526, 121);
            this.chbx_PWR.Name = "chbx_PWR";
            this.chbx_PWR.Size = new System.Drawing.Size(56, 15);
            this.chbx_PWR.TabIndex = 2;
            this.chbx_PWR.Text = "Power";
            this.chbx_PWR.UseSelectable = true;
            this.chbx_PWR.CheckedChanged += new System.EventHandler(this.chbx_PWR_CheckedChanged);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 740);
            this.Controls.Add(this.chbx_PWR);
            this.Controls.Add(this.chbx_ALT);
            this.Controls.Add(this.chbx_CAD);
            this.Controls.Add(this.chbx_SPD);
            this.Controls.Add(this.chbx_HR);
            this.Controls.Add(this.zoomButton);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "Graph";
            this.Text = "Graph";
            this.Load += new System.EventHandler(this.Graph_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private MetroFramework.Controls.MetroButton zoomButton;
        private MetroFramework.Controls.MetroCheckBox chbx_HR;
        private MetroFramework.Controls.MetroCheckBox chbx_SPD;
        private MetroFramework.Controls.MetroCheckBox chbx_CAD;
        private MetroFramework.Controls.MetroCheckBox chbx_ALT;
        private MetroFramework.Controls.MetroCheckBox chbx_PWR;
    }
}