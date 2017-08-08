using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cryptography
{
    public partial class FeistelForm : MetroForm
    {
        public FeistelForm()
        {
            InitializeComponent();
        }

        MyCryptography mcr = new MyCryptography();
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .,;-'";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Text = mcr.feistelNetwork(richTextBox1.Text, alphabet, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), false);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Text = mcr.feistelNetwork(richTextBox1.Text, alphabet, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), true);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
