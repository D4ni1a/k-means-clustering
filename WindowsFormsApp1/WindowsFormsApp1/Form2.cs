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
    public class Creator
    {
        public string p2;
        Algorithm Result;Algorithm NNN;
        public void Choose (string str1, string str2, string str3, string strName)
        {
            //Считывание данных из файла
            using (StreamReader reader = File.OpenText(Form1.path))
            {
                string line = null;
                int count = 0;
                
                int N = System.Convert.ToInt32(str1);
                int M = System.Convert.ToInt32(str2);
                int K = System.Convert.ToInt32(str3);
                NNN = new Algorithm(N, M);
                    do
                    {
                    //Построчное считывание из файла и запись в объект класса Algorithm
                        line = reader.ReadLine();
                        if (line == null) break;
                        for (int i = 0; i < M; i++)
                        {
                            NNN[count, i] = System.Convert.ToInt32(line.Split(' ')[i]);
                        }
                        count += 1;
                    }
                    while (line != null);
                //Проведение метода к средних
                    Result = NNN.K_Mean(K);
                }
            //Формирование имени нового файла из директории исходника и непосредственно имени
            string p1 = Form1.path;
            p2 = "";
            string[] p3 = p1.Split('\\');
            for (int i = 0; i < p1.Split('\\').Length - 1; i++)
            {
                p2 += (p3[i] + "\\");
            }
            p2 += strName;
            p2 += ".txt";
            //Запись результата в файл
            using (StreamWriter writer = File.CreateText(p2))
            {

                for (int i = 0; i < Result.n; i++)
                {
                    string text = "";
                    for (int j = 0; j < Result.m; j++)
                    {
                        text += (System.Convert.ToString(Result[i, j]) + " ");
                    }
                    writer.WriteLine(text);
                }
            }
        }
    }
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string[] file;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length != 0)
                {

                    Creator create = new Creator();


                    create.Choose(textBox2.Text, textBox3.Text, textBox4.Text, textBox1.Text);



                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    
                    string fileContent;
                    using (StreamReader reader = new StreamReader(create.p2))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    MessageBox.Show(fileContent);
                
                }
                else
                {
                    MessageBox.Show("Введите название нового файла");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат введенных данных");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Неверный формат введенных данных");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Количество измерений должно соответствовать количеству стобцов. Количество примеров - количеству строк. " +
                "Значение k - предполагаему количеству кластеров. В случае несоответствия данных в файле заданном параметрам или необходимому формату, " +
                "их необходимо исправить, иначе программа будет работать неверно");
        }
    }
}
