using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MetroFramework.Forms;

namespace Cryptography
{
    public partial class DESForm : MetroForm
    {
        public DESForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "cText.txt";
            MyCryptography.DESEncryptor(richTextBox1.Text, textBox1.Text, textBox2.Text, filename);
            richTextBox2.Text = Convert.ToBase64String( File.ReadAllBytes(filename));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = "cText.txt";
            richTextBox2.Text = MyCryptography.DESDecryptor(textBox1.Text, textBox2.Text, filename);
        }
    }
}
