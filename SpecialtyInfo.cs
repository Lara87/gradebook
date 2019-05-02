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
    public partial class SpecialtyInfo : Form
    {
        Specialties pForm;
        bool AddORUpd;
        Specialty specialty;
        Type t;
        int indexCB;


        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SpecialtyInfo(Specialty spec, bool AddORUpd, Specialties pForm)
        {
            this.AddORUpd = AddORUpd;
            this.pForm = pForm;
            specialty = spec;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void fillComboboxNameStactDepUp()
        {
            String strdep = String.Format("SELECT DISTINCT [Код подразделения], [Наименование подразделения] FROM Table_Struct_Department");
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                indexCB = 0;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(strdep, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<object> strdepList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                    var k1 = (new { idStrDep = reader["Код подразделения"], strDep = reader["Наименование подразделения"].ToString().Trim()});
                    strdepList.Add(k1);
                    if (k1.idStrDep.ToString() == specialty.idStructDep.ToString())
                    { indexCB = i; }
                    i++;
                }
                comboBox1NameStrDep.DataSource = strdepList;
                comboBox1NameStrDep.DisplayMember = "strDep";
                comboBox1NameStrDep.ValueMember = "idStrDep";
                if (strdepList.Count > 0)
                {
                    comboBox1NameStrDep.SelectedIndex = indexCB;
                }
                t = strdepList[0].GetType();
            }
        }

        void fillComboboxNameStactDepAdd()
        {
            String strdep = String.Format("SELECT DISTINCT [Код подразделения], [Наименование подразделения] FROM Table_Struct_Department");
            using (SqlConnection sqlConnection = new SqlConnection(url))
            {
                indexCB = 0;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(strdep, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<object> strdepList = new List<object>();
                int i = 0;
                while (reader.Read())
                {
                    strdepList.Add(new { idStrDep = reader["Код подразделения"], strDep = reader["Наименование подразделения"].ToString().Trim() });
                    
                }
                comboBox1NameStrDep.DataSource = strdepList;
                comboBox1NameStrDep.DisplayMember = "strDep";
                comboBox1NameStrDep.ValueMember = "idStrDep";
                if (strdepList.Count > 0)
                {
                    comboBox1NameStrDep.SelectedIndex = 0;
                }
                t = strdepList[0].GetType();
            }
        }

        private void SpecialtyInfo_Load(object sender, EventArgs e)
        {
            if(!AddORUpd)//Upd = false
            {
                textBox1Spec.Text = specialty.NameSpecialty;
               
                fillComboboxNameStactDepUp();
            }
            else
            {
                fillComboboxNameStactDepAdd();
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SpecialtyInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //specialty.Close();
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            var comboStrDep = comboBox1NameStrDep.SelectedItem;
            dynamic srtDep = comboStrDep;
            var IdStrDep = srtDep.idStrDep;


            var NameStrDep = srtDep.strDep;

            using (SqlConnection sqlc = new SqlConnection(url))
            {
                if (AddORUpd)//Upd - false
                {
                    //Add
                    if ((textBox1Spec.Text.Length <= 2))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = String.Format("INSERT INTO Table_Specialty ([Специальность], [Код подразделения]) VALUES ('{0}', '{1}') Select Scope_identity()", textBox1Spec.Text.ToString().Trim(), IdStrDep);
                        sqlc.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlc);
                        sqlCommand.ExecuteScalar();
                        pForm.AddSpecialty(new Specialty() { NameSpecialty = textBox1Spec.Text, idStructDep = IdStrDep, nameStructDep = NameStrDep });
                        MessageBox.Show("Данные успешно добавлены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    if ((textBox1Spec.Text.Length <= 1))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = "UPDATE Table_Specialty SET [Специальность] = '" + textBox1Spec.Text.Trim() + "', [Код подразделения] = '" + IdStrDep + "' WHERE [Код специальности] = '" + specialty.idSpecialty + "' ";
                        sqlc.Open();
                        SqlCommand command = new SqlCommand(s, sqlc);
                        command.ExecuteNonQuery();
                        
                        specialty.NameSpecialty = textBox1Spec.Text.Trim();
                        specialty.nameStructDep = NameStrDep;
                        specialty.idStructDep = IdStrDep;
                        pForm.UpdateSpecialty(specialty);
                        MessageBox.Show("Данные успешно изменены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
