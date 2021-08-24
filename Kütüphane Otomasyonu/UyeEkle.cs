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
using System.Text.RegularExpressions;//regex / güvenli parola olmayı sağlayan hazır kodları içinde barındırır.
using System.IO; //input-output yani giriş-çıkış işlemlerini yapmamızı sağlar

namespace Kütüphane_Otomasyonu
{
	public partial class UyeEkle : Form
	{
		public UyeEkle()
		{
			InitializeComponent();
		}
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");
        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select tcno AS [TC Kimlik No],ad AS [Adı],soyad AS [Soyadı],yetki AS [Yetki],kullaniciadi AS[Kullanıcı Adı],parola AS [Parola] from kullanicilar Order By ad ASC", baglantim);
                DataSet dshafıza = new DataSet();
                kullanicilari_listele.Fill(dshafıza);
                dataGridView1.DataSource = dshafıza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {

                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void UyeEkle_Load(object sender, EventArgs e)
		{
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karakter Olmalı");
            radioButton1.Checked = true;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            kullanicilari_goster();

        }

		private void button4_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || ((int)e.KeyChar == 8))

                e.Handled = false;
            else
                e.Handled = true;
        }

		private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)//char.IsLetter = harf | charIsControl=backsapace | char.IsSeperator=boşluk tuşu | char.IsDigit =Sayı

                e.Handled = false;
            else
                e.Handled = true;
        }

		private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

		private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
		{
            toolTip1.SetToolTip(textBox4, "Kullanıcı Adı 8 karakterden oluşmalıdır.");
        }
        int parola_skoru = 0;

		private void textBox5_TextChanged(object sender, EventArgs e)
		{
            //parola skoru belirleme 
            string parola_seviyesi = "";
            int kucukhaarf_skoru = 0; int buyukharf_skoru = 0; int rakam_skoru = 0; int sembol_skoru = 0;
            string sifre = textBox5.Text;// regex kütüphansesinden faydalanma | regex kütüphanesi ingilizce karakterleri baz aldığından,Türkçe karakterlerde sorun yaşamamak amacıyla sifre sitring ifadesindeki karakterleri İngilizce karakteerlere dönüştürmemiz gerekir.
            string duzeltılmıs_sifre = "";
            duzeltılmıs_sifre = sifre;
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('ı', 'i');//replace=dönüştür
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('İ', 'I');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('Ç', 'C');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('ç', 'c');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('Ş', 'S');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('ş', 's');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('Ğ', 'g');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('ğ', 'g');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('Ü', 'U');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('ü', 'u');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('Ö', 'O');
            duzeltılmıs_sifre = duzeltılmıs_sifre.Replace('ö', 'o');
            if (sifre != duzeltılmıs_sifre)
            {
                sifre = duzeltılmıs_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Parolanızdaki Türkçe Karakterler İngilizce Karakterlere Dönüştürülmüştür", "Kütüphane Programı");
            }







            //bir küçük harf=10 p 2 ve daha fazlası= 20p
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;//küçük harfleri çıkartır.
            kucukhaarf_skoru = Math.Min(2, az_karakter_sayisi) * 10;
            //buuyuk harf için aynı
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;//büyük harfleri çıkartır.
            buyukharf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;
            //rakamlar için aynı
            int rakam_sayısı = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;//rakamları çıkartır.
            rakam_skoru = Math.Min(2, rakam_sayısı) * 10;
            //sembol(özel karakter) için aynı
            int sembol_sayısı = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayısı;
            sembol_skoru = Math.Min(2, sembol_sayısı) * 10;


            parola_skoru = kucukhaarf_skoru + buyukharf_skoru + rakam_skoru + sembol_skoru;
            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;

            if (kucukhaarf_skoru == 0 || buyukharf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label22.Text = "Büyük harf, küçük harf , rakam ve sembol mutlaka kullanmalısın !";
            if (kucukhaarf_skoru != 0 && buyukharf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label22.Text = "";
            if (parola_skoru < 70)
            {
                progressBar1.ForeColor = Color.Red;
                parola_seviyesi = "Güvenli Değil!";

            }
            else if (parola_skoru == 70)
            {
                parola_seviyesi = "Orta";
                progressBar1.BackColor = Color.Yellow;
            }
            else if (parola_skoru > 70)
            {
                parola_seviyesi = " Güçlü";
                progressBar1.BackColor = Color.Green;
            }
            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = Convert.ToString(parola_seviyesi);
            progressBar1.Value = parola_skoru;

        }

		private void textBox6_TextChanged(object sender, EventArgs e)
		{
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola tekrarı eşleşmiyor");
            else
                errorProvider1.Clear();
        }
        private void temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();

        }

		private void button1_Click(object sender, EventArgs e)
		{
            string yetki = "";
            bool kayitkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno ='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayıtokuma = selectsorgu.ExecuteReader();
            while (kayıtokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();
            if (kayitkontrol == false)
            {
                //Tcno kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")

                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.White;
                // adı veri kontrölü
                if (textBox2.Text == "" || textBox2.Text.Length < 2)
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.White;
                // soyadı veri kontölü
                if (textBox3.Text == "" || textBox3.Text.Length < 2)
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.White;
                //kullanıcı adı kontrölü
                if (textBox4.Text == "" || textBox4.Text.Length != 8)
                    label5.ForeColor = Color.Red;

                else
                    label5.ForeColor = Color.White;
                //parola veri kontrolü
                if (textBox5.Text == "" || parola_skoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.White;
                //parola tekrar veri kontrolü
                if (textBox6.Text == "" || textBox6.Text != textBox5.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.White;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length >= 2 && textBox3.Text != "" && textBox3.Text.Length >= 2 && textBox4.Text != "" && textBox4.TextLength==8 && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";



                    try
                    {
                        baglantim.Open();

                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();// ekle komutunu access tablosuna ekle
                        baglantim.Close();
                        MessageBox.Show("Yeni Kullanıcı Kaydı Oluşturuldu", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        kullanicilari_goster();
                        temizle();

                    }
                    catch (Exception hatamsj)
                    {

                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Yeniden Gözden Geçiriniz.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası Daha Önceden Kayıtlı", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void button3_Click(object sender, EventArgs e)
		{
            string yetki = "";

            //Tcno kontrolü
            if (textBox1.Text.Length < 11 || textBox1.Text == "")

                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.White;
            // adı veri kontrölü
            if (textBox2.Text == "" || textBox2.Text.Length < 2)
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.White;
            // soyadı veri kontölü
            if (textBox3.Text == "" || textBox3.Text.Length < 2)
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.White;
            //kullanıcı adı kontrölü
            if (textBox4.Text == "" || textBox4.Text.Length != 8)
                label5.ForeColor = Color.Red;

            else
                label5.ForeColor = Color.White;
            //parola veri kontrolü
            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.White;
            //parola tekrar veri kontrolü
            if (textBox6.Text == "" || textBox6.Text != textBox5.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.White;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length >= 2 && textBox3.Text != "" && textBox3.Text.Length >= 2 && textBox4.Text != ""&& textBox4.TextLength == 8 && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "Kullanıcı";



                try
                {
                    baglantim.Open();

                    OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad= '" + textBox2.Text + "',soyad ='" + textBox3.Text + "',yetki='" + yetki + "', kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "'where  tcno='" + textBox1.Text + "'", baglantim);
                    guncellekomutu.ExecuteNonQuery();// ekle komutunu access tablosuna ekle
                    baglantim.Close();
                    MessageBox.Show("Kayıt Bilgileri Güncellendi.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster();
                }
                catch (Exception hatamsj)
                {

                    MessageBox.Show(hatamsj.Message);
                    baglantim.Close();

                }
            }
            else
            {
                MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Yeniden Gözden Geçiriniz.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button4_Click_1(object sender, EventArgs e)
		{
            if (textBox1.Text.Length == 11)
            {
                bool kayıt_arama_durumu = false;
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader oku = selectsorgu.ExecuteReader();
                while (oku.Read())
                {
                    kayıt_arama_durumu = true;
                    OleDbCommand delete_sorgu = new OleDbCommand("delete from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                    delete_sorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı Kaydı Silindi.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_goster();
                    temizle();
                    break;
                }
                if (kayıt_arama_durumu == false)
                    MessageBox.Show("Silineccek Kayıt Bulunamadı.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                temizle();

            }
            else

                MessageBox.Show("TC Numarası Yanlış Girildi.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button5_Click(object sender, EventArgs e)
		{
            temizle();
        }

		private void button2_Click(object sender, EventArgs e)
		{
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand select_sorgu = new OleDbCommand("select * from kullanicilar where tcno = '" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayit_okuma = select_sorgu.ExecuteReader();
                while (kayit_okuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayit_okuma.GetValue(1).ToString();
                    textBox3.Text = kayit_okuma.GetValue(2).ToString();
                    if (kayit_okuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayit_okuma.GetValue(4).ToString();
                    textBox5.Text = kayit_okuma.GetValue(5).ToString();
                    textBox6.Text = kayit_okuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aranan Kayıt Blunamadı", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                baglantim.Close();

            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli Bir Kayıt Giriniz", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temizle();
            }
        }

		private void textBox4_TextChanged(object sender, EventArgs e)
		{

		}

		private void button6_Click(object sender, EventArgs e)
		{
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();


		}

		private void progressBar1_Click(object sender, EventArgs e)
		{

		}

		private void label10_Click(object sender, EventArgs e)
		{

		}
	}
}
