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
        String ProjektServiceFile;
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
                    string selectedFilePath = openFileDialog.FileName;
                }
                   
        }

        
    }
}
