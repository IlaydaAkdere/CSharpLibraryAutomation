using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;// access kütüphanesi eklendi

namespace Kütüphane_Otomasyonu
{
	public partial class KitapEkle : Form
	{
		public KitapEkle()
		{
			InitializeComponent();
		}
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");
		//veriyolun4erlirleme
		private void temzile()//nesneleri temizlemek için kullanıyoruz
		{
			textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Text = ""; comboBox2.Text = "";
		}

		private void kitaplari_goster()
			// kitapları datagride aktarmak için metot.
		{
			try
			{
				baglantim.Open(); //DB bağlantısını açar
				OleDbDataAdapter kitaplari_listele = new OleDbDataAdapter("select serino AS [Seri Numarası],kitapadi AS [Kitap Adı],yazar AS [Yazar],baskiyili AS [Baskı Yılı],sayfasayisi AS[Sayfa Sayısı],yayinevi AS [Yayınevi] from kitaplar", baglantim);//veritabanındaki verilerin daha güzel görünmesi için düzenleme(mesela kitapadi değil Kitap Adı
				DataSet dshafıza = new DataSet();//verileri gruplandırmak yani set yapmak için dataset i çağırdık
				kitaplari_listele.Fill(dshafıza);//verileri kaydettik
				dataGridView1.DataSource = dshafıza.Tables[0];//datagride ilk sütundan itibaren yerleştirdik
				baglantim.Close();//bağlantıyı kapattık

			}
			catch (Exception hatamsj)
			{

				MessageBox.Show(hatamsj.Message, "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);//üstteki kodlar çalışmazsa hata nedenini mesaj olarak gösterir.
				baglantim.Close();
				// bağlantı üstte kapanmazsa burda kapanacak
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{//anasayfaya dönüş tuşu
			this.Hide();//bu formu gizle
			Form2 frm2 = new Form2();// form2 ye bir değişken atadık(frm2).
			frm2.Show();//form2 yi yani yönetici panelini göster

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{

			//ad için veri kontrolüü
			if (textBox1.Text == "")

				label1.ForeColor = Color.Red;
			else
				label1.ForeColor = Color.Black;
			// yazar için
			if (textBox2.Text == "")
				label2.ForeColor = Color.Red;
			else
				label2.ForeColor = Color.Black;
			// serino veri kontrolü
			if (textBox3.Text == "")
				label4.ForeColor = Color.Red;
			else
				label4.ForeColor = Color.Black;
			//baskıyılı adı kontrölü
			if (textBox4.Text == "")
				label6.ForeColor = Color.Red;

			else
				label6.ForeColor = Color.Black;
			//sayfasayısı veri kontrolü
			if (textBox5.Text == "")
				label3.ForeColor = Color.Red;
			else
				label3.ForeColor = Color.Black;
			// veri kontrolü
			if (comboBox2.Text == "")
				label5.ForeColor = Color.Red;
			else
				label5.ForeColor = Color.Black;
			if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox2.Text != "")
			{


				try
				{



					baglantim.Open();

					OleDbCommand eklekomutu = new OleDbCommand("insert into kitaplar values('" + textBox4.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + comboBox2.Text + "')", baglantim);
					eklekomutu.ExecuteNonQuery();
					// ekle komutunu access tablosuna ekle
					baglantim.Close();
					MessageBox.Show("Yeni Kullanıcı Kaydı Oluşturuldu", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					kitaplari_goster();
					temzile();


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
		}//ekle

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
			textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
			textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
			textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
			comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
			//text4/text1/text2
		}

		private void button4_Click(object sender, EventArgs e)
		{
			//ad için veri kontrolüü
			if (textBox1.Text == "")

				label1.ForeColor = Color.Red;
			else
				label1.ForeColor = Color.White;
			// yazar için
			if (textBox2.Text == "")
				label2.ForeColor = Color.Red;
			else
				label2.ForeColor = Color.White;
			// serino veri kontrolü
			if (textBox3.Text == "")
				label4.ForeColor = Color.Red;
			else
				label4.ForeColor = Color.White;
			//baskıyılı adı kontrölü
			if (textBox4.Text == "")
				label6.ForeColor = Color.Red;

			else
				label6.ForeColor = Color.White;
			//sayfasayısı veri kontrolü
			if (textBox5.Text == "")
				label3.ForeColor = Color.Red;
			else
				label3.ForeColor = Color.White;
			// veri kontrolü
			if (comboBox2.Text == "")
				label5.ForeColor = Color.Red;
			else
				label5.ForeColor = Color.White;
			if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox2.Text != "")
			{
				try
				{
					baglantim.Open();

					OleDbCommand guncellekomutu = new OleDbCommand("update kitaplar set kitapadi= '" + textBox1.Text + "',yazar ='" + textBox2.Text + "',baskiyili='" + textBox5.Text + "', sayfasayisi='" + textBox3.Text + "',yayinevi='" + textBox5.Text + "'where  serino='" + textBox4.Text + "'", baglantim);
					guncellekomutu.ExecuteNonQuery();// ekle komutunu access tablosuna ekle
					baglantim.Close();
					MessageBox.Show("Kayıt Bilgileri Güncellendi.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					kitaplari_goster();
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
		}//güncelle
	
		private void KitapEkle_Load(object sender, EventArgs e)
		{
			kitaplari_goster();
			comboBox2.Items.Add("Türkiye İş Bankası Kültür Yayınları"); comboBox2.Items.Add("Metis Yayınları"); comboBox2.Items.Add("Can Yayınları"); comboBox2.Items.Add("Doğan Kitap");
			label9.Text = dataGridView1.RowCount.ToString();
		}//form

		private void button5_Click(object sender, EventArgs e)
		{
			temzile();
		}//temizle butonu

		private void button3_Click(object sender, EventArgs e)
		{
			baglantim.Open();
			OleDbCommand delete_sorgu = new OleDbCommand("delete from kitaplar where serino='" + textBox4.Text + "'", baglantim);
			delete_sorgu.ExecuteNonQuery();
			MessageBox.Show("Kullanıcı Kaydı Silindi.", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			baglantim.Close();
			kitaplari_goster();
			temzile();
		}

		private void progressBar1_Click(object sender, EventArgs e)
		{

		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

		private void textBox4_TextChanged(object sender, EventArgs e)
		{

		}
	}

		

		
	}
	    
