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

namespace Kütüphane_Otomasyonu
{
	public partial class Form3 : Form
	{
		public Form3()
		{
			InitializeComponent();
		}
		
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");
		DataSet dataset = new DataSet();//kayıtları tut
		private void kitaplari_goster()// kitapları datagride aktarmak için metot.
		{
			try
			{
				baglantim.Open();// bağlantıyı aç
				OleDbDataAdapter kitaplari_listele = new OleDbDataAdapter("select serino AS [Seri Numarası],kitapadi AS [Kitap Adı],yazar AS [Yazar],baskiyili AS [Baskı Yılı],sayfasayisi AS[Sayfa Sayısı],yayinevi AS [Yayınevi] from kitaplar", baglantim);//veritabanındaki verilerin daha güzel görünmesi için düzenleme(mesela kitapadi değil Kitap Adı
				DataSet dshafıza = new DataSet();//verileri gruplandırmak yani set yapmak için dataset i çağırdık
				kitaplari_listele.Fill(dshafıza);//verileri kaydettik
				dataGridView1.DataSource = dshafıza.Tables[0];//datagride ilk sütundan itibaren yerleştirdik
				baglantim.Close();//bağlantıyı kapattık

			}
			catch (Exception hatamsj)
			{

				MessageBox.Show(hatamsj.Message, "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);//üstteki kodlar çalışmazsa hata nedenini mesaj olarak gösterir.
				baglantim.Close();// bağlantı üstte kapanmazsa burda kapanacak
			}
		}
		public static string kuladi, parolam;
		private void Form3_Load(object sender, EventArgs e)
		{
			kitaplari_goster();
		
			textBox1.Text = Form1.kul_adi;textBox2.Text = Form1.parola;
		
			
		}
		private void temizle()
		{
			textBox3.Clear();textBox4.Clear();textBox5.Clear();textBox6.Clear();textBox7.Clear();
		}
		private void button2_Click(object sender, EventArgs e)
		{
			try
			{



				baglantim.Open();

				OleDbCommand eklekomutu = new OleDbCommand("insert into favorilerim values('" + label8.Text + "','" + textBox6.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + comboBox2.Text + "')", baglantim);
				eklekomutu.ExecuteNonQuery();// ekle komutunu access tablosuna ekle
				baglantim.Close();
				MessageBox.Show("Yeni Kullanıcı Kaydı Oluşturuldu", "Kütüphane Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				kitaplari_goster();
				


			}
			catch (Exception hatamsj)
			{

				MessageBox.Show(hatamsj.Message);
				baglantim.Close();

			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			this.Close();
			
		}
		
		private void textBox7_TextChanged(object sender, EventArgs e)
		{
			DataTable tablo = new DataTable();
			baglantim.Open();
			OleDbDataAdapter adtr = new OleDbDataAdapter("select * from kitaplar where yazar  like '" + textBox7.Text + "%' or kitapadi like '" + textBox7.Text + "%'", baglantim);
			adtr.Fill(tablo);
			dataGridView1.DataSource = tablo;
			baglantim.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Favorilerim favorilerim = new Favorilerim();
			favorilerim.Show();
			this.Hide();
		}
		
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			BilgilerimiGüncelle bilgilerimiGüncelle = new BilgilerimiGüncelle();
			bilgilerimiGüncelle.Show();
			this.Hide();
			
		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			label8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			textBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
			textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
			textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
			textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
			comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
		}

		private void textBox4_TextChanged(object sender, EventArgs e)
		{

		}

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
		{
			
		}
	}
}
