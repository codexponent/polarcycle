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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graph));
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zoomButton = new MetroFramework.Controls.MetroButton();
            this.chbx_HR = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_SPD = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_CAD = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_ALT = new MetroFramework.Controls.MetroCheckBox();
            this.chbx_PWR = new MetroFramework.Controls.MetroCheckBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.AvgHR = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.AvgAlt = new MetroFramework.Controls.MetroLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.AvgPow = new MetroFramework.Controls.MetroLabel();
            this.AvgCad = new MetroFramework.Controls.MetroLabel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.AvgFtp = new MetroFramework.Controls.MetroLabel();
            this.AvgSpd = new MetroFramework.Controls.MetroLabel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.AvgTSS = new MetroFramework.Controls.MetroLabel();
            this.AvgNorm = new MetroFramework.Controls.MetroLabel();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.metroLabel15 = new MetroFramework.Controls.MetroLabel();
            this.AvgIF = new MetroFramework.Controls.MetroLabel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(37, 63);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1284, 466);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraphControl1_MouseDownEvent);
            this.zedGraphControl1.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraphControl1_MouseUpEvent);
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // zoomButton
            // 
            this.zoomButton.Location = new System.Drawing.Point(101, 28);
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
            this.chbx_HR.Checked = true;
            this.chbx_HR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_HR.Location = new System.Drawing.Point(414, 36);
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
            this.chbx_SPD.Checked = true;
            this.chbx_SPD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_SPD.Location = new System.Drawing.Point(533, 36);
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
            this.chbx_CAD.Checked = true;
            this.chbx_CAD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_CAD.Location = new System.Drawing.Point(652, 36);
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
            this.chbx_ALT.Checked = true;
            this.chbx_ALT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_ALT.Location = new System.Drawing.Point(771, 36);
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
            this.chbx_PWR.Checked = true;
            this.chbx_PWR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_PWR.Location = new System.Drawing.Point(890, 36);
            this.chbx_PWR.Name = "chbx_PWR";
            this.chbx_PWR.Size = new System.Drawing.Size(56, 15);
            this.chbx_PWR.TabIndex = 2;
            this.chbx_PWR.Text = "Power";
            this.chbx_PWR.UseSelectable = true;
            this.chbx_PWR.CheckedChanged += new System.EventHandler(this.chbx_PWR_CheckedChanged);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(1277, 28);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 3;
            this.metroButton1.Text = "Back";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(37, 555);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 62);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(37, 655);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 62);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(133, 555);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(121, 19);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Average HeartRate";
            // 
            // AvgHR
            // 
            this.AvgHR.AutoSize = true;
            this.AvgHR.Location = new System.Drawing.Point(133, 574);
            this.AvgHR.Name = "AvgHR";
            this.AvgHR.Size = new System.Drawing.Size(12, 19);
            this.AvgHR.TabIndex = 5;
            this.AvgHR.Text = ".";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(133, 655);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(107, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Average Altitude";
            // 
            // AvgAlt
            // 
            this.AvgAlt.AutoSize = true;
            this.AvgAlt.Location = new System.Drawing.Point(133, 674);
            this.AvgAlt.Name = "AvgAlt";
            this.AvgAlt.Size = new System.Drawing.Size(12, 19);
            this.AvgAlt.TabIndex = 5;
            this.AvgAlt.Text = ".";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(301, 555);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(67, 62);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(301, 655);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(67, 62);
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(397, 555);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(113, 19);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Average Cadence";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(397, 655);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(98, 19);
            this.metroLabel4.TabIndex = 5;
            this.metroLabel4.Text = "Average Power";
            // 
            // AvgPow
            // 
            this.AvgPow.AutoSize = true;
            this.AvgPow.Location = new System.Drawing.Point(397, 674);
            this.AvgPow.Name = "AvgPow";
            this.AvgPow.Size = new System.Drawing.Size(12, 19);
            this.AvgPow.TabIndex = 5;
            this.AvgPow.Text = ".";
            // 
            // AvgCad
            // 
            this.AvgCad.AutoSize = true;
            this.AvgCad.Location = new System.Drawing.Point(397, 574);
            this.AvgCad.Name = "AvgCad";
            this.AvgCad.Size = new System.Drawing.Size(12, 19);
            this.AvgCad.TabIndex = 5;
            this.AvgCad.Text = ".";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(563, 555);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(67, 62);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(563, 655);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(67, 62);
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(659, 555);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(99, 19);
            this.metroLabel7.TabIndex = 5;
            this.metroLabel7.Text = "Average Speed";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(659, 655);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(31, 19);
            this.metroLabel8.TabIndex = 5;
            this.metroLabel8.Text = "FTP";
            // 
            // AvgFtp
            // 
            this.AvgFtp.AutoSize = true;
            this.AvgFtp.Location = new System.Drawing.Point(659, 674);
            this.AvgFtp.Name = "AvgFtp";
            this.AvgFtp.Size = new System.Drawing.Size(12, 19);
            this.AvgFtp.TabIndex = 5;
            this.AvgFtp.Text = ".";
            // 
            // AvgSpd
            // 
            this.AvgSpd.AutoSize = true;
            this.AvgSpd.Location = new System.Drawing.Point(659, 574);
            this.AvgSpd.Name = "AvgSpd";
            this.AvgSpd.Size = new System.Drawing.Size(12, 19);
            this.AvgSpd.TabIndex = 5;
            this.AvgSpd.Text = ".";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(823, 555);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(67, 62);
            this.pictureBox7.TabIndex = 4;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(823, 655);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(67, 62);
            this.pictureBox8.TabIndex = 4;
            this.pictureBox8.TabStop = false;
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(919, 555);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(78, 19);
            this.metroLabel11.TabIndex = 5;
            this.metroLabel11.Text = "Normalized";
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.Location = new System.Drawing.Point(919, 655);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(130, 19);
            this.metroLabel12.TabIndex = 5;
            this.metroLabel12.Text = "Training Strees Score";
            // 
            // AvgTSS
            // 
            this.AvgTSS.AutoSize = true;
            this.AvgTSS.Location = new System.Drawing.Point(919, 674);
            this.AvgTSS.Name = "AvgTSS";
            this.AvgTSS.Size = new System.Drawing.Size(12, 19);
            this.AvgTSS.TabIndex = 5;
            this.AvgTSS.Text = ".";
            // 
            // AvgNorm
            // 
            this.AvgNorm.AutoSize = true;
            this.AvgNorm.Location = new System.Drawing.Point(919, 574);
            this.AvgNorm.Name = "AvgNorm";
            this.AvgNorm.Size = new System.Drawing.Size(12, 19);
            this.AvgNorm.TabIndex = 5;
            this.AvgNorm.Text = ".";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(1091, 600);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(67, 62);
            this.pictureBox9.TabIndex = 4;
            this.pictureBox9.TabStop = false;
            // 
            // metroLabel15
            // 
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.Location = new System.Drawing.Point(1187, 600);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(95, 19);
            this.metroLabel15.TabIndex = 5;
            this.metroLabel15.Text = "Intensity Factor";
            // 
            // AvgIF
            // 
            this.AvgIF.AutoSize = true;
            this.AvgIF.Location = new System.Drawing.Point(1187, 619);
            this.AvgIF.Name = "AvgIF";
            this.AvgIF.Size = new System.Drawing.Size(12, 19);
            this.AvgIF.TabIndex = 5;
            this.AvgIF.Text = ".";
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(183, 28);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(75, 23);
            this.metroButton2.TabIndex = 6;
            this.metroButton2.Text = "Interval";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 740);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.AvgIF);
            this.Controls.Add(this.AvgNorm);
            this.Controls.Add(this.AvgSpd);
            this.Controls.Add(this.AvgCad);
            this.Controls.Add(this.AvgHR);
            this.Controls.Add(this.AvgTSS);
            this.Controls.Add(this.AvgFtp);
            this.Controls.Add(this.AvgPow);
            this.Controls.Add(this.AvgAlt);
            this.Controls.Add(this.metroLabel12);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel15);
            this.Controls.Add(this.metroLabel11);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.chbx_PWR);
            this.Controls.Add(this.chbx_ALT);
            this.Controls.Add(this.chbx_CAD);
            this.Controls.Add(this.chbx_SPD);
            this.Controls.Add(this.chbx_HR);
            this.Controls.Add(this.zoomButton);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "Graph";
            this.Text = "Graph";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Graph_FormClosed);
            this.Load += new System.EventHandler(this.Graph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
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
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel AvgHR;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel AvgAlt;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel AvgPow;
        private MetroFramework.Controls.MetroLabel AvgCad;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel AvgFtp;
        private MetroFramework.Controls.MetroLabel AvgSpd;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroLabel AvgTSS;
        private MetroFramework.Controls.MetroLabel AvgNorm;
        private System.Windows.Forms.PictureBox pictureBox9;
        private MetroFramework.Controls.MetroLabel metroLabel15;
        private MetroFramework.Controls.MetroLabel AvgIF;
        private MetroFramework.Controls.MetroButton metroButton2;
    }
}