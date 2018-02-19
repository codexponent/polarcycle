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
                fullPathName = Path.GetFullPath(openFileDialog.FileName);
                fileName = openFileDialog.FileName;
                MetroMessageBox.Show(this, "File Name: " + fullPathName, "File Successfully Uploaded and Parsed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HomePage homePage = new HomePage();
                homePage.getInformation(fileName);
                homePage.Show();
                this.Hide();                

            } else if (openFileDialog.ShowDialog() == DialogResult.Cancel) {
                MetroMessageBox.Show(this, "Canceled", "File Uploaded Canceled", MessageBoxButtons.OK, MessageBoxIcon.None);
            }

        }
    }
}
