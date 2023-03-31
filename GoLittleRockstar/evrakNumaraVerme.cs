using GoLittleRockstar.Model;
using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar
{
    public partial class evrakNumaraVerme : UserControl
    {
        public List<clsKurumSirket> kurumBilgisi;
        public List<clsKurumSirket> sirketBilgisi;
        public clsKurum kurum;
        public clsSirket sirket;
        public clsKurumSirket kurumSirket;
        public clsEvrak evrak;

        public bool kurulusUpdate = true;
        public evrakNumaraVerme()
        {
            InitializeComponent();
        }

        private void evrakNumaraVerme_Load(object sender, EventArgs e)
        {
            evrak = new clsEvrak();
            kurumSirket = new clsKurumSirket();

            cmbCikanSirket.DataBindings.Add("SelectedValue", evrak, "sirket_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbGidenKurum.DataBindings.Add("SelectedValue", evrak, "kurum_id", true, DataSourceUpdateMode.OnPropertyChanged);
            dateYaziTarihi.DataBindings.Add("Value", evrak, "Tarih", true, DataSourceUpdateMode.OnPropertyChanged);
            chkKep.DataBindings.Add("CheckState", evrak, "KepDurum", true, DataSourceUpdateMode.OnPropertyChanged);
            txtEvrakKonusu.DataBindings.Add("Text", evrak, "Aciklama", false, DataSourceUpdateMode.OnPropertyChanged);

            txtUnvan.DataBindings.Add("Text", kurumSirket, "ad", true, DataSourceUpdateMode.OnPropertyChanged);

            UpdateForm();

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtUnvan.Text is null || txtUnvan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Unvan Girin.", "Uyarı");
                return;
            }
            if (chkKurum.CheckState == CheckState.Checked && chkSirket.CheckState == CheckState.Checked)
            {
                MessageBox.Show("Tek Seçim Yapın.", "Uyarı");
                return;
            }
            if (chkKurum.CheckState == CheckState.Checked && chkSirket.CheckState == CheckState.Unchecked)
            {
                kurum = new clsKurum();
                kurum.kurumAdi = kurumSirket.ad;


                using (var contex = new context())
                {
                    contex.tblKurumlar.Add(kurum);
                    await contex.SaveChangesAsync();
                    MessageBox.Show("Kayıt Tamamlandı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (chkKurum.CheckState == CheckState.Unchecked && chkSirket.CheckState == CheckState.Checked)
            {
                sirket = new clsSirket();
                sirket.sirketAdi = kurumSirket.ad;

                using (var contex = new context())
                {
                    contex.tblSirket.Add(sirket);
                    await contex.SaveChangesAsync();
                    MessageBox.Show("Kayıt Tamamlandı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            kurulusUpdate = true;
            txtUnvan.Clear();
            UpdateForm();
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            clsEvrak evrakNumara = new clsEvrak();

            using (var contex = new context())
            {
                contex.tblEvrak.Add(evrak);
                await contex.SaveChangesAsync();

                var Sql = "select * from \"tblEvrak\" order by evrak_id desc LIMIT 1";
                evrakNumara = await contex.tblEvrak.FromSqlRaw(Sql).FirstOrDefaultAsync();
            }

            MessageBox.Show("Kayıt Tamamlandı. Numaranız: " + evrakNumara.sirket_id + "." + DateTime.Now.Year + "." + evrakNumara.evrak_id + "", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            label1.Text = "" + evrakNumara.sirket_id + "." + DateTime.Now.Year + "." + evrakNumara.evrak_id + "";

            evrak.evrak_id = 0;

            nullText();

        }
        public async void UpdateForm()
        {
            sirketBilgisi = new List<clsKurumSirket>();
            kurumBilgisi = new List<clsKurumSirket>();

            if (kurulusUpdate == true)
            {
                using (var contex = new context())
                {
                    var Sql = "select \"sirket_id\" id,\"sirketAdi\" ad from \"tblSirket\"";
                    var SirketList = await contex.kurumSirket.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    foreach (clsKurumSirket i in SirketList)
                    {
                        sirketBilgisi.Add(i);
                    }
                    Sql = "select \"kurum_id\" id,\"kurumAdi\" ad from \"tblKurumlar\"";
                    var KurumList = await contex.kurumSirket.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    foreach (clsKurumSirket i in KurumList)
                    {
                        kurumBilgisi.Add(i);
                    }
                }
            }
            cmbCikanSirket.DataSource = sirketBilgisi;
            cmbGidenKurum.DataSource = kurumBilgisi;

            kurulusUpdate = false;

            nullText();

        }
        public void nullText()
        {
            txtEvrakKonusu.Text = "Evrak Konusu Belirtiniz.";
            txtUnvan.Text = "Unvan Bilgisi Giriniz.";
            cmbCikanSirket.Text = "Yazının Çıkacağı Şirketi Seçiniz";
            cmbGidenKurum.Text = "Yazının Gideceği Kurumu Seçiniz";
        }
    }
}
