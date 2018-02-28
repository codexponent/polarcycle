using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using ZedGraph;

namespace PolarCycle {
    public partial class Graph : MetroForm {
        private string fileName;
        private string MaxHR;
        private int speedRateMax;
        private int altitudeRateMax;
        private int powerRateMax;
        private string StartTime;
        private string Version;
        private int seconds;
        DateTime myDateTime1;
        GraphPane myPane;
        MetroFramework.Controls.MetroGrid metroGrid1;
        PointPairList hrList;
        PointPairList speedList;
        PointPairList cadenceList;
        PointPairList altitudeList;
        PointPairList powerList;
        private string SMode;
        private string SMode0;
        private string SMode1;
        private string SMode2;
        private string SMode3;
        private string SMode4;
        private string SMode5;
        private string SMode6;
        private string SMode7;
        private string SMode8;
        LineItem hrCurve, speedCurve, cadenceCurve, altitudeCurve, powerCurve, powerBalanceCurve;
        public Graph() {
            InitializeComponent();
        }
        /// <summary>
        /// Getting the Information from the Upload File 
        /// </summary>
        /// <param name="fileName">Uploaded FileName</param>
        public void getInformation(string fileName, 
                                    string MaxHR, 
                                    string StartTime, 
                                    MetroFramework.Controls.MetroGrid metroGrid1, 
                                    string Version,
                                    string SMode,
                                    string SMode0,
                                    string SMode1,
                                    string SMode2,
                                    string SMode3,
                                    string SMode4,
                                    string SMode5,
                                    string SMode6,
                                    string SMode7,
                                    string SMode8,
                                    int seconds) {
            this.fileName = fileName;
            this.MaxHR = MaxHR;
            this.StartTime = StartTime;
            this.metroGrid1 = metroGrid1;
            this.Version = Version;
            this.SMode = SMode;
            this.SMode0 = SMode0;
            this.SMode1 = SMode1;
            this.SMode2 = SMode2;
            this.SMode3 = SMode3;
            this.SMode4 = SMode4;
            this.SMode5 = SMode5;
            this.SMode6 = SMode6;
            this.SMode7 = SMode7;
            this.SMode8 = SMode8;
            this.seconds = seconds;
        }

