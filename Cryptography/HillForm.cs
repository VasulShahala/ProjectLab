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
    public partial class HillForm : MetroForm
    {
        public HillForm()
        {
            InitializeComponent();
        }

        MyCryptography mcr = new MyCryptography();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 9)
            {
                try
                {
                    string result = mcr.shifrHilla(textBox1.Text, richTextBox1.Text, textBox2.Text, true);
                    richTextBox2.Text = result;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else MessageBox.Show("Ключ повинен бути довжиною в 9 символів та алфавіт довжиною більше 0!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 9)
            {
                try
                {
                    string result = mcr.shifrHilla(textBox1.Text, richTextBox1.Text, textBox2.Text, false);
                    richTextBox2.Text = result;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else MessageBox.Show("Ключ повинен бути довжиною в 9 символів та алфавіт довжиною більше 0!");
        }
    }
}
