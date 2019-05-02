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

    public partial class Students : Form
    {
        List<Student> studentsList = new List<Student>();
        Start start;
        String imgDelete = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\del.png";
        String imgEdit = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\pen.png";
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
               
        public Students(Start s)
        {
            start = s;
            InitializeComponent();
        }

        public void AddStudent(Student s)
        {
            studentsList.Add(s);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        public void UpdateStudent(Student s)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        void LoadData()
        {
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                sqlc.Open();
                var com = new SqlCommand("SELECT  Table_Student.[Номер зачетной книжки], Table_Student.[Фамилия студента], " +
                    "Table_Student.[Имя студента], Table_Student.[Отчество студента], Table_Student.[Дата рождения], Table_Student.[Телефон], " +
                    "Table_Student.[Код специальности], Table_Student.[Номер курса], Table_Student.[Номер группы], Table_Student.[Код подразделения], " +
                    "Table_Specialty.[Код специальности], Table_Specialty.[Специальность], Table_Specialty.[Код подразделения], " +
                    "Table_Struct_Department.[Код подразделения], Table_Struct_Department.[Наименование подразделения] FROM Table_Student, " +
                    "Table_Specialty, Table_Struct_Department WHERE Table_Student.[Код специальности] = Table_Specialty.[Код специальности] " +
                    "AND Table_Student.[Код подразделения] = Table_Struct_Department.[Код подразделения] ", sqlc);
                SqlDataReader sqldr = com.ExecuteReader();
                while (sqldr.Read())
                {
                    var student = new Student();
                    student.Name = sqldr["Имя студента"].ToString().Trim();
                    student.Surn = sqldr["Фамилия студента"].ToString().Trim();
                    student.MidlN = sqldr["Отчество студента"].ToString().Trim();
                    student.DateB = sqldr["Дата рождения"].ToString().Trim();
                    student.DateB = student.DateB.Remove(10);
                    student.NumTel = sqldr["Телефон"].ToString().Trim();
                    student.Spec = sqldr["Специальность"].ToString().Trim();
                    student.Inst = sqldr["Наименование подразделения"].ToString().Trim();
                    student.NumBook = sqldr["Номер зачетной книжки"].ToString().Trim();
                    student.IdInst = sqldr["Код подразделения"].ToString().Trim();
                    student.IdSpec = sqldr["Код специальности"].ToString().Trim();
                    student.NumCourse = sqldr["Номер курса"].ToString().Trim();
                    student.NumGroup = sqldr["Номер группы"].ToString().Trim();
                    studentsList.Add(student);
                }
            }
        }

        void FillDGV()
        {
        DataGridViewImageColumn imgC = new DataGridViewImageColumn();
        DataGridViewImageColumn imgC2 = new DataGridViewImageColumn();
        dataGridView1.Columns.AddRange(imgC, imgC2);

            for (int i = 0; i <= 7; i++)
            {
                DataGridViewTextBoxColumn cl = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(cl);
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderText = "Номер зачетной книжки";
            dataGridView1.Columns[3].HeaderText = "Ф.И.О. студента";
            dataGridView1.Columns[4].HeaderText = "Дата рождения";
            
            dataGridView1.Columns[5].HeaderText = "Телефон";
            dataGridView1.Columns[6].HeaderText = "Специальность";
            dataGridView1.Columns[7].HeaderText = "Наименование подразделения";
            dataGridView1.Columns[8].HeaderText = "Номер курса";
            dataGridView1.Columns[9].HeaderText = "Номер группы";
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[6].Width = 300;
            dataGridView1.Columns[7].Width = 200;
            dataGridView1.Columns[8].Width = 100;

            foreach (var el in studentsList)
            {
                //el - studentList[i]
                //ячейки удалить и изменить
                DataGridViewRow r = new DataGridViewRow();
                r.Height = 32;

                //создаём кнопки редак и удал
                DataGridViewImageCell cc = new DataGridViewImageCell();
                var idel = Image.FromFile(imgDelete);
                cc.Value = idel;
                r.Cells.Add(cc);
                DataGridViewImageCell cc2 = new DataGridViewImageCell();
                var iedit = Image.FromFile(imgEdit);
                cc2.Value = iedit;
                r.Cells.Add(cc2);
                imgC.Width = 32;
                imgC2.Width = 32;

                var nBook = el.NumBook;
                DataGridViewCell c = new DataGridViewTextBoxCell();
                c.Value = nBook;
                r.Cells.Add(c);

                var snm = shotName(el.Surn, el.Name, el.MidlN);
                DataGridViewCell c1 = new DataGridViewTextBoxCell();
                c1.Value = snm;
                r.Cells.Add(c1);

                var dateB = el.DateB;
                DataGridViewCell c3 = new DataGridViewTextBoxCell();
                c3.Value = dateB;
                r.Cells.Add(c3);

                var nTel = el.NumTel;
                DataGridViewCell c4 = new DataGridViewTextBoxCell();
                c4.Value = nTel;
                r.Cells.Add(c4);

                var spec = el.Spec;
                DataGridViewCell c5 = new DataGridViewTextBoxCell();
                c5.Value = spec;
                r.Cells.Add(c5);

                var inst = el.Inst;
                DataGridViewCell c6 = new DataGridViewTextBoxCell();
                c6.Value = inst;
                r.Cells.Add(c6);

                var nCourse = el.NumCourse;
                DataGridViewCell c7 = new DataGridViewTextBoxCell();
                c7.Value = nCourse;
                r.Cells.Add(c7);

                var nGroup = el.NumGroup;
                DataGridViewCell c8 = new DataGridViewTextBoxCell();
                c8.Value = nGroup;
                r.Cells.Add(c8);

                dataGridView1.Rows.Add(r);
            }              
        }

        private void Students_Load(object sender, EventArgs e)
        {
            LoadData();
            FillDGV();
            if ((start.role == 0) || (start.role == 1))
            {
                buttonAddStudent.Visible = true;
            }
        }

        String shotName(String a, String b, String c)//функция кот из полного имения отчества и фамилии делает Ф****.И.О.
        {
            //List<string> hg = new List<string>();
            var N = b.ToUpper().ElementAt(0);
            var MN = c.ToUpper().ElementAt(0);
            a += " " + N + ". " + MN + ".";
            return a;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            start.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0)&& (start.role==0) || (start.role == 1))
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(url))
                    {
                        sqlConnection.Open();
                        var rowIndex = e.RowIndex;
                        var cell = dataGridView1.Rows[rowIndex].Cells[2].Value;
                        var com1 = new SqlCommand("DELETE FROM Table_Student WHERE [Номер зачетной книжки] = '" + cell + "' ", sqlConnection);
                        SqlDataReader sqldr = com1.ExecuteReader();
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                    MessageBox.Show("Данные успешно удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if ((e.ColumnIndex == 1) && (start.role == 0) || (start.role == 1))
            {
                //edit
                StudentsInfo studentsInfo = new StudentsInfo(studentsList[e.RowIndex], false,this);
                studentsInfo.Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StudentsInfo studentsInfo = new StudentsInfo(null, true, this);
            studentsInfo.Show();
        }

        private void Students_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }

    }
    public class Student
    {
        public String NumTel;
        public String Name;
        public String Surn;
        public String MidlN;
        public String NumBook;
        public String Spec;
        public String Inst;
        public String IdSpec;
        public String IdInst;
        public String DateB;
        public String NumGroup;
        public String NumCourse;
    }
}
