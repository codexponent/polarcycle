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
using System.Text.RegularExpressions;

namespace PolarCycle {
    public partial class Graph : MetroForm {

        // Part-2
        Dictionary<string, string> param;
        double normalizedPower, ftp, intensityFactor, trainingStressScore, thresholdPower;
        int selectionStartIndex;
        int selectionEndIndex;
        List<Data> DataList;
        bool isHeartRateAvailable, isSpeedAvailable, isCadenceAvailable, isAltitudeAvailable, isPowerAvailable;
        bool isFirstTimeLoaded = true;
        bool isDataLoaded = false;
        double maxHR, avgHR, minHR, maxSpeed, avgSpeed, minSpeed, maxCadence, avgCadence, minCadence, maxAltitude, avgAltitude, minAltitude, maxPower, avgPower, minPower;
        string[] columnNames = new string[] { "Heart Rate", "Speed", "Cadence", "Altitude", "Power", "Time" };
        bool graphSelected = false;
        private string IntervalData;

        private string fileName;
        private string MaxHR;
        private int speedRateMax;
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

        private void metroButton2_Click(object sender, EventArgs e) {
            Interval interval = new Interval();
            interval.getInformation(param, IntervalData, DataList);
            interval.Show();
            this.Hide();
        }

        private bool zedGraphControl1_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e) {
            if (graphSelected) {
                object nearestObject;
                int index;
                graphSelected = false;

                for (int i = 0; i < zedGraphControl1.Height; i++) {
                    this.zedGraphControl1.GraphPane.FindNearestObject(new PointF(e.X, i), this.CreateGraphics(), out nearestObject, out index);

                    if (nearestObject != null && nearestObject.GetType() == typeof(LineItem)) {
                        //setting selection end value
                        selectionEndIndex = index;
                        graphSelected = true;
                        break;
                    }
                }
            }

            if (graphSelected)
                ReloadAdvanceMetrics();

            graphSelected = false;

            return false;
        }

