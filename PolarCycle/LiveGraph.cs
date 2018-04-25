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
using System.Threading;

namespace PolarCycle {
    public partial class LiveGraph : MetroForm {
        GraphPane myPane;
        DateTime dateTime;
        DateTime endDateTime;
        PointPairList hrList = new PointPairList();
        LineItem hrCurve;
        public LiveGraph() {
            InitializeComponent();
        }

        private void LiveGraph_Load(object sender, EventArgs e) {
            myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Heart Rate Graph";
            myPane.XAxis.Title.Text = "Bits Per Second";
            myPane.YAxis.Title.Text = "BPM";

            serialPort1.Open();
            timer1.Start();

            //(EndDate - StartDate).TotalDays
            dateTime = DateTime.Now;
            Console.WriteLine(dateTime);
            drawGraph();
        }

        private void drawGraph() {
            // Displaying heart rate in yaxis in red color
            //var YAxis = myPane.AddYAxis("Heart Rate");
            //myPane.YAxis.Color = System.Drawing.Color.Red;

            

            

            // Setting min scale of xaxis to starttime and max scale to endtime
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 300;

            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 360;
            // myPane.XAxis.Scale.MinorUnit = DateUnit.Second;
            // myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;

            // Scroll min to start time and scroll max to endtime
            zedGraphControl1.ScrollMinX = 0;
            zedGraphControl1.ScrollMaxX = 100;

            // myPane.XAxis.Type = AxisType.Date;
            // myPane.XAxis.Scale.Format = "HH:mm:ss";
            myPane.XAxis.MinorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.IsVisible = true;

            //double xCoord = 0;
            // Point pair list for graph
            
            // speedList = new PointPairList();
            // cadenceList = new PointPairList();
            // altitudeList = new PointPairList();
            // powerList = new PointPairList();
            int counter = 0;
            //xCoord = (double)new XDate(startDate.AddSeconds(seconds * counter));
            

            //for (int i = 0; i < 1024; i++) {
                //hrList.Add(i, i);
            //}

            hrCurve = myPane.AddCurve("Heart Rate", hrList, System.Drawing.Color.Red, SymbolType.None);
            //hrCurve.YAxisIndex = YAxis;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            
        }   // Draw Graph

        private void metroButton1_Click(object sender, EventArgs e) {
            UploadForm uploadForm = new UploadForm();
            uploadForm.Show();
            this.Hide();
            serialPort1.Close();
        }

        private void LiveGraph_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        double xCoord = 0;
        double yCoord = 0;
        double heartYCord = 0;
        string reading = "";
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {

            //  Thread th = Thread.CurrentThread;
            //th.Name = "MainThread";

            //Console.WriteLine("This is {0}", th.Name);
            //Thread.Sleep(1000);
            //Console.WriteLine("Child thread resumes");

            endDateTime = DateTime.Now;
            reading = serialPort1.ReadLine();
            if (Double.TryParse(reading, out yCoord)) {
                Console.WriteLine("readigg raw");
                Console.WriteLine(reading);
                yCoord = Double.Parse(reading);
                
                    heartYCord += yCoord;
                
                
                //150
                hrList.Add(xCoord, yCoord);

                if ((endDateTime - dateTime).TotalSeconds > 60) {
                    MetroMessageBox.Show(this, "Your Heart Rate is: " + heartYCord/xCoord, "Heart Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    xCoord += 1;
                    zedGraphControl1.Invalidate();
                }

                
            }

        }
        
        private void timer1_Tick(object sender, EventArgs e) {
        }
    }
}
