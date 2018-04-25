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
using System.Collections;
using System.Text.RegularExpressions;

namespace PolarCycle {
    public partial class Details : MetroForm {
        private string fileName;
        //Initializes a new instance of the Dictionary
        IDictionary<string, string> parameter = new Dictionary<string, string>();
        public Details() {
            InitializeComponent();
        }
        /// <summary>
        /// Getting the Information from the Upload File 
        /// </summary>
        /// <param name="fileName">Uploaded FileName</param>
        public void getInformation(string fileName) {
            this.fileName = fileName;
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
        /// Gives the date in a user specified format
        /// </summary>
        /// <param name="date">String which contains the date</param>
        /// <param name="time">String which contains the time</param>
        /// <returns>Returns a DateTime with year, month, day, hour, min, sec</returns>
        private DateTime GetDate(string date, string time) {
            int year = Convert.ToInt32(date.Substring(0, 4));
            int month = Convert.ToInt32(date.Substring(4, 2));
            int day = Convert.ToInt32(date.Substring(6, 2));

            string[] timeComponents = time.Split(':');
            int hour = Convert.ToInt32(timeComponents[0]);
            int min = Convert.ToInt32(timeComponents[1]);
            int sec = (int)Convert.ToDouble(timeComponents[2]);

            DateTime start = new DateTime(year, month, day, hour, min, sec);
            return start;
        }

        private void Details_Load(object sender, EventArgs e) {
            try {
                using (StreamReader sr = new StreamReader(fileName)) {
                    string line;
                    bool paramsLine = false;
                    // Read and display lines from the file until the end of the file is reached.
                    while ((line = sr.ReadLine()) != null) { // Read until the [Params]
                        if (line == "[Params]") {
                            // line = [Params]
                            paramsLine = true;
                        }

                        if (paramsLine == true) {
                            string[] parm = SplitEquals(line);
                            if (parm.Length > 1) {
                                // Checking for 2 parameters
                                Console.WriteLine("Param    " + parm[0] + " " + parm[1]);
                                // Adding to the dictionary
                                parameter.Add(parm[0], parm[1]);

                                if (line.Substring(0, 1) == "[") {
                                    // Checking for another List and stopping unwanted code to run; EOParamsLine
                                    // Could have also checked for "[Note]" but it is not the same for all hm file
                                    paramsLine = false;
                                }
                            }
                        }
                    }
                }
                StartTime.Text = "StartTime:\n" + parameter["StartTime"];
                Monitor.Text = "Monitor:\n" + parameter["Monitor"];
                Version.Text = "Version:\n" + parameter["Version"];
                SMode.Text = "SMode:\n" + parameter["SMode"];
                DateLabel.Text = "Date:\n" + GetDate(parameter["Date"], parameter["StartTime"]).ToLongDateString();
                Length.Text = "Length:\n" + parameter["Length"];
                MaxHR.Text = "MaxHR:\n" + parameter["MaxHR"];
                RestHR.Text = "RestHR:\n" + parameter["RestHR"];
                VO2max.Text = "VO2max:\n" + parameter["RestHR"];
                Weight.Text = "Weight:\n" + parameter["Weight"];
                Interval.Text = "Interval:\n" + parameter["Interval"];
            } catch (Exception ex) {
                MetroMessageBox.Show(this, ex.ToString(), "Could not read the contents", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void metroButton1_Click(object sender, EventArgs e) {
            HomePage homePage = new HomePage();
            homePage.getInformation(fileName);
            homePage.Show();
            this.Hide();
        }

        private void Details_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
