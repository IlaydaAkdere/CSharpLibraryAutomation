﻿using System;
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
	public partial class KitapListele : Form
	{
		public KitapListele()
		{
			InitializeComponent();
		}
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
		OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kutuphane.mdb");
		DataSet dataset = new DataSet();//kayıtları tut
		private void button1_Click(object sender, EventArgs e)
		{
			this.Hide();
			Form2 frm2 = new Form2();// form2 ye bir değişken atadık(frm2).
			frm2.Show();
		}

		private void KitapListele_Load(object sender, EventArgs e)
		{
			kitaplari_goster();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			DataTable tablo = new DataTable();
			baglantim.Open();
			OleDbDataAdapter adtr = new OleDbDataAdapter("select * from kitaplar where yazar  like '" + textBox1.Text + "%' or kitapadi like '"+textBox1.Text +"%'",baglantim);
			adtr.Fill(tablo);
			dataGridView1.DataSource = tablo;
			baglantim.Close();
		}
	}
}
