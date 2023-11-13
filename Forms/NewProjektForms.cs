using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Services;

namespace FirebaseBackupWindowsForm.Forms
{
    public partial class NewProjektForms : Form
    {
        ProjectService projectService;
        String projektName;
        String projektServiceFile;
        public NewProjektForms()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.RestoreDirectory = true;

                openFileDialog.Filter = "json files |*.json";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // A kiválasztott fájl elérési útját elérheted az OpenFileDialog.FileName tulajdonságon keresztül
                    projektServiceFile = openFileDialog.Selected

                }
                   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewProjektForms_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void projektNameTextBox_TextChanged(object sender, EventArgs e)
        {
            projektName = projektNameTextBox.Text;
        }
    }
}
