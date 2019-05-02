using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class SubjectInfo : Form
    {
        Subjects pForm;
        bool AddORUpd;
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        Subject subj;

        public SubjectInfo(Subject subject, bool AddORUpd, Subjects pForm)
        {
            this.AddORUpd = AddORUpd;
            this.pForm = pForm;
            subj = subject;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SubjectInfo_Load(object sender, EventArgs e)
        {
            if(!AddORUpd)//Upd = false
            {
                textBox1.Text = subj.NameSubject;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                if (AddORUpd)//Upd = false
                {
                    //Add
                    if (textBox1.Text.Length <= 3)
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = String.Format("INSERT INTO Table_Subject ([Наименование дисциплины]) VALUES ('{0}') Select Scope_identity()", textBox1.Text.Trim());
                        
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        pForm.AddSubject(new Subject() { NameSubject = textBox1.Text });
                        MessageBox.Show("Данные успешно добавлены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (textBox1.Text.Length <= 3)
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = "UPDATE Table_Subject SET [Наименование дисциплины]= '" + textBox1.Text.Trim() + "'  WHERE [Код дисциплины] = '" + subj.IdSubject + "' ";
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        subj.NameSubject = textBox1.Text.Trim();
                        pForm.UpdateSubject(subj);
                        MessageBox.Show("Данные успешно сохранены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
