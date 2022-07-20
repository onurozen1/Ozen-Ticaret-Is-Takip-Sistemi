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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        void temizle()
        {
            maskedTextBox1.Clear();
            textBox1.Clear();
        }

        void para()
        {
            SqlCommand komut = new SqlCommand("select sum(tbl_kiralar.kirapara) from tbl_kiralar inner join Tbl_Yerler on Tbl_Kiralar.Yer=Tbl_Yerler.Yerid where Tbl_Yerler.Yerdurum=1", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label8.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
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

        DataSet1TableAdapters.DataTable2TableAdapter ds = new DataSet1TableAdapters.DataTable2TableAdapter();
            
        private void Form4_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Yerler where Yerdurum=1", bgl.baglanti());
            da.Fill(dt);
            comboBox1.DisplayMember = "Yerad";
            comboBox1.ValueMember = "Yerid";
            comboBox1.DataSource = dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.KiraGetirme(comboBox1.Text);
            temizle();
            para();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || maskedTextBox1.MaskFull == false) 
            {
                MessageBox.Show("Boş alanları lütfen doldurunuz !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    ds.KiraEkle(byte.Parse(comboBox1.SelectedValue.ToString()), Convert.ToInt32(textBox1.Text), maskedTextBox1.Text);
                    MessageBox.Show("Kira bilgileri eklendi .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = ds.KiraGetirme(comboBox1.Text);
                    temizle();
                    para();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen ödenmiş tutarı doğru biçimde yazınız .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        int kiraid = 3000;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                kiraid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen silmek istediğiniz kiraya tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (kiraid == 3000)
            {
                MessageBox.Show("Lütfen silmek istediğiniz kiraya tıklayınız .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Kirayı silinsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secenek == DialogResult.Yes)
                {
                    ds.KiraSil(short.Parse(kiraid.ToString()));
                    dataGridView1.DataSource = ds.KiraGetirme(comboBox1.Text);
                    temizle();
                    para();
                }
            }
        }
    }
}
