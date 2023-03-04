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

namespace triggerUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-B2AV4A5\\SQLDEVELOPER;Initial Catalog=TriggerUygulama;Integrated Security=True");

        void LİSTELE()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from KITAPLAR", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void KİTAPADEDİ()
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select * from SAYAC", baglanti);
            SqlDataReader rd = kmt.ExecuteReader();
            while (rd.Read())
            {
                lblAdet.Text = rd[0].ToString();
            }
            baglanti.Close();
        }
        void Temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtYazar.Text = "";
            txtSayfa.Text = "";
            txtYayinevi.Text = "";
            TxtTur.Text = "";
            txtAd.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LİSTELE();
            KİTAPADEDİ();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("insert into KITAPLAR(AD,YAZAR,SAYFA,YAYINEVI,TUR) values(@e1,@e2,@e3,@e4,@e5)", baglanti);
            kmt1.Parameters.AddWithValue("@e1", txtAd.Text);
            kmt1.Parameters.AddWithValue("@e2", txtYazar.Text);
            kmt1.Parameters.AddWithValue("@e3", txtSayfa.Text);
            kmt1.Parameters.AddWithValue("@e4", txtYayinevi.Text);
            kmt1.Parameters.AddWithValue("@e5", TxtTur.Text);
            kmt1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap sisteme kaydedildi");
            LİSTELE();
            KİTAPADEDİ();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtYayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtTur.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            DialogResult dialogResult= MessageBox.Show("Kitabı silmek istediğinize emin misiniz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand kmt = new SqlCommand("delete from KITAPLAR where ID=" + txtId.Text,baglanti);
                kmt.ExecuteNonQuery();
                MessageBox.Show("Kitap silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            baglanti.Close();
            LİSTELE();
            KİTAPADEDİ();

        }

        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("update KITAPLAR set AD=@G1,YAZAR=@G2,SAYFA=@G3,YAYINEVI=@G4,TUR=@G5 where ID=@G6", baglanti);
            kmt.Parameters.AddWithValue("@g1", txtAd.Text);
            kmt.Parameters.AddWithValue("@g2", txtYazar.Text);
            kmt.Parameters.AddWithValue("@g3", txtSayfa.Text);
            kmt.Parameters.AddWithValue("@g4", txtYayinevi.Text);
            kmt.Parameters.AddWithValue("@g5", TxtTur.Text);
            kmt.Parameters.AddWithValue("@g6", txtId.Text);
            kmt.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LİSTELE();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
