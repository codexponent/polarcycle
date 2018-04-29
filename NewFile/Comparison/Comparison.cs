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
using System.IO;
using ZedGraph;
using System.Text.RegularExpressions;

namespace PolarCycle {
    public partial class Comparison : MetroForm {

        // FileNames
        private string fileName1;
        private string fileName2;

        // Summary
        private string fileName;
        private string HRM;
        private string line;
        private int NumberOfLines;
        private string Version;
        private string Interval;
        private string StartTime;
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
        private int seconds;
        private int counter;
        private double[] heartgraph;
        private DateTime myDateTime;
        private DateTime newDateTime;
        private string heart;
        private string speed;
        private string cadence;
        private string altitude;
        private string power;
        private string powerbal;
        private string airp;
        private int altitudeRateMax;
        private int powerRateMax;
        private bool graphActive;
        private string MaxHR;

        //Graph
        private int speedRateMax;
        DateTime myDateTime1;
        GraphPane myPane1;
        GraphPane myPane2;
        PointPairList hrList;
        PointPairList speedList;
        PointPairList cadenceList;
        PointPairList altitudeList;
        PointPairList powerList;
        LineItem hrCurve, speedCurve, cadenceCurve, altitudeCurve, powerCurve, powerBalanceCurve;

        // Part-2
        Dictionary<string, string> param1 = new Dictionary<string, string>();
        List<Data> DataList1 = new List<Data>();
        Dictionary<string, string> param2 = new Dictionary<string, string>();
        List<Data> DataList2 = new List<Data>();

        double normalizedPower, ftp, intensityFactor, trainingStressScore, thresholdPower;
        int selectionStartIndex;
        int selectionEndIndex;
        bool isHeartRateAvailable, isSpeedAvailable, isCadenceAvailable, isAltitudeAvailable, isPowerAvailable;
        bool isFirstTimeLoaded = true;

        string[] columnNames = new string[] { "Heart Rate", "Speed", "Cadence", "Altitude", "Power", "Time" };
        bool graphSelected = false;
        private string IntervalData;
        bool isDataLoaded = false;
        double maxHR, avgHR, minHR, maxSpeed, avgSpeed, minSpeed, maxCadence, avgCadence, minCadence, maxAltitude, avgAltitude, minAltitude, maxPower, avgPower, minPower;

        double normalizedPower2, ftp22, intensityFactor2, trainingStressScore2, thresholdPower2;
        int selectionStartIndex2;
        int selectionEndIndex2;
        bool isHeartRateAvailable2, isSpeedAvailable2, isCadenceAvailable2, isAltitudeAvailable2, isPowerAvailable2;
        bool isFirstTimeLoaded2 = true;

        bool graphSelected2 = false;
        private string IntervalData2;
        bool isDataLoaded2 = false;
        double maxHR2, avgHR2, minHR2, maxSpeed2, avgSpeed2, minSpeed2, maxCadence2, avgCadence2, minCadence2, maxAltitude2, avgAltitude2, minAltitude2, maxPower2, avgPower2, minPower2;

        private void zedGraphControl1_Paint(object sender, PaintEventArgs e) {
            Console.WriteLine("Paint Event Called");
        }

        PointF pf;
        PointF pf1, pf2;

        private void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState) {
            Console.WriteLine("Zoom event Called");
            //float dx = Math.Abs(pf1.X - pf2.X);
            //float dy = Math.Abs(pf1.Y - pf2.Y);
            //float dz;
            //float dX = zedGraphControl1.Width;
            //float dY = zedGraphControl1.Height;

            //if (dx < dy) {
                //dz = dx / dX;
            //} else {
                //dz = dy / dY;
            //}

            //zedGraphControl2.ZoomPane(myPane2, dz, pf, zedGraphControl1.IsZoomOnMouseCenter);
            //newState.ApplyState(myPane2);
            //zedGraphControl2.Invalidate();
        }

        // New
        private string StartTime1;
        private string StartTime2;

