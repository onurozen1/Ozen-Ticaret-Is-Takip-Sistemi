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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet1TableAdapters.Tbl_AraclarTableAdapter ds = new DataSet1TableAdapters.Tbl_AraclarTableAdapter();

        void temizle()
        {
            Txtmarka.Clear();
            Txtmodel.Clear();
            Txtyıl.Clear();
            Txtplaka.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        BAGLANTI bgl = new BAGLANTI();
        
        void istatistik()
        {
            SqlCommand komut = new SqlCommand("Select Count(*) From Tbl_Araclar where Aracdurum=1", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label6.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut2 = new SqlCommand("Select Max(Aracyıl) From Tbl_Araclar where Aracdurum=1", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                label9.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut3 = new SqlCommand("Select Min(Aracyıl) From Tbl_Araclar where Aracdurum=1", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                label10.Text = dr3[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.AracListele();

            istatistik();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (Txtmarka.Text == "" || Txtmodel.Text == "" || Txtyıl.Text == "" || Txtplaka.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    ds.AracEkle(Txtmarka.Text, Txtmodel.Text, short.Parse(Txtyıl.Text), Txtplaka.Text);
                    MessageBox.Show("Araç başarıyla eklendi .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = ds.AracListele();
                    temizle();
                    istatistik();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen değerleri doğru biçimde giriniz .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        byte aracid = 255;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                aracid = byte.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                Txtmarka.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Txtmodel.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                Txtyıl.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                Txtplaka.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen işlem yapmak istediğiniz araca tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (aracid == 255)  
            {
                MessageBox.Show("Lütfen silmek istediğiniz araca tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Araç silinsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secenek == DialogResult.Yes)
                {
                    ds.AracSil(Convert.ToByte(aracid));
                    dataGridView1.DataSource = ds.AracListele();
                    temizle();
                    istatistik();
                }
            }
        }

        private void Txtmarka_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Txtara_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.AracGetime(Txtara.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
