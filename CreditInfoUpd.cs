using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class CreditInfoUpd : Form
    {
        Credits pForm;
        bool AddORUpd;
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        Credit cr;
        Type t;
        int indexCB;

        public CreditInfoUpd(Credit credit, bool AddORUpd, Credits pForms)
        {
            this.AddORUpd = AddORUpd;
            this.pForm = pForms;
            cr = credit;
            InitializeComponent();
        }

        private void CreditInfoUpd_Load(object sender, EventArgs e)
        {
            if (!AddORUpd)//Upd = false
            {
                var nB = cr.NumBook;
                textBox2NumBook.Text = nB.ToString().Trim();
                textBox1NumHour.Text = cr.NumHours;
                textBox1Grades.Text = cr.Grades;
                dateTimePickerDateCredit.Text = cr.DateCredit;
                textBox2Semester.Text = cr.Semester;
                textBox4Subject.Text = cr.NameSubject;
                var studName = pForm.shotName(cr.SurnameStudent, cr.NameStudent, cr.MidlnameStudent);
                textBox1NameStud.Text = studName;
                textBox2TypeCredit.Text = cr.FormCredit;
                textBox4Subject.Text = cr.NameSubject;
                var teacherName = pForm.shotName(cr.SurnameTeacher, cr.NameTeacher, cr.MidlnameTeacher);
                textBox3NameTeach.Text = teacherName;
            }

        }

        bool testRegExpGrades(String gr)//регулярка для оценки
        {
            RegexOptions option = RegexOptions.IgnoreCase;
            Regex reg = new Regex("(?i)(\\W|^)(хорошо|отлично|удовлетворительно|зачёт)(\\W|$)", option);
            MatchCollection matches = reg.Matches(gr);
            if (matches.Count > 0)
            {
                return true;
            }
            else return false;
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            var dateCredit = dateTimePickerDateCredit.Value.ToShortDateString();//дата
            String dCr = dateCredit.ToString();

            String grades = textBox1Grades.Text.ToString().Trim();//оценка

            using (SqlConnection sql = new SqlConnection(url))
            {
                if (!AddORUpd)//Upd = false
                {
                    if (!(testRegExpGrades(grades)))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = "Update Table_Credit SET [Оценка] = '" + textBox1Grades.Text.Trim() + "', [Количество часов] = '" + textBox1NumHour.Text.Trim() + "', " +
                            "[Дата сдачи] = '" + dCr + "', [Семестр] = '" + textBox2Semester.Text.Trim() + "' WHERE [Номер зачетной книжки] = '" + cr.NumBook + "' ";
                        sql.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sql);
                        sqlCommand.ExecuteNonQuery();
                        cr.NumHours = textBox1NumHour.Text.Trim();
                        cr.Grades = textBox1Grades.Text.Trim();
                        cr.DateCredit = dCr;
                        cr.Semester = textBox2Semester.Text.Trim();
                        pForm.UpdateCredit(cr);
                        MessageBox.Show("Данные успешное обновлены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


            private void textBox1NumHour_KeyPress(object sender, KeyPressEventArgs e)//чтобы в окно кол-во часов только цифры
            {
                char number = e.KeyChar;
                if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
                {
                    e.Handled = true;
                }
            }

            private void textBox2Semester_KeyPress(object sender, KeyPressEventArgs e)//чтобы в окно семестр только цифры
            {
                char number = e.KeyChar;
                if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
                {
                    e.Handled = true;
                }
            }

            private void CreditInfoUpd_FormClosed(object sender, FormClosedEventArgs e)
            {

            }

            private void textBox2NumBook_KeyPress(object sender, KeyPressEventArgs e)
            {
                char number = e.KeyChar;
                if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
                {
                    e.Handled = true;
                }
            }
    }
}
    



