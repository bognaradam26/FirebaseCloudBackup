using Firebase.Models;
using Firebase.Services;

namespace FirebaseBackupWindowsForm.Forms
{
    public partial class ProjektInfoForm : Form
    {
        Project showedProject;
        public ProjektInfoForm(Project project)
        {
            InitializeComponent();

            showedProject = project;
            richTextBox1.Text += "Projekt id:" + "\n" + project.ProjectId.ToString() + "\n" + "Service account file path:" + "\n" + project.ServiceAccountFilePath;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Project backup = new("utalom-3b9c1", "D:\\projects\\FirebaseBackupWindowsForm\\utalom-3b9c1.json");
            string json = await BackupService.BackupData(backup);
            richTextBox2.Text = json;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
