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
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{
    public partial class CreditInfoAdd : Form
    {
        Credits pForm;
        bool AddORUpd1;
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        Credit cred;
        Type t;
        int indexCB;

        public CreditInfoAdd(Credit credit, bool AddORUpd, Credits pForms)
        {
            this.AddORUpd1 = AddORUpd;
            this.pForm = pForms;
            cred = credit;
            InitializeComponent();
        }

        void fillComboboxNameStud()
        {
            String student = String.Format("SELECT DISTINCT [Номер зачетной книжки], [Фамилия студента], [Имя студента], [Отчество студента] FROM Table_Student");
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                indexCB = 0;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(student, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<object> studList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                    var nStud = pForm.shotName((reader["Фамилия студента"] as String).Trim(), (reader["Имя студента"] as String).Trim(), (reader["Отчество студента"] as String).Trim());
                    var k1 = (new { idStud = reader["Номер зачетной книжки"], nameStud = nStud });
                    studList.Add(k1); 
                }
                comboBox1NameStud.DataSource = studList;
                comboBox1NameStud.DisplayMember = "nameStud";
                comboBox1NameStud.ValueMember = "idStud";
                if (studList.Count > 0)
                {
                    comboBox1NameStud.SelectedIndex = 0;
                }
                t = studList[0].GetType();
            }
        }

        void fillComboboxNameTeach()
        {
            String teacher = String.Format("SELECT DISTINCT [Код преподавателя], [Фамилия преподавателя], [Имя преподавателя], [Отчество преподавателя] FROM Table_Teacher");
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                indexCB = 0;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(teacher, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<object> teachList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                    var nTeach = pForm.shotName((reader["Фамилия преподавателя"] as String).Trim(), (reader["Имя преподавателя"] as String).Trim(), (reader["Отчество преподавателя"] as String).Trim());
                    var k1 = (new { idTeach = reader["Код преподавателя"], nameTeach = nTeach });
                    teachList.Add(k1);
                }
                comboBox1NameTeach.DataSource = teachList;
                comboBox1NameTeach.DisplayMember = "nameTeach";
                comboBox1NameTeach.ValueMember = "idTeach";
                if (teachList.Count > 0)
                {
                    comboBox1NameTeach.SelectedIndex = 0;
                }
                t = teachList[0].GetType();
            }
        }

        void fillComboboxNumsBooks()
         {
             String numBooks = "SELECT DISTINCT [Код студента], [Номер зачетной книжки] FROM Table_Credit";
             using (SqlConnection sqlc = new SqlConnection(url))
             {
                indexCB = 0;
                sqlc.Open();
                SqlCommand com = new SqlCommand(numBooks, sqlc);
                SqlDataReader reader = com.ExecuteReader();
                List<object> numBooksList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                numBooksList.Add(new { idNumBooks = reader["Код студента"], numBooks = reader["Номер зачетной книжки"]});
                }
                comboBox2NumBook.DataSource = numBooksList;
                comboBox2NumBook.DisplayMember = "numBooks";
                comboBox2NumBook.ValueMember = "idNumBooks";
                if (numBooksList.Count > 0)
                {
                     comboBox2NumBook.SelectedIndex = 0;
                }
                t = numBooksList[0].GetType();
            }
         }

        void fillComboboxTypeCredit()
        {
            String typeCredit = "Select DISTINCT [Код формы зачета], [Наименование] FROM Table_Creadit_type";
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                indexCB = 0;
                sqlc.Open();
                SqlCommand com = new SqlCommand(typeCredit, sqlc);
                SqlDataReader reader = com.ExecuteReader();
                List<object> typeCreditList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                    typeCreditList.Add(new { idTypeCredit = reader["Код формы зачета"], nameCredit = (reader["Наименование"] as String).Trim() });
                }
                comboBox2FormCredit.DataSource = typeCreditList;
                comboBox2FormCredit.DisplayMember = "nameCredit";
                comboBox2FormCredit.ValueMember = "idTypeCredit";
                if (typeCreditList.Count > 0)
                {
                    comboBox2FormCredit.SelectedIndex = 0;
                }
                t = typeCreditList[0].GetType();
            }
        }

        void fillComboboxSubject()
        {
            String subject = "Select DISTINCT [Код дисциплины], [Наименование дисциплины] FROM Table_Subject";
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                indexCB = 0;
                sqlc.Open();
                SqlCommand com = new SqlCommand(subject, sqlc);
                SqlDataReader reader = com.ExecuteReader();
                List<object> subjectsList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                    subjectsList.Add(new { idSubject = reader["Код дисциплины"], nameSubject = (reader["Наименование дисциплины"] as String).Trim() });
                }
                comboBox3NameSubject.DataSource = subjectsList;
                comboBox3NameSubject.DisplayMember = "nameSubject";
                comboBox3NameSubject.ValueMember = "idSubject";
                if (subjectsList.Count > 0)
                {
                    comboBox3NameSubject.SelectedIndex = 0;
                }
                t = subjectsList[0].GetType();
            }
        }

        private void CreditInfoAdd_Load(object sender, EventArgs e)
        {
            fillComboboxNameStud();
            fillComboboxTypeCredit();
            fillComboboxSubject();
            fillComboboxNameTeach();
            fillComboboxNumsBooks();
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            pForm.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            String nS, snS, mnS, nT, snT, mnT, nSb, nTC;
            var comboStud = comboBox1NameStud.SelectedItem;
            dynamic stud = comboStud;
            var IdStud = stud.idStud;

            var comboSubject = comboBox3NameSubject.SelectedItem;
            dynamic subj = comboSubject;
            var IdSubj = subj.idSubject;

            var comboTeach = comboBox1NameTeach.SelectedItem;
            dynamic teach = comboTeach;
            var IdTeach = teach.idTeach;

            var grades = textBox1Grades.Text.ToString().Trim();

            var dateCredit = dateTimePickerDateCredit.Value.ToShortDateString();
            String sDT = dateCredit.ToString();

            var comboCred = comboBox2FormCredit.SelectedItem;
            dynamic typeCred = comboCred;
            var IdCred = typeCred.idTypeCredit;


            var comboNumBook = comboBox2NumBook.SelectedItem;
            dynamic numBook = comboNumBook;
            var IdNumBook = numBook.idNumBooks;

            using (SqlConnection sql = new SqlConnection(url))
            {
                if (AddORUpd1)//Upd = false
                {
                    //Add
                    if (!(testRegExpGrades(grades)))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = String.Format("INSERT INTO Table_Credit ([Код дисциплины], [Код преподавателя], [Оценка], " +
                            "[Количество часов], [Дата сдачи], [Семестр], [Номер зачетной книжки], [Код формы зачета])" +
                            "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}') Select Scope_identity()", IdSubj, IdTeach, textBox1Grades.Text.ToString().Trim(),
                            textBox1NumHour.Text.ToString().Trim(), sDT, textBox2Semester.Text.ToString().Trim(), IdNumBook, IdCred);
                            sql.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sql);
                        var k = sqlCommand.ExecuteScalar();

                        var com = new SqlCommand("SELECT Table_Student.[Фамилия студента], Table_Student.[Имя студента], Table_Student.[Отчество студента], " +
                            "Table_Teacher.[Фамилия преподавателя], Table_Teacher.[Имя преподавателя], Table_Teacher.[Отчество преподавателя], " +
                            "Table_Subject.[Наименование дисциплины], Table_Creadit_type.[Наименование] FROM Table_Credit, Table_Student, Table_Teacher, " +
                            "Table_Subject, Table_Creadit_type WHERE Table_Credit.[Номер зачетной книжки] = Table_Student.[Номер зачетной книжки] " +
                            "AND Table_Teacher.[Код преподавателя] = Table_Credit.[Код преподавателя] AND Table_Subject.[Код дисциплины] = Table_Credit.[Код дисциплины] " +
                            "AND Table_Creadit_type.[Код формы зачета] = Table_Credit.[Код формы зачета] AND[Код студента] = '" + k + "' ", sql);
                        SqlDataReader sqldr = com.ExecuteReader();                 
                         while(sqldr.Read())
                        {
                            var crcr = new Credit();
                            crcr.NameStudent = sqldr["Имя студента"].ToString().Trim();
                            crcr.SurnameStudent = sqldr["Фамилия студента"].ToString().Trim();
                            crcr.MidlnameStudent = sqldr["Отчество студента"].ToString().Trim();
                            crcr.SurnameTeacher = sqldr["Фамилия преподавателя"].ToString().Trim();
                            crcr.NameTeacher = sqldr["Имя преподавателя"].ToString().Trim();
                            crcr.MidlnameTeacher = sqldr["Отчество преподавателя"].ToString().Trim();
                            crcr.NameSubject = sqldr["Наименование дисциплины"].ToString().Trim();
                            crcr.FormCredit = sqldr["Наименование"].ToString().Trim();
                            nS = crcr.NameStudent.ToString().Trim();
                            snS = crcr.SurnameStudent.ToString().Trim();
                            mnS = crcr.MidlnameStudent.ToString().Trim();
                            nT = crcr.NameTeacher.ToString().Trim();
                            snT = crcr.SurnameTeacher.ToString().Trim();
                            mnT  = crcr.MidlnameTeacher.ToString().Trim();
                            nSb = crcr.NameSubject.ToString().Trim();
                            nTC = crcr.FormCredit.ToString().Trim();

                            pForm.AddCredit(new Credit() { IdSubject = IdSubj, IdTeacher = IdTeach, Grades = grades, NumHours = textBox1NumHour.Text.ToString().Trim(),
                            DateCredit = sDT, Semester = textBox2Semester.Text.ToString().Trim(), NumBook = IdNumBook, IdCredit = IdCred, NameStudent = nS,
                        FormCredit = nTC, SurnameTeacher = snT, NameSubject = nSb, SurnameStudent = snS,
                        NameTeacher = nT, MidlnameStudent = mnS, MidlnameTeacher = mnT});

                        }
                        MessageBox.Show("Данные успешно добавлены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox1NumBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}

/* int k = (int)gr;
 double j = k;

 var grS = gr.ToString();
 var l = Int32.Parse(grS);
         Double.Parse(grS);
 */
/*
      void fillComboboxNameSubj()
      {
          String subject = String.Format("SELECT [Код дисциплины], [Наименование дисциплины] FROM Table_Subject");
          using (SqlConnection sqlConnection = new SqlConnection(url))
          {
              indexCB = 0;
              sqlConnection.Open();
              SqlCommand sqlCommand = new SqlCommand(subject, sqlConnection);
              SqlDataReader reader = sqlCommand.ExecuteReader();
              List<object> subjectList = new List<object>();
              int i = 0;
              while (reader.Read())
              {                   
                  var k1 = (new { idSubject = reader["Код дисциплины"], nameSubject = (reader["Наименование дисциплины"] as String).Trim() });
                  subjectList.Add(k1);
                  if (k1.idSubject.ToString() == cr.IdSubject.ToString())
                  { indexCB = i; }
                  i++;
              }
              comboBox3NameSubject.DataSource = subjectList;
              comboBox3NameSubject.DisplayMember = "nameSubject";
              comboBox3NameSubject.ValueMember = "idSubject";
              if (subjectList.Count > 0)
              {
                  comboBox3NameSubject.SelectedIndex = indexCB;
              }
              t = subjectList[0].GetType();
          }
      }

      void fillComboboxNameStud()
      {
          String student = String.Format("SELECT [Номер зачетной книжки], [Фамилия студента], [Имя студента], [Отчество студента] FROM Table_Student");
          using (SqlConnection sqlConnection = new SqlConnection(url))
          {
              indexCB = 0;
              sqlConnection.Open();
              SqlCommand sqlCommand = new SqlCommand(student, sqlConnection);
              SqlDataReader reader = sqlCommand.ExecuteReader();
              List<object> studList = new List<object>();
              int i = 0;
              while (reader.Read())
              {
                  var nStud = pForm.shotName((reader["Фамилия студента"] as String).Trim(), (reader["Имя студента"] as String).Trim(), (reader["Отчество студента"] as String).Trim());
                  var k1 = (new { idStud = reader["Номер зачетной книжки"], nameStud = nStud });
                  studList.Add(k1);
                  if (k1.idStud.ToString() == cr.NumBook.ToString())
                  { indexCB = i; }
                  i++;
              }
              comboBox2NameStud.DataSource = studList;
              comboBox2NameStud.DisplayMember = "nameStud";
              comboBox2NameStud.ValueMember = "idStud";
              if (studList.Count > 0)
              {
                  comboBox2NameStud.SelectedIndex = indexCB;
              }
              t = studList[0].GetType();
          }
      }

      void queryListFormCredit()
      {
          String credit = String.Format("Select [Код формы зачета], [Наименование] FROM Table_Creadit_type");
          using (SqlConnection sqlConnection = new SqlConnection(url))
          {
              indexCB = 0;
              sqlConnection.Open();
              SqlCommand sqlCommand = new SqlCommand(credit, sqlConnection);
              SqlDataReader reader = sqlCommand.ExecuteReader();
              List<object> creditList = new List<object>();
              int i = 0;
              while (reader.Read())
              {
               var k = (new { idCredit = (reader["Код формы зачета"]).ToString(), typeCredit = (reader["Наименование"] as String).Trim()});
                  creditList.Add(k);
                  if(k.idCredit.ToString() == cr.IdCredit.ToString())
                  { indexCB = i; }
                  i++;
              }
              comboBox2FormCredit.DataSource = creditList;
              comboBox2FormCredit.DisplayMember = "typeCredit";
              comboBox2FormCredit.ValueMember = "idCredit";
              if(creditList.Count > 0)
              {
                  comboBox2FormCredit.SelectedIndex = indexCB;
              }
              t = creditList[0].GetType();
          }
      }
      */
