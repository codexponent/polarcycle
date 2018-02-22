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
    public partial class HomePage : MetroForm {
        private string fileName;
        public HomePage() {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e) {

        }

        /// <summary>
        /// Getting the Information from the Upload File 
        /// </summary>
        /// <param name="fileName">Uploaded FileName</param>
        public void getInformation(string fileName) {
            this.fileName = fileName;
        }

        /// <summary>
        /// Details Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTile1_Click(object sender, EventArgs e) {
            Details details = new Details();
            details.getInformation(fileName);
            details.Show();
            this.Hide();
        }
        /// <summary>
        /// Summary Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTile2_Click(object sender, EventArgs e) {
            Summary summary = new Summary();
            summary.getInformation(fileName, false);
            summary.Show();
            this.Hide();
        }
        /// <summary>
        /// Graph Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTile3_Click(object sender, EventArgs e) {
            Summary summary = new Summary();
            summary.getInformation(fileName, true);
            summary.Show();
            this.Hide();
        }
    }
}