        private bool zedGraphControl1_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e) {
            if (graphSelected)
                return false;

            object nearestObject;
            int index;

            for (int i = 0; i < zedGraphControl1.Height; i++) {
                this.zedGraphControl1.GraphPane.FindNearestObject(new PointF(e.X, i), this.CreateGraphics(), out nearestObject, out index);

                if (nearestObject != null && nearestObject.GetType() == typeof(LineItem)) {
                    graphSelected = true;
                    //setting selection start value
                    selectionStartIndex = index;
                    break;
                }
            }

            return false;
        }

        public Graph() {
            param = new Dictionary<string, string>();
            DataList = new List<Data>();
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
                                    int seconds,
                                    string Interval) {
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
            this.IntervalData = Interval;
            //Console.WriteLine("Check Here Final Day");
            //Console.WriteLine(fileName);


            // clearing all data before loading another data
            clearAllData();
            // reading the data from the file
            ReadFilesContent(fileName);

            isDataLoaded = true;
            // calculating summary of heart data
            // calculateHRDataSummaryValues();
            // diplaying headers, heart rate data and its summary and graph
            // ShowResult();
            AdvanceMetrics(DataList);

            isFirstTimeLoaded = false;
        }

        private void chbx_HR_CheckedChanged(object sender, EventArgs e) {
            hrCurve.IsVisible = chbx_HR.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_SPD_CheckedChanged(object sender, EventArgs e) {
            speedCurve.IsVisible = chbx_SPD.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_CAD_CheckedChanged(object sender, EventArgs e) {
            cadenceCurve.IsVisible = chbx_CAD.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_ALT_CheckedChanged(object sender, EventArgs e) {
            altitudeCurve.IsVisible = chbx_ALT.Checked;
            zedGraphControl1.Invalidate();
        }

        private void chbx_PWR_CheckedChanged(object sender, EventArgs e) {
            powerCurve.IsVisible = chbx_PWR.Checked;
            zedGraphControl1.Invalidate();
        }

        private void zoomButton_Click(object sender, EventArgs e) {
            zedGraphControl1.ZoomOutAll(myPane);
            zedGraphControl1.Invalidate();
            //showing zoom out data advancemetric calculation
            AdvanceMetrics(DataList);
        }

        private void metroButton1_Click(object sender, EventArgs e) {
            HomePage homePage = new HomePage();
            homePage.getInformation(fileName);
            homePage.Show();
            this.Hide();
        }

        private void Graph_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
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

            // Console.WriteLine("Checking Here");
            // Console.WriteLine(metroGrid1);
            // Console.WriteLine(metroGrid1.Rows.Count);
            // Console.WriteLine(metroGrid1.Columns.Count);

            drawGraph();
        }
        /// <summary>
        /// Draws the Graph
        /// </summary>
        private void drawGraph() {

            // Displaying heart rate in yaxis in red color 
            var YAxis = myPane.AddYAxis("Heart Rate");
            myPane.YAxis.Color = System.Drawing.Color.Red;
            myPane.YAxis.Scale.Max = int.Parse(MaxHR);

            // Displaying speed in yaxis in green color
            var Y2Axis = myPane.AddYAxis("Speed");
            myPane.Y2Axis.Color = System.Drawing.Color.Green;
            myPane.Y2Axis.Scale.Max = speedRateMax;

            // Displaying cadence in yaxis in blue color
            var Y3Axis = myPane.AddYAxis("Cadence");
            myPane.YAxisList[Y3Axis].Color = System.Drawing.Color.Blue;
            //myPane.YAxisList[Y3Axis].Scale.Max = int.Parse(lbl.Text);

            // Displaying altitude in yaxis gray color 
            var Y4Axis = myPane.AddYAxis("Altitude");
            myPane.YAxisList[Y4Axis].Color = System.Drawing.Color.Gray;

            // Displaying power in yaxis in magenta color
            var Y5Axis = myPane.AddYAxis("Power");
            myPane.YAxisList[Y5Axis].Color = System.Drawing.Color.Magenta;

            // Setting starttime and end time
            DateTime startDate = myDateTime1;
            startDate = Convert.ToDateTime(StartTime);
            DateTime endDate = startDate.AddSeconds(seconds * metroGrid1.Rows.Count);

            // Console.WriteLine("startDate");
            // Console.WriteLine(startDate);
            // Console.WriteLine(metroGrid1.Rows.Count);
            // Console.WriteLine("endDate");
            // Console.WriteLine(endDate);

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
                        Console.WriteLine("Debug Here");
                        Console.WriteLine(rowData.Cells[4].Value.ToString());
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

        /// <summary>
        /// Gets the data
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDate() {
            // returns the formatted date as per requirements
            return new DateTime(int.Parse(param["Date"].Substring(0, 4)), int.Parse(param["Date"].Substring(4, 2)), int.Parse(param["Date"].Substring(6, 2)));
        }

        /// <summary>
        /// Calculates the distance covered
        /// </summary>
        /// <returns>Distance</returns>
        private double getDistanceCovered() {
            double distance = 0;
            int interval = int.Parse(param["Interval"]);

            for (int i = 0; i < DataList.Count; i++) {
                double result = (((double)DataList[i].speed / 10) / 3600) * interval;

                distance += result;
            }

            return distance;
        }

        /// <summary>
        /// Clears all the data
        /// </summary>
        private void clearAllData() {
            if (!isFirstTimeLoaded) {
                param = new Dictionary<string, string>();
                DataList = new List<Data>();

                //clearing all the graph curves
                if (hrCurve != null)
                    hrCurve.Clear();

                if (speedCurve != null)
                    speedCurve.Clear();

                if (altitudeCurve != null)
                    altitudeCurve.Clear();

                if (cadenceCurve != null)
                    cadenceCurve.Clear();

                if (powerCurve != null)
                    powerCurve.Clear();

                //clearing the graph component
                zedGraphControl1.GraphPane.GraphObjList.Clear();
                zedGraphControl1.GraphPane.CurveList.Clear();
                zedGraphControl1.GraphPane.YAxisList.Clear();
                zedGraphControl1.Invalidate();

                //clearing all the max value data
                maxHR = 0;
                maxSpeed = 0;
                maxCadence = 0;
                maxAltitude = 0;
                maxPower = 0;

                //clearing all the min value data
                minHR = 0;
                minSpeed = 0;
                minCadence = 0;
                minAltitude = 0;
                minPower = 0;

                //clearing all the average value data
                avgHR = 0;
                avgPower = 0;
                avgAltitude = 0;
                avgCadence = 0;
                avgSpeed = 0;

                //clearing all data from the label

                AvgAlt.Text = "----";
                AvgCad.Text = "----";
                AvgFtp.Text = "----";
                AvgHR.Text = "----"; 
                AvgPow.Text = "----";
                AvgSpd.Text = "----";
                AvgTSS.Text = "----";
                AvgIF.Text = "----";
                AvgNorm.Text = "----";



                //
                //lblMaxHeartRate.Text = "-";
                //lblMinHeartRate.Text = "-";
                //lblMaxSpeed.Text = "-";
                //lblMinSpeed.Text = "-";
                //lblDistanceCovered.Text = "-";
                //lblMaxCadence.Text = "-";
                //lblMinCadence.Text = "-";
                //lblMaxAltitude.Text = "-";
                //lblMinAltitude.Text = "-";
                //lblMaxPower.Text = "-";
                //lblMinPower.Text = "-";

                //check all checklist
                //HRCheck.Checked = true;
                //SPDCheck.Checked = true;
                //CADCheck.Checked = true;
                //ALTCheck.Checked = true;
                //POWCheck.Checked = true;
            }
        }

        /// <summary>
        /// Reads the file content
        /// </summary>
        /// <param name="filePath">Choosen filename</param>
        private void ReadFilesContent(string filePath) {
            string header = null;
            // reading all the files
            string[] lines = System.IO.File.ReadAllLines(filePath);

            // Display the file contents by using a foreach loop.
            //checking all the smode value
            foreach (string line in lines) {

                if (line.StartsWith("[") && line.EndsWith("]")) {
                    header = Regex.Match(line, @"(?<=\[)(.*?)(?=\])", RegexOptions.Singleline | RegexOptions.IgnoreCase).ToString();
                    continue;
                }

                switch (header) {
                    case "Params": {
                        try {
                            var parameter = line.Split('=');
                            param.Add(parameter[0], parameter[1]);

                            isHeartRateAvailable = true;

                            if (parameter[0] == "SMode") {
                                string smode = parameter[1];

                                isSpeedAvailable = (smode[0] == '1');
                                isCadenceAvailable = (smode[1] == '1');
                                isAltitudeAvailable = (smode[2] == '1');
                                isPowerAvailable = (smode[3] == '1');
                            }

                            if (parameter[0] == "Mode") {
                                string mode = parameter[1];

                                isSpeedAvailable = true;
                                isCadenceAvailable = (mode[0] == '0');
                                isAltitudeAvailable = (mode[0] == '1');
                                isPowerAvailable = false;

                            }
                        } catch (Exception ex) {
                            continue;
                        }
                    }
                    break;

                    case "HRData": {
                            Console.WriteLine("Final Exam here");
                            Console.WriteLine(line);
                        setData(line);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="data">Data from the FileName</param>
        private void setData(string data) {
            Data tempHrData = new Data();
            string smode = param["SMode"];

            var hrdataArray = data.Split('\t');

            if (isHeartRateAvailable)
                tempHrData.heart_rate = int.Parse(hrdataArray[0]);

            if (isSpeedAvailable)
                tempHrData.speed = int.Parse(hrdataArray[1]);

            if (isCadenceAvailable)
                tempHrData.cadence = int.Parse(hrdataArray[2]);

            if (isAltitudeAvailable)
                tempHrData.altitude = int.Parse(hrdataArray[3]);

            if (isPowerAvailable)
                tempHrData.power = int.Parse(hrdataArray[4]);

            // add air pressure if available in smode
            if (param["Version"] == "107" && smode[8] == '1') {
                tempHrData.air_pressure = int.Parse(hrdataArray[6]);
            }

            DataList.Add(tempHrData);
        }
        
        /// <summary>
        /// Checks the Unit
        /// </summary>
        /// <returns>Boolean Value</returns>
        private bool isEuroUnitUsed() {
            bool isEuroUnit = false;

            switch (param["Version"]) {
                case "105": {
                        isEuroUnit = (param["Mode"][2] == '0');
                    }
                    break;

                case "106":
                case "107": {
                        isEuroUnit = (param["SMode"][7] == '0');
                    }
                    break;
            }

            return isEuroUnit;
        }

        /// <summary>
        /// Reload the Graph
        /// </summary>
        private void ReloadAdvanceMetrics() {
            List<Data> newHrData = new List<Data>();

            //getting data from selectionstaert to selectionend
            for (int i = selectionStartIndex; i <= selectionEndIndex; i++)
                newHrData.Add(DataList[i]);

            AdvanceMetrics(newHrData);
        }

        /// <summary>
        /// Runs the Graph
        /// </summary>
        /// <param name="hrData">DataList of the Choosen FileName</param>
        private void AdvanceMetrics(List<Data> hrData) {
            //checking for data
            //if (!isDataLoaded)
                //return;

            //unit as per Smode value
            string speedUnit = (isEuroUnitUsed() ? " km/h" : " mph");
            string altitudeUnit = (isEuroUnitUsed() ? " meters" : " feet");

            double sumIncreasedPower = 0;
            //getting interval from param 
            int interval = int.Parse(param["Interval"]);
            //calculationg total duration
            int duration = hrData.Count * interval;

            foreach (Data data in hrData)
                sumIncreasedPower += Math.Pow(data.power, 4);

            //passing all the average value to corresponding label
            AvgHR.Text = Math.Round(hrData.Average(data => data.heart_rate)).ToString() + " bpm";
            AvgSpd.Text = Math.Round(hrData.Average(data => data.speed), 2).ToString() + speedUnit;
            AvgCad.Text = Math.Round(hrData.Average(data => data.cadence)).ToString();
            AvgAlt.Text = Math.Round(hrData.Average(data => data.altitude), 2).ToString() + altitudeUnit;
            AvgPow.Text = Math.Round(hrData.Average(data => data.power)).ToString() + " watts";
            string avpow1 = Math.Round(hrData.Average(data => data.power)).ToString();
            int pow = int.Parse(avpow1);

            //normaized power(NP) calculation
            normalizedPower = Math.Pow(sumIncreasedPower * ((double)interval / 3600), 0.25);

            //functional threshold power(FTP) calculation
            ftp = 0.95 * pow;

            //intensity factor calculation
            intensityFactor = normalizedPower / ftp;

            //training stress score (tss) calculation
            trainingStressScore = (duration * normalizedPower * intensityFactor) / (ftp * 36);

            //thrsshold calculation
            thresholdPower = 1.05 * ftp;

            ///passing all tha  advanced metrics value to corresponding label
            AvgNorm.Text = Math.Round(normalizedPower, 2).ToString() + " Watts";
            AvgIF.Text = Math.Round(intensityFactor, 2).ToString();
            AvgTSS.Text = Math.Round(trainingStressScore, 2).ToString();
            AvgFtp.Text = Math.Round(ftp, 2).ToString();

        }
    }
}