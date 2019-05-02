using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class InfoProg : Form
    {
      //  Auth auth;
        Start start;
        String pic = @"C:\Users\Любовь\Desktop\Учеба\Базы данных\WindowsFormsApp2\womanstudent.ico";

        public InfoProg(Start s)
        {
            start = s;
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void InfoProg_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.Text = "\n" +
                "Астраханский государтсвенный технический университет" + "\n" +
                "Институт информационных технологий и коммуникаций" + "\n" +
                "Кафедра автоматизированные системы обработки информации и управления" + "\n" +
                "\n" +
                "\n" +
                "\n" +
                "Курсовой проект" + "\n" +
                "\n" +
                "Программа: Зачетная книжка студента" + "\n" +
                "по дисциплине: Базы данных"+ "\n" +
                "Проект выполнен студенткой группы ЗИНРБ - 31 Черниковой Л.В." + "\n" +
                "\n" +
                "Руководитель работы: преподаватель Учаев Д.Ю.";
            Image image = Image.FromFile(pic);
            Clipboard.SetImage(image);
            richTextBox1.Paste();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            start.Show();
            this.Hide();
        }

        private void InfoProg_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Close();
        }
    }
}
