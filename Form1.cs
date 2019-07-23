using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace File_Encoder_Decoder
{
    public partial class Form1 : Form
    {
        string data;
        string[] path = new string[1];

        public Form1()
        {
            InitializeComponent();
            data = "";
            path[0] = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt|SMZ Files|*.smz|HTML Files|*.html|CSS Files|*.css|Javascript Files|*.js";
            ofd.Title = "Open File To Encode/Decode";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                button4.Enabled = true;
                path[0] = ofd.FileName;
                StreamReader sr = new StreamReader(File.OpenRead(path[0]));
                data = sr.ReadToEnd();
                sr.Dispose();
                button1.Enabled = false;
            }
            if (data.Length > 0)
            {
                char[] ch = data.ToArray();
                if (ch[ch.Length - 3] == '0' && ch[ch.Length - 2] == 'x' && ch[ch.Length - 1] == '1')
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter sr = new StreamWriter(File.Create(path[0]));
            char[] ch = data.ToArray();

            for (int i = 0; i < ch.Length; i++)
            {
                int a = ch[i] + 1;
                ch[i] = (char)a;
                sr.Write(ch[i]);
            }
            sr.Write("0x1");

            sr.Dispose();
            MessageBox.Show("You File Has Been Successfully Encoded", "File Encoded",MessageBoxButtons.OK,MessageBoxIcon.Information);
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = true;
            path[0] = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sr = new StreamWriter(File.Create(path[0]));
            char[] ch = data.ToArray();

            for (int i = 0; i < ch.Length-3; i++)
            {
                int a = ch[i] - 1;
                ch[i] = (char)a;
                sr.Write(ch[i]);
            }

            sr.Dispose();
            MessageBox.Show("You File Has Been Successfully Decoded", "File Decoded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = true;
            path[0] = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = true;
            data = "";
            path[0] = "";
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            button4.Enabled = true;
            path = e.Data.GetData(DataFormats.FileDrop) as string[];
            StreamReader sr = new StreamReader(File.OpenRead(path[0]));
            data = sr.ReadToEnd();
            sr.Dispose();
            button1.Enabled = false;
            if (data.Length > 0)
            {
                char[] ch = data.ToArray();
                if (ch[ch.Length - 3] == '0' && ch[ch.Length - 2] == 'x' && ch[ch.Length - 1] == '1')
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
        }
    }
}
