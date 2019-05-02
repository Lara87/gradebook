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
    public partial class Subjects : Form
    {
        List<Subject> subjectsList = new List<Subject>();
        Start start;
        String imgDelete = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\del.png";
        String imgEdit = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\pen.png";
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public Subjects(Start s)
        {
            start = s;
            InitializeComponent();
        }

        public void AddSubject(Subject subject)
        {
            subjectsList.Add(subject);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            FillDGV();
        }

        public void UpdateSubject(Subject subject)
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
                var command = new SqlCommand("SELECT [Код дисциплины], [Наименование дисциплины] FROM Table_Subject", sqlConnection);
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    var subject = new Subject();
                    subject.IdSubject = sqlDataReader["Код дисциплины"].ToString().Trim();
                    subject.NameSubject = sqlDataReader["Наименование дисциплины"].ToString().Trim();
                    subjectsList.Add(subject);
                }
            }
        }

            void FillDGV()
            {
                DataGridViewImageColumn imgC = new DataGridViewImageColumn();
                DataGridViewImageColumn imgC2 = new DataGridViewImageColumn();
                dataGridView1.Columns.AddRange(imgC, imgC2);

                for (int i = 0; i <= 1; i++)
                {
                    DataGridViewTextBoxColumn dataGVTB = new DataGridViewTextBoxColumn();
                    dataGridView1.Columns.Add(dataGVTB);
                }
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[2].HeaderText = "Код дисциплины";
                dataGridView1.Columns[3].HeaderText = "Наименование дисциплины";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Width = 350;

                foreach (var el in subjectsList)
                {
                    //el - studentList[i]
                    //ячейки удалить и изменить
                    DataGridViewRow dataGridViewRow = new DataGridViewRow();
                    dataGridViewRow.Height = 32;

                    //creat button del or edit
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

                    var idS = el.IdSubject;
                    DataGridViewCell c1 = new DataGridViewTextBoxCell();
                    c1.Value = idS;
                    dataGridViewRow.Cells.Add(c1);

                    var nSubj = el.NameSubject;
                    DataGridViewCell c = new DataGridViewTextBoxCell();
                    c.Value = nSubj;
                    dataGridViewRow.Cells.Add(c);

                    dataGridView1.Rows.Add(dataGridViewRow);


                }

            }
        private void Subject_Load(object sender, EventArgs e)
        {
                LoadData();
                FillDGV();
            if((start.role == 0) || (start.role == 1) || (start.role == 2))
                {
                    buttonAddSubject.Visible = true;
                }
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
                        var com1 = new SqlCommand("DELETE FROM Table_Subject WHERE [Код дисциплины] = '" + cell + "' ", sqlConnection);
                        SqlDataReader sqldr = com1.ExecuteReader();
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                    MessageBox.Show("Данные успешно удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if ((e.ColumnIndex == 1) && (start.role == 0) || (start.role == 1) || (start.role == 2))
            {
                //edit
                SubjectInfo subjectInfo = new SubjectInfo(subjectsList[e.RowIndex], false, this);
                subjectInfo.Show();
            }

        }

        private void buttonAddSubject_Click(object sender, EventArgs e)
        {
            SubjectInfo subjectInfo = new SubjectInfo(null, true, this);
            subjectInfo.Show();
        }

        private void Subjects_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }
    }

    public class Subject
    {
    public String IdSubject;
    public String NameSubject;
    }
}
