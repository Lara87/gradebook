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
    public partial class Credits : Form
    {
        Auth auth;
        Start start;
        List<Credit> creditsList = new List<Credit>();
        String imgDelete = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\del.png";
        String imgEdit = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\pen.png";
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Credits(Start s)
        {
            start = s;
            InitializeComponent();
        }

        public void AddCredit(Credit credit)
        {
            creditsList.Add(credit);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();

        }

        public void UpdateCredit(Credit credit)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        public void LoadData()
        {
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                sqlConnection.Open();
                var com = new SqlCommand("SELECT Table_Credit.[Код студента], Table_Credit.[Код дисциплины],Table_Credit.[Код преподавателя], " +
                    "Table_Credit.[Оценка], Table_Credit.[Количество часов], Table_Credit.[Дата сдачи], Table_Credit.[Семестр], " +
                    "Table_Credit.[Номер зачетной книжки], Table_Credit.[Код формы зачета], Table_Student.[Номер зачетной книжки], " +
                    "Table_Student.[Фамилия студента], Table_Student.[Имя студента], Table_Student.[Отчество студента], " +
                    "Table_Teacher.[Код преподавателя], Table_Teacher.[Фамилия преподавателя], Table_Teacher.[Имя преподавателя], " +
                    "Table_Teacher.[Отчество преподавателя], Table_Subject.[Код дисциплины], Table_Subject.[Наименование дисциплины], " +
                    "Table_Creadit_type.[Код формы зачета], Table_Creadit_type.[Наименование] " +
                    "FROM Table_Credit, Table_Student, Table_Teacher, Table_Subject, Table_Creadit_type " +
                    "WHERE  Table_Credit.[Номер зачетной книжки] = Table_Student.[Номер зачетной книжки] AND " +
                    "Table_Teacher.[Код преподавателя] = Table_Credit.[Код преподавателя] AND Table_Subject.[Код дисциплины] = Table_Credit.[Код дисциплины] " +
                    "AND Table_Creadit_type.[Код формы зачета] = Table_Credit.[Код формы зачета] ", sqlConnection);
                SqlDataReader sqldr = com.ExecuteReader();
                while (sqldr.Read())
                {
                    var credit = new Credit();
                    var idstud = sqldr["Код студента"].ToString().Trim();
                    credit.IdStud = Int32.Parse(idstud);
                    credit.NameStudent = sqldr["Имя студента"].ToString().Trim();
                    credit.MidlnameStudent = sqldr["Отчество студента"].ToString().Trim();
                    credit.SurnameStudent = sqldr["Фамилия студента"].ToString().Trim();
                    credit.NameTeacher = sqldr["Имя преподавателя"].ToString().Trim();
                    credit.MidlnameTeacher = sqldr["Отчество преподавателя"].ToString().Trim();
                    credit.SurnameTeacher = sqldr["Фамилия преподавателя"].ToString().Trim();
                    credit.NumHours = sqldr["Количество часов"].ToString().Trim();
                    credit.Grades = sqldr["Оценка"].ToString().Trim();
                    credit.DateCredit = sqldr["Дата сдачи"].ToString().Trim();
                    credit.DateCredit = credit.DateCredit.Remove(10);
                    credit.Semester = sqldr["Семестр"].ToString().Trim();
                    var numbooks = sqldr["Номер зачетной книжки"].ToString().Trim();
                    credit.NumBook = Int32.Parse(numbooks);
                    credit.FormCredit = sqldr["Наименование"].ToString().Trim();
                    credit.NameSubject = sqldr["Наименование дисциплины"].ToString().Trim();
                    var idsubj = sqldr["Код дисциплины"].ToString().Trim();
                    credit.IdSubject = Int32.Parse(idsubj);
                    var idteach = sqldr["Код преподавателя"].ToString().Trim();
                    credit.IdTeacher = Int32.Parse(idteach);
                    var idcred = sqldr["Код формы зачета"].ToString().Trim();
                    credit.IdCredit = Int32.Parse(idcred);
                    creditsList.Add(credit);
                }
            }
        }

        public void FillDGV()
        {
            DataGridViewImageColumn imgC = new DataGridViewImageColumn();
            DataGridViewImageColumn imgC2 = new DataGridViewImageColumn();
            dataGridView1.Columns.AddRange(imgC, imgC2);

            for (int i = 0; i <= 10; i++)
            {
                DataGridViewTextBoxColumn cl = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(cl);
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].HeaderText = "Ф.И.О. студента";
            dataGridView1.Columns[5].HeaderText = "Номер зачетной книжки";
            dataGridView1.Columns[6].HeaderText = "Дисциплина";
            dataGridView1.Columns[7].HeaderText = "Форма зачета";
            dataGridView1.Columns[8].HeaderText = "Оценка";
            dataGridView1.Columns[9].HeaderText = "Ф.И.О. преподавателя";
            dataGridView1.Columns[10].HeaderText = "Семестр";
            dataGridView1.Columns[11].HeaderText = "Кол-во часов";
            dataGridView1.Columns[12].HeaderText = "Дата сдачи";
            dataGridView1.Columns[4].Width = 170;
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[6].Width = 130;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 170;
            dataGridView1.Columns[10].Width = 90;

            foreach (var el in creditsList)
            {
                //el - creditsList[i]
                //cells del and edit
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.Height = 32;

                //creat button edit and del

                DataGridViewImageCell cc = new DataGridViewImageCell();
                var idel = Image.FromFile(imgDelete);
                cc.Value = idel;
                dataGridViewRow.Cells.Add(cc);

                DataGridViewImageCell cc2 = new DataGridViewImageCell();
                var iedit = Image.FromFile(imgEdit);
                cc2.Value = iedit;
                dataGridViewRow.Cells.Add(cc2);

                imgC.Width = 32;
                imgC2.Width = 32;

                var idStud = el.IdStud;
                DataGridViewCell ccc0 = new DataGridViewTextBoxCell();
                ccc0.Value = idStud;
                dataGridViewRow.Cells.Add(ccc0);

                var idCred = el.IdCredit;
                DataGridViewCell c0 = new DataGridViewTextBoxCell();
                c0.Value = idCred;
                dataGridViewRow.Cells.Add(c0);

                var nameStudent = shotName(el.SurnameStudent, el.NameStudent, el.MidlnameStudent);
                DataGridViewCell c1 = new DataGridViewTextBoxCell();
                c1.Value = nameStudent;
                dataGridViewRow.Cells.Add(c1);

                var numBook = el.NumBook;
                DataGridViewCell c2 = new DataGridViewTextBoxCell();
                c2.Value = numBook;
                dataGridViewRow.Cells.Add(c2);

                var nameSub = el.NameSubject;
                DataGridViewCell c3 = new DataGridViewTextBoxCell();
                c3.Value = nameSub;
                dataGridViewRow.Cells.Add(c3);

                var fCredit = el.FormCredit;
                DataGridViewCell c4 = new DataGridViewTextBoxCell();
                c4.Value = fCredit;
                dataGridViewRow.Cells.Add(c4);

                var grades = el.Grades;
                DataGridViewCell c5 = new DataGridViewTextBoxCell();
                c5.Value = grades;
                dataGridViewRow.Cells.Add(c5);

                var nameTeach = shotName(el.SurnameTeacher, el.NameTeacher, el.MidlnameTeacher);
                DataGridViewCell c6 = new DataGridViewTextBoxCell();
                c6.Value = nameTeach;
                dataGridViewRow.Cells.Add(c6);

                var semester = el.Semester;
                DataGridViewCell c7 = new DataGridViewTextBoxCell();
                c7.Value = semester;
                dataGridViewRow.Cells.Add(c7);

                var hours = el.NumHours;
                DataGridViewCell c8 = new DataGridViewTextBoxCell();
                c8.Value = hours;
                dataGridViewRow.Cells.Add(c8);

                var dateCred = el.DateCredit;
                DataGridViewCell c9 = new DataGridViewTextBoxCell();
                c9.Value = dateCred;
                dataGridViewRow.Cells.Add(c9);

                dataGridView1.Rows.Add(dataGridViewRow);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            FillDGV();
            if ((start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                button1Add.Visible = true;
            }
        }

        public String shotName(String a, String b, String c)
        {
            //List<string> hg = new List<string>();
            var N = b.ToUpper().ElementAt(0);
            var MN = c.ToUpper().ElementAt(0);
            a += " " + N + ". " + MN + ".";
            return a;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            start.Show();
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(url))
                    {
                        sqlConnection.Open();
                        var rowIndex = e.RowIndex;
                        var cell = dataGridView1.Rows[rowIndex].Cells[2].Value;
                        var com1 = new SqlCommand("DELETE FROM Table_Credit WHERE [Код студента] = '" + cell + "' ", sqlConnection);
                        SqlDataReader sqldr = com1.ExecuteReader();
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                    MessageBox.Show("Данные успешно удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if ((e.ColumnIndex == 1) && (start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                //edit
                CreditInfoUpd creditInfoupd = new CreditInfoUpd(creditsList[e.RowIndex], false, this);
                creditInfoupd.Show();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreditInfoAdd creditInfoAdd = new CreditInfoAdd(null, true, this);
            creditInfoAdd.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                sqlConnection.Open();
                var command = new SqlCommand(" SELECT Table_Credit.[Номер зачетной книжки], Table_Student.[Фамилия студента], Table_Student.[Имя студента], " +
                    "Table_Student.[Отчество студента], Table_Teacher.[Фамилия преподавателя], Table_Teacher.[Имя преподавателя], Table_Teacher.[Отчество преподавателя], " +
                    "Table_Subject.[Наименование дисциплины], Table_Creadit_type.[Наименование], Table_Credit.Оценка, Table_Credit.Семестр, Table_Credit.[Количество часов], " +
                    "Table_Credit.[Дата сдачи] FROM Table_Credit, Table_Student, Table_Teacher, Table_Subject, Table_Creadit_type " +
                    "WHERE Table_Credit.[Номер зачетной книжки] = Table_Student.[Номер зачетной книжки] AND Table_Teacher.[Код преподавателя] = Table_Credit.[Код преподавателя] " +
                    "AND Table_Subject.[Код дисциплины] = Table_Credit.[Код дисциплины] AND Table_Creadit_type.[Код формы зачета] = Table_Credit.[Код формы зачета] " +
                    "AND Table_Credit.[Номер зачетной книжки] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Student.[Фамилия студента] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Student.[Имя студента] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Student.[Отчество студента] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Teacher.[Фамилия преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Teacher.[Имя преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Teacher.[Отчество преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Subject.[Наименование дисциплины] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Creadit_type.Наименование LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Credit.[Дата сдачи] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "or Table_Credit.[Количество часов] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "or Table_Credit.Оценка LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "or Table_Credit.Семестр LIKE '%" + textBox1.Text.ToString().Trim() + " %' ", sqlConnection);

                SqlDataReader sqldr = command.ExecuteReader();

                while (sqldr.Read())
                {
                    var credit = new Credit();
                    var idstud = sqldr["Код студента"].ToString().Trim();
                    credit.IdStud = Int32.Parse(idstud);
                    credit.NameStudent = sqldr["Имя студента"].ToString().Trim();
                    credit.MidlnameStudent = sqldr["Отчество студента"].ToString().Trim();
                    credit.SurnameStudent = sqldr["Фамилия студента"].ToString().Trim();
                    credit.NameTeacher = sqldr["Имя преподавателя"].ToString().Trim();
                    credit.MidlnameTeacher = sqldr["Отчество преподавателя"].ToString().Trim();
                    credit.SurnameTeacher = sqldr["Фамилия преподавателя"].ToString().Trim();
                    credit.NumHours = sqldr["Количество часов"].ToString().Trim();
                    credit.Grades = sqldr["Оценка"].ToString().Trim();
                    credit.DateCredit = sqldr["Дата сдачи"].ToString().Trim();
                    credit.DateCredit = credit.DateCredit.Remove(10);
                    credit.Semester = sqldr["Семестр"].ToString().Trim();
                    var numbooks = sqldr["Номер зачетной книжки"].ToString().Trim();
                    credit.NumBook = Int32.Parse(numbooks);
                    credit.FormCredit = sqldr["Наименование"].ToString().Trim();
                    credit.NameSubject = sqldr["Наименование дисциплины"].ToString().Trim();
                    var idsubj = sqldr["Код дисциплины"].ToString().Trim();
                    credit.IdSubject = Int32.Parse(idsubj);
                    var idteach = sqldr["Код преподавателя"].ToString().Trim();
                    credit.IdTeacher = Int32.Parse(idteach);
                    var idcred = sqldr["Код формы зачета"].ToString().Trim();
                    credit.IdCredit = Int32.Parse(idcred);
                    creditsList.Add(credit);
                }
            }
            //dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();
            FillDGV();
        }
    }

        public class Credit
        {
            public String FormCredit;
            public int NumBook;
            public String Semester;
            public String DateCredit;
            public String NumHours;
            public String Grades;
            public String NameStudent;
            public String SurnameStudent;
            public String MidlnameStudent;
            public String NameTeacher;
            public String SurnameTeacher;
            public String MidlnameTeacher;
            public String NameSubject;
            public int IdTeacher;
            public int IdSubject;
            public int IdCredit;
            public int IdStud;
            public int indexCombo;
        }
}

//IEnumerable
//creditsList.Find(f => f.IdTeacher == 7.ToString());