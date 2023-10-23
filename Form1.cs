using Firebase.Models;
using Firebase.Services;

namespace FirebaseBackupWindowsForm
{
    public partial class Form1 : Form
    {
        public static BackupService backupService = new();
        public static ProjectService projectService = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        public void GetAllProjects()
        {
            foreach (var project in projectService.GetAllProjects())
            {
                ListViewItem item = new ListViewItem(project.ProjectId);

                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Project backup = new("utalom-3b9c1", "D:\\projects\\FirebaseBackupWindowsForm\\utalom-3b9c1.json");
                string json = await BackupService.BackupData(backup);
                richTextBox1.AppendText(json);
            }
        }
    }
}