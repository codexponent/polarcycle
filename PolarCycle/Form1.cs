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
    public partial class Form1 : MetroForm {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void metroTile1_Click(object sender, EventArgs e) {
            UploadForm uploadForm = new UploadForm();
            uploadForm.Show();
            this.Hide();
        }
    }
}
