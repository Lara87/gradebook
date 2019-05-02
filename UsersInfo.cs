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
    public partial class UsersInfo : Form
    {
        Users pForm;
        bool AddORUpd;
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        User users;


        public UsersInfo(User user, bool AddORUpd, Users pForm)
        {
            this.AddORUpd = AddORUpd;
            this.pForm = pForm;
            users = user;
           
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void UsersInfo_Load(object sender, EventArgs e)
        {
            if (!AddORUpd)//Upd - false
            {
                textBoxLog.Text = users.loginUser;
                textBox2Pass.Text = users.passUser;
                textBox1Name.Text = users.roleUser;
            }
            else
            {
                
            }

        }

        private void UsersInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlc = new SqlConnection(url))
            {
                if (AddORUpd)//Upd - false
                {
                    //Add
                    if ((textBox1Name.Text.Length <= 3) || (textBox2Pass.Text.Length <= 5) || (textBoxLog.Text.Length <= 2)||(textBox1Name.Text.Length >= 15) || (textBox2Pass.Text.Length >= 15) || (textBoxLog.Text.Length >= 15))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = String.Format("INSERT INTO Table_User ([Login], [Password], [Role]) VALUES ('{0}', '{1}', '{2}') Select Scope_identity()", textBoxLog.Text.Trim(), textBox2Pass.Text.Trim(), textBox1Name.Text.Trim());
                        sqlc.Open();
                        SqlCommand sqlCommand = new SqlCommand(s, sqlc);
                        sqlCommand.ExecuteNonQuery();
                        pForm.AddUser(new User() { loginUser = textBoxLog.Text, passUser = textBox2Pass.Text, roleUser = textBox1Name.Text });
                        MessageBox.Show("Данные успешно добавлены!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if ((textBox1Name.Text.Length <= 3) || (textBox2Pass.Text.Length <= 5) || (textBoxLog.Text.Length <= 2)|| (textBox1Name.Text.Length >= 15) || (textBox2Pass.Text.Length >= 15) || (textBoxLog.Text.Length >= 15))
                    {
                        MessageBox.Show("Данные введены некорректно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String s = "UPDATE Table_User SET [Login] = '" + textBoxLog.Text.Trim() + "', [Password] = '" + textBox2Pass.Text.Trim() + "', [Role] = '" + textBox1Name.Text.Trim() + "' WHERE [Id_Role] = '" + users.idRole + "' ";
                        sqlc.Open();
                        SqlCommand command = new SqlCommand(s, sqlc);
                        command.ExecuteNonQuery();
                        users.loginUser = textBoxLog.Text.Trim();
                        users.passUser = textBox2Pass.Text.Trim();
                        users.roleUser = textBox1Name.Text.Trim();
                        pForm.UpdateUser(users);
                        MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
        }
    }
}
