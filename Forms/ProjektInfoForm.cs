using Firebase.Models;
using Firebase.Services;
using FirebaseBackupWindowsForm.Services;

namespace FirebaseBackupWindowsForm.Forms
{
    public partial class ProjektInfoForm : Form
    {
        public static ProjectService projectService = new();
        private Project hasznaltprojekt;
        public ProjektInfoForm(Project project)
        {
            InitializeComponent();
            this.hasznaltprojekt = new Project(project.ProjectId, project.ServiceAccountFilePath, project.LastBackupDate);
            richTextBox1.Text += project.ProjectId.ToString();
            richTextBox2.Text += project.ServiceAccountFilePath.ToString();
            richTextBox3.Text += project.LastBackupDate;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100; // Beállítjuk a ProgressBar maximális értékét
            progressBar1.Value = 0;
            await Task.Run(() => BackupService.BackupData(hasznaltprojekt, progressBar1));
            richTextBox3.Text = DateTime.Now.ToString();

        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            await RestoreServices.RestoreData(hasznaltprojekt);
        }

    }
}
