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
        public partial class Ogretmen : Form
        {
            public Ogretmen()
            {
                InitializeComponent();
            }
            SqlBaglantisi bgln = new SqlBaglantisi();
            private bool boslukkontrol(string a)
            {
                if (a == "")
                    return (false);
                else
                    return (true);
            }
            private double ortalama(string s1, string s2, string p1, string p2)
            {
                double sayi, top = 0, s = 0;
                if (boslukkontrol(s1) == true)
                {
                    sayi = Convert.ToDouble(s1);
                    top = top + sayi;
                    s++;
                }
                if (boslukkontrol(s2) == true)
                {
                    sayi = Convert.ToDouble(s2);
                    top = top + sayi;
                    s++;
                }
                if (boslukkontrol(p1) == true)
                {
                    sayi = Convert.ToDouble(p1);
                    top = top + sayi;
                    s++;
                }
                if (boslukkontrol(p2) == true)
                {
                    sayi = Convert.ToDouble(p2);
                    top = top + sayi;
                    s++;
                }
                return (top / s);
            }
           
    
            DataTable dt;
            OleDbDataAdapter da;
            void temizle()
            {
                // Tabloyu Temizle //
                dt.Clear();
            }
            void tabloekle()
            {
                string b = eokul.ogretmentc;
                long a = Convert.ToInt64(b);
               // long a = Convert.ToInt64(b);
                dt = new DataTable();
                //2//da = new OleDbDataAdapter("SELECT Tbl_OgretmenNot.Ogrenci_No, Tbl_OgretmenNot.Sinif_Adi, Tbl_Ogrenciler.Ogrenci_Ad, Tbl_Ogrenciler.Ogrenci_SoyAd, Tbl_OgretmenNot.Ders_No, Tbl_OgretmenNot.Yazili_1, Tbl_OgretmenNot.Yazili_2, Tbl_OgretmenNot.Perf_1, Tbl_OgretmenNot.Perf_2, Tbl_OgretmenNot.Ortalama FROM Tbl_Ogrenciler INNER JOIN Tbl_OgretmenNot ON Tbl_Ogrenciler.Ogrenci_No = Tbl_OgretmenNot.Ogrenci_No WHERE Tbl_OgretmenNot.Sinif_Adi='" + comboBox3.Text + "'", bgln.sqlbaglan());
                // da = new OleDbDataAdapter("Select Not_id,Sinif_Adi,Ogrenci_No,Ders_No,Yazili_1,Yazili_2,Perf_1,Perf_2,Ortalama from Tbl_OgretmenNot where Ogretmen_TC='"+b+"'"+" and Sinif_Adi='" + comboBox3.Text + "'", bgln.sqlbaglan());
                da = new OleDbDataAdapter("SELECT Tbl_OgretmenNot.Not_id,Tbl_OgretmenNot.Ogrenci_No, Tbl_OgretmenNot.Sinif_Adi, Tbl_Ogrenciler.Ogrenci_Ad, Tbl_Ogrenciler.Ogrenci_SoyAd, Tbl_OgretmenNot.Ders_No, Tbl_OgretmenNot.Yazili_1, Tbl_OgretmenNot.Yazili_2, Tbl_OgretmenNot.Perf_1, Tbl_OgretmenNot.Perf_2, Tbl_OgretmenNot.Ortalama FROM Tbl_Ogrenciler INNER JOIN Tbl_OgretmenNot ON Tbl_Ogrenciler.Ogrenci_No = Tbl_OgretmenNot.Ogrenci_No WHERE Tbl_OgretmenNot.Sinif_Adi='" + comboBox3.Text + "'" + " and Tbl_OgretmenNot.Ders_No='" + comboBox1.Text + "'", bgln.sqlbaglan());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            }
            private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
            {

                 string b = eokul.ogretmentc;
            
                long a = Convert.ToInt64(b);
                OleDbCommand cmd2 = new OleDbCommand("Select * from Tbl_Ogretmen_Sinif_Ders_İliski where Ogretmen_Tc ='" + b + "'" + " and Sinif_Adi='" + comboBox3.Text + "'", bgln.sqlbaglan());
                OleDbDataReader dr2 = cmd2.ExecuteReader();
                
                
                OleDbCommand cmd = new OleDbCommand("Select * from Tbl_Ogrenci_Sinif_İliski where Sinif_Adi='" + comboBox3.Text + "'", bgln.sqlbaglan());
                OleDbDataReader dr = cmd.ExecuteReader(); 
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                yazili1.Text = "";
                yazili2.Text = "";
                sozlu1.Text = "";
                sozlu2.Text = "";
                lblort.Text = "00";
                txtogrncno.Text = "";
                txtnotid.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                
                while (dr.Read())
                {

                    // KolonAdı //
                    comboBox2.Items.Add(dr["Ogrenci_No"].ToString());
                    if (dr2.Read())
                    {
                        comboBox1.Items.Add(dr2["Ders_No"].ToString());
                    }

                }
                
                try
                {
                    comboBox1.SelectedIndex = 0;
                    tabloekle();
                }
                catch (Exception)
                {
                    MessageBox.Show("Bu Sınıfta Öğrenci Bulunmamakta.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    try
                    {
                        temizle();
                        tabloekle();
                    }
                    catch(Exception)
                    {
                        
                    }
                }
              
                 
                
                
                
            }


            string b = eokul.ogretmentc;
        void SINIFEKLE()
            {
                OleDbCommand cmd = new OleDbCommand("Select * from Tbl_Ogretmen_Sinif_Ders_İliski where Ogretmen_Tc='" + b + "'", bgln.sqlbaglan());
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // KolonAdı //
                    comboBox3.Items.Add(dr["Sinif_Adi"].ToString());

                }
                bgln.sqlbaglan().Close();
            }
            
            private void Ogretmen_Load(object sender, EventArgs e)
            {

                
               
          
            
                // Datagriddeki Tabloyu ve Combobox'daki verileri yansıt // 
                // --> Şuan Eklenmedi
                SINIFEKLE();
                // Combobox a ders adı ekleme //
                // --> Şuan Eklenmedi
             
                // Devamsizlik için datagrid
               // devamsizlik();
            }
            private void Ogretmen_FormClosing(object sender, FormClosingEventArgs e)
            {
                Application.Exit();
            }
            
            private void button2_Click(object sender, EventArgs e)
            {

                OleDbCommand guncelle = new OleDbCommand("update Tbl_OgretmenNot set Ogrenci_No=@p1,Ders_No=@p2,Yazili_1=@p3,Yazili_2=@p4,Perf_1=@p5,Perf_2=@p6,Ogretmen_TC=@8,ortalama=@p7 where Not_İd=" + txtnotid.Text, bgln.sqlbaglan());

                guncelle.Parameters.AddWithValue("@p1", comboBox2.Text);
                guncelle.Parameters.AddWithValue("@p2", comboBox1.Text);
                guncelle.Parameters.AddWithValue("@p3", yazili1.Text);
                guncelle.Parameters.AddWithValue("@p4", yazili2.Text);
                guncelle.Parameters.AddWithValue("@p5", sozlu1.Text);
                guncelle.Parameters.AddWithValue("@p6", sozlu2.Text);
                guncelle.Parameters.AddWithValue("@p8",b);


                
                 
                // Değerler Boş İse Hata Çıktısını Ver //
                    if (comboBox1.Text == "" || txtnotid.Text == "")
                    {
                        MessageBox.Show("Lütfen alanları doldurduğunuza emin olun","Bilgi");
                    }
                   
                    // Değerler Boş Değilse İşlemi Gerçekleştir //
                    else if (comboBox1.Text != "" || txtnotid.Text == "")
                    {

                        lblort.Text = ortalama(yazili1.Text, yazili2.Text, sozlu1.Text, sozlu2.Text).ToString("0");
                        guncelle.Parameters.AddWithValue("@p7", lblort.Text);
                        guncelle.ExecuteReader();
                        MessageBox.Show("Ders Güncellendi!", "E-Okul Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        temizle();
                        tabloekle();
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;
                        checkBox4.Checked = false;
                    }
                    bgln.sqlbaglan().Close();
                    
            }
            
        
            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                // DataGridden Seçilen İşlemi TextBox'a Yansıtma //


                /*
                yazili1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                yazili2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                sozlu1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                sozlu2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtnotid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                */
                txtnotid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
              comboBox2.Text =  dataGridView1.CurrentRow.Cells[1].Value.ToString();
            yazili1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                yazili2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                sozlu1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                sozlu2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                lblort.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();







             
             
              
               
                /*
               OleDbCommand cmd2 = new OleDbCommand("Select * from Tbl_OgretmenNot where Not_İd", bgln.sqlbaglan());
               OleDbDataReader dr2 = cmd2.ExecuteReader();
               while (dr2.Read())
               {
                   // KolonAdı //
                   if (txtnotid.Text == dr2["Not_İd"].ToString())
                   {
                       comboBox2.Text = dr2["Ogrenci_No"].ToString();
                       comboBox1.Text = dr2["Ders_No"].ToString();
                   }


               }
                 */

               bgln.sqlbaglan().Close();
            }

            private void button4_Click(object sender, EventArgs e)
            {
                // Geri Dönme İşlemi // 
                eokul geri = new eokul();
                geri.Show();
                this.Hide();
            }

            private void button3_Click(object sender, EventArgs e)
            {
                // Not Silme İşlemi //
                OleDbCommand komut = new OleDbCommand("delete from Tbl_OgretmenNot where Not_id=@p1",bgln.sqlbaglan());
                komut.Parameters.AddWithValue("@p1",txtnotid.Text);
                komut.ExecuteReader();
                bgln.sqlbaglan().Close();
                MessageBox.Show("Silindi","Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtnotid.Text = "";
                txtnotid.Text = "";
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                yazili1.Text = "";
                yazili2.Text = "";
                sozlu1.Text = "";
                sozlu2.Text = "";
                temizle();
                tabloekle();
                SINIFEKLE();
                
            }
            
            private void txtogrncno_TextChanged(object sender, EventArgs e)
            {
                //SELECT Tbl_OgretmenNot.Not_id,Tbl_OgretmenNot.Ogrenci_No, Tbl_OgretmenNot.Sinif_Adi, Tbl_Ogrenciler.Ogrenci_Ad, Tbl_Ogrenciler.Ogrenci_SoyAd, Tbl_OgretmenNot.Ders_No, Tbl_OgretmenNot.Yazili_1, Tbl_OgretmenNot.Yazili_2, Tbl_OgretmenNot.Perf_1, Tbl_OgretmenNot.Perf_2, Tbl_OgretmenNot.Ortalama FROM Tbl_Ogrenciler INNER JOIN Tbl_OgretmenNot ON Tbl_Ogrenciler.Ogrenci_No = Tbl_OgretmenNot.Ogrenci_No
                try
                {
                    if (comboBox3.Text != "")
                    {
                        // OleDbDataAdapter da = new OleDbDataAdapter("Select Not_id,Sinif_Adi,Ogrenci_No,Ders_No,Yazili_1,Yazili_2,Perf_1,Perf_2,Ortalama from Tbl_OgretmenNot  Where Ogrenci_No like '%" + txtogrncno.Text + "%' and Sinif_Adi like '%" + comboBox3.Text + "%'", bgln.sqlbaglan());
                        OleDbDataAdapter da = new OleDbDataAdapter("SELECT Tbl_OgretmenNot.Not_id,Tbl_OgretmenNot.Ogrenci_No, Tbl_OgretmenNot.Sinif_Adi, Tbl_Ogrenciler.Ogrenci_Ad, Tbl_Ogrenciler.Ogrenci_SoyAd, Tbl_OgretmenNot.Ders_No, Tbl_OgretmenNot.Yazili_1, Tbl_OgretmenNot.Yazili_2, Tbl_OgretmenNot.Perf_1, Tbl_OgretmenNot.Perf_2, Tbl_OgretmenNot.Ortalama FROM Tbl_Ogrenciler INNER JOIN Tbl_OgretmenNot ON Tbl_Ogrenciler.Ogrenci_No = Tbl_OgretmenNot.Ogrenci_No  Where Tbl_OgretmenNot.Ogrenci_No like '%" + txtogrncno.Text + "%' and Tbl_OgretmenNot.Sinif_Adi like '%" + comboBox3.Text + "%' and Tbl_OgretmenNot.Ders_No like '%"+comboBox1.Text+"%'", bgln.sqlbaglan());
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    da.Dispose();
 

                    }
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen Sınıf Seçiniz","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                   
                }
              
            }

            private void yazili1_TextChanged(object sender, EventArgs e)
            {
            }

           

            private void button2_MouseHover(object sender, EventArgs e)
            {
                ToolTip Aciklama = new ToolTip();
                Aciklama.ToolTipTitle = "Güncelle";
                Aciklama.ToolTipIcon = ToolTipIcon.Info;
                Aciklama.IsBalloon = true;
                Aciklama.SetToolTip(button2, "Notları Günceller");
            }

            private void yazili1_Leave(object sender, EventArgs e)
            {




                try
                {
                    if (Convert.ToInt32(yazili1.Text) < 0 || Convert.ToInt32(yazili1.Text) > 100)
                    {
                   
                        yazili1.Text = "";
                        txtnotid.Focus();

                    }
                
                }
                catch(Exception)
                {
                    txtnotid.Focus();
                    MessageBox.Show("Değer 0-100 arasında olmalı");
                
                }
            }

            private void sozlu1_Leave(object sender, EventArgs e)
            {
                try
                {
                    if (Convert.ToInt32(sozlu1.Text) < 0 || Convert.ToInt32(sozlu1.Text) > 100)
                    {

                        sozlu1.Text = "";
                        txtnotid.Focus();
                    }
                }
                catch (Exception)
                {
                    txtnotid.Focus();
                    MessageBox.Show("Değer 0-100 arasında olmalı");
                
                }
            }

            private void yazili2_Leave(object sender, EventArgs e)
            {
                try
                {
                    if (Convert.ToInt32(yazili2.Text) < 0 || Convert.ToInt32(yazili2.Text) > 100)
                    {

                        yazili2.Text = "";
                        txtnotid.Focus();
                    }
                }
                catch (Exception)
                {
                    txtnotid.Focus();
                    MessageBox.Show("Değer 0-100 arasında olmalı");
                
                }
            }

            private void sozlu2_Leave(object sender, EventArgs e)
            {
                try
                {
                    if (Convert.ToInt32(sozlu2.Text) < 0 || Convert.ToInt32(sozlu2.Text) > 100)
                    {

                        sozlu2.Text = "";
                        txtnotid.Focus();
                    }
                }
                catch (Exception)
                {
                    txtnotid.Focus();
                    MessageBox.Show("Değer 0-100 arasında olmalı");
              
                }
            }

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox1.Checked == true)
                {
                    yazili1.Enabled = true;
                }
                else
                {
                    yazili1.Enabled = false;
                }
            }

            private void checkBox2_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox2.Checked == true)
                {
                    yazili2.Enabled = true;
                }
                else
                {
                    yazili2.Enabled = false;
                }
            }

            private void checkBox3_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox3.Checked == true)
                {
                    sozlu1.Enabled = true;
                }
                else
                {
                    sozlu1.Enabled = false;
                }
            }

            private void checkBox4_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox4.Checked == true)
                {
                    sozlu2.Enabled = true;
                }
                else
                {
                    sozlu2.Enabled = false;
                }
            }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
            {

            }

            private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
            {
               txtnotid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                yazili1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                yazili2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                sozlu1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                sozlu2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                lblort.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            }

           

            
        
        }
    }
