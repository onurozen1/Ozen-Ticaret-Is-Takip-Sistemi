using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ozenticaret_Proje
{
    public partial class Form8 : Form
    {
        public Form8()
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

        BAGLANTI bgl = new BAGLANTI();
       
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Boş alanları lütfen doldurunuz .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand komut = new SqlCommand("Select * From Tbl_Adminler where Kullanıcıad=@p1 and Sifre=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    Form7 frm = new Form7();
                    frm.isim = textBox1.Text;
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı ! Lütfen tekrar deneyiniz .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
