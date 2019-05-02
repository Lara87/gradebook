namespace WindowsFormsApp2
{
    partial class Start
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.buttonTeachers = new System.Windows.Forms.Button();
            this.buttonUsers = new System.Windows.Forms.Button();
            this.buttonStudents = new System.Windows.Forms.Button();
            this.buttonSpecialty = new System.Windows.Forms.Button();
            this.buttonSubjects = new System.Windows.Forms.Button();
            this.buttonCreditBook = new System.Windows.Forms.Button();
            this.button1InfoProgram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTeachers
            // 
            this.buttonTeachers.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonTeachers.Location = new System.Drawing.Point(80, 147);
            this.buttonTeachers.Name = "buttonTeachers";
            this.buttonTeachers.Size = new System.Drawing.Size(193, 38);
            this.buttonTeachers.TabIndex = 0;
            this.buttonTeachers.Text = "Преподаватели";
            this.buttonTeachers.UseVisualStyleBackColor = true;
            this.buttonTeachers.Click += new System.EventHandler(this.buttonTeachers_Click);
            // 
            // buttonUsers
            // 
            this.buttonUsers.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonUsers.Location = new System.Drawing.Point(80, 352);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(193, 38);
            this.buttonUsers.TabIndex = 1;
            this.buttonUsers.Text = "Пользователи";
            this.buttonUsers.UseVisualStyleBackColor = true;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // buttonStudents
            // 
            this.buttonStudents.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonStudents.Location = new System.Drawing.Point(80, 78);
            this.buttonStudents.Name = "buttonStudents";
            this.buttonStudents.Size = new System.Drawing.Size(193, 38);
            this.buttonStudents.TabIndex = 2;
            this.buttonStudents.Text = "Студенты";
            this.buttonStudents.UseVisualStyleBackColor = true;
            this.buttonStudents.Click += new System.EventHandler(this.buttonStudents_Click);
            // 
            // buttonSpecialty
            // 
            this.buttonSpecialty.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSpecialty.Location = new System.Drawing.Point(80, 215);
            this.buttonSpecialty.Name = "buttonSpecialty";
            this.buttonSpecialty.Size = new System.Drawing.Size(193, 38);
            this.buttonSpecialty.TabIndex = 3;
            this.buttonSpecialty.Text = "Специальность";
            this.buttonSpecialty.UseVisualStyleBackColor = true;
            this.buttonSpecialty.Click += new System.EventHandler(this.buttonSpecialty_Click);
            // 
            // buttonSubjects
            // 
            this.buttonSubjects.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSubjects.Location = new System.Drawing.Point(80, 284);
            this.buttonSubjects.Name = "buttonSubjects";
            this.buttonSubjects.Size = new System.Drawing.Size(193, 38);
            this.buttonSubjects.TabIndex = 4;
            this.buttonSubjects.Text = "Дисциплины";
            this.buttonSubjects.UseVisualStyleBackColor = true;
            this.buttonSubjects.Click += new System.EventHandler(this.buttonSubjects_Click);
            // 
            // buttonCreditBook
            // 
            this.buttonCreditBook.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreditBook.Location = new System.Drawing.Point(80, 12);
            this.buttonCreditBook.Name = "buttonCreditBook";
            this.buttonCreditBook.Size = new System.Drawing.Size(193, 38);
            this.buttonCreditBook.TabIndex = 6;
            this.buttonCreditBook.Text = "Зачетная книжка";
            this.buttonCreditBook.UseVisualStyleBackColor = true;
            this.buttonCreditBook.Click += new System.EventHandler(this.buttonCreditBook_Click);
            // 
            // button1InfoProgram
            // 
            this.button1InfoProgram.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.button1InfoProgram.Location = new System.Drawing.Point(80, 414);
            this.button1InfoProgram.Name = "button1InfoProgram";
            this.button1InfoProgram.Size = new System.Drawing.Size(193, 38);
            this.button1InfoProgram.TabIndex = 7;
            this.button1InfoProgram.Text = "О программе";
            this.button1InfoProgram.UseVisualStyleBackColor = true;
            this.button1InfoProgram.Click += new System.EventHandler(this.button1_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 464);
            this.Controls.Add(this.button1InfoProgram);
            this.Controls.Add(this.buttonCreditBook);
            this.Controls.Add(this.buttonSubjects);
            this.Controls.Add(this.buttonSpecialty);
            this.Controls.Add(this.buttonStudents);
            this.Controls.Add(this.buttonUsers);
            this.Controls.Add(this.buttonTeachers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Start";
            this.Text = "Главная форма";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Start_FormClosed);
            this.Load += new System.EventHandler(this.Start_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTeachers;
        private System.Windows.Forms.Button buttonUsers;
        private System.Windows.Forms.Button buttonStudents;
        private System.Windows.Forms.Button buttonSpecialty;
        private System.Windows.Forms.Button buttonSubjects;
        private System.Windows.Forms.Button buttonCreditBook;
        private System.Windows.Forms.Button button1InfoProgram;
    }
}