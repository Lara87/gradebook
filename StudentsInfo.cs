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
    public partial class StudentsInfo : Form
    {
        Students pForm;
        bool AddORUpd;
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        Student st;

        public StudentsInfo(Student stud, bool AddORUpd, Students pForm)
        {
            this.AddORUpd = AddORUpd;
            this.pForm = pForm;
            st = stud;
            InitializeComponent() ;
        }

       
        void fillCombobox2()
        {
            var j = comboBox2StructDep.SelectedItem;
            dynamic x = j;
            var res = x.indexInst;

            String spec = String.Format("Select [Специальность], [Код специальности] FROM Table_Specialty Where [Код подразделения] = {0} ", res);
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                sqlc.Open();
                SqlCommand com = new SqlCommand(spec, sqlc);
                SqlDataReader reader = com.ExecuteReader();
                List<object> specList = new List<object>();
                while (reader.Read())
                {
                    specList.Add(new { index = reader["Код специальности"], nameSpec = (reader["Специальность"] as String).Trim() });
                }
                comboBox1Spec.DataSource = specList;
                comboBox1Spec.DisplayMember = "nameSpec";
                comboBox1Spec.ValueMember = "index";
                if(specList.Count>0)
                {
                    comboBox1Spec.SelectedIndex = 0;
                }                
            }
        }

        void queryListInst(int cbi = 0)
        {
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                String queryListInst = "Select [Наименование подразделения], [Код подразделения] FROM Table_Struct_Department";
                sqlc.Open();
                var com = new SqlCommand(queryListInst, sqlc);
                SqlDataReader sqldr = com.ExecuteReader();

                List<object> inst = new List<object>();//создали список анонимный подразделений

                while(sqldr.Read())
                {
                    inst.Add(new  {indexInst = (sqldr["Код подразделения"]).ToString(), nameInst = (sqldr["Наименование подразделения"] as String).Trim() });
                }
                /*
                List<object> j = new List<object>()
                {
                    new { index = "in", text = "jk" },
                    new { index = "in2", text = "jk3" }
                };*/
                comboBox2StructDep.DataSource = inst;
                comboBox2StructDep.DisplayMember = "nameInst";
                comboBox2StructDep.ValueMember = "indexInst";
                comboBox2StructDep.SelectedIndex = cbi;
            }
            fillCombobox2();
        }

        private void StudentsInfo_Load(object sender, EventArgs e)
        {
            if (!AddORUpd)//Upd - false
            {
                label9.Visible = false;
                label1.Visible = true;
                textBox1NumBook.Visible = true;
                textBox1NumBook.Text = st.NumBook;
                textBox2Surname.Text = st.Surn;
                textBox3Name.Text = st.Name;
                textBox4MidlName.Text = st.MidlN;
                dateTimePicker1DateBirth.Text = st.DateB;
                maskedTextBox1NumTel.Text = st.NumTel;
                textBox6NumCourse.Text = st.NumCourse;
                maskedTextBox1NumGroup.Text = st.NumGroup;
                queryListInst(Int32.Parse(st.IdInst));
            }
            else
            {
                queryListInst();
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var date = dateTimePicker1DateBirth.Value.ToShortDateString();//выводим только дату, без времени - обрезали формат
            String sDT = date.ToString();

            var comboInst = comboBox2StructDep.SelectedItem;
            dynamic inst = comboInst;
            var IdInst = (String)(inst.indexInst);

            var j = comboBox1Spec.SelectedItem;
            dynamic x = j;
            var res = (Int32)(x.index);
            var IdSpec = res.ToString();

            using (SqlConnection sqlc = new SqlConnection(url))
            {
                if (AddORUpd)//Upd - false
                {
                    //Add
                    if ((textBox2Surname.Text.Length <= 2) || (textBox3Name.Text.Length <= 1) || (textBox4MidlName.Text.Length <= 1) || (textBox6NumCourse.Text.Length < 1)||(maskedTextBox1NumGroup.Text.Length < 4))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        label1.Hide();
                        label1.Visible = false;
                        textBox1NumBook.Visible = false;
                        String s = String.Format("INSERT INTO Table_Student ([Фамилия студента], [Имя студента], [Отчество студента], [Дата рождения], [Телефон], " +
                            "[Код специальности], [Номер курса], [Номер группы], [Код подразделения] ) " +
                            "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')  Select Scope_identity()", textBox2Surname.Text.Trim(), textBox3Name.Text.Trim(), textBox4MidlName.Text.Trim(), sDT, maskedTextBox1NumTel.Text.Trim(), res, textBox6NumCourse.Text.Trim(), maskedTextBox1NumGroup.Text.Trim(), inst.indexInst);
                        sqlc.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlc);
                        var id = sqlCommand.ExecuteScalar();
                        pForm.AddStudent(new Student() { DateB = sDT, Surn = textBox2Surname.Text, Name = textBox3Name.Text, MidlN = textBox4MidlName.Text, NumTel = maskedTextBox1NumTel.Text, Inst = inst.nameInst, Spec = x.nameSpec, NumBook = id.ToString(), IdInst = IdInst , IdSpec = IdSpec, NumCourse = textBox6NumCourse.Text, NumGroup = maskedTextBox1NumGroup.Text});
                        MessageBox.Show("Данные успешно добавлены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    if ((textBox2Surname.Text.Length <= 2) || (textBox3Name.Text.Length <= 2) || (textBox4MidlName.Text.Length <= 2) || (textBox6NumCourse.Text.Length < 1) || (maskedTextBox1NumGroup.Text.Length < 3))
                    {
                        MessageBox.Show("Данные введены некорректно!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = "UPDATE Table_Student SET [Фамилия студента] = '" + textBox2Surname.Text.Trim() + "', [Имя студента] = '" + textBox3Name.Text.Trim() + "', [Отчество студента] = '" + textBox4MidlName.Text.Trim() + "'," +
                            " [Дата рождения] = '" + sDT + "'," +
                            " [Телефон] = '" + maskedTextBox1NumTel.Text.Trim() +"', [Номер курса] = '" + textBox6NumCourse.Text.Trim() + "', [Номер группы] = '" + maskedTextBox1NumGroup.Text.Trim() + "' WHERE [Номер зачетной книжки] = '" + st.NumBook + "' ";
                        sqlc.Open();
                        SqlCommand command = new SqlCommand(s, sqlc);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно сохранены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        st.Surn = textBox2Surname.Text.Trim();
                        st.Name = textBox3Name.Text.Trim();
                        st.MidlN = textBox4MidlName.Text.Trim();
                        st.DateB = sDT.Trim();
                        st.NumTel = maskedTextBox1NumTel.Text.Trim();
                        st.NumCourse = textBox6NumCourse.Text.Trim();
                        st.NumGroup = maskedTextBox1NumGroup.Text.Trim();
                        pForm.UpdateStudent(st);
                    }
                }         
            }
        }
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            fillCombobox2();
        }

        private void textBox6NumCourse_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void textBox2Surname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox3Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox4MidlName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }
    }
}
