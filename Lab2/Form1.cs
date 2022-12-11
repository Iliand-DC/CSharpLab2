using System;
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
        private byte[] to_lowercase(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                if (bytes[i] >= 65 && bytes[i] <= 90) 
                {
                    bytes[i] = (byte)(bytes[i] + 32);
                }
            return bytes;
        }
        private bool IsPalindrom(string s)
        {
            for (int i = 0; i < s.Length / 2; i++)
                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }
        private bool IsPalindrom(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length / 2; i++)
                if (bytes[i] != bytes[bytes.Length - i - 1])
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
            string s1;
            textBox2.Clear();
            for(int i=0;i<textBox1.Lines.Count();i++)
            {
                int polinomCount = 0;
                s1 = textBox1.Lines[i];
                string[] s_mas = s1.Split(' ');
                foreach (string str in s_mas)
                    if (IsPalindrom(str.ToLower())&&str!="")
                    {
                        polinomCount++;
                    }
                if (polinomCount > 1)
                {
                    if (textBox2.Text.Length > 0)
                        textBox2.AppendText(Environment.NewLine);
                    textBox2.AppendText(s1);
                }
            }
        }
        private void option2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s1, res = "";
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
                        if (IsPalindrom(to_lowercase(res))&&res!="")
                        {
                            polinomCount++;
                        }
                        res = "";
                    }
                    j++;
                }
                if (polinomCount > 1)
                {
                    if (textBox2.Text.Length > 0)
                        textBox2.AppendText(Environment.NewLine);
                    textBox2.AppendText(s1);
                }
            }
        }
        private void option3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0)
            {
                UnicodeEncoding uniEncoding = new UnicodeEncoding();
                byte[] secondString =
                uniEncoding.GetBytes(Path.GetInvalidPathChars());
                using (MemoryStream memStream =
                new MemoryStream(textBox1.TextLength *
                UnicodeEncoding.CharSize))
                {
                    memStream.Write(uniEncoding.GetBytes(textBox1.Text),
                    0, textBox1.TextLength * UnicodeEncoding.CharSize);
                    textBox2.Clear();
                    byte[] byteArray = new byte[textBox1.TextLength * UnicodeEncoding.CharSize];
                    memStream.Seek(0, SeekOrigin.Begin);
                    StreamReader memrdr = new StreamReader(memStream,
                    Encoding.Unicode);
                    int c1 = -1, c2 = memrdr.Read();
                    int i = 0;
                    while (c2 != -1)
                    {
                        // CR LF: возврат каретки + перевод строки,
                        // символы Юникода 000D + 000A
                        while (c2 != '\r' && c2 != -1)
                        {
                            c1 = c2;
                            c2 = memrdr.Read();
                            byteArray[i] = (byte)c1;
                            textBox2.AppendText(byteArray[i].ToString());
                            i++;
                        }
                        if (c2 == '\r')
                        {
                            c2 = memrdr.Read();
                            c1 = c2;
                            byteArray[i] = (byte)c1;
                            c2 = memrdr.Read();
                        }
                        textBox2.AppendText("\n");
                    }
                    textBox2.Clear();
                    //char[] charArray = new char[byteArray.Length];
                    string charArray = "";
                    for (i = 0; i < byteArray.Length; i++)
                    {
                        if (byteArray[i] != '\0')
                            charArray += ((char)byteArray[i]).ToString();
                        //charArray[i] = (char)byteArray[i];
                    }
                    charArray += ' ';
                    int j = 0;
                    int palindromCount = 0;
                    string tempString = "", result = "";
                    while (j < charArray.Length)
                    {
                        if (charArray[j] != ' ' && charArray[j] != '\n')
                        {
                            tempString += charArray[j];
                        }
                        else if (charArray[j] == '\n')
                        {
                            result += '\n';
                        }
                        else
                        {
                            if (IsPalindrom(to_lowercase(tempString)))
                            {
                                palindromCount++;
                                result += tempString + ' ';
                            }
                            tempString = "";
                        }
                        j++;
                    }
                    if (palindromCount > 1)
                    {
                        if (textBox2.Text.Length > 0)
                            textBox2.AppendText(Environment.NewLine);
                        textBox2.AppendText(result);
                    }

                    ////byte[] byteArray = Encoding.Unicode.GetBytes(textBox1.Text);
                    //int count = 0;
                    //int countPolindrom = 0;
                    //int start = 0;
                    //textBox2.Text += ' ';
                    //byte[] temp = new byte[byteArray.Length];
                    //for (i = 0; i < byteArray.Length; i++)
                    //{
                    //    if (byteArray[i] != 32)
                    //    {
                    //        count++;
                    //        temp[i] = byteArray[i];
                    //    }
                    //    else
                    //    {
                    //        temp[i] = 0;
                    //        //byte[] temp = new byte[count];
                    //        //for (int j = start; j < i; j++) temp[j] = byteArray[j];
                    //        //if(IsPalindrom(to_lowercase(temp)))
                    //        //{
                    //        //    countPolindrom++;
                    //        //    for (int j = start; j < i; j++) result[j] = byteArray[j];
                    //        //}
                    //        //start = i;
                    //    }
                    //}
                    //if(IsPalindrom(to_lowercase(temp)))
                    //textBox2.Clear();
                    //for (i = 0; i < result.Length; i++) textBox2.AppendText(((char)result[i]).ToString());
                    ////textBox2.Text += '\n';
                    ////textBox2.Text += count;
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
                saveFileName = openFileDialog1.FileName;
                textBox2.Clear();
                f_In.Close();
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

        private void save(string saveFileName, TextBox tb)
        {
            StreamWriter sw = new StreamWriter(saveFileName);
            sw.WriteLine(tb.Text);
            sw.Close();
        }
        private void SaveFromTB1(object sender, EventArgs e)
        {
            if (saveFileName != "")
            {
                save(saveFileName, textBox1);
            }
            else if (saveFileDialog1.FileName != "")
            {
                save(saveFileDialog1.FileName, textBox1);
            }
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }
        private void SaveFromTB2(object sender, EventArgs e)
        {
            if (saveFileDialog2.FileName != "")
            {
                save(saveFileDialog2.FileName, textBox2);
            }
            else
                saveResultAsToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFromTB1(sender, e);
            SaveFromTB2(sender, e);
        }
    }
}