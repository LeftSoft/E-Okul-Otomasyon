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
    public partial class Mudur : Form
    {
        public Mudur()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgln = new SqlBaglantisi();
        DataTable dt3;
        OleDbDataAdapter da3;
        DataTable dt2;
        OleDbDataAdapter da2;
        void tabloekle2()
        {
            dt2 = new DataTable();
            da2 = new OleDbDataAdapter("Select * From Tbl_Ogrenciler", bgln.sqlbaglan());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void tablotemizle2()
        {
            dt2.Clear();
        }
        void ogrenciders()
        {
            OleDbCommand cmd = new OleDbCommand("Select * From Tbl_Dersler",bgln.sqlbaglan());
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0].ToString());
            }
        }
        private void Mudur_Load(object sender, EventArgs e)
        {
            çek();
            OgrDERSEKLE();
            OgretmenTC();
            OgretmenSUBE();
            tabloekle();
            tabloekle2();
            devamsizlik();
            numaraekle();
            subeekle();
            ogrenciders();
            comboBox4.SelectedIndex = 0;
        }
        void temizle()
        {
            // Tabloyu Temizle //
            dt3.Clear();
        }
        void tablotemizle()
        {
            dt2.Clear();
        }
        void tabloekle()
        {
            dt3 = new DataTable();
            da3 = new OleDbDataAdapter("Select * From Tbl_Ogretmenler", bgln.sqlbaglan());
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tc.Text == "" || sifre.Text == "")
                {
                    MessageBox.Show("Lütfen Alanları Doldurmayı Unutmayın", "Alan Doldur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("insert into Tbl_Ogretmenler(Ogretmen_Tc,Ogretmen_Sifre,Ogretmen_Adi,Ogretmen_SoyAdi) values (@tc,@sifre,@isim,@soyisim)", bgln.sqlbaglan());
                    komut.Parameters.AddWithValue("@tc", tc.Text);
                    komut.Parameters.AddWithValue("@sifre", sifre.Text);
                    komut.Parameters.AddWithValue("@isim", ogrisim.Text);
                    komut.Parameters.AddWithValue("@soyisim", ogrsoyisim.Text);
                    komut.ExecuteReader();
                    MessageBox.Show("Başarıyla Öğretmen Eklendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    temizle();
                    tabloekle();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Sistemde Böyle Bir Öğretmen Zaten Mevcut.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void Mudur_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            sifre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ogrisim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            ogrsoyisim.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
         try { 
            if (tc.Text == "" || sifre.Text == "")
            {
                MessageBox.Show("Lütfen Alanları Doldurmayı Unutmayın", "Alan Doldur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "")
            {
                 MessageBox.Show("Lütfen Bir Alan Seçin", "Alan Seç", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OleDbCommand guncelle = new OleDbCommand("update Tbl_Ogretmenler set Ogretmen_Tc=@p1,Ogretmen_Sifre=@p2,Ogretmen_Adi=@p3,Ogretmen_SoyAdi=@p4 where ID=" + textBox2.Text, bgln.sqlbaglan());

                guncelle.Parameters.AddWithValue("@p1", tc.Text);
                guncelle.Parameters.AddWithValue("@p2", sifre.Text);
                guncelle.Parameters.AddWithValue("@p3", ogrisim.Text);
                guncelle.Parameters.AddWithValue("@p4", ogrsoyisim.Text);
                guncelle.ExecuteReader();
                MessageBox.Show("Bilgiler Güncellendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                tabloekle();
                bgln.sqlbaglan().Close();
            }
                }
            catch(Exception)
            {
               MessageBox.Show("Bir Hata Var","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (tc.Text == "" || sifre.Text == "")
                {
                    MessageBox.Show("Lütfen Alanı Boş Bırakmayın");
                }
                
                else
                {
                    OleDbCommand sil = new OleDbCommand("delete from Tbl_Ogretmenler where ID=@p1", bgln.sqlbaglan());
                    sil.Parameters.AddWithValue("@p1", textBox2.Text);
                    sil.ExecuteReader();
                    MessageBox.Show("Bilgiler Silindi!");
                    temizle();
                    tabloekle();
                    tc.Text = "";
                    textBox2.Text = "";
                    sifre.Text = "";
                    ogrisim.Text = "";
                    ogrsoyisim.Text = "";
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Bir Hata Var", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                     /// BURASI ÖĞRENCİ EKLEME YERİ // 
        void subeekle()
        {
            OleDbCommand cmd2 = new OleDbCommand("Select * from Tbl_Siniflar where Sinif_Adi", bgln.sqlbaglan());
            OleDbDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                // KolonAdı //

                comboBox1.Items.Add(dr2["Sinif_Adi"].ToString());

            }
            bgln.sqlbaglan().Close();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {

            
            OleDbCommand ekle = new OleDbCommand("insert into Tbl_Ogrenci_Sinif_İliski(Ogrenci_No,Sinif_Adi) values (@okulno,@sube)", bgln.sqlbaglan());
            ekle.Parameters.AddWithValue("@okulno", okulno.Text);
            ekle.Parameters.AddWithValue("@sube", comboBox1.Text);
            ekle.ExecuteReader();
           
                }
            catch(Exception)
            {
                MessageBox.Show("Böyle Bir Öğrenci Bulunmakta","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        
        private void button17_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Lütfen Bekleyiniz..");
            for (int i = 0; i < comboBox4.Items.Count; i++)
            {
               
                comboBox4.SelectedIndex = i;
                OleDbCommand ekle = new OleDbCommand("insert into Tbl_OgretmenNot(Ogretmen_TC,Sinif_Adi,Ogrenci_No,Ders_No,Yazili_1,Yazili_2,Perf_1,Perf_2,Ortalama) values (@tc,@sinif,@ogrencino,@dersno,@y1,@y2,@p1,@p2,@ort)", bgln.sqlbaglan());
                ekle.Parameters.AddWithValue("@tc", "");
                ekle.Parameters.AddWithValue("@sinif", comboBox1.Text);
                ekle.Parameters.AddWithValue("@ogrencino", okulno.Text);
                ekle.Parameters.AddWithValue("@dersno", comboBox4.Text);
                ekle.Parameters.AddWithValue("@y1", "");
                ekle.Parameters.AddWithValue("@y2", "");
                ekle.Parameters.AddWithValue("@p1", "");
                ekle.Parameters.AddWithValue("@p2", "");
                ekle.Parameters.AddWithValue("@ort", 0);
                ekle.ExecuteReader();
            }
            
            MessageBox.Show("Başarıyla Öğrenci Eklendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tablotemizle();
            tabloekle2();
        }
        private void btnekle_Click(object sender, EventArgs e)
        {
            try
           {

                
                if (tckimlik.Text == "" || isim.Text == "" || okulno.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Alanları Doldurmayı Unutmayın", "Alan Doldur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (tckimlik.Text.Length < 11)
                {
                    MessageBox.Show("TC KİMLİK 11 Haneli Olmalıdır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               

                else
                {

                    OleDbCommand ekle = new OleDbCommand("insert into Tbl_Ogrenciler(Ogrenci_No,Sinif_Adi,Ogrenci_Tc,Ogrenci_Ad,Ogrenci_SoyAd) values (@okulno,@sube,@tc,@isim,@soyisim)", bgln.sqlbaglan());
                    ekle.Parameters.AddWithValue("@okulno", okulno.Text);
                    ekle.Parameters.AddWithValue("@sube", comboBox1.Text);
                    ekle.Parameters.AddWithValue("@tc", tckimlik.Text);
                    ekle.Parameters.AddWithValue("@isim", isim.Text);
                    ekle.Parameters.AddWithValue("@soyisim", soyisim.Text);
                    ekle.ExecuteReader();
                    
                    
                    
                    
                    /// İKİNCİ TIKLAMA İŞLEMİ
                    button4_Click(sender, e);
                    button17_Click(sender, e);
                    listBox1.Items.Clear();
                    numaraekle();

                   
                }
           }
           catch(Exception)
           {
               MessageBox.Show("Bu Numara İçerisinde Başka Bir\nÖğrenci Zaten Kayıtlı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
      }
        private void button8_Click(object sender, EventArgs e)
        {
            OleDbCommand guncelle = new OleDbCommand("update Tbl_Ogrenci_Sinif_İliski set Ogrenci_No=@p1,Sinif_Adi=@p2 where Ogrenci_No=" + okulno.Text, bgln.sqlbaglan());

            guncelle.Parameters.AddWithValue("@p1", okulno.Text);
            guncelle.Parameters.AddWithValue("@p2", comboBox1.Text);
            
            guncelle.ExecuteReader();
           
            tablotemizle2();
            tabloekle2();
            bgln.sqlbaglan().Close();
        }
        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (tckimlik.Text == "" || isim.Text == "" || okulno.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Alanları Doldurmayı Unutmayın", "Alan Doldur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (tckimlik.Text.Length < 11)
                {
                    MessageBox.Show("TC KİMLİK 11 Haneli Olmalıdır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
                else
                {
                    OleDbCommand guncelle = new OleDbCommand("update Tbl_Ogrenciler set Ogrenci_No=@p1,Ogrenci_Tc=@p2,Ogrenci_Ad=@p3,Ogrenci_SoyAd=@p4 where Ogrenci_No=" + okulno.Text, bgln.sqlbaglan());

                    guncelle.Parameters.AddWithValue("@p1", okulno.Text);
                    guncelle.Parameters.AddWithValue("@p2", tckimlik.Text);
                    guncelle.Parameters.AddWithValue("@p3", isim.Text);
                    guncelle.Parameters.AddWithValue("@p4", soyisim.Text);
                    guncelle.ExecuteReader();
                    MessageBox.Show("Bilgiler Güncellendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tablotemizle2();
                    tabloekle2();
                    /// İKİNCİ TIKLAMA İŞLEMİ
                    button8_Click(sender, e);
                    listBox1.Items.Clear();
                    numaraekle();
                    bgln.sqlbaglan().Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Sayı Girilecek Yerlere Harf veya Sembol Girmeyiniz.");
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            OleDbCommand sil = new OleDbCommand("delete from Tbl_Ogrenci_Sinif_İliski where Ogrenci_No=@p1", bgln.sqlbaglan());
            sil.Parameters.AddWithValue("@p1", okulno.Text);
            sil.ExecuteReader();
            tablotemizle2();
            tabloekle2();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            OleDbCommand sil = new OleDbCommand("delete from Tbl_OgretmenNot where Ogrenci_No=@p1", bgln.sqlbaglan());
            sil.Parameters.AddWithValue("@p1", okulno.Text);
            sil.ExecuteReader();
            tablotemizle2();
            tabloekle2();
        }
        private void btnsil_Click(object sender, EventArgs e)
        {
            OleDbCommand sil = new OleDbCommand("delete from Tbl_Ogrenciler where Ogrenci_No=@p1", bgln.sqlbaglan());
            sil.Parameters.AddWithValue("@p1", okulno.Text);
            sil.ExecuteReader();
            MessageBox.Show("Bilgiler Silindi!");
            tablotemizle2();
            tabloekle2();
            // İKİNCİ TIKLAMA İŞLEMİ
            button9_Click(sender, e);
            button18_Click(sender, e);
            tckimlik.Text = "";
            okulno.Text = "";
            isim.Text = "";
            soyisim.Text = "";
            comboBox1.Items.Clear();
            subeekle();
            listBox1.Items.Clear();
            numaraekle();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            okulno.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            tckimlik.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            isim.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            soyisim.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            OleDbCommand cmd2 = new OleDbCommand("Select * from Tbl_Ogrenci_Sinif_İliski where Ogrenci_No", bgln.sqlbaglan());
            OleDbDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                // KolonAdı //
                if (okulno.Text == dr2["Ogrenci_No"].ToString())
                {
                    okulno.Text = dr2["Ogrenci_No"].ToString();
                    comboBox1.Text = dr2["Sinif_Adi"].ToString();
                }


            }

            bgln.sqlbaglan().Close();
            
        }
       
       

       
        // ÖĞRENCİ DEVAMSIZLIK İŞLEMLERİ
        DataTable dt4;
        OleDbDataAdapter da4;
        void devamsizliktablotemizle()
        {
            dt4.Clear();
        }
        void devamsizlik()
        {
            dt4 = new DataTable();
            da4 = new OleDbDataAdapter("Select * From Tbl_OgrenciDevamsizlik", bgln.sqlbaglan());
            da4.Fill(dt4);
            dataGridView3.DataSource = dt4;
            this.dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        
        void numaraekle()
        {
            OleDbCommand cmd2 = new OleDbCommand("Select * from Tbl_Ogrenciler where Ogrenci_No", bgln.sqlbaglan());
            OleDbDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                // KolonAdı //
                listBox1.Items.Add(dr2["Ogrenci_No"].ToString());
            }
            
            bgln.sqlbaglan().Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || CmbDevamsizlik.Text == "")
                {
                    MessageBox.Show("Lütfen alanları doldurduğunuza emin olun", "Bilgi");
                }
                else if (textBox1.Text != "" || CmbDevamsizlik.Text != "")
                {
                    OleDbCommand komut = new OleDbCommand("insert into Tbl_OgrenciDevamsizlik(ogrnc_no,devamsizlik_tur,devamsizlik) values (@p1,@p2,@p3)", bgln.sqlbaglan());
                    komut.Parameters.AddWithValue("@p1", textBox1.Text);
                    komut.Parameters.AddWithValue("@p2", CmbDevamsizlik.Text);
                    komut.Parameters.AddWithValue("@p3", dtZaman.Text);
                    komut.ExecuteReader();
                    MessageBox.Show("Başarıyla Ders Eklendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bgln.sqlbaglan().Close();
                    devamsizliktablotemizle();
                    devamsizlik();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Öğrenciye 1 Günde Aynı Anda Devamsızlık Veremezsiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void TxtOgrenciAraD_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Tbl_OgrenciDevamsizlik where ogrnc_no like '%" + TxtOgrenciAraD.Text + "%'", bgln.sqlbaglan());
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bgln.sqlbaglan().Close();
           
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand guncelle = new OleDbCommand("update Tbl_OgrenciDevamsizlik set ogrnc_no=@p1,devamsizlik_tur=@p2,devamsizlik=@p3 where ID=" + txtidd.Text, bgln.sqlbaglan());
                if (textBox1.Text == "" || CmbDevamsizlik.Text == "")
                {
                    MessageBox.Show("Lütfen alanları doldurduğunuza emin olun", "Bilgi");
                }
                else if (textBox1.Text != "" || CmbDevamsizlik.Text != "")
                {
                    guncelle.Parameters.AddWithValue("@p1", textBox1.Text);
                    guncelle.Parameters.AddWithValue("@p2", CmbDevamsizlik.Text);
                    guncelle.Parameters.AddWithValue("@p3", dtZaman.Text);
                    guncelle.ExecuteReader();
                    MessageBox.Show("Devamsızlık Güncellendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    devamsizliktablotemizle();
                    devamsizlik();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Oops bir şeyler eksik gitti", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("delete from Tbl_OgrenciDevamsizlik where ID=@p1", bgln.sqlbaglan());
            komut.Parameters.AddWithValue("@p1", txtdsil.Text);
            OleDbDataReader dr = komut.ExecuteReader();
            bgln.sqlbaglan().Close();
            MessageBox.Show("Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            devamsizliktablotemizle();
            devamsizlik();
            textBox1.Text = "";
            txtdsil.Text = "";
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtidd.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            txtdsil.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            CmbDevamsizlik.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
            dtZaman.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
        }
        OleDbCommand Olecm;
        OleDbDataReader reader;
        OleDbDataAdapter data;
        DataSet dsss;
        void çek()
        {
            data = new OleDbDataAdapter("SELECT Tbl_Ogretmenler.Ogretmen_Tc,Tbl_Ogretmenler.Ogretmen_Adi, Tbl_Ogretmenler.Ogretmen_SoyAdi FROM Tbl_Ogretmenler", bgln.sqlbaglan());
            dsss = new DataSet();
            data.Fill(dsss);
           
        }
        void OgretmenTC()
        {
            Olecm = new OleDbCommand("Select * From Tbl_Ogretmenler where Ogretmen_Adi + ' ' + Ogretmen_Soyadi", bgln.sqlbaglan());
            reader = Olecm.ExecuteReader();

             while (reader.Read())
            {
                // KolonAdı //

                CmbTC.Items.Add(reader[3] + " " + reader[4].ToString());
                

            }
            bgln.sqlbaglan().Close();

        }
        void OgretmenSUBE()
        {
            OleDbCommand cmd2 = new OleDbCommand("Select * from Tbl_Siniflar where Sinif_Adi", bgln.sqlbaglan());
            OleDbDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                // KolonAdı //

                CmbSube.Items.Add(dr2["Sinif_Adi"].ToString());
                comboBox3.Items.Add(dr2["Sinif_Adi"].ToString());
                comboBox2.Items.Add(dr2["Sinif_Adi"].ToString());

            }
            bgln.sqlbaglan().Close();

        }
        DataTable dt6;
        OleDbDataAdapter da6;
        void OgrDERSTEMİZLE()
        {
            dt6.Clear();
        }
        void OgrDERSEKLE()
        {
            dt6 = new DataTable();
            da6 = new OleDbDataAdapter("Select * From Tbl_Ogretmen_Sinif_Ders_İliski", bgln.sqlbaglan());
            da6.Fill(dt6);
            dataGridView4.DataSource = dt6;
            this.dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void button10_Click(object sender, EventArgs e)
        {
         // try
          // {
                if (CmbTC.Text == "" || CmbSube.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Lütfen alanları doldurduğunuza emin olun", "Bilgi");
                }
                else if (CmbTC.Text != "" && CmbSube.Text != "" && textBox3.Text != "")
                {
                    OleDbCommand komut = new OleDbCommand("insert into Tbl_Ogretmen_Sinif_Ders_İliski(Ogretmen_Tc,Sinif_Adi,Ders_No) values (@p1,@p2,@p3)", bgln.sqlbaglan());
                    komut.Parameters.AddWithValue("@p1", label23.Text);
                    komut.Parameters.AddWithValue("@p2", CmbSube.Text);
                    komut.Parameters.AddWithValue("@p3", textBox3.Text);
                    komut.ExecuteReader();
                    MessageBox.Show("Başarıyla Eklendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OgrDERSTEMİZLE();
                    OgrDERSEKLE();
                    bgln.sqlbaglan().Close();
                   
                }
           //}
            //catch (Exception)
           // {
            //    MessageBox.Show("HATAAAAA", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand guncelle = new OleDbCommand("update Tbl_Ogretmen_Sinif_Ders_İliski set Ogretmen_Tc=@p1,Sinif_Adi=@p2,Ders_No=@p3 where Kimlik=" + textBox4.Text, bgln.sqlbaglan());
                if (CmbTC.Text == "" || CmbSube.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Lütfen alanları doldurduğunuza emin olun", "Bilgi");
                }
                else if (CmbTC.Text != "" && CmbSube.Text != "" && textBox3.Text != "")
                {
                    guncelle.Parameters.AddWithValue("@p1", label23.Text);
                    guncelle.Parameters.AddWithValue("@p2", CmbSube.Text);
                    guncelle.Parameters.AddWithValue("@p3", textBox3.Text);
                    guncelle.ExecuteReader();
                    MessageBox.Show("Bilgiler Güncellendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OgrDERSTEMİZLE();
                    OgrDERSEKLE();
                    
                  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Oops bir şeyler eksik gitti", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("delete from Tbl_Ogretmen_Sinif_Ders_İliski where Kimlik=@p1", bgln.sqlbaglan());
            komut.Parameters.AddWithValue("@p1", textBox4.Text);
            OleDbDataReader dr = komut.ExecuteReader();
            bgln.sqlbaglan().Close();
            MessageBox.Show("Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox4.Text = "";
            CmbSube.Items.Clear();
            comboBox3.Items.Clear();
            comboBox2.Items.Clear();
            OgretmenSUBE();
            textBox3.Text = "";
            OgrDERSTEMİZLE();
            OgrDERSEKLE();
            
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            label23.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
          
            OleDbCommand cmd = new OleDbCommand("SELECT Tbl_Ogretmen_Sinif_Ders_İliski.Ogretmen_Tc, Tbl_Ogretmenler.Ogretmen_Adi, Tbl_Ogretmenler.Ogretmen_SoyAdi FROM Tbl_Ogretmenler INNER JOIN Tbl_Ogretmen_Sinif_Ders_İliski ON Tbl_Ogretmenler.Ogretmen_Tc = Tbl_Ogretmen_Sinif_Ders_İliski.Ogretmen_Tc WHERE Tbl_Ogretmen_Sinif_Ders_İliski.Ogretmen_Tc='"+label23.Text+"'",bgln.sqlbaglan());
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (label23.Text == dr[0].ToString())
                {
                    CmbTC.SelectedItem = dr[1] + " " + dr[2].ToString();
                }
                
            }



            /*
            for (int i = 0; i < CmbTC.Items.Count; i++)
            {
                if (label23.Text == dsss.Tables[0].Rows[i][1].ToString() + " " + dsss.Tables[0].Rows[i][2].ToString())
                {
                    CmbTC.SelectedValue = reader[3] + " " + reader[4].ToString();
                   
                }
            }
            */
            CmbSube.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
            
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            eokul eokul = new eokul();
            eokul.Show();
            this.Hide();
        }

       
        

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    OleDbCommand komut = new OleDbCommand("insert into Tbl_Siniflar(Sinif_Adi) values (@p1)", bgln.sqlbaglan());
                    komut.Parameters.AddWithValue("@p1", textBox5.Text);
                    komut.ExecuteReader();
                    MessageBox.Show("Başarıyla Eklendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CmbSube.Items.Clear();
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    OgretmenSUBE(); 
                }
                else
                {
                    MessageBox.Show("Lütfen Alanı Boş Geçmeyiniz.");
                }
                
               
            }
            catch(Exception)
            {
                MessageBox.Show("Böyle Bir Sınıf Zaten Mevcut","HATA",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
            

                OleDbCommand guncelle = new OleDbCommand("update Tbl_Siniflar set Sinif_Adi=@p1 where ID=" + label21.Text, bgln.sqlbaglan());
                guncelle.Parameters.AddWithValue("@p1", textBox6.Text);
                guncelle.ExecuteReader();
                MessageBox.Show("Bilgiler Güncellendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CmbSube.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                OgretmenSUBE();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lütfen Alanı Doldurmayı Unutmayın "+ex);
         }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Tbl_Siniflar", bgln.sqlbaglan());
            DataSet ds = new DataSet();
            da.Fill(ds);
           
                label21.Text = ds.Tables[0].Rows[comboBox3.Items.IndexOf(comboBox3.Text)][0].ToString(); 
            
            

           
        }

        private void button15_Click(object sender, EventArgs e)
        {

            OleDbCommand sil = new OleDbCommand("delete from Tbl_Siniflar where ID=@p1", bgln.sqlbaglan());
            sil.Parameters.AddWithValue("@p1", label22.Text);
            sil.ExecuteReader();
            MessageBox.Show("Bilgiler Silindi!");
            CmbSube.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            OgretmenSUBE();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Tbl_Siniflar", bgln.sqlbaglan());
            DataSet ds = new DataSet();
            da.Fill(ds);

            label22.Text = ds.Tables[0].Rows[comboBox2.Items.IndexOf(comboBox2.Text)][0].ToString(); 
        }
        
        private void CmbTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            for (int i = 0; i < CmbTC.Items.Count; i++)
            {
                if (CmbTC.Text == dsss.Tables[0].Rows[i][1].ToString() +" "+ dsss.Tables[0].Rows[i][2].ToString())
                {
                   label23.Text = dsss.Tables[0].Rows[i][0].ToString();
                }
            }
           
        }

        
    }
}
