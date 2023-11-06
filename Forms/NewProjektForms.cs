using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirebaseBackupWindowsForm.Forms
{
    public partial class NewProjektForms : Form
    {
        public NewProjektForms() => InitializeComponent();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.json";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // A kiválasztott fájl elérési útját elérheted az OpenFileDialog.FileName tulajdonságon keresztül
                string selectedFilePath = ofd.FileName;
            }
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
