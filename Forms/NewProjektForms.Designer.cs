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
            this.button1 = new System.Windows.Forms.Button();
            this.projektNameTextBox = new System.Windows.Forms.TextBox();
            this.projektServiceFileTextBox = new System.Windows.Forms.TextBox();
            this.projektNameLabel = new System.Windows.Forms.Label();
            this.projektServiceFileLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // projektNameTextBox
            // 
            this.projektNameTextBox.Location = new System.Drawing.Point(83, 94);
            this.projektNameTextBox.Name = "projektNameTextBox";
            this.projektNameTextBox.Size = new System.Drawing.Size(100, 23);
            this.projektNameTextBox.TabIndex = 1;
            this.projektNameTextBox.TextChanged += new System.EventHandler(this.projektNameTextBox_TextChanged);
            // 
            // projektServiceFileTextBox
            // 
            this.projektServiceFileTextBox.Location = new System.Drawing.Point(83, 152);
            this.projektServiceFileTextBox.Name = "projektServiceFileTextBox";
            this.projektServiceFileTextBox.Size = new System.Drawing.Size(100, 23);
            this.projektServiceFileTextBox.TabIndex = 2;
            // 
            // projektNameLabel
            // 
            this.projektNameLabel.AutoSize = true;
            this.projektNameLabel.Location = new System.Drawing.Point(85, 76);
            this.projektNameLabel.Name = "projektNameLabel";
            this.projektNameLabel.Size = new System.Drawing.Size(75, 15);
            this.projektNameLabel.TabIndex = 3;
            this.projektNameLabel.Text = "Projekt neve:";
            // 
            // projektServiceFileLabel
            // 
            this.projektServiceFileLabel.AutoSize = true;
            this.projektServiceFileLabel.Location = new System.Drawing.Point(83, 134);
            this.projektServiceFileLabel.Name = "projektServiceFileLabel";
            this.projektServiceFileLabel.Size = new System.Drawing.Size(105, 15);
            this.projektServiceFileLabel.TabIndex = 4;
            this.projektServiceFileLabel.Text = "Projekt service fajl:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(195, 313);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(71, 313);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // NewProjektForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.projektServiceFileLabel);
            this.Controls.Add(this.projektNameLabel);
            this.Controls.Add(this.projektServiceFileTextBox);
            this.Controls.Add(this.projektNameTextBox);
            this.Controls.Add(this.button1);
            this.Name = "NewProjektForms";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.NewProjektForms_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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