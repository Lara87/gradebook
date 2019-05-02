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
    public partial class Specialties : Form
    {
        List<Specialty> specialtyList = new List<Specialty>();
        Start start;
        String imgDelete = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\del.png";
        String imgEdit = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\pen.png";
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Specialties(Start s)
        {
            
            start = s;
            InitializeComponent();
        }

        public void AddSpecialty(Specialty specialty)
        {
            specialtyList.Add(specialty);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();

        }

        public void UpdateSpecialty(Specialty specialty)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        void LoadData()
        {
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                sqlConnection.Open();
                var sqlCom = new SqlCommand("SELECT S.[Специальность], S.[Код специальности], SD.[Наименование подразделения], SD.[Код подразделения]" +
                    "FROM Table_Specialty AS S, Table_Struct_Department AS SD WHERE S.[Код подразделения] = SD.[Код подразделения] ", sqlConnection);
                SqlDataReader sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    var specialty = new Specialty();
                    specialty.idSpecialty = sqlDR["Код специальности"].ToString().Trim();
                    var idSpec = sqlDR["Код подразделения"].ToString().Trim();
                    specialty.idStructDep = Int32.Parse(idSpec);

                    specialty.NameSpecialty = sqlDR["Специальность"].ToString().Trim();
                    specialty.nameStructDep = sqlDR["Наименование подразделения"].ToString().Trim();
                    specialtyList.Add(specialty);
                }
            }
        }

        void FillDGV()
        {
            //ячейки удалить и изменить
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
            dataGridView1.Columns[2].HeaderText = "Код специальности";
            dataGridView1.Columns[3].HeaderText = "Код подразделения";
            dataGridView1.Columns[4].HeaderText = "Специальность";
            dataGridView1.Columns[5].HeaderText = "Структурное подразделение";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Width = 320;
            dataGridView1.Columns[5].Width = 320;

            foreach (var el in specialtyList)
            {
                //el - studentList[i]
                //ячейки удалить и изменить
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.Height = 32;
                //создаём кнопки редак и удал
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

                var nameIdSpec = el.idSpecialty;
                DataGridViewCell cc3 = new DataGridViewTextBoxCell();
                cc3.Value = nameIdSpec;
                dataGridViewRow.Cells.Add(cc3);

                var nameIdStructDep = el.idStructDep;
                DataGridViewCell cc4 = new DataGridViewTextBoxCell();
                cc4.Value = nameIdStructDep;
                dataGridViewRow.Cells.Add(cc4);

                var nameSpecialty = el.NameSpecialty;
                DataGridViewCell c1 = new DataGridViewTextBoxCell();
                c1.Value = nameSpecialty;
                dataGridViewRow.Cells.Add(c1);

                var nameStructDep = el.nameStructDep;
                DataGridViewCell c2 = new DataGridViewTextBoxCell();
                c2.Value = nameStructDep;
                dataGridViewRow.Cells.Add(c2);

                

                

                dataGridView1.Rows.Add(dataGridViewRow);

            }
        }

          

        private void Specialty_Load(object sender, EventArgs e)
        {
            LoadData();
            FillDGV();
            if((start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                button1Add.Visible = true;
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                //del 

                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(url))
                    {
                        sqlConnection.Open();
                        var rowIndex = e.RowIndex;
                        var cell = dataGridView1.Rows[rowIndex].Cells[2].Value;
                        var com1 = new SqlCommand(" DELETE FROM Table_Specialty WHERE [Код специальности] = '" + cell + "' ", sqlConnection);
                        SqlDataReader sqldr = com1.ExecuteReader();
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                    MessageBox.Show("Данные успешно удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if ((e.ColumnIndex == 1) && (start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                //edit
               SpecialtyInfo specialtyInfo = new SpecialtyInfo(specialtyList[e.RowIndex], false, this);
               specialtyInfo.Show();
            }

        }

        private void Specialty_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }

        private void button1Add_Click(object sender, EventArgs e)
        {
            SpecialtyInfo specialtyInfo = new SpecialtyInfo(null, true, this);
            specialtyInfo.Show();
        }
    }

    public class Specialty
    {
        public String idSpecialty;
        public String NameSpecialty;
        public int idStructDep;
        public String nameStructDep;

    }
}
