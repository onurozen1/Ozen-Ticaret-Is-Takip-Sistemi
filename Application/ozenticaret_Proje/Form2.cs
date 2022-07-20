using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ozenticaret_Proje
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frarac = new Form1();
            frarac.Show();
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 frbakım = new Form3();
            frbakım.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 frgayrimenkul = new Form5();
            frgayrimenkul.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 frmkira = new Form4();
            frmkira.Show();
        }
    }
}
