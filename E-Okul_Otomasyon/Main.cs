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
    public partial class eokul : Form
    {
        public eokul()
        {
            InitializeComponent();
        }
        public static string tckimlik;
        public static string ogretmentc;
        Random rnd = new Random();
        int rst;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void Form1_Load(object sender, EventArgs e)
        {
            rst = rnd.Next(1000, 9999);
            captcha1.Text = rst.ToString();
            captcha2.Text = rst.ToString();
            captcha3.Text = rst.ToString();

        }

        private void ogrngiris_Click(object sender, EventArgs e)
        {
            // Veritabanı Bağlantı Sınıfını Çağırma //
            bgl.sqlbaglan().Close();
            tckimlik = txtogrnno.Text;
            try
            {
                // Boş Bırakılma Hata Çıktısı //
                if (txtogrnrakam.Text == "" || txtogrntc.Text == "" || txtogrnno.Text == "")
                {
                    MessageBox.Show("Alanlar Boş Geçilemez.", "Alanı Doldur!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Random Üretilen Değerin Girilen Sayıyla Eşleşme Durumu //
                else if (rst == Convert.ToInt32(txtogrnrakam.Text))
                {
                    // Boş Bırakılmama Durumu //
                    if (txtogrnrakam.Text != "" || txtogrntc.Text != "" || txtogrnno.Text != "")
                    {
                        // Veri Komutu Oluşturma //
                        OleDbCommand komut = new OleDbCommand("Select * From Tbl_Ogrenciler where Ogrenci_Tc=@p1 and Ogrenci_No=@p2", bgl.sqlbaglan());
                        komut.Parameters.AddWithValue("@p1", txtogrntc.Text);
                        komut.Parameters.AddWithValue("@p2", txtogrnno.Text);
                        OleDbDataReader dr = komut.ExecuteReader();
                        // Veri Okuma İşlemi //
                        if (dr.Read())
                        {
                            


                            Ogrenci ogr = new Ogrenci();
                            ogr.Show();
                            this.Hide();
                        }
                            else
                        {
                            MessageBox.Show("Hatalı TC veya Okul No", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Restart();
                        }
                         
                       
                    }
                }
                // Random Üretilen Değerin Girilen Sayıyla Eşleşmeme Durumu //
                else if (rst != Convert.ToInt32(txtogrnrakam.Text))
                {
                    MessageBox.Show("Resimdeki Rakamlar Geçerli Değil");

                }
            }
            catch(Exception)
            {
               MessageBox.Show("Lütfen Harf veya Sembol Girmeyiniz","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
                


        }

        private void ogrtgiris_Click(object sender, EventArgs e)
        {
            ogretmentc = txtogrtkadi.Text;
            bgl.sqlbaglan().Close();

            try
            {
                // Boş Bırakılma Hata Çıktısı //
                if (txtogrtkadi.Text == "" || txtogrtsifre.Text == "" || txtogrtrakam.Text == "")
                {
                    MessageBox.Show("Alanlar Boş Geçilemez.", "Alanı Doldur!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Random Üretilen Değerin Girilen Sayıyla Eşleşme Durumu //
                else if (rst == Convert.ToInt32(txtogrtrakam.Text))
                {
                    // Boş Bırakılmama Durumu //
                    if (txtogrtkadi.Text != "" || txtogrtsifre.Text != "" || txtogrtrakam.Text != "")
                    {
                        // Veri Komutu Oluşturma //
                        OleDbCommand komut = new OleDbCommand("Select * From Tbl_Ogretmenler where Ogretmen_Tc=@p1 and Ogretmen_Sifre=@p2", bgl.sqlbaglan());
                        komut.Parameters.AddWithValue("@p1", txtogrtkadi.Text);
                        komut.Parameters.AddWithValue("@p2", txtogrtsifre.Text);
                        OleDbDataReader dr = komut.ExecuteReader();
                        // Veri Okuma İşlemi //
                        if (dr.Read())
                        {
                            Ogretmen ogrt = new Ogretmen();
                            ogrt.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Restart();
                        }
                    }
                }
                // Random Üretilen Değerin Girilen Sayıyla Eşleşmeme Durumu //
                else if (rst != Convert.ToInt32(txtogrtrakam.Text))
                {
                    MessageBox.Show("Resimdeki Rakamlar Geçerli Değil", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Harf veya Sembol Girmeyiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bgl.sqlbaglan().Close();

            try
            {
                // Boş Bırakılma Hata Çıktısı //
                if (txtmdrkadi.Text == "" || txtmdrsifre.Text == "" || txtmdrrakam.Text == "")
                {
                    MessageBox.Show("Alanlar Boş Geçilemez.", "Alanı Doldur!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Random Üretilen Değerin Girilen Sayıyla Eşleşme Durumu //
                else if (rst == Convert.ToInt32(txtmdrrakam.Text))
                {
                    // Boş Bırakılmama Durumu //
                    if (txtmdrkadi.Text != "" || txtmdrsifre.Text != "" || txtmdrrakam.Text != "")
                    {
                        // Veri Komutu Oluşturma //
                        OleDbCommand komut = new OleDbCommand("Select * From Tbl_MudurGiris where KullaniciAdi=@p1 and Sifre=@p2", bgl.sqlbaglan());
                        komut.Parameters.AddWithValue("@p1", txtmdrkadi.Text);
                        komut.Parameters.AddWithValue("@p2", txtmdrsifre.Text);
                        OleDbDataReader dr = komut.ExecuteReader();
                        // Veri Okuma İşlemi //
                        if (dr.Read())
                        {
                            Mudur ogrt = new Mudur();
                            ogrt.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Restart();
                        }
                    }
                }
                // Random Üretilen Değerin Girilen Sayıyla Eşleşmeme Durumu //
                else if (rst != Convert.ToInt32(txtmdrrakam.Text))
                {
                    MessageBox.Show("Resimdeki Rakamlar Geçerli Değil", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Harf veya Sembol Girmeyiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void panel9_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel9_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
