﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private string to_lowercase(string s)
        {
            string res="";
            for(int i = 0; i < s.Length; i++)
                res+=to_lowercase(s[i]);
            return res;
        }
        private char to_lowercase(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return (char)(c + 32);
            }

            return c;
        }
        private bool IsPalindrom(string s)
        {
            for (int i = 0; i < s.Length / 2; i++)

                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }
        string fileName, saveFileName;
        public Form1()
        {
            InitializeComponent();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Дан текстовый файл, содержащий слова, разделенные одним или
несколькими пробелами. Создать на его основе новый файл, состоящий
только из тех строк первого файла, в которых имеются два слова – палиндрома (вне зависимости от регистра)."
                );
        }
        private void option1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s1, s2 = "";
            textBox2.Clear();
            for(int i=0;i<textBox1.Lines.Count();i++)
            {
                int polinomCount = 0;
                s1 = textBox1.Lines[i];
                string[] s_mas = s1.Split(' ');
                foreach (string str in s_mas)
                    if (IsPalindrom(str.ToLower()))
                    {
                        polinomCount++;
                        s2 += String.Concat(str, ' ');
                    }
                if (polinomCount > 1)
                {
                    if (textBox2.Text.Length > 0)
                        textBox2.AppendText(Environment.NewLine);
                    textBox2.AppendText(s2);
                }
                s2 = "";
                polinomCount = 0;
            }
        }
        private void option2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s1, s2 = "", res = "";
            textBox2.Clear();
            for(int i=0;i<textBox1.Lines.Count();i++)
            {
                int polinomCount = 0;
                s1 = textBox1.Lines[i]+' ';
                int j = 0;
                while (j < s1.Length)
                {
                    if (s1[j] != ' ')
                    {
                        res += s1[j];
                    }
                    else
                    {
                        if (IsPalindrom(to_lowercase(res)))
                        {
                            polinomCount++;
                            s2 += res + ' ';
                        }
                        res = "";
                    }
                    j++;
                }
                if (polinomCount > 1)
                {
                    if (textBox2.Text.Length > 0)
                        textBox2.AppendText(Environment.NewLine);
                    textBox2.AppendText(s2);
                }
                s2 = "";
            }
        }
        private void option3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0)
            {
                byte[] byteArray = Encoding.Unicode.GetBytes(textBox1.Text);
                textBox2.Clear();
                for (int i = 0; i < byteArray.Length; i++)
                {
                    textBox2.Text += byteArray[i];
                    textBox2.Text += ' ';
                }
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() 
                == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader f_In = new StreamReader(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
                textBox1.Text = f_In.ReadToEnd();
                textBox2.Clear();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Текстовый документ(*.txt)|*.txt|Все файлы (*.*)|*.*";
            if(saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName);
                streamWriter.WriteLine(textBox1.Text);
                streamWriter.Close();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void saveResultAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.Filter = "Текстовый документ(*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (saveFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog2.FileName);
                streamWriter.WriteLine(textBox2.Text);
                streamWriter.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName);
                streamWriter.WriteLine(textBox1.Text);
                streamWriter.Close();
            }
            catch(System.ArgumentException)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            try
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog2.FileName);
                streamWriter.WriteLine(textBox2.Text);
                streamWriter.Close();
            }
            catch(System.ArgumentException)
            {
                saveResultAsToolStripMenuItem_Click(sender, e);
            }
        }
    }
}