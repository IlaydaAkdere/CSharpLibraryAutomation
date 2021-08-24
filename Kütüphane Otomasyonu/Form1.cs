using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;// access kütüphanesini ekledik
namespace Kütüphane_Otomasyonu
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");// bağlantı yolumuzu belirledik.
		public static string tcno, adi, soyadi, yetki;// veritabanından gelecek olan verileri(tcno,ad,soyad,yetki) kullanmak için değişkenleri tanımladık ve daha sonra diğer formlarda kullanmak için "public" olarak sınıflandırdık.

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();//Çıkış tuşuna veya ESC tuşuna basılınca formun kapanmasını sağladık.
		}

	

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
		
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			
		}

		int hak = 3;// giriş hakkını kontrol edebilmek için hak adında bir değişken tanımladık ve değerini 3 olarak atadık
		bool durum = false;// giriş işlemi başarılıysa true, başarısızsa false olacak şekilde bool türünde bir değişken tanımladık.Şuan değeri false çünkü daha giriş yapılamadı.

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
		{
			
			// form içerisine girdik.
			this.Text = "Kullanıcı Girişi";// formun textini yani formun adını Kullanıcı Giriş yaprık. Bunu özellikler kısmında text yerinde yazabiliriz ama burdan anlatayım dedim.
			this.AcceptButton = button1; //enter tuşuna basılınca button1 çalışacak.
			this.CancelButton = button2;// esc tuşuna basılınca button2 çalışacak.
			label5.Text = Convert.ToString(hak);//kalan hakkı burada göreceğiz.Form ilk açıldığında 3 atadığımız için 3 olarak gözükücek.
			radioButton1.Checked = true;//radiobutton1 seçili gelicek.
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;//formun stilini fixedtoolwindow yaptık.Yani form açıldığında küçültme ve yarım ekran tuşları gidecek sadece x kalacak.
			

		
		}

		public static string kul_adi, parola;
		
		private void button1_Click(object sender, EventArgs e)
		{
			
			if (hak != 0)//eğer hakkımız 0'a eşit değilse yani giriş hakkımız varsa...
			{
				baglantim.Open();//veritabanı bağlantısını açıyoruz.
				OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);// kullanicilar tablomuzun içindeki verileri çağırır. Buradaki * ın anlamı tüm verileri çağır demek.
				OleDbDataReader kayıtokuma = selectsorgu.ExecuteReader();//sorgudan gelen verileri okumamızı sağlar.
				while (kayıtokuma.Read())//kayıt okunduysa...
				{
					if (radioButton1.Checked==true)// ve radiobutton1 yani yönetici seçiliyse...
					{
						if (kayıtokuma["kullaniciadi"].ToString()==textBox1.Text && kayıtokuma["parola"].ToString()==textBox2.Text && kayıtokuma["yetki"].ToString()=="Yönetici")//Eğer kayıtokuma dan gelen kullanıcı adı textbox1 ile, parola textbox2 ile ve yetki Yönetici ye eşitse...
						{
							durum = true;// giriş başarılı olduğu için durumu true olarak atadık.
							tcno = kayıtokuma.GetValue(0).ToString();//Bütün durumlar sağlandığı için tcno yu accessin ilk alanına yani tcno alanında aktardık.(Bilgisayarda sayma işlemleri 0 dan başlıyor bu yüzden tcno 0. alan)
							adi = kayıtokuma.GetValue(1).ToString();//adı access in 1. alanına aktardık
							soyadi = kayıtokuma.GetValue(2).ToString();// soyadı access in 2. alanına aktardık.
							yetki = kayıtokuma.GetValue(3).ToString();// yetkiyi access in 3. alanına aktardık.
							this.Hide();// Yönetici girişi başarılı olduğu için bu form kapanıcak ve yönetici formu açılacak. Yönetici formunu bunun alt satırına geçince yazıyoruz. Bunun için önce yeni bir form açmamız gerekli. Proje --> form ekle (Project --> add windows form) ile ekliyoruz.
							Form2 frm2 = new Form2();// form2 ye bir değişken atadık(frm2).
							frm2.Show();//frm2 yani Form2 yi açtık.
							break;

						}
					}
					if (radioButton2.Checked==true)//Eğer radiobuttın2 seçiliyse...
					{
						if (kayıtokuma["kullaniciadi"].ToString() == textBox1.Text && kayıtokuma["parola"].ToString() == textBox2.Text && kayıtokuma["yetki"].ToString() == "Kullanıcı")
						{
							durum = true;
							tcno = kayıtokuma.GetValue(0).ToString();
							adi = kayıtokuma.GetValue(1).ToString();
							soyadi = kayıtokuma.GetValue(2).ToString();
							yetki = kayıtokuma.GetValue(3).ToString();
							kul_adi = kayıtokuma.GetValue(4).ToString();
							parola = kayıtokuma.GetValue(5).ToString();

							this.Hide();
							Form3 frm3 = new Form3();// bu sefer 3. forma aktaracağız.Yani kullanıcı formuna. Aynı yöntemle yeni bir form ekleyelim.
							frm3.Show();
							
							break;
						}
					}
				}
				if (durum==false)//eğer durum hâlâ false ise yani ne yönetici ne de kullanıcı girişi yapılmadıysa...
				{
					hak--;// giriş hakkını bir bir düşür.
					baglantim.Close();// bağlantıyı kapat.
				}
				label5.Text = Convert.ToString(hak);//kalan hakkı gösterir.
				if (hak==0)//eğer giriş yapılmadıysa ve giriş hakkı kalmadıysa...
				{
					button1.Enabled = false;//button1 i pasif yap
					MessageBox.Show("Giriş Hakkı Kalmadı", "Kütüphane Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);//Başlığı Kütüphane Otomasyonu olan böyle bir mesaj kutusu çıkar.
					this.Close();//formu kapatır.
				}
			}
			
		}
	}
}
