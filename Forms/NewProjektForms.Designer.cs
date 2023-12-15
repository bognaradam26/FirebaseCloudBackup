namespace FirebaseBackupWindowsForm.Forms
{
    partial class NewProjektForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [STAThread]
        private void InitializeComponent()
        {
            button1 = new Button();
            projektNameTextBox = new TextBox();
            projektServiceFileTextBox = new TextBox();
            projektNameLabel = new Label();
            projektServiceFileLabel = new Label();
            openFileDialog1 = new OpenFileDialog();
            backButton = new Button();
            saveButton = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(195, 152);
            button1.Name = "button1";
            button1.Size = new Size(57, 23);
            button1.TabIndex = 0;
            button1.Text = "Keresés";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // projektNameTextBox
            // 
            projektNameTextBox.Location = new Point(83, 94);
            projektNameTextBox.Name = "projektNameTextBox";
            projektNameTextBox.Size = new Size(100, 23);
            projektNameTextBox.TabIndex = 1;
            projektNameTextBox.TextChanged += projektNameTextBox_TextChanged;
            // 
            // projektServiceFileTextBox
            // 
            projektServiceFileTextBox.Location = new Point(83, 152);
            projektServiceFileTextBox.Name = "projektServiceFileTextBox";
            projektServiceFileTextBox.Size = new Size(100, 23);
            projektServiceFileTextBox.TabIndex = 2;
            projektServiceFileTextBox.TextChanged += projektServiceFileTextBox_TextChanged;
            // 
            // projektNameLabel
            // 
            projektNameLabel.AutoSize = true;
            projektNameLabel.Location = new Point(85, 76);
            projektNameLabel.Name = "projektNameLabel";
            projektNameLabel.Size = new Size(75, 15);
            projektNameLabel.TabIndex = 3;
            projektNameLabel.Text = "Projekt neve:";
            // 
            // projektServiceFileLabel
            // 
            projektServiceFileLabel.AutoSize = true;
            projektServiceFileLabel.Location = new Point(83, 134);
            projektServiceFileLabel.Name = "projektServiceFileLabel";
            projektServiceFileLabel.Size = new Size(105, 15);
            projektServiceFileLabel.TabIndex = 4;
            projektServiceFileLabel.Text = "Projekt service fájl:";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // backButton
            // 
            backButton.Location = new Point(195, 313);
            backButton.Name = "backButton";
            backButton.Size = new Size(75, 23);
            backButton.TabIndex = 5;
            backButton.Text = "Vissza";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += backButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(71, 313);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 6;
            saveButton.Text = "Mentés";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // NewProjektForms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(saveButton);
            Controls.Add(backButton);
            Controls.Add(projektServiceFileLabel);
            Controls.Add(projektNameLabel);
            Controls.Add(projektServiceFileTextBox);
            Controls.Add(projektNameTextBox);
            Controls.Add(button1);
            Name = "NewProjektForms";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox projektNameTextBox;
        private TextBox projektServiceFileTextBox;
        private Label projektNameLabel;
        private Label projektServiceFileLabel;
        private OpenFileDialog openFileDialog1;
        private Button backButton;
        private Button saveButton;
    }
}