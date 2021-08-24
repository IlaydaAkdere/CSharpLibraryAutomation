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
	public partial class BilgilerimiGüncelle : Form
	{
		public BilgilerimiGüncelle()
		{
			InitializeComponent();
		}
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");
		private void BilgilerimiGüncelle_Load(object sender, EventArgs e)
		{
			textBox1.Text = Form1.kul_adi;textBox2.Text = Form1.parola;
			textBox1.MaxLength = 10;
			textBox2.MaxLength = 10;
			progressBar1.Maximum = 100;
			progressBar1.Value = 0;

		}
		
		private void button2_Click(object sender, EventArgs e)
		{
			Form3 form3 = new Form3();
			form3.Show();
			this.Hide();
		}
		
		
		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text == "")

				label1.ForeColor = Color.Red;
			else
				label1.ForeColor = Color.White;
			// yazar için
			if (textBox2.Text == "" || parola_skoru < 70)
				label2.ForeColor = Color.Red;
			else
				label2.ForeColor = Color.White;
			if (label1.ForeColor==Color.White&& label2.ForeColor==Color.White)
			{
				try
				{
					baglantim.Open();

					OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set kullaniciadi= '" + textBox1.Text + "',parola ='" + textBox2.Text + "'where  tcno='" + Form1.tcno + "'", baglantim);
					guncellekomutu.ExecuteNonQuery();// ekle komutunu access tablosuna ekle
					baglantim.Close();
					MessageBox.Show("Kayıt Bilgileri Güncellendi.Tekrar Giriş Yapacağınız Zaman Bu Bilgileri Kullanabilirsiniz.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					
					
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
		int parola_skoru = 0;
		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			string parola_seviyesi = "";
			int kucukhaarf_skoru = 0; int buyukharf_skoru = 0; int rakam_skoru = 0; int sembol_skoru = 0;
			string sifre = textBox2.Text;// regex kütüphansesinden faydalanma | regex kütüphanesi ingilizce karakterleri baz aldığından,Türkçe karakterlerde sorun yaşamamak amacıyla sifre sitring ifadesindeki karakterleri İngilizce karakteerlere dönüştürmemiz gerekir.
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
				textBox2.Text = sifre;
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
				label8.Text = "Büyük harf, küçük harf , rakam ve sembol mutlaka kullanmalısın !";
			if (kucukhaarf_skoru != 0 && buyukharf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
				label8.Text = "";
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
			label5.Text = "%" + Convert.ToString(parola_skoru);
			label6.Text = Convert.ToString(parola_seviyesi);
			progressBar1.Value = parola_skoru;

		}
	}
}
