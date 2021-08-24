using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_Otomasyonu
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			KitapEkle frm4 = new KitapEkle();
			frm4.Show();
			this.Hide();

		}

		private void button3_Click(object sender, EventArgs e)
		{
			KitapListele Ktplist = new KitapListele();
			Ktplist.Show();
			this.Hide();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			this.Text = "Yönetici Paneli";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			UyeEkle uyeekle = new UyeEkle();
			uyeekle.Show();
			this.Hide();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			UyeListele uyelistele = new UyeListele();
			uyelistele.Show();
			this.Hide();
		}
	}
}
