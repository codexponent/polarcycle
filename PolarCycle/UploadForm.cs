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
    public partial class UploadForm : MetroForm {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private string fullPathName;
        private string fileName;
        private string fullPathName1;
        private string fullPathName2;
        private string fileName1;
        private string fileName2;
        public UploadForm() {
            InitializeComponent();
        }

        private void UploadForm_Load(object sender, EventArgs e) {

        }
        
        /// <summary>
        /// Open File Explorer Here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTile1_Click(object sender, EventArgs e) {
            openFileDialog.Title = "Open .HRM File";
            openFileDialog.Filter = "HRM files (*.hrm)|*.hrm";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                // Fully Qualified Name
                try {
                    fullPathName = Path.GetFullPath(openFileDialog.FileName);
                    fileName = openFileDialog.FileName;
                    MetroMessageBox.Show(this, "File Name: " + fullPathName, "File Successfully Uploaded and Parsed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    HomePage homePage = new HomePage();
                    homePage.getInformation(fileName);
                    homePage.Show();
                    this.Hide();
                } catch (Exception error) {
                    MetroMessageBox.Show(this, "Canceled", error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.None);
                    throw;
                }

            } else if (openFileDialog.ShowDialog() == DialogResult.Cancel) {
                MetroMessageBox.Show(this, "Canceled", "File Uploaded Canceled", MessageBoxButtons.OK, MessageBoxIcon.None);
            }

        }

        private void metroTile2_Click(object sender, EventArgs e) {
            LiveGraph liveGraph = new LiveGraph();
            liveGraph.Show();
            this.Hide();
        }

        private void UploadForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void metroTile3_Click(object sender, EventArgs e) {
            openFileDialog.Title = "Open .HRM File";
            openFileDialog.Filter = "HRM files (*.hrm)|*.hrm";
            openFileDialog.Multiselect = true;
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                // Fully Qualified Name
                try {
                    fullPathName1 = Path.GetFullPath(openFileDialog.FileNames[0]);
                    fullPathName2 = Path.GetFullPath(openFileDialog.FileNames[1]);

                    fileName1 = openFileDialog.FileNames[0];
                    fileName2 = openFileDialog.FileNames[1];
                    MetroMessageBox.Show(this, "File Name: " + fullPathName1 + fullPathName2, "File Successfully Uploaded and Parsed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //HomePage homePage = new HomePage();
                    //homePage.getInformation(fileName);
                    //homePage.Show();
                    //this.Hide();
                    Comparison comparison = new Comparison();
                    comparison.getInformation(fileName1, fileName2);
                    comparison.Show();
                    this.Hide();
                } catch (Exception error) {
                    MetroMessageBox.Show(this, "Canceled", error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.None);
                    throw;
                }

            } else if (openFileDialog.ShowDialog() == DialogResult.Cancel) {
                MetroMessageBox.Show(this, "Canceled", "File Uploaded Canceled", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }
    }
}
