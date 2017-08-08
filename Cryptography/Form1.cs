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
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HillForm hf = new HillForm();
            hf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FeistelForm ff = new FeistelForm();
            ff.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RSAForm rf = new RSAForm();
            rf.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DESForm df = new DESForm();
            df.ShowDialog();
        }
    }
}
