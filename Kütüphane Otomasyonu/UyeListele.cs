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
	public partial class UyeListele : Form
	{
		public UyeListele()
		{
			InitializeComponent();
		}
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");
		DataSet dataset = new DataSet();//üyeleri tut

		private void kullanicilari_goster()// kitapları datagride aktarmak için metot.
		{
			try
			{
				baglantim.Open();// bağlantıyı aç
				OleDbDataAdapter kitaplari_listele = new OleDbDataAdapter("select tcno AS [TC Kimlik Numarası],ad AS [Adı],soyad AS [Soyadı],yetki AS [Yetki],kullaniciadi AS[Kullanıcı Adı],parola AS [Parola] from kullanicilar", baglantim);//veritabanındaki verilerin daha güzel görünmesi için düzenleme(mesela kullaniciadi değil Kullanıcı Adı
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
		private void button1_Click(object sender, EventArgs e)
		{
			Form2 frm2 = new Form2();
			frm2.Show();
			this.Hide();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			DataTable tablo = new DataTable();
			baglantim.Open();
			OleDbDataAdapter adtr = new OleDbDataAdapter("select * from kullanicilar where tcno  like '" + textBox1.Text + "%' or kullaniciadi like '" + textBox1.Text + "%'", baglantim);
			adtr.Fill(tablo);
			dataGridView1.DataSource = tablo;
			baglantim.Close();
		}

		private void UyeListele_Load(object sender, EventArgs e)
		{
			kullanicilari_goster();
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
