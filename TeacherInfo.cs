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
    public partial class TeacherInfo : Form
    {
        Teachers pForm;
        bool AddORUpd;
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        Teacher tch;
        
        public TeacherInfo(Teacher teach, bool AdORUpd, Teachers pForm)
        {
            this.AddORUpd = AdORUpd;
            this.pForm = pForm;
            tch = teach;
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TeacherInfo_Load(object sender, EventArgs e)
        {
            if(!AddORUpd)//Upd - false
            {
                textBox1SurName.Text = tch.SurnameT;
                textBox2Name.Text = tch.NameT;
                textBox3MidlName.Text = tch.MidlNameT;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                if (AddORUpd)//Upd - false
                {
                    //Add
                    if ((textBox1SurName.Text.Length <= 2) || (textBox2Name.Text.Length <= 3) || (textBox3MidlName.Text.Length <= 2))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = String.Format("INSERT INTO Table_Teacher ([Фамилия преподавателя], [Имя преподавателя], [Отчество преподавателя]) VALUES ('{0}', '{1}', '{2}') SELECT Scope_identity()", textBox1SurName.Text.Trim(), textBox2Name.Text.Trim(), textBox3MidlName.Text.Trim());
                        sqlc.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlc);
                        sqlCommand.ExecuteNonQuery();
                        pForm.AddTeacher(new Teacher() { SurnameT = textBox1SurName.Text, NameT = textBox2Name.Text, MidlNameT = textBox3MidlName.Text });
                        MessageBox.Show("Данные успешно добавлены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if ((textBox1SurName.Text.Length <= 2) || (textBox2Name.Text.Length <= 3) || (textBox3MidlName.Text.Length <= 2))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = "UPDATE Table_Teacher SET [Фамилия преподавателя] = '" + textBox1SurName.Text.Trim() + "', [Имя преподавателя] = '" + textBox2Name.Text.Trim() + "'," +
                            " [Отчество преподавателя] = '" + textBox3MidlName.Text.Trim() + "' WHERE [Код преподавателя] = '" + tch.IdTeacher + "' ";
                        sqlc.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlc);
                        sqlCommand.ExecuteNonQuery();
                        tch.SurnameT = textBox1SurName.Text.Trim();
                        tch.NameT = textBox2Name.Text.Trim();
                        tch.MidlNameT = textBox3MidlName.Text.Trim();
                        pForm.UpdateTeacher(tch);
                        MessageBox.Show("Данные успешно изменены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
