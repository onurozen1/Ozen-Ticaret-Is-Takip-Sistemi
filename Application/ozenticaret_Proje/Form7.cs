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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 25;
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                Form2 fr = new Form2();
                fr.Show();
                this.Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public string isim;
        private void Form7_Load(object sender, EventArgs e)
        {
            label3.Text = isim;
        }
    }
}
