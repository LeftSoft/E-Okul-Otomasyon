using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace E_Okul_Otomasyon
{
    public partial class Ogrenci : Form
    {
        public Ogrenci()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgln = new SqlBaglantisi();
        OleDbDataReader oku;
        OleDbCommand komut;
        double t = 0;
        int kreditoplam = 0;
        int k = 0;
        void DersKontrol()
        {
            int a = Convert.ToInt32(label1.Text);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Tbl_OgretmenNot.*, Tbl_Dersler.DersKredi FROM Tbl_Dersler INNER JOIN Tbl_OgretmenNot ON Tbl_Dersler.Ders_No = Tbl_OgretmenNot.Ders_No where Tbl_OgretmenNot.Ogrenci_No=" + a, bgln.sqlbaglan());
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < comboBox4.Items.Count; i++)
			{
                if (Convert.ToInt32(ds.Tables[0].Rows[i][9]) != 0)
                {
                   
                    t += Convert.ToDouble(ds.Tables[0].Rows[i][9]);
                    kreditoplam += Convert.ToInt32(ds.Tables[0].Rows[i][10]);
                    lblortalamadeger.Text = (t * Convert.ToInt32(ds.Tables[0].Rows[i][10])).ToString();
                    k += Convert.ToInt32(lblortalamadeger.Text);
                 
                    t = 0;
                
                }
                
			}
            lblortalamadeger.Text = (Convert.ToInt32(k) / kreditoplam).ToString();
           
            
            if (Convert.ToDouble(lblortalamadeger.Text) >= 50)
            {
                lblortalamadeger.ForeColor = Color.Green;
            }
            else
            {
                lblortalamadeger.ForeColor = Color.Red;
            }

        }
        void ekle()
        {
            int a = Convert.ToInt32(label1.Text);
             komut = new OleDbCommand("Select * From Tbl_OgretmenNot where Ogrenci_No=" + a, bgln.sqlbaglan());
             oku = komut.ExecuteReader();
            while (oku.Read())
            {



                ListViewItem ekle = new ListViewItem();
                
                ekle.Text= oku["Ders_No"].ToString();
                ekle.SubItems.Add(oku["Yazili_1"].ToString());
                ekle.SubItems.Add(oku["Yazili_2"].ToString());
                ekle.SubItems.Add(oku["Perf_1"].ToString());
                ekle.SubItems.Add(oku["Perf_2"].ToString());
                ekle.SubItems.Add(oku["Ortalama"].ToString());

                listView1.Items.Add(ekle);
                
            }
            bgln.sqlbaglan().Close();
        }

        void devamsizlikekle()
        {
            int a = Convert.ToInt32(label1.Text);
            OleDbCommand komut = new OleDbCommand("Select * From Tbl_OgrenciDevamsizlik where Ogrnc_no=" + a, bgln.sqlbaglan());
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["devamsizlik_tur"].ToString();
                ekle.SubItems.Add(oku["devamsizlik"].ToString());
                

                listView2.Items.Add(ekle);
            }
            bgln.sqlbaglan().Close();
        }
    /// <summary>
    ///  DERS GÜNCELLEEMEEMEMEMEM
    /// </summary>
       
        void dersguncelleme()
        {
            OleDbCommand cmd = new OleDbCommand("Select DersKredi From Tbl_Dersler", bgln.sqlbaglan());
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               comboBox4.Items.Add(dr[0].ToString());
            }
        }

      

        private void Ogrenci_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = eokul.tckimlik;
                int a = Convert.ToInt32(label1.Text);

                ekle();
                devamsizlikekle();
                // Tablo Verisini Çekme // 
                OleDbCommand komut = new OleDbCommand("Select * from Tbl_Ogrenciler where Ogrenci_No=" + a, bgln.sqlbaglan());
                OleDbCommand komut2 = new OleDbCommand("Select * from Tbl_Ogrenci_Sinif_İliski where Ogrenci_No=" + a, bgln.sqlbaglan());
                OleDbDataReader dr = komut.ExecuteReader();
                OleDbDataReader dr2 = komut2.ExecuteReader();
                // Komutu Okuma İşlemi //
                if (dr.Read())
                {
                    // KolonAdı //
                    txtadsoyad.Text = dr["Ogrenci_Ad"] + " " + dr["Ogrenci_Soyad"].ToString();

                }
                if (dr2.Read())
                {
                    txtsube.Text = dr2["Sinif_Adi"].ToString();
                }
                bgln.sqlbaglan().Close();
                dersguncelleme();
                comboBox4.SelectedIndex = 0;
                DersKontrol();
            }
            catch(Exception)
            {

            }
        }

        private void Ogrenci_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            eokul eokul = new eokul();
            eokul.Show();
            this.Hide();
        }
    }
}
