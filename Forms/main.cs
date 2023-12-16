using Firebase.Models;
using Firebase.Services;
using FirebaseBackupWindowsForm.Forms;

namespace FirebaseBackupWindowsForm
{
    public partial class main : Form
    {
        public static BackupService backupService = new();
        public static ProjectService projectService = new();
        NewProjektForms newProjektForm = new NewProjektForms();
        public main()
        {
            InitializeComponent();
        }


        public void GetAllProjects()
        {
            listView1.Items.Clear();
            foreach (var project in projectService.GetAllProjects())
            {
                ListViewItem item = new ListViewItem(project.ProjectId);

                listView1.Items.Add(item);
            }

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

        private void NewProjectButton_Click(object sender, EventArgs e)
        {
            newProjektForm.ShowDialog();
            GetAllProjects();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                projectService.DeleteProject(projectService.findById(listView1.SelectedItems[0].Text));
            }
            GetAllProjects();
        }
    }
}