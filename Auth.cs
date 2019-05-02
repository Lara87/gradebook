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
    public partial class Auth : Form
    {
        String url = @"Data Source=LAPTOP-JF0MI718\NEWSQLSERVER;Initial Catalog=ChernikovaLV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if((textBox1.Text.Length>1)&&(textBox2.Text.Length>1))
            {
                using (SqlConnection sqlC = new SqlConnection(url))
                {
                    sqlC.Open();
                    SqlCommand sqlCom = new SqlCommand("SELECT Id_Role, Role FROM Table_User WHERE Login='"+textBox1.Text+"' AND Password='"+textBox2.Text+"'", sqlC);
                    SqlDataReader sqlDR = sqlCom.ExecuteReader();
                    if (sqlDR.Read())
                    {
                        var k = sqlDR.GetValue(0).ToString();
                        var m = sqlDR.GetValue(1);
                        Start start = new Start(this, Int32.Parse(k));
                        start.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль введены неверно!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Auth_Load(object sender, EventArgs e)
        {
            
        }
    }
}
