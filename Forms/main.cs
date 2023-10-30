using Firebase.Models;
using Firebase.Services;
using FirebaseBackupWindowsForm.Forms;

namespace FirebaseBackupWindowsForm
{
    public partial class main : Form
    {
        public static BackupService backupService = new();
        public static ProjectService projectService = new();
        public main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Project selectedProject = projectService.findById(listView1.SelectedItems[0].Text);

                ProjektInfoForm projektInfoForm = new ProjektInfoForm(selectedProject);
                projektInfoForm.ShowDialog();
            }
        }
    }
}