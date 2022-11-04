using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public static string path="";
        string[] file;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog OPF = new OpenFileDialog();
                OPF.Filter = "Файлы txt|*.txt";
                if (OPF.ShowDialog() == DialogResult.OK)
                {
                    path = (OPF.FileName);
                }
                string fileContent;
                using (StreamReader reader = new StreamReader(path))
                {
                    fileContent = reader.ReadToEnd();
                }
                MessageBox.Show(fileContent);
            }
            catch
            {
                MessageBox.Show("Выберите файл");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (path.Length==0)
            {
                MessageBox.Show("Выберите сначала файл");
            }
            else
            {
                Form2 A = new Form2();
                A.Show();  
            }      

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Необходимо загрузить файл с данными. Пример представляет из себя набор признаков, записанных через пробел. Каждый пример должен быть записан в новой строчке.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
