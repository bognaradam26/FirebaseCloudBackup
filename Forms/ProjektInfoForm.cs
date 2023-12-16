using Firebase.Models;
using Firebase.Services;
using FirebaseBackupWindowsForm.Services;

namespace FirebaseBackupWindowsForm.Forms
{
    public partial class ProjektInfoForm : Form
    {
        public ProjektInfoForm(Project project)
        {
            InitializeComponent();

            richTextBox1.Text += "Projekt id:" + "\n" + project.ProjectId.ToString() + "\n" + "Service account file path:" + "\n" + project.ServiceAccountFilePath;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100; // Beállítjuk a ProgressBar maximális értékét
            progressBar1.Value = 0;
            Project backup = new("utalom-3b9c1", Directory.GetCurrentDirectory() + "\\utalom-3b9c1.json");
            await Task.Run(() => BackupService.BackupData(backup, progressBar1));
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            Project backup = new("utalom-3b9c1", Directory.GetCurrentDirectory() + "\\utalom-3b9c1.json");
            await RestoreServices.RestoreData(backup);
        }
    }
}
