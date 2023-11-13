using Firebase.Services;
using Firebase.Models;

namespace FirebaseBackupWindowsForm.Forms
{
    public partial class NewProjektForms : Form
    {
        public static ProjectService projectService = new();
        String projektName;
        String projektServiceFile;
        public NewProjektForms()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.RestoreDirectory = true;

            openFileDialog.Filter = "json files |*.json";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // A kiválasztott fájl elérési útját elérheted az OpenFileDialog.FileName tulajdonságon keresztül
                projektServiceFileTextBox.Text = openFileDialog.FileName;
            }

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void projektNameTextBox_TextChanged(object sender, EventArgs e)
        {
            projektName = projektNameTextBox.Text;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (projektName != null && projektServiceFile != null)
            {
                Console.WriteLine("Asddd: " + projektName.ToString() + "\n" + projektServiceFile.ToString());
                Project projectToSave = new Project(projektName.ToString(), projektServiceFile.ToString());
                projectService.AddProject(projectToSave);
                Close();
            } else
            {
                throw new Exception("Üres mező");
            }
            
        }

        private void projektServiceFileTextBox_TextChanged(object sender, EventArgs e)
        {
            projektServiceFile = projektServiceFileTextBox.Text;
        }
    }
}
