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

namespace PolarCycle {
    public partial class Interval : MetroForm {
        string[] columnNames = new string[] { "Heart Rate", "Speed", "Cadence", "Altitude", "Power", "Time" };
        double thresholdPower;
        double ftp;
        Dictionary<string, string> param;
        string IntervalData;
        List<Data> DataList;

        public Interval() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets all the information from Graph
        /// </summary>
        /// <param name="param">Dictionary to be used</param>
        /// <param name="IntervalData">The Interval Data</param>
        /// <param name="DataList">DataList of the corresponding filename</param>
        public void getInformation(Dictionary<string, string> param, string IntervalData, List<Data> DataList, double threshold, double ftp) {
            this.param = param;
            this.IntervalData = IntervalData;
            this.DataList = DataList;
            this.thresholdPower = threshold;
            this.ftp = ftp;
            Console.WriteLine("Check Interval Here");
            Console.WriteLine("Param");
            Console.WriteLine(param);
            Console.WriteLine("IntervalData");
            Console.WriteLine(IntervalData);
            Console.WriteLine("DataList");
            Console.WriteLine(DataList);
        }

        private void Interval_Load(object sender, EventArgs e) {
            IntervalDetect();
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
        /// Function to Detect Interval
        /// </summary>
        private void IntervalDetect() {
            DateTime dtime;
            DataTable dt = new DataTable();
            //checking speed units
            string speedUnit = (isEuroUnitUsed() ? " km/h" : " mph");
            //checking altitude units
            string altitudeUnit = (isEuroUnitUsed() ? " meters" : " feet");
            //getting date value from header
            string date = param["Date"];
            Console.WriteLine("Date");
            Console.WriteLine(date);
            //getting time value from header
            string[] timesArr = param["StartTime"].Split(':');
            //getting year from date
            int year = Convert.ToInt32(date.Substring(0, 4));
            //getting month from date
            int month = Convert.ToInt32(date.Substring(4, 2));
            //getting day from date
            int day = Convert.ToInt32(date.Substring(6, 2));
            //getting hour from timearr
            int hour = Convert.ToInt32(timesArr[0]);
            //getting minutes from timearr
            int minutes = Convert.ToInt32(timesArr[1]);
            //getting seconds from timearr
            int seconds = (int)Convert.ToDouble(timesArr[2]);

            dtime = new DateTime(year, month, day, hour, minutes, seconds);

            int i = 0;
            int startIndex = 0;
            int counter = 0;
            int numIntervals = 0;
            int interval = int.Parse(IntervalData);

            Console.WriteLine("Interval Data Here");
            Console.WriteLine(interval);

            List<Data> filtered;
            intervalPanel.Controls.Clear();
            //adding interval columns in datagrid view
            dt.Columns.Add("Interval");

            foreach (string col in columnNames) {
                dt.Columns.Add(col);
            }

            //when input power is greter than thresoldpower
            foreach (Data hr in DataList) {
                if (hr.power > thresholdPower) {
                    if (counter == 0) {
                        startIndex = i;
                    }
                    counter++;
                } else {
                    if (counter >= 10) {
                        numIntervals++;
                        filtered = new List<Data>();

                        for (int j = startIndex; j < i; j++) {
                            Data hrd = new Data();

                            hrd.heart_rate = DataList[j].heart_rate;
                            hrd.speed = DataList[j].speed;
                            hrd.cadence = DataList[j].cadence;
                            hrd.altitude = DataList[j].altitude;
                            hrd.power = DataList[j].power;

                            filtered.Add(hrd);

                            DataRow dr = dt.NewRow();
                            //for interval
                            dr[0] = "Interval " + numIntervals;
                            //for heart rate
                            dr[1] = hrd.heart_rate;
                            //for speed
                            dr[2] = hrd.speed;
                            //for cadence
                            dr[3] = hrd.cadence;
                            //for altitude
                            dr[4] = hrd.altitude;
                            //for power
                            dr[5] = hrd.power;
                            //for time
                            dr[6] = dtime.AddSeconds(j * interval).ToLongTimeString();

                            dt.Rows.Add(dr);
                        }

                        System.Windows.Forms.Label headingLabel = new System.Windows.Forms.Label();

                        headingLabel.AutoSize = true;
                        headingLabel.Font = new System.Drawing.Font("trebuchet ms", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        headingLabel.Name = "intervalHeading" + numIntervals;
                        headingLabel.Text = "Interval " + numIntervals + ":";
                        headingLabel.Location = new Point(0, (numIntervals - 1) * 175);

                        System.Windows.Forms.Label detailsLabel = new System.Windows.Forms.Label();

                        //passing all intervals information to detailsLabel 
                        detailsLabel.AutoSize = true;
                        detailsLabel.Font = new System.Drawing.Font("calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        detailsLabel.Name = "detailsInterval" + numIntervals;
                        detailsLabel.Text = "Average Heart Rate: \t" + Math.Round(filtered.Average(data => data.heart_rate)) + " bpm \n"
                                            + "Average Speed: \t" + Math.Round(filtered.Average(data => data.speed)) + speedUnit + " \n"
                                            + "Average Cadence: \t" + Math.Round(filtered.Average(data => data.cadence)) + " \n"
                                            + "Average Altitude: \t" + Math.Round(filtered.Average(data => data.altitude)) + altitudeUnit + " \n"
                                            + "Average Power: \t" + Math.Round(filtered.Average(data => data.power)) + " watts \n";

                        detailsLabel.Location = new Point(0, 25 + (numIntervals - 1) * 175);

                        intervalPanel.Controls.Add(headingLabel);
                        intervalPanel.Controls.Add(detailsLabel);
                    }
                    counter = 0;
                }
                i++;
            }
            //adding data into intervalsGrird
            intervalGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            intervalGrid.DataSource = dt;
        }

        private void IntervalDetectWithParams(int num) {
            DateTime dtime;
            DataTable dt = new DataTable();
            //checking speed units
            string speedUnit = (isEuroUnitUsed() ? " km/h" : " mph");
            //checking altitude units
            string altitudeUnit = (isEuroUnitUsed() ? " meters" : " feet");
            //getting date value from header
            string date = param["Date"];
            Console.WriteLine("Date");
            Console.WriteLine(date);
            //getting time value from header
            string[] timesArr = param["StartTime"].Split(':');
            //getting year from date
            int year = Convert.ToInt32(date.Substring(0, 4));
            //getting month from date
            int month = Convert.ToInt32(date.Substring(4, 2));
            //getting day from date
            int day = Convert.ToInt32(date.Substring(6, 2));
            //getting hour from timearr
            int hour = Convert.ToInt32(timesArr[0]);
            //getting minutes from timearr
            int minutes = Convert.ToInt32(timesArr[1]);
            //getting seconds from timearr
            int seconds = (int)Convert.ToDouble(timesArr[2]);

            dtime = new DateTime(year, month, day, hour, minutes, seconds);

            int i = 0;
            int startIndex = 0;
            int counter = 0;
            int numIntervals = 0;
            int interval = int.Parse(IntervalData);

            Console.WriteLine("Interval Data Here");
            Console.WriteLine(interval);

            List<Data> filtered;
            intervalPanel.Controls.Clear();
            //adding interval columns in datagrid view
            dt.Columns.Add("Interval");

            foreach (string col in columnNames) {
                dt.Columns.Add(col);
            }

            //when input power is greter than thresoldpower
            foreach (Data hr in DataList) {
                if (hr.power > thresholdPower) {
                    if (counter == 0) {
                        startIndex = i;
                    }
                    counter++;
                } else {
                    if (counter >= num) {
                        numIntervals++;
                        filtered = new List<Data>();

                        for (int j = startIndex; j < i; j++) {
                            Data hrd = new Data();

                            hrd.heart_rate = DataList[j].heart_rate;
                            hrd.speed = DataList[j].speed;
                            hrd.cadence = DataList[j].cadence;
                            hrd.altitude = DataList[j].altitude;
                            hrd.power = DataList[j].power;

                            filtered.Add(hrd);

                            DataRow dr = dt.NewRow();
                            //for interval
                            dr[0] = "Interval " + numIntervals;
                            //for heart rate
                            dr[1] = hrd.heart_rate;
                            //for speed
                            dr[2] = hrd.speed;
                            //for cadence
                            dr[3] = hrd.cadence;
                            //for altitude
                            dr[4] = hrd.altitude;
                            //for power
                            dr[5] = hrd.power;
                            //for time
                            dr[6] = dtime.AddSeconds(j * interval).ToLongTimeString();

                            dt.Rows.Add(dr);
                        }

                        System.Windows.Forms.Label headingLabel = new System.Windows.Forms.Label();

                        headingLabel.AutoSize = true;
                        headingLabel.Font = new System.Drawing.Font("trebuchet ms", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        headingLabel.Name = "intervalHeading" + numIntervals;
                        headingLabel.Text = "Interval " + numIntervals + ":";
                        headingLabel.Location = new Point(0, (numIntervals - 1) * 175);

                        System.Windows.Forms.Label detailsLabel = new System.Windows.Forms.Label();

                        //passing all intervals information to detailsLabel 
                        detailsLabel.AutoSize = true;
                        detailsLabel.Font = new System.Drawing.Font("calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        detailsLabel.Name = "detailsInterval" + numIntervals;
                        detailsLabel.Text = "Average Heart Rate: \t" + Math.Round(filtered.Average(data => data.heart_rate)) + " bpm \n"
                                            + "Average Speed: \t" + Math.Round(filtered.Average(data => data.speed)) + speedUnit + " \n"
                                            + "Average Cadence: \t" + Math.Round(filtered.Average(data => data.cadence)) + " \n"
                                            + "Average Altitude: \t" + Math.Round(filtered.Average(data => data.altitude)) + altitudeUnit + " \n"
                                            + "Average Power: \t" + Math.Round(filtered.Average(data => data.power)) + " watts \n";

                        detailsLabel.Location = new Point(0, 25 + (numIntervals - 1) * 175);

                        intervalPanel.Controls.Add(headingLabel);
                        intervalPanel.Controls.Add(detailsLabel);
                    }
                    counter = 0;
                }
                i++;
            }
            //adding data into intervalsGrird
            intervalGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            intervalGrid.DataSource = dt;
        }

        private void Interval_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e) {
            Random random = new Random();
            int num = random.Next(1, 10);
            IntervalDetectWithParams(num);
        }

        private void metroButton2_Click(object sender, EventArgs e) {
            //double num = ftp + 0.1;
            double num = (ftp + 0.1)/100;
            //double num = ftp * 0.1;
            int numCasted = 20 * Convert.ToInt32(num);
            Console.WriteLine("NumCasted");
            Console.WriteLine(numCasted);
            IntervalDetectWithParams(numCasted);
        }

        private void metroButton3_Click(object sender, EventArgs e) {
            IntervalDetect();
        }
    }
}
