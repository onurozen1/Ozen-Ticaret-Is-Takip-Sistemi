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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        void temizle()
        {
            maskedTextBox1.Clear();
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();

        BAGLANTI bgl = new BAGLANTI();

        private void Form3_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Araclar where Aracdurum=1", bgl.baglanti());
            da.Fill(dt);
            comboBox1.DisplayMember = "Aracplaka";
            comboBox1.ValueMember = "Aracid";
            comboBox1.DataSource = dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskFull == false || textBox1.Text == "")
            {
                MessageBox.Show("Boş alanları lütfen doldurunuz !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    ds.BakımEkle(byte.Parse(comboBox1.SelectedValue.ToString()), maskedTextBox1.Text, Convert.ToInt32(textBox1.Text));
                    MessageBox.Show("Bakım bilgileri eklendi .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = ds.BakımGetirme(comboBox1.Text);
                    temizle();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen değerleri doğru biçimde yazınız .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        int bakimid = 3000;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bakimid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen silmek istediğiniz bakıma tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (bakimid == 3000)
            {
                MessageBox.Show("Lütfen silmek istediğiniz bakıma tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Bakım silinsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secenek == DialogResult.Yes)
                {
                    ds.BakımSil(short.Parse(bakimid.ToString()));
                    dataGridView1.DataSource = ds.BakımGetirme(comboBox1.Text);
                    temizle();
                }
            }
        }

        private void Txtara_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.BakımGetirme(comboBox1.Text);
            temizle();

            SqlCommand komut = new SqlCommand("Select * From Tbl_Araclar where Aracplaka=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label8.Text = dr[1].ToString();
                label9.Text = dr[2].ToString();
                label10.Text = dr[3].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
