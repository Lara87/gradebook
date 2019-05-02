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
    public partial class Users : Form
    {
        List<User> userList = new List<User>();//создали список для пользователей

        String imgDelete = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\del.png";
        String imgEdit = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\pen.png";

        Start start;

        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Users(Start s)
        {
            start = s;
            InitializeComponent();
        }

        public void AddUser(User user)//добавляем нового ползователя
        {
            userList.Add(user);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        public void UpdateUser(User user)//обновляем данные у существующего пользователя
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        void DelUser()
        {

        }

        void LoadData()//загружаем данные
        {
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                sqlConnection.Open();
                var command = new SqlCommand("SELECT DISTINCT [Id_Role], [Login], [Password], [Role] FROM Table_User", sqlConnection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    var user = new User();
                    user.loginUser = sqlDataReader["Login"].ToString().Trim();
                    user.passUser = sqlDataReader["Password"].ToString().Trim();
                    user.roleUser = sqlDataReader["Role"].ToString().Trim();
                    user.idRole = sqlDataReader["Id_Role"].ToString().Trim();
                    userList.Add(user);
                }
            }
        }

        void FillDGV()//заполняем таблицу
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
            dataGridView1.Columns[2].HeaderText = "Id";
            dataGridView1.Columns[3].HeaderText = "Логин";
            dataGridView1.Columns[4].HeaderText = "Пароль";
            dataGridView1.Columns[5].HeaderText = "Наименование";
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[2].Visible = false;

            foreach (var el in userList)
            {
                //el - userList[i] - бежим по списку элементов
                //ячейки удалить и изменить
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                //создаём кнопки редак и удал
                DataGridViewImageCell dataGridViewCell = new DataGridViewImageCell();
                var idel = Image.FromFile(imgDelete);
                dataGridViewCell.Value = idel;
                dataGridViewRow.Cells.Add(dataGridViewCell);

                DataGridViewImageCell dataGridViewCell2 = new DataGridViewImageCell();
                var iedit = Image.FromFile(imgEdit);
                dataGridViewCell2.Value = iedit;
                dataGridViewRow.Cells.Add(dataGridViewCell2);

                var idrole = el.idRole;
                DataGridViewCell dGVCell = new DataGridViewTextBoxCell();
                dGVCell.Value = idrole;
                dataGridViewRow.Cells.Add(dGVCell);

                var log = el.loginUser;
                DataGridViewCell dGVCell0 = new DataGridViewTextBoxCell();
                dGVCell0.Value = log;
                dataGridViewRow.Cells.Add(dGVCell0);

                var pass = el.passUser;
                DataGridViewCell dGVCell1 = new DataGridViewTextBoxCell();
                dGVCell1.Value = pass;
                dataGridViewRow.Cells.Add(dGVCell1);

                var role = el.roleUser;
                DataGridViewCell dGVCell2 = new DataGridViewTextBoxCell();
                dGVCell2.Value = role;
                dataGridViewRow.Cells.Add(dGVCell2);

                dataGridView1.Rows.Add(dataGridViewRow);

            }
        }

        private void Users_Load(object sender, EventArgs e)
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
                        var com1 = new SqlCommand("DELETE FROM Table_User WHERE [Id_Role] = '" + cell + "' ", sqlConnection);
                        SqlDataReader sqldr = com1.ExecuteReader();
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                    MessageBox.Show("Данные успешно удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            if (e.ColumnIndex == 1)
            {
                //edit
                UsersInfo usersInfo = new UsersInfo(userList[e.RowIndex], false, this);
                usersInfo.Show();
            }
        }

        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }

        private void button1Add_Click(object sender, EventArgs e)
        {
            UsersInfo usersInfo = new UsersInfo(null, true, this);
            usersInfo.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                sqlConnection.Open();
                var command = new SqlCommand(" SELECT *FROM Table_User WHERE Table_Teacher.[Фамилия преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Teacher.[Имя преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + " %' " +
                    "OR Table_Teacher.[Отчество преподавателя] LIKE '%" + textBox1.Text.ToString().Trim() + " %' ", sqlConnection);

                SqlDataReader sqldr = command.ExecuteReader();

                while (sqldr.Read())
                {
                    var teacher = new Teacher();
                    teacher.SurnameT = sqldr["Фамилия преподавателя"].ToString().Trim();
                    teacher.NameT = sqldr["Имя преподавателя"].ToString().Trim();
                    teacher.MidlNameT = sqldr["Отчество преподавателя"].ToString().Trim();
                    teacher.IdTeacher = sqldr["Код преподавателя"].ToString().Trim();

                    //teachersList.Add(teacher);
                }

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

            }


            FillDGV();
        }
    }

    public class User
        {
        public String idRole;
        public String loginUser;
        public String passUser;
        public String roleUser;
        }
}

   