        private Axis axis2;

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
                        selectionEndIndex2 = index;
                        graphSelected = true;
                        graphSelected2 = true;
                        break;
                    }
                }
            }

            if (graphSelected)
                ReloadAdvanceMetrics();
                ReloadAdvanceMetrics2();

            graphSelected = false;
            graphSelected2 = false;

            //pf.X = (pf.X + e.X) / 2;
            //pf.Y = (pf.Y + e.Y) / 2;

            //pf2 = new PointF(e.X, e.Y);

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
                    selectionStartIndex2 = index;
                    break;
                }

            }
            //pf = new PointF(e.X, e.Y);
            //pf1 = new PointF(e.X, e.Y);

            return false;
        }

        private bool zedGraphControl2_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e) {
            if (graphSelected2) {
                object nearestObject;
                int index;
                graphSelected2 = false;

                for (int i = 0; i < zedGraphControl2.Height; i++) {
                    this.zedGraphControl2.GraphPane.FindNearestObject(new PointF(e.X, i), this.CreateGraphics(), out nearestObject, out index);

                    if (nearestObject != null && nearestObject.GetType() == typeof(LineItem)) {
                        //setting selection end value
                        selectionEndIndex2 = index;
                        graphSelected2 = true;
                        break;
                    }
                }
            }

            //if (graphSelected2)
                //ReloadAdvanceMetrics2();

            //graphSelected2 = false;

            return false;
        }

        private bool zedGraphControl2_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e) {
            if (graphSelected2)
                return false;

            object nearestObject;
            int index;

            for (int i = 0; i < zedGraphControl2.Height; i++) {
                this.zedGraphControl2.GraphPane.FindNearestObject(new PointF(e.X, i), this.CreateGraphics(), out nearestObject, out index);

                if (nearestObject != null && nearestObject.GetType() == typeof(LineItem)) {
                    graphSelected2 = true;
                    //setting selection start value
                    selectionStartIndex2 = index;
                    break;
                }
            }

            return false;
        }

        private void zoomButton_Click(object sender, EventArgs e) {

            zedGraphControl1.ZoomOutAll(zedGraphControl1.GraphPane);

            
            //zedGraphControl1.ZoomOutAll(myPane1);//new
            //zedGraphControl1.Invalidate();//new

            //zedGraphControl2.ZoomPane(myPane2, 1, new PointF(0, 0), zedGraphControl1.IsZoomOnMouseCenter);

            //myPane2.XAxis = Axis.Default;
            //myPane2.X2Axis = Axis.Default;

            //myPane2.Re

            //zedGraphControl2.RestoreScale(myPane2); //new
            //zedGraphControl2.ZoomOut(myPane2);
            //zedGraphControl2.ZoomOutAll(myPane2);

            //zedGraphControl2.GraphPane.CurveList.Clear();
            //zedGraphControl2.GraphPane.GraphObjList.Clear();
            //drawGraph(myPane2, StartTime2, zedGraphControl2, metroGrid2);
            //zedGraphControl2.Invalidate();//new
            //showing zoom out data advancemetric calculation
            AdvanceMetrics(DataList1);
            AdvanceMetrics2(DataList2);
        }

        private void chbx_HR_CheckedChanged(object sender, EventArgs e) {
            hrCurve.IsVisible = chbx_HR.Checked;
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
        }

        private void chbx_SPD_CheckedChanged(object sender, EventArgs e) {
            speedCurve.IsVisible = chbx_SPD.Checked;
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
        }

        private void chbx_CAD_CheckedChanged(object sender, EventArgs e) {
            cadenceCurve.IsVisible = chbx_CAD.Checked;
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
        }

        private void chbx_ALT_CheckedChanged(object sender, EventArgs e) {
            altitudeCurve.IsVisible = chbx_ALT.Checked;
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
        }

        private void chbx_PWR_CheckedChanged(object sender, EventArgs e) {
            powerCurve.IsVisible = chbx_PWR.Checked;
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
        }

        private void metroLabel15_Click(object sender, EventArgs e) {
            //Not Used
        }

        private void metroLabel11_Click(object sender, EventArgs e) {
            // Not Used
        }

        public Comparison() {
            InitializeComponent();
        }

        // Summary
        /// <summary>
        /// Splitting the Data on '='
        /// </summary>
        /// <param name="line">The string that is to be split</param>
        /// <returns>Splitted string</returns>
        private string[] SplitEquals(string line) {
            return line.Split('=');
        }

        /// <summary>
        /// Splitting the Data on ' '  (Space)
        /// </summary>
        /// <param name="line">The string that is to be split</param>
        /// <returns>Splitted string</returns>
        private string[] SplitSpace(string line) {
            return line.Split(' ');
        }
        //Summary

        /// <summary>
        /// Retrieves all the information from the UploadFrom
        /// </summary>
        /// <param name="fileName1">First File Selected</param>
        /// <param name="fileName2">Second File Selected</param>
        public void getInformation(string fileName1, string fileName2) {
            this.fileName1 = fileName1;
            this.fileName2 = fileName2;
        }

        /// <summary>
        /// Loads the summary part of the comparison
        /// </summary>
        /// <param name="metroGrid1">MetroGrid to which the summary should be written</param>
        /// <param name="fileName">Filename that is selected</param>
        /// <param name="SpeedLabelAverage">Speed Label</param>
        /// <param name="HeartRateLabelAverage">Heart Label</param>
        /// <param name="PowerLabelAverage">Power Label</param>
        /// <param name="AltitudeLabelAverage">Altitude Label</param>
        /// <param name="DistanceLabel">Distance Label</param>
        /// <returns>DateTime Value</returns>
        private string summaryLoad(MetroFramework.Controls.MetroGrid metroGrid1, 
                                    string fileName, 
                                    MetroFramework.Controls.MetroLabel SpeedLabelAverage,
                                    MetroFramework.Controls.MetroLabel HeartRateLabelAverage,
                                    MetroFramework.Controls.MetroLabel PowerLabelAverage,
                                    MetroFramework.Controls.MetroLabel AltitudeLabelAverage,
                                    MetroFramework.Controls.MetroLabel DistanceLabel
                                    ) {
            // For Managing The Size of dataGridView 1 according to Windows Size
            metroGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);
            // For Retrieving Smode and Displaying
            string line;
            bool paramsLine = false;
            // Read and display lines from the file until the end of the file is reached.
            /*while ((line = sr.ReadLine()) != null) {
                
            }*/
            // For Retrieving Smode and Displaying
            //Console.WriteLine("Version Version");
            //Console.WriteLine(Version);

            HRM = null;
            NumberOfLines = File.ReadAllLines(fileName).Length;
            while ((line = sr.ReadLine()) != null) {

                if (line == "[Params]") {
                    // line = [Params]
                    paramsLine = true;
                }
                // Only taking the important parameters from the [Params] Section for further
                // action for Summary Section
                if (paramsLine == true) {
                    string[] parm = SplitEquals(line);
                    if (parm.Length > 1) {
                        if (parm[0] == "SMode") {
                            SMode = parm[1];
                        }
                        if (parm[0] == "Version") {
                            Version = parm[1];
                        }
                        if (parm[0] == "Interval") {
                            Interval = parm[1];
                        }
                        if (parm[0] == "StartTime") {
                            StartTime = parm[1];
                        }
                        if (parm[0] == "MaxHR") {
                            MaxHR = parm[1];
                        }
                        if (line.Substring(0, 1) == "[") {
                            // Checking for another List and stopping unwanted code to run; EOParamsLine
                            paramsLine = false;
                        }
                    }
                }

                if (line.IndexOf("[HRData]") != -1) {
                    //[HRData] is accessible
                    break;
                }
            }
            line = sr.ReadLine();
            //The First Line of [HRData]

            Console.WriteLine("Checking the line here");
            Console.WriteLine("line");
            Console.WriteLine(line);

            // Setup an accumulator
            double mph = 0;
            int heartTotal = 0;
            int speedTotal = 0;
            int powerTotal = 0;
            int altitudeTotal = 0;

            // Binary Forms of Smode
            // Taking the 1st, 2nd, 3rd, 4th, 5th, 6th, 7th and 8th Smode Character Seperately
            SMode0 = SMode[0].ToString();
            SMode1 = SMode[1].ToString();
            SMode2 = SMode[2].ToString();
            SMode3 = SMode[3].ToString();
            SMode4 = SMode[4].ToString();
            SMode5 = SMode[5].ToString();
            SMode6 = SMode[6].ToString();
            SMode7 = SMode[7].ToString();

            // Setup for version '105'
            if (Version == "105") {
                // Adding timestamp as column
                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                time.HeaderText = "TimeStamp";
                int col1 = metroGrid1.Columns.Add(time);

                // Adding heart rate as column
                DataGridViewColumn heartrate = new DataGridViewTextBoxColumn();
                heartrate.HeaderText = "Heart Rate (BMP)";
                int col2 = metroGrid1.Columns.Add(heartrate);

                if (SMode0 == "1") {
                    // Showing cloumn whend speed smode is 1
                    DataGridViewColumn speeding = new DataGridViewTextBoxColumn();
                    if (SMode6 == "0") {
                        // Measurement unit according to Europe
                        speeding.HeaderText = "Speed (Km/Hr)";
                    } else if (SMode6 == "1") {
                        // Measurement unit accordint to US
                        speeding.HeaderText = "Speed (mph)";
                    }
                    // Adding speed as column and data on it
                    int col3 = metroGrid1.Columns.Add(speeding);
                }
                if (SMode1 == "1") {
                    //showing cloumn whend cadence smode is 1
                    DataGridViewColumn cadencer = new DataGridViewTextBoxColumn();
                    //adding cadence as column and data on it
                    cadencer.HeaderText = "Cadence (RPM)";
                    int col4 = metroGrid1.Columns.Add(cadencer);
                }
            }

            // Setup for version '106'
            else if (Version == "106") {
                // Adding timestamp as column
                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                time.HeaderText = "TimeStamp";
                int col1 = metroGrid1.Columns.Add(time);

                // Adding heart rate as column and data on it
                DataGridViewColumn heartrate = new DataGridViewTextBoxColumn();
                heartrate.HeaderText = "Heart Rate (BMP)";
                int col2 = metroGrid1.Columns.Add(heartrate);
                if (SMode0 == "1") {
                    // Showing cloumn whend speed smode is 1
                    DataGridViewColumn speeding = new DataGridViewTextBoxColumn();
                    if (SMode6 == "0") {
                        speeding.HeaderText = "Speed (Km/Hr)";
                    } else if (SMode6 == "1") {
                        speeding.HeaderText = "Speed (mph)";
                    }
                    // Adding timestamp as column
                    int col3 = metroGrid1.Columns.Add(speeding);
                }
                if (SMode1 == "1") {
                    // Showing cloumn whend cadence smode is 1

                    DataGridViewColumn cadencer = new DataGridViewTextBoxColumn();
                    // Adding candece as column and data on it
                    cadencer.HeaderText = "Cadence (RPM)";
                    int col4 = metroGrid1.Columns.Add(cadencer);
                }
                if (SMode2 == "1") {
                    // Showing cloumn whend altitude smode is 1
                    DataGridViewColumn alt = new DataGridViewTextBoxColumn();
                    if (SMode6 == "0") {
                        alt.HeaderText = "Altitude (M)";
                    } else if (SMode6 == "1") {
                        alt.HeaderText = "Altitude (FT)";
                    }
                    // Adding altitude as column and data on it
                    int col5 = metroGrid1.Columns.Add(alt);
                }
                if (SMode3 == "1") {
                    // Showing cloumn whend power smode is 1
                    DataGridViewColumn pwr = new DataGridViewTextBoxColumn();
                    // Adding power as column and data on it
                    pwr.HeaderText = "Power in Watts";
                    int col6 = metroGrid1.Columns.Add(pwr);
                }
                if (SMode4 == "1") {
                    // Showing cloumn whend power balance smode is 1
                    DataGridViewColumn pwrblnc = new DataGridViewTextBoxColumn();
                    // Adding power balance as column and data on it
                    pwrblnc.HeaderText = "Power Balance";
                    int col7 = metroGrid1.Columns.Add(pwrblnc);
                }
            }

            // Setup for version '107'
            else if (Version == "107") {
                SMode8 = SMode[8].ToString(); // Geting airpressure value

                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                // Adding timestamp as column 
                time.HeaderText = "TimeStamp";
                int col1 = metroGrid1.Columns.Add(time);

                DataGridViewColumn heartrate = new DataGridViewTextBoxColumn();
                // Adding heart as column
                heartrate.HeaderText = "Heart Rate (BMP)";
                int col2 = metroGrid1.Columns.Add(heartrate);
                if (SMode0 == "1") {
                    // Showing cloumn when speed smode is 1
                    DataGridViewColumn speeding = new DataGridViewTextBoxColumn();
                    if (SMode6 == "0") {
                        speeding.HeaderText = "Speed (Km/Hr)";
                    } else if (SMode6 == "1") {
                        speeding.HeaderText = "Speed (mph)";
                    }
                    // Adding speed as column and data on it
                    int col3 = metroGrid1.Columns.Add(speeding);
                }
                if (SMode1 == "1") {
                    // Showing cloumn when cadence smode is 1
                    DataGridViewColumn cadencer = new DataGridViewTextBoxColumn();
                    cadencer.HeaderText = "Cadence (RPM)";
                    int col4 = metroGrid1.Columns.Add(cadencer);
                }
                if (SMode2 == "1") {
                    // Showing cloumn when altitude smode is 1
                    DataGridViewColumn alt = new DataGridViewTextBoxColumn();
                    if (SMode6 == "0") {
                        alt.HeaderText = "Altitude (M)";
                    } else if (SMode6 == "1") {
                        alt.HeaderText = "Altitude (FT)";
                    }
                    // Adding altitude as column 
                    int col5 = metroGrid1.Columns.Add(alt);
                }
                if (SMode3 == "1") {
                    //showing cloumn when power smode is 1
                    DataGridViewColumn pwr = new DataGridViewTextBoxColumn();
                    //adding power as column and data on it
                    pwr.HeaderText = "Power in Watts";
                    int col6 = metroGrid1.Columns.Add(pwr);
                }
                if (SMode4 == "1") {
                    //showing cloumn when power balance smode is 1
                    DataGridViewColumn pwrblnc = new DataGridViewTextBoxColumn();
                    //adding power balance as column and data on it
                    pwrblnc.HeaderText = "Power Balance";
                    int col7 = metroGrid1.Columns.Add(pwrblnc);
                }
                if (SMode8 == "1") {
                    //showing cloumn when airpressure smode is 1
                    DataGridViewColumn airpressure = new DataGridViewTextBoxColumn();
                    //adding air pressure as column and data on it
                    airpressure.HeaderText = "Air Pressure";
                    int col8 = metroGrid1.Columns.Add(airpressure);
                }
            } else {
                //showing error message 
                MessageBox.Show("Wrong File Selected", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            // Setting interval to seconds
            int interval = int.Parse(Interval);
            seconds = Convert.ToInt32(interval);
            counter = 0;
            heartgraph = new double[0];

            // Setting starttime value to mydatetime variable 
            myDateTime = Convert.ToDateTime(StartTime);

            // Adding Data on metrogridview
            while (line != null) {
                // Increment counter each pass through the loop.
                counter++;
                // Adding interval second to starttime 
                myDateTime = myDateTime.AddSeconds(seconds);

                if (Version == "105") {
                    // Spliting the value of heart rate and adding each data to hearttotal 
                    // 0 signifies heart rate only
                    heart = line.Split('\t')[0];
                    int heartint = Convert.ToInt32(heart);
                    List<string> heartarr = new List<string>();
                    heartarr.Add(heart);
                    heartTotal += heartint;

                    // Spliting the value of speed and adding each data to speedtotal 
                    // 1 signifies speed
                    speed = line.Split('\t')[1];
                    double speeed = int.Parse(speed);
                    // Converting into km/hr
                    double speed1 = Math.Round((speeed / 10), 2);
                    int speedint = Convert.ToInt32(speed);
                    speedTotal += speedint;
                    mph = ((double)speedint / (double)1.6);

                    // Spliting the value of cadence
                    // 2 signifies cadence
                    cadence = line.Split('\t')[2];
                    int cadenceint = Convert.ToInt32(cadence);

                    // Repetation Kicks in for other lines
                    line = sr.ReadLine();
                    // Adding data to datagridview
                    metroGrid1.Rows.Add(myDateTime, heart, speed1, cadence);

                } else if (Version == "106") {
                    // Spliting the value of heart rate and adding each data to hearttotal 
                    heart = line.Split('\t')[0];
                    int heartint = Convert.ToInt32(heart);
                    List<string> heartarr = new List<string>();
                    heartarr.Add(heart);
                    heartTotal += heartint;

                    // Spliting the value of speed and adding each data to speedtotal 
                    speed = line.Split('\t')[1];
                    double speeed = int.Parse(speed);
                    // Converting into km/hr
                    double speed1 = Math.Round((speeed / 10), 2);
                    int speedint = Convert.ToInt32(speed);
                    speedTotal += speedint;
                    mph = ((double)speedint / (double)1.6);

                    // Spliting the value of cadence
                    cadence = line.Split('\t')[2];
                    int cadenceint = Convert.ToInt32(cadence);

                    // Spliting the value of altitude and adding each data to altitudetotal 
                    altitude = line.Split('\t')[3];
                    int altitudeint = Convert.ToInt32(altitude);
                    altitudeTotal += altitudeint;

                    // Spliting the value of power and adding each data to powertotal 
                    power = line.Split('\t')[4];
                    int powerint = Convert.ToInt32(power);
                    powerTotal += powerint;


                    try {
                        powerbal = line.Split('\t')[5];
                        int Powerbalint = Convert.ToInt32(powerbal);

                        line = sr.ReadLine();
                        // Adding data to datagridview
                        metroGrid1.Rows.Add(myDateTime, heart, speed1, cadence, altitude, power, powerbal);
                    } catch (Exception error) {
                        MetroMessageBox.Show(this, "Error in File. PLease Launch Application Again", error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.None);
                        Application.Exit();
                        //throw;
                    }



                } else if (Version == "107") {
                    // Spliting the value of heart rate and adding each data to hearttotal 
                    heart = line.Split('\t')[0];
                    int heartint = Convert.ToInt32(heart);
                    List<string> heartarr = new List<string>();
                    heartarr.Add(heart);
                    heartTotal += heartint;

                    // Spliting the value of speed and adding each data to speedtotal 
                    speed = line.Split('\t')[1];
                    double speeed = int.Parse(speed);

                    // Converting into km/hr
                    double speed1 = Math.Round((speeed / 10), 2);
                    int speedint = Convert.ToInt32(speed);
                    speedTotal += speedint;
                    mph = ((double)speedint / (double)1.6);

                    // Spliting the value of cadence
                    cadence = line.Split('\t')[2];
                    int cadenceint = Convert.ToInt32(cadence);

                    // Spliting the value of altitude and adding each data to altitudetotal 
                    altitude = line.Split('\t')[3];
                    int altitudeint = Convert.ToInt32(altitude);
                    altitudeTotal += altitudeint;

                    // Spliting the value of power and adding each data to powertotal 
                    power = line.Split('\t')[4];
                    int powerint = Convert.ToInt32(power);
                    powerTotal += powerint;

                    // Spliting the value of power balance
                    powerbal = line.Split('\t')[5];
                    int Powerbalint = Convert.ToInt32(powerbal);

                    //spliting the value of air pressure 
                    airp = line.Split('\t')[6];
                    int airpint = Convert.ToInt32(airp);
                    line = sr.ReadLine();
                    //aadding the data into correspondint columns
                    metroGrid1.Rows.Add(myDateTime, heart, speed1, cadence, altitude, power, powerbal, airp);

                }
            }


            // Now at the end we check to make sure counter is not zero and then divide heartTotal by our counter
            double heartAvg = 0.0;
            double speedAvg = 0.0;
            double powerAvg = 0.0;
            double altAvg = 0.0;
            if (counter > 0) {
                // Calculating Average
                // Average Fornula
                heartAvg = heartTotal / counter;
                speedAvg = (speedTotal / counter);

                powerAvg = powerTotal / counter;
                altAvg = altitudeTotal / counter;

                // Calculating distance travelled 
                double timesec = int.Parse(Interval) * counter;
                double timehr = Math.Round((timesec / 3600), 2);
                double distance = Math.Round((speedAvg / 10), 2) * timehr;

                DistanceLabel.Text = distance.ToString() + " Km/Hr";

                // Calculating Max Heart Rate
                int[] maxHeartRate = (from DataGridViewRow row in metroGrid1.Rows
                                      where row.Cells[1].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[1].FormattedValue)).ToArray();
                //HeartRateLabelMax.Text = "Maximum Heart Rate: \n" + maxHeartRate.Max().ToString();

                // Calculating Min Heart Rate
                int[] minHeartRate = (from DataGridViewRow row in metroGrid1.Rows
                                      where row.Cells[1].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[1].FormattedValue)).ToArray();

                if (SMode0 == "1") {
                    //Calculating Max Speed
                    decimal[] maxSpeed = (from DataGridViewRow row in metroGrid1.Rows
                                          where row.Cells[2].FormattedValue.ToString() != string.Empty
                                          select Convert.ToDecimal(row.Cells[2].FormattedValue)).ToArray();
                    //SpeedLabelMax.Text = "Maximum Speed Level: \n" + maxSpeed.Max().ToString() + " km/h";
                } else if (SMode0 == "0") {
                    //when smode is 0 all the calculation is 0
                    //SpeedLabelMax.Text = "Maximum Speed Level: \n 0 km/h";
                    SpeedLabelAverage.Text = "";
                }

                if (Version == "106" || Version == "107") {
                    if (SMode2 == "1") {
                        // Calculating Max altitude
                        int[] maxAltitude = (from DataGridViewRow row in metroGrid1.Rows
                                             where row.Cells[4].FormattedValue.ToString() != string.Empty
                                             select Convert.ToInt32(row.Cells[4].FormattedValue)).ToArray();
                        //AltitudeLevelMax.Text = "Maximum Altitude: \n" + maxAltitude.Max().ToString() + " Meters";
                        altitudeRateMax = maxAltitude.Max();
                    } else if (SMode2 == "0") {
                        // When smode is 0 all the calculation is 0
                        //AltitudeLevelMax.Text = "Maximum Altitude: \n 0 Meters";
                        AltitudeLabelAverage.Text = "";
                        altitudeRateMax = 0;
                    }
                    if (SMode3 == "1") {
                        // Calculating Max Power
                        int[] maxPower = (from DataGridViewRow row in metroGrid1.Rows
                                          where row.Cells[5].FormattedValue.ToString() != string.Empty
                                          select Convert.ToInt32(row.Cells[5].FormattedValue)).ToArray();
                        //PowerLabelMax.Text = "Maximum Power Level: \n" + maxPower.Max().ToString() + " watts";
                        powerRateMax = maxPower.Max();
                    } else if (SMode3 == "0") {
                        // When smode is 0 all the calculation is 0
                        //PowerLabelMax.Text = "Maximum Power Level:\n 0 Watts";
                        PowerLabelAverage.Text = "";
                        powerRateMax = 0;
                    }
                }
            }

            // Setting all averages with corresponding measurement units 
            SpeedLabelAverage.Text = Math.Round((speedAvg / 10), 2) + " km/h";
            HeartRateLabelAverage.Text = heartAvg.ToString();
            PowerLabelAverage.Text = powerAvg.ToString() + " watts";
            AltitudeLabelAverage.Text = altAvg.ToString() + " meters";

            return StartTime;
        }

        private void Comparison_Load(object sender, EventArgs e) {
            //Summary
            StartTime1 = summaryLoad(metroGrid1, fileName1, speedboth1, heartboth1, powerboth1, altitudeboth1, distanceboth1);
            StartTime2 = summaryLoad(metroGrid2, fileName2, speedboth2, heartboth2, powerboth2, altitudeboth2, distanceboth2);
            //Summary

            //Graph
            //myPane1 = zedGraphControl1.GraphPane;

            MasterPane master = zedGraphControl1.MasterPane;
            master.PaneList.Clear();

            myPane1 = new GraphPane();
            myPane2 = new GraphPane();

            master.Add(myPane1);
            master.Add(myPane2);

            zedGraphControl1.IsSynchronizeXAxes = true;
            zedGraphControl1.IsSynchronizeYAxes = true;

            Graphics g = this.CreateGraphics();
            master.SetLayout(g, PaneLayout.SquareColPreferred);
            g.Dispose();

            

            myPane1.Title.Text = "HRM Cycle Application Graph";
            myPane1.XAxis.Title.Text = "Parameters";
            myPane1.YAxis.Title.Text = "Parameters Values";

            //myPane2 = zedGraphControl2.GraphPane;
            myPane2.Title.Text = "HRM Cycle Application Graph";
            myPane2.XAxis.Title.Text = "Parameters";
            myPane2.YAxis.Title.Text = "Parameters Values";
            drawGraph(myPane1, StartTime1, zedGraphControl1, metroGrid1);
            drawGraph(myPane2, StartTime2, zedGraphControl2, metroGrid2);
            //Graph

            zedGraphControl1.AxisChange();

            // Both Graph
            // clearing all data before loading another data
            clearAllData();
            // reading the data from the file
            ReadFilesContent(fileName1);


            // calculating summary of heart data
            //test
            //calculateHRDataSummaryValues();
            // diplaying headers, heart rate data and its summary and graph
            // ShowResult();
            isDataLoaded = true;
            AdvanceMetrics(DataList1);
            isFirstTimeLoaded = false;
            // Both Graph

            ReadFilesContent2(fileName2);


            // calculating summary of heart data
            //test
            //calculateHRDataSummaryValues();
            // diplaying headers, heart rate data and its summary and graph
            // ShowResult();
            isDataLoaded2 = true;
            AdvanceMetrics2(DataList2);
            isFirstTimeLoaded2 = false;

        }

        /// <summary>
        /// Draws the Graph
        /// </summary>
        /// <param name="myPane">Pane to be drawn according to the filename</param>
        /// <param name="StartTime">StartTime of corresponding Graph</param>
        /// <param name="zedGraphControl1">ZedGraphControl of corresponding Graph</param>
        /// <param name="metroGrid1">MetroGrid of which summary should be retrieved</param>
        private void drawGraph(GraphPane myPane, 
                                string StartTime, 
                                ZedGraphControl zedGraphControl1, 
                                MetroFramework.Controls.MetroGrid metroGrid1) {

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

        // Part - 2
        /// <summary>
        /// Gets the data
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDate() {
            // returns the formatted date as per requirements
            return new DateTime(int.Parse(param1["Date"].Substring(0, 4)), int.Parse(param1["Date"].Substring(4, 2)), int.Parse(param1["Date"].Substring(6, 2)));
        }

        /// <summary>
        /// Calculates the distance covered
        /// </summary>
        /// <returns>Distance</returns>
        private double getDistanceCovered() {
            double distance = 0;
            int interval = int.Parse(param1["Interval"]);
            for (int i = 0; i < DataList1.Count; i++) {
                double result = (((double)DataList1[i].speed / 10) / 3600) * interval;
                distance += result;
            }
            return distance;
        }

        /// <summary>
        /// Clears all the data
        /// </summary>
        private void clearAllData() {
            if (!isFirstTimeLoaded) {
                param1 = new Dictionary<string, string>();
                DataList1 = new List<Data>();
                param2 = new Dictionary<string, string>();
                DataList2 = new List<Data>();

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
                zedGraphControl2.GraphPane.GraphObjList.Clear();
                zedGraphControl2.GraphPane.CurveList.Clear();
                zedGraphControl2.GraphPane.YAxisList.Clear();
                zedGraphControl2.Invalidate();

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

                altitude1.Text = "----";
                cadence1.Text = "----";
                ftp1.Text = "----";
                heartrate1.Text = "----";
                power1.Text = "----";
                speed1.Text = "----";
                tss1.Text = "----";
                if1.Text = "----";
                normalized1.Text = "----";
                altitude2.Text = "----";
                cadence2.Text = "----";
                ftp2.Text = "----";
                heartrate2.Text = "----";
                power2.Text = "----";
                speed2.Text = "----";
                tss2.Text = "----";
                if2.Text = "----";
                normalized2.Text = "----";

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
                                param1.Add(parameter[0], parameter[1]);

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
            string smode = param1["SMode"];

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
            if (param1["Version"] == "107" && smode[8] == '1') {
                tempHrData.air_pressure = int.Parse(hrdataArray[6]);
            }

            DataList1.Add(tempHrData);
        }

        /// <summary>
        /// Checks the Unit
        /// </summary>
        /// <returns>Boolean Value</returns>
        private bool isEuroUnitUsed() {
            bool isEuroUnit = false;

            switch (param1["Version"]) {
                case "105": {
                        isEuroUnit = (param1["Mode"][2] == '0');
                    }
                    break;

                case "106":
                case "107": {
                        isEuroUnit = (param1["SMode"][7] == '0');
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
                newHrData.Add(DataList1[i]);

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
            int interval = int.Parse(param1["Interval"]);
            //calculationg total duration
            int duration = hrData.Count * interval;

            foreach (Data data in hrData)
                sumIncreasedPower += Math.Pow(data.power, 4);

            //passing all the average value to corresponding label
            heartrate1.Text = Math.Round(hrData.Average(data => data.heart_rate)).ToString() + " bpm";
            speed1.Text = Math.Round(hrData.Average(data => data.speed), 2).ToString() + speedUnit;
            cadence1.Text = Math.Round(hrData.Average(data => data.cadence)).ToString();
            altitude1.Text = Math.Round(hrData.Average(data => data.altitude), 2).ToString() + altitudeUnit;
            power1.Text = Math.Round(hrData.Average(data => data.power)).ToString() + " watts";
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
            normalized1.Text = Math.Round(normalizedPower, 2).ToString() + " Watts";
            if1.Text = Math.Round(intensityFactor, 2).ToString();
            tss1.Text = Math.Round(trainingStressScore, 2).ToString();
            ftp1.Text = Math.Round(ftp, 2).ToString();

        }
        //Part - 2

        //Part -3
        /// <summary>
        /// Gets the data
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDate2() {
            // returns the formatted date as per requirements
            return new DateTime(int.Parse(param2["Date"].Substring(0, 4)), int.Parse(param2["Date"].Substring(4, 2)), int.Parse(param2["Date"].Substring(6, 2)));
        }

        /// <summary>
        /// Calculates the distance covered
        /// </summary>
        /// <returns>Distance</returns>
        private double getDistanceCovered2() {
            double distance = 0;
            int interval = int.Parse(param2["Interval"]);
            for (int i = 0; i < DataList2.Count; i++) {
                double result = (((double)DataList2[i].speed / 10) / 3600) * interval;
                distance += result;
            }
            return distance;
        }

        /// <summary>
        /// Reads the file content
        /// </summary>
        /// <param name="filePath">Choosen filename</param>
        private void ReadFilesContent2(string filePath) {
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
                                param2.Add(parameter[0], parameter[1]);

                                isHeartRateAvailable2 = true;

                                if (parameter[0] == "SMode") {
                                    string smode = parameter[1];

                                    isSpeedAvailable2 = (smode[0] == '1');
                                    isCadenceAvailable2 = (smode[1] == '1');
                                    isAltitudeAvailable2 = (smode[2] == '1');
                                    isPowerAvailable2 = (smode[3] == '1');
                                }

                                if (parameter[0] == "Mode") {
                                    string mode = parameter[1];

                                    isSpeedAvailable2 = true;
                                    isCadenceAvailable2 = (mode[0] == '0');
                                    isAltitudeAvailable2 = (mode[0] == '1');
                                    isPowerAvailable2 = false;

                                }
                            } catch (Exception ex) {
                                continue;
                            }
                        }
                        break;

                    case "HRData": {
                            Console.WriteLine("Final Exam here");
                            Console.WriteLine(line);
                            setData2(line);
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="data">Data from the FileName</param>
        private void setData2(string data) {
            Data tempHrData = new Data();
            string smode = param2["SMode"];

            var hrdataArray = data.Split('\t');

            if (isHeartRateAvailable2)
                tempHrData.heart_rate = int.Parse(hrdataArray[0]);

            if (isSpeedAvailable2)
                tempHrData.speed = int.Parse(hrdataArray[1]);

            if (isCadenceAvailable2)
                tempHrData.cadence = int.Parse(hrdataArray[2]);

            if (isAltitudeAvailable2)
                tempHrData.altitude = int.Parse(hrdataArray[3]);

            if (isPowerAvailable2)
                tempHrData.power = int.Parse(hrdataArray[4]);

            // add air pressure if available in smode
            if (param2["Version"] == "107" && smode[8] == '1') {
                tempHrData.air_pressure = int.Parse(hrdataArray[6]);
            }

            DataList2.Add(tempHrData);
        }
        /// <summary>
        /// Checks the Unit
        /// </summary>
        /// <returns>Boolean Value</returns>
        private bool isEuroUnitUsed2() {
            bool isEuroUnit = false;

            switch (param2["Version"]) {
                case "105": {
                        isEuroUnit = (param2["Mode"][2] == '0');
                    }
                    break;

                case "106":
                case "107": {
                        isEuroUnit = (param2["SMode"][7] == '0');
                    }
                    break;
            }

            return isEuroUnit;
        }
        /// <summary>
        /// Reload the Graph
        /// </summary>
        private void ReloadAdvanceMetrics2() {
            List<Data> newHrData = new List<Data>();

            //getting data from selectionstaert to selectionend
            for (int i = selectionStartIndex2; i <= selectionEndIndex2; i++)
                newHrData.Add(DataList2[i]);

            AdvanceMetrics2(newHrData);
        }
        /// <summary>
        /// Runs the Graph
        /// </summary>
        /// <param name="hrData">DataList of the Choosen FileName</param>
        private void AdvanceMetrics2(List<Data> hrData) {
            //checking for data
            //if (!isDataLoaded)
            //return;

            //unit as per Smode value
            string speedUnit = (isEuroUnitUsed2() ? " km/h" : " mph");
            string altitudeUnit = (isEuroUnitUsed2() ? " meters" : " feet");

            double sumIncreasedPower = 0;
            //getting interval from param 
            int interval = int.Parse(param2["Interval"]);
            //calculationg total duration
            int duration = hrData.Count * interval;

            foreach (Data data in hrData)
                sumIncreasedPower += Math.Pow(data.power, 4);

            //passing all the average value to corresponding label
            heartrate2.Text = Math.Round(hrData.Average(data => data.heart_rate)).ToString() + " bpm";
            speed2.Text = Math.Round(hrData.Average(data => data.speed), 2).ToString() + speedUnit;
            cadence2.Text = Math.Round(hrData.Average(data => data.cadence)).ToString();
            altitude2.Text = Math.Round(hrData.Average(data => data.altitude), 2).ToString() + altitudeUnit;
            power2.Text = Math.Round(hrData.Average(data => data.power)).ToString() + " watts";
            string avpow1 = Math.Round(hrData.Average(data => data.power)).ToString();
            int pow = int.Parse(avpow1);

            //normaized power(NP) calculation
            normalizedPower = Math.Pow(sumIncreasedPower * ((double)interval / 3600), 0.25);

            //functional threshold power(FTP) calculation
            ftp = 0.95 * pow;

            //intensity factor calculation
            intensityFactor = normalizedPower2 / ftp22;

            //training stress score (tss) calculation
            trainingStressScore = (duration * normalizedPower2 * intensityFactor2) / (ftp22 * 36);

            //thrsshold calculation
            thresholdPower = 1.05 * ftp22;

            ///passing all tha  advanced metrics value to corresponding label
            normalized2.Text = Math.Round(normalizedPower2, 2).ToString() + " Watts";
            if2.Text = Math.Round(intensityFactor2, 2).ToString();
            tss2.Text = Math.Round(trainingStressScore2, 2).ToString();
            ftp2.Text = Math.Round(ftp22, 2).ToString();

        }
        //Part -3

        private void Comparison_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e) {
            UploadForm uploadForm = new UploadForm();
            uploadForm.Show();
            this.Hide();

        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }
    }
}
