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

namespace PolarCycle {
    public partial class Summary : MetroForm {
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
        public Summary() {
            InitializeComponent();
        }
        /// <summary>
        /// Getting the Information from the Upload File 
        /// </summary>
        /// <param name="fileName">Uploaded FileName</param>
        /// <param name="graphActive">Boolean Value to check whether the graph is active</param>
        public void getInformation(string fileName, Boolean graphActive) {
            this.fileName = fileName;
            this.graphActive = graphActive;
        }

        /// <summary>
        /// Splitting the Data on '='
        /// </summary>
        /// <param name="line">The string that is to be split</param>
        /// <returns>Splitted string</returns>
        private string[] SplitEquals(string line) {
            return line.Split('=');
        }

        /// <summary>
        /// Splitting the Data on ' '
        /// </summary>
        /// <param name="line">The string that is to be split</param>
        /// <returns>Splitted string</returns>
        private string[] SplitSpace(string line) {
            return line.Split(' ');
        }

        private void Summary_Load(object sender, EventArgs e) {
            metroGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);
            // For Retrieving Smode and Displaying
            string line;
            bool paramsLine = false;
            // Read and display lines from the file until the end of the file is reached.
            /*while ((line = sr.ReadLine()) != null) {
                
            }*/
            // For Retrieving Smode and Displaying
            Console.WriteLine("Version Version");
            Console.WriteLine(Version);

            HRM = null;
            NumberOfLines = File.ReadAllLines(fileName).Length;
            while ((line = sr.ReadLine()) != null) {

                if (line == "[Params]") {
                    // line = [Params]
                    paramsLine = true;
                }
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

            // Setup an accumulator
            double mph = 0;
            int heartTotal = 0;
            int speedTotal = 0;
            int powerTotal = 0;
            int altitudeTotal = 0;

            // Binary Forms of Smode
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
                        // Measurement unit accordind to Europe
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

                    line = sr.ReadLine();
                    // Adding data to datagridview
                    metroGrid1.Rows.Add(myDateTime, heart, speed1, cadence, altitude, power);
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
                heartAvg = heartTotal / counter;
                speedAvg = (speedTotal / counter);

                powerAvg = powerTotal / counter;
                altAvg = altitudeTotal / counter;

                // Calculating distance travelled 
                double timesec = int.Parse(Interval) * counter;
                double timehr = Math.Round((timesec / 3600), 2);
                double distance = Math.Round((speedAvg / 10), 2) * timehr;

                DistanceLabel.Text = "Distance:\n" + distance.ToString() + " Km/Hr";

                // Calculating Max Heart Rate
                int[] maxHeartRate = (from DataGridViewRow row in metroGrid1.Rows
                                        where row.Cells[1].FormattedValue.ToString() != string.Empty
                                        select Convert.ToInt32(row.Cells[1].FormattedValue)).ToArray();
                HeartRateLabelMax.Text = "Maximum Heart Rate: \n" + maxHeartRate.Max().ToString();

                // Calculating Min Heart Rate
                int[] minHeartRate = (from DataGridViewRow row in metroGrid1.Rows
                                        where row.Cells[1].FormattedValue.ToString() != string.Empty
                                        select Convert.ToInt32(row.Cells[1].FormattedValue)).ToArray();

                if (SMode0 == "1") {
                    //Calculating Max Speed
                    decimal[] maxSpeed = (from DataGridViewRow row in metroGrid1.Rows
                                            where row.Cells[2].FormattedValue.ToString() != string.Empty
                                            select Convert.ToDecimal(row.Cells[2].FormattedValue)).ToArray();
                    SpeedLabelMax.Text = "Maximum Speed Level: \n" + maxSpeed.Max().ToString() + " km/h";
                } else if (SMode0 == "0") {
                    //when smode is 0 all the calculation is 0
                    SpeedLabelMax.Text = "Maximum Speed Level: \n 0 km/h";
                    SpeedLabelAverage.Text = "Speed Level Average: \n 0 Km/hr";
                }

                if (Version == "106" || Version == "107") {
                    if (SMode2 == "1") {
                        // Calculating Max altitude
                        int[] maxAltitude = (from DataGridViewRow row in metroGrid1.Rows
                                                where row.Cells[4].FormattedValue.ToString() != string.Empty
                                                select Convert.ToInt32(row.Cells[4].FormattedValue)).ToArray();
                        AltitudeLevelMax.Text = "Maximum Altitude: \n" + maxAltitude.Max().ToString() + " Meters";
                        altitudeRateMax = maxAltitude.Max();
                    } else if (SMode2 == "0") {
                        // When smode is 0 all the calculation is 0
                        AltitudeLevelMax.Text = "Maximum Altitude: \n 0 Meters";
                        AltitudeLabelAverage.Text = "Altitude Average: \n 0 Meters";
                        altitudeRateMax = 0;
                    }
                    if (SMode3 == "1") {
                        // Calculating Max Power
                        int[] maxPower = (from DataGridViewRow row in metroGrid1.Rows
                                            where row.Cells[5].FormattedValue.ToString() != string.Empty
                                            select Convert.ToInt32(row.Cells[5].FormattedValue)).ToArray();
                        PowerLabelMax.Text = "Maximum Power Level: \n" + maxPower.Max().ToString() + " watts";
                        powerRateMax = maxPower.Max();
                    } else if (SMode3 == "0") {
                        // When smode is 0 all the calculation is 0
                        PowerLabelMax.Text = "Maximum Power Level:\n 0 Watts";
                        PowerLabelAverage.Text = "Power Level Average:\n 0 Watts";
                        powerRateMax = 0;
                    }
                }
            }

            // Setting all averages with corresponding measurement units 
            SpeedLabelAverage.Text = "Speed Average:\n" + Math.Round((speedAvg / 10), 2) + " km/h";
            HeartRateLabelAverage.Text = "Heart Rate Average:\n" + heartAvg.ToString();
            PowerLabelAverage.Text = "Power Level Average:\n" + powerAvg.ToString() + " watts";
            AltitudeLabelAverage.Text = "Altitude Average:\n" + altAvg.ToString() + " meters";

            if (graphActive == false) {
                // Do Nothing
            } else {
                Graph graph = new Graph();
                graph.getInformation(fileName, 
                                        MaxHR, 
                                        StartTime, 
                                        metroGrid1, 
                                        Version, 
                                        SMode, 
                                        SMode0, 
                                        SMode1, 
                                        SMode2, 
                                        SMode3,
                                        SMode4,
                                        SMode5,
                                        SMode6,
                                        SMode7,
                                        SMode8,
                                        seconds);
                graph.Show();
                this.Hide();
            }

        }
    }
}
