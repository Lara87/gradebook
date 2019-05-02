using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Start : Form
    {
        Auth auth;

        public int role;

        public Start(Auth a, int role)
        {
            this.role = role;
            auth = a;
            InitializeComponent();
        }
        /*
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }*/

        private void buttonCreditBook_Click(object sender, EventArgs e)
        {
            Credits form1 = new Credits(this);
            form1.Show();
            this.Hide();
        }

        private void buttonTeachers_Click(object sender, EventArgs e)
        {
            Teachers teacher = new Teachers(this);
            teacher.Show();
            this.Hide();
        }

        private void buttonStudents_Click(object sender, EventArgs e)
        {
            Students students = new Students(this);
            students.Show();
            this.Hide();
        }

        private void buttonSpecialty_Click(object sender, EventArgs e)
        {
            Specialties specialty = new Specialties(this);
            specialty.Show();
            this.Hide();
        }

        private void buttonSubjects_Click(object sender, EventArgs e)
        {
            Subjects subject = new Subjects(this);
            subject.Show();
            this.Hide();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            Users users = new Users(this);
            users.Show();
            this.Hide();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            if(role == 1)
            {
                buttonUsers.Enabled = false;
            }
            else if(role==2)
            {
                buttonUsers.Enabled = false;
                buttonSubjects.Enabled = false;
                buttonTeachers.Enabled = false;
                buttonSpecialty.Enabled = false;
            }
            else if(role!=0)
            {
                buttonUsers.Enabled = false;
                buttonStudents.Enabled = false;
                buttonTeachers.Enabled = false;
            }
        }

        private void Start_FormClosed(object sender, FormClosedEventArgs e)
        {
           auth.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InfoProg infoProg = new InfoProg(this);
            infoProg.Show();
            this.Hide();

        }
    }
}
