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
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(195, 220);
            button1.Name = "button1";
            button1.Size = new Size(25, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // textBox1
            // 
            projektNameTextBox.Location = new Point(83, 94);
            projektNameTextBox.Name = "textBox1";
            projektNameTextBox.Size = new Size(100, 23);
            projektNameTextBox.TabIndex = 1;
            // 
            // textBox2
            // 
            projektServiceFileTextBox.Location = new Point(89, 220);
            projektServiceFileTextBox.Name = "textBox2";
            projektServiceFileTextBox.Size = new Size(100, 23);
            projektServiceFileTextBox.TabIndex = 2;
            // 
            // label1
            // 
            projektNameLabel.AutoSize = true;
            projektNameLabel.Location = new Point(85, 76);
            projektNameLabel.Name = "label1";
            projektNameLabel.Size = new Size(38, 15);
            projektNameLabel.TabIndex = 3;
            projektNameLabel.Text = "Projekt neve:";
            // 
            // label2
            // 
            projektServiceFileLabel.AutoSize = true;
            projektServiceFileLabel.Location = new Point(89, 202);
            projektServiceFileLabel.Name = "label2";
            projektServiceFileLabel.Size = new Size(38, 15);
            projektServiceFileLabel.TabIndex = 4;
            projektServiceFileLabel.Text = "Projekt service fajl:";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // NewProjektForms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}