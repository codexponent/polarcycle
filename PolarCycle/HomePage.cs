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

        private void metroTile1_Click(object sender, EventArgs e) {
            Details details = new Details();
            details.getInformation(fileName);
            details.Show();
            this.Hide();
        }

        private void metroTile2_Click(object sender, EventArgs e) {
            Summary summary = new Summary();
            summary.getInformation(fileName);
            summary.Show();
            this.Hide();
        }

        private void metroTile3_Click(object sender, EventArgs e) {
            Graph graph = new Graph();
            graph.getInformation(fileName);
            graph.Show();
            this.Hide();
        }
    }
}
