namespace FirebaseBackupWindowsForm
{
    partial class main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ColumnHeader ProjectId;
            listView1 = new ListView();
            button1 = new Button();
            ProjectId = new ColumnHeader();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.CausesValidation = false;
            listView1.Columns.AddRange(new ColumnHeader[] { ProjectId });
            listView1.Location = new Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(251, 288);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // ProjectId
            // 
            ProjectId.Text = "ProjectId";
            ProjectId.Width = 120;
            // 
            // button1
            // 
            button1.Location = new Point(12, 306);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Details";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 379);
            Controls.Add(button1);
            Controls.Add(listView1);
            Name = "main";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader ProjectId;
        private Button button1;
    }
}