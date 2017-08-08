using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using MetroFramework.Forms;

namespace Cryptography
{
    public partial class RSAForm : MetroForm
    {
        public RSAForm()
        {
            InitializeComponent();
        }
        string RSAQ = "";
        BigInteger[] trueEncoded;
        MyCryptography mcr = new MyCryptography();
        string RSAP = "";
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .,-";
        BigInteger[] keys2;

        private void button1_Click(object sender, EventArgs e)
        {
            RSAQ = textBox2.Text;
            RSAP = textBox1.Text;

            if (richTextBox1.Text.Length != 0)
            {
                BigInteger[] text = mcr.RSAgetLetterNumber(richTextBox1.Text, alphabet);
                if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
                {
                    BigInteger[] keys = mcr.RSAgetKeys(BigInteger.Parse(textBox1.Text), BigInteger.Parse(textBox2.Text));
                    keys2 = mcr.RSAgetKeys(BigInteger.Parse(RSAP), BigInteger.Parse(RSAQ));
                    textBox3.Text = keys[0].ToString();
                    textBox4.Text = keys[1].ToString();
                    textBox5.Text =( Convert.ToInt32(textBox3.Text)* Convert.ToInt32(textBox4.Text)).ToString();
                    trueEncoded = mcr.RSAEncode(keys2[0], keys2[1], text);
                    string result = "";
                    foreach (var letter in trueEncoded)
                        result += alphabet[(int)letter % alphabet.Length].ToString();
                    richTextBox2.Text = result;
                }
                else MessageBox.Show("Заповніть поля простих числел!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (trueEncoded != null)
            {
                trueEncoded = mcr.RSADecode(keys2[2], keys2[1], trueEncoded);
                richTextBox2.Text = mcr.RSAgetLetterFromNumber(trueEncoded, alphabet);
            }
            else MessageBox.Show("Спочатку зашифруйте текст!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
