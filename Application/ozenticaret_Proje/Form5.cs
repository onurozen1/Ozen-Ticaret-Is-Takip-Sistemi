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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        DataSet1TableAdapters.Tbl_YerlerTableAdapter ds = new DataSet1TableAdapters.Tbl_YerlerTableAdapter();

        BAGLANTI bgl = new BAGLANTI();

        void gayrimenkulsayi()
        {
            SqlCommand komut = new SqlCommand("Select Count(*) From Tbl_Yerler where Yerdurum=1", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label8.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.YerListele();

            gayrimenkulsayi();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ds.YerEkle(textBox1.Text);
                MessageBox.Show("Gayrimenkul başarıyla eklendi .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.YerListele();
                textBox1.Text = "";
                gayrimenkulsayi();
            }
        }
        byte yerid = 255;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                yerid = byte.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen silmek istediğiniz gayrimenkule tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (yerid == 255)
            {
                MessageBox.Show("Lütfen silmek istediğiniz gayrimenkule tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult secenek= MessageBox.Show("Gayrimenkul silinsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secenek == DialogResult.Yes)
                {
                    ds.YerSil(byte.Parse(yerid.ToString()));
                    dataGridView1.DataSource = ds.YerListele();
                    textBox1.Text = "";
                    gayrimenkulsayi();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