        private void chbx_HR_CheckedChanged(object sender, EventArgs e) {
            hrCurve.IsVisible = !chbx_HR.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_SPD_CheckedChanged(object sender, EventArgs e) {
            speedCurve.IsVisible = !chbx_SPD.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_CAD_CheckedChanged(object sender, EventArgs e) {
            cadenceCurve.IsVisible = !chbx_CAD.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_ALT_CheckedChanged(object sender, EventArgs e) {
            altitudeCurve.IsVisible = !chbx_ALT.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_PWR_CheckedChanged(object sender, EventArgs e) {
            powerCurve.IsVisible = !chbx_PWR.Checked;
            zedGraphControl1.Invalidate();
        }

        private void zoomButton_Click(object sender, EventArgs e) {
            zedGraphControl1.ZoomOutAll(myPane);
            zedGraphControl1.Invalidate();
        }

        private void Graph_Load(object sender, EventArgs e) {
            // Setting Zedgraph component 
            myPane = zedGraphControl1.GraphPane;
            // Zedgrapg title
            myPane.Title.Text = "HRM Cycle Application Graph";
            // Xaxis title
            myPane.XAxis.Title.Text = "Parameters";
            // Yaxis title
            myPane.YAxis.Title.Text = "Parameters Values";

            Console.WriteLine("Checking Here");
            Console.WriteLine(metroGrid1);
            Console.WriteLine(metroGrid1.Rows.Count);
            Console.WriteLine(metroGrid1.Columns.Count);

            drawGraph();
        }
        /// <summary>
        /// Draws the Graph
        /// </summary>
        private void drawGraph() {
            // Displaying heart rate in yaxis in red color with max scale value lblmaxHR
            var YAxis = myPane.AddYAxis("Heart Rate");
            myPane.YAxis.Color = System.Drawing.Color.Red;
            myPane.YAxis.Scale.Max = int.Parse(MaxHR);

            // Displaying speed in yaxis in green color with max scale value speedrateMAX
            var Y2Axis = myPane.AddYAxis("Speed");
            myPane.Y2Axis.Color = System.Drawing.Color.Green;
            myPane.Y2Axis.Scale.Max = speedRateMax;

            // Displaying cadence in yaxis in blue color
            var Y3Axis = myPane.AddYAxis("Cadence");
            myPane.YAxisList[Y3Axis].Color = System.Drawing.Color.Blue;
            //myPane.YAxisList[Y3Axis].Scale.Max = int.Parse(lbl.Text);

            // Displaying altitude in yaxis gray color with max scale value altituderateMAX
            var Y4Axis = myPane.AddYAxis("Altitude");
            myPane.YAxisList[Y4Axis].Color = System.Drawing.Color.Gray;
            myPane.YAxisList[Y4Axis].Scale.Max = altitudeRateMax;

            // Displaying power in yaxis in magenta color with max scale value powerrateMax
            var Y5Axis = myPane.AddYAxis("Power");
            myPane.YAxisList[Y5Axis].Color = System.Drawing.Color.Magenta;
            myPane.YAxisList[Y5Axis].Scale.Max = powerRateMax;

            // Setting starttime and end time
            DateTime startDate = myDateTime1;
            DateTime endDate = startDate.AddSeconds(seconds * metroGrid1.Rows.Count);

            Console.WriteLine("startDate");
            Console.WriteLine(startDate);
            Console.WriteLine(metroGrid1.Rows.Count);
            Console.WriteLine("endDate");
            Console.WriteLine(endDate);

            /*
            //Console.WriteLine("Checking MetroGrid Working?");
            //Console.WriteLine(metroGrid1.Rows.Count);
            */

            // Setting min scale of xaxis to starttime and max scale to endtime
            myPane.XAxis.Scale.Min = new XDate(startDate);
            myPane.XAxis.Scale.Max = new XDate(endDate);
            myPane.XAxis.Scale.MinorUnit = DateUnit.Second;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;

            // Scroll min to start time and scroll max to endtime
            zedGraphControl1.ScrollMinX = new XDate(startDate);
            zedGraphControl1.ScrollMaxX = new XDate(endDate);

            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.Format = "HH:mm:ss";
            myPane.XAxis.MinorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.IsVisible = true;

            double xCoord = 0;
            // Point pair list for graph
            hrList = new PointPairList();
            speedList = new PointPairList();
            cadenceList = new PointPairList();
            altitudeList = new PointPairList();
            powerList = new PointPairList();

            int dataCount = (metroGrid1.Rows.Count - 1);
            int counter = 0;
            // Graph setup for version to 106 and 107
            if (Version == "106" || Version == "107") {

                foreach (DataGridViewRow rowData in metroGrid1.Rows) {
                    if (counter == dataCount)
                        break;
                    // Adding interval as seconds
                    xCoord = (double)new XDate(startDate.AddSeconds(seconds * counter));
                    //Console.WriteLine("rowData[1].Value.ToString");
                    //Console.WriteLine(rowData.Cells[1].Value.ToString());

                    hrList.Add(xCoord, double.Parse(rowData.Cells[1].Value.ToString()));
                    // Ploting graph according to smode when bits value is 1
                    if (SMode0 == "1") {
                        // Ploting graph value of speed
                        speedList.Add(xCoord, double.Parse(rowData.Cells[2].Value.ToString()));
                    }
                    if (SMode1 == "1") {
                        // Ploting graph value of cadence
                        cadenceList.Add(xCoord, double.Parse(rowData.Cells[3].Value.ToString()));
                    }
                    if (SMode2 == "1") {
                        // Ploting graph value of altitude
                        altitudeList.Add(xCoord, double.Parse(rowData.Cells[4].Value.ToString()));
                    }
                    if (SMode3 == "1") {
                        // Ploting graph value of power
                        powerList.Add(xCoord, double.Parse(rowData.Cells[5].Value.ToString()));
                    }
                    counter++;
                }

                Console.WriteLine("Checking Actual Data Here");
                Console.WriteLine(hrList);
                Console.WriteLine(speedList);
                Console.WriteLine(cadenceList);
                Console.WriteLine(altitudeList);
                Console.WriteLine(powerList);

                // Setting each graph title, color and symbol
                hrCurve = myPane.AddCurve("Heart Rate", hrList, System.Drawing.Color.Red, SymbolType.None);
                if (SMode0 == "1") {
                    // Setting speed as graph title, color as blue and symbol as none
                    speedCurve = myPane.AddCurve("Speed", speedList, System.Drawing.Color.Blue, SymbolType.None);
                    speedCurve.YAxisIndex = Y2Axis;
                }
                if (SMode1 == "1") {
                    // Setting cadence as graph title, color as green and symbol as none
                    cadenceCurve = myPane.AddCurve("Cadence", cadenceList, System.Drawing.Color.Green, SymbolType.None);
                    cadenceCurve.YAxisIndex = Y3Axis;
                }
                if (SMode2 == "1") {
                    // Setting altitude as graph title, color as yellow and symbol as none
                    altitudeCurve = myPane.AddCurve("Altitude", altitudeList, System.Drawing.Color.Yellow, SymbolType.None);
                    altitudeCurve.YAxisIndex = Y4Axis;
                }
                if (SMode3 == "1") {
                    // Setting power as graph title, color as purple and symbol as none
                    powerCurve = myPane.AddCurve("Power Watt", powerList, System.Drawing.Color.Purple, SymbolType.None);
                    powerCurve.YAxisIndex = Y5Axis;
                }
                hrCurve.YAxisIndex = YAxis;

                // Refreshing graph
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            // Setting up graph for version 105
            else if (Version == "105") {

                foreach (DataGridViewRow rowData in metroGrid1.Rows) {
                    if (counter == dataCount)
                        break;

                    hrList.Add(xCoord, double.Parse(rowData.Cells[1].Value.ToString()));
                    speedList.Add(xCoord, double.Parse(rowData.Cells[2].Value.ToString()));
                    // cadenceList.Add(xCoord, double.Parse(rowData.Cells[3].Value.ToString()));
                    /*
                    lblMaxPower.Text = "0 Watts";
                    lblMaxAltitude.Text = "0 Meter";
                    */
                    counter++;
                }

                hrCurve = myPane.AddCurve("Heart Rate", hrList, System.Drawing.Color.Red, SymbolType.None);
                speedCurve = myPane.AddCurve("Speed", speedList, System.Drawing.Color.Blue, SymbolType.None);
                cadenceCurve = myPane.AddCurve("Cadence", cadenceList, System.Drawing.Color.Green, SymbolType.None);

                hrCurve.YAxisIndex = YAxis;
                speedCurve.YAxisIndex = Y2Axis;
                cadenceCurve.YAxisIndex = Y3Axis;

                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }   // Draw Graph

        private void zedGraphControl1_Load(object sender, EventArgs e) {

        }

        private void zoomButton_MouseHover(object sender, EventArgs e) {

        }

        private void zoomButton_MouseLeave(object sender, EventArgs e) {

        }
    }
}
