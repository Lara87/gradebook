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
    public partial class Teachers : Form
    {
        List<Teacher> teachersList = new List<Teacher>();
        Start start;
        String imgDelete = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\del.png";
        String imgEdit = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\pen.png";
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Teachers(Start s)
        {
            start = s;
            InitializeComponent();
        }

        public void AddTeacher(Teacher teacher)//добавляем преподавателя
        {
            teachersList.Add(teacher);//добавляем данные в список
            dataGridView1.Rows.Clear();//чистим строки
            dataGridView1.Columns.Clear();//чистим колонки
            FillDGV();//заполняем таблицу
        }

        public void UpdateTeacher(Teacher teacher)//обновляем данные у преподавателя
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        void LoadData()
        {
            using (SqlConnection sqlC = new SqlConnection(url))//область видимости
            {
                sqlC.Open();
                var com = new SqlCommand("SELECT [Код преподавателя], [Фамилия преподавателя], [Имя преподавателя], [Отчество преподавателя] FROM Table_Teacher", sqlC);
                SqlDataReader sqlDR = com.ExecuteReader();

                while (sqlDR.Read())
                {
                var teacher = new Teacher();
                teacher.SurnameT = sqlDR["Фамилия преподавателя"].ToString().Trim();
                teacher.NameT = sqlDR["Имя преподавателя"].ToString().Trim();
                teacher.MidlNameT = sqlDR["Отчество преподавателя"].ToString().Trim();
                teacher.IdTeacher = sqlDR["Код преподавателя"].ToString().Trim();

                teachersList.Add(teacher);
                }
            }
        }

        void FillDGV()
        {
            DataGridViewImageColumn imgC = new DataGridViewImageColumn();
            DataGridViewImageColumn imgC2 = new DataGridViewImageColumn();
            dataGridView1.Columns.AddRange(imgC, imgC2);

            for (int i = 0; i <= 3; i++)
            {
                DataGridViewTextBoxColumn cl = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(cl);
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderText = "Код преподавателя";
            dataGridView1.Columns[3].HeaderText = "Фамилия преподавателя";
            dataGridView1.Columns[4].HeaderText = "Имя преподавателя";
            dataGridView1.Columns[5].HeaderText = "Отчество преподавателя";
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[2].Visible = false;

            foreach (var el in teachersList)
            {
                //el - teacherList[i]
                //ячейки удалить и изменить
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.Height = 32;

                //создаем кнопки редак и удал

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

                var idT = el.IdTeacher;
                DataGridViewCell c4 = new DataGridViewTextBoxCell();
                c4.Value = idT;
                dataGridViewRow.Cells.Add(c4);

                var surname = el.SurnameT;
                DataGridViewCell c1 = new DataGridViewTextBoxCell();
                c1.Value = surname;
                dataGridViewRow.Cells.Add(c1);

                var name = el.NameT;
                DataGridViewCell c2 = new DataGridViewTextBoxCell();
                c2.Value = name;
                dataGridViewRow.Cells.Add(c2);

                var midlname = el.MidlNameT;
                DataGridViewCell c3 = new DataGridViewTextBoxCell();
                c3.Value = midlname;
                dataGridViewRow.Cells.Add(c3);

                dataGridView1.Rows.Add(dataGridViewRow);
            }
        }


        private void Teacher_Load(object sender, EventArgs e)
        {
            LoadData();
            FillDGV();
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
            if (e.ColumnIndex == 0)
            {
                //del 
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(url))
                    {
                        sqlConnection.Open();
                        var rowIndex = e.RowIndex;
                        var cell = dataGridView1.Rows[rowIndex].Cells[2].Value;
                        var com1 = new SqlCommand("DELETE FROM Table_Teacher WHERE [Код преподавателя] = '" + cell + "' ", sqlConnection);
                        SqlDataReader sqldr = com1.ExecuteReader();
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                    MessageBox.Show("Данные успешно удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (e.ColumnIndex == 1)
            {
                //edit
                TeacherInfo teacherInfo = new TeacherInfo(teachersList[e.RowIndex], false, this);
                teacherInfo.Show();
            }
        }

        private void button1AddTeacher_Click(object sender, EventArgs e)
        {
            TeacherInfo teacherInfo = new TeacherInfo(null, true, this);
            teacherInfo.Show();
        }

        private void Teachers_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                sqlConnection.Open();
                var command = new SqlCommand(" SELECT *FROM Table_Teacher WHERE Table_Teacher.[Фамилия преподавателя] LIKE '%"+textBox1.Text.ToString().Trim()+"%' or " +
                    "Table_Teacher.[Имя преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + "%' or " +
                    "Table_Teacher.[Отчество преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + "%'", sqlConnection);

                SqlDataReader sqldr = command.ExecuteReader();
                
                while (sqldr.Read())
                {
                    var teacher = new Teacher();
                    teacher.SurnameT = sqldr["Фамилия преподавателя"].ToString().Trim();
                    teacher.NameT = sqldr["Имя преподавателя"].ToString().Trim();
                    teacher.MidlNameT = sqldr["Отчество преподавателя"].ToString().Trim();
                    teacher.IdTeacher = sqldr["Код преподавателя"].ToString().Trim();                   
                    teachersList.Add(teacher);
                }
            }

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();

        }
    }

    public class Teacher
    {
        public String IdTeacher;
        public String SurnameT;
        public String NameT;
        public String MidlNameT;
    }

}
