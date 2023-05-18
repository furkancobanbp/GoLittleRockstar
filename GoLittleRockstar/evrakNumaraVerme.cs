using GoLittleRockstar.Functions;
using GoLittleRockstar.Model;
using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar
{
    public partial class evrakNumaraVerme : UserControl
    {
        public queries query = new queries();

        public List<clsKurumSirket> kurumBilgisi;
        public List<clsKurumSirket> sirketBilgisi;
        public List<clsKurumSirket> teminatKurumBilgisi;
        public List<clsKurumSirket> teminatSirketBilgisi;
        public List<clsBanka> bankaBilgisi;

        public List<clsKurumSirket> teminatTipBilgisi;
        public List<clsKurumSirket> teminatTurBilgisi;

        public clsKurum kurum;
        public clsSirket sirket;
        public clsKurumSirket kurumSirket;       
        public clsEvrak evrak;
        public clsTeminat teminat;
        public clsBanka banka;

        public bool kurulusUpdate = true;

        
        public evrakNumaraVerme()
        {
            InitializeComponent();
        }

        private async void evrakNumaraVerme_Load(object sender, EventArgs e)
        {
            evrak = new clsEvrak();
            kurumSirket = new clsKurumSirket();
            teminat = new clsTeminat();
            banka = new clsBanka();

            cmbCikanSirket.DataBindings.Add("SelectedValue", evrak, "sirket_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbGidenKurum.DataBindings.Add("SelectedValue", evrak, "kurum_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTeminatVerilenKurum.DataBindings.Add("SelectedValue", teminat, "kurum_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTeminatCikaranKurum.DataBindings.Add("SelectedValue", teminat, "sirket_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTeminatTipi.DataBindings.Add("SelectedValue", teminat, "tip_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbBankaBilgisi.DataBindings.Add("SelectedValue", teminat, "banka_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTeminatTur.DataBindings.Add("SelectedValue", teminat, "tur_id", true, DataSourceUpdateMode.OnPropertyChanged);

            dateYaziTarihi.DataBindings.Add("Value", evrak, "Tarih", true, DataSourceUpdateMode.OnPropertyChanged);
            dateTeminatTarihi.DataBindings.Add("Value", teminat, "Tarih", true, DataSourceUpdateMode.OnPropertyChanged);

            chkKep.DataBindings.Add("CheckState", evrak, "KepDurum", true, DataSourceUpdateMode.OnPropertyChanged);

            txtEvrakKonusu.DataBindings.Add("Text", evrak, "Aciklama", false, DataSourceUpdateMode.OnPropertyChanged);
            txtUnvan.DataBindings.Add("Text", kurumSirket, "ad", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMektupNumarasi.DataBindings.Add("Text", teminat, "MektupNo", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTeminatTutari.DataBindings.Add("Text", teminat, "TeminatTutari", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBankaUnvani.DataBindings.Add("Text", banka, "BankaAdi", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTeminatHk.DataBindings.Add("Text", teminat, "teminatBilgi", true, DataSourceUpdateMode.OnPropertyChanged);

            kurulusUpdate = true;
            await UpdateForm();

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
            await UpdateForm();
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

            MessageBox.Show("Kayıt Tamamlandı. Numaranız: "
                + evrakNumara.sirket_id + "." + DateTime.Now.Year + "."
                + evrakNumara.evrak_id + "", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            label1.Text = "" + evrakNumara.sirket_id + "." + DateTime.Now.Year + "." + evrakNumara.evrak_id + "";

            evrak.evrak_id = 0;

            await nullText();

        }
        public async Task UpdateForm()
        {
            sirketBilgisi = new List<clsKurumSirket>();
            kurumBilgisi = new List<clsKurumSirket>();
            bankaBilgisi = new List<clsBanka>();

            teminatSirketBilgisi = new List<clsKurumSirket>();
            teminatKurumBilgisi = new List<clsKurumSirket>();
            teminatTurBilgisi = new List<clsKurumSirket>();

            teminatTipBilgisi = new List<clsKurumSirket>();

            if (kurulusUpdate == true)
            {
                using (var contex = new context())
                {
                    var Sql = "select \"sirket_id\" id,\"sirketAdi\" ad from \"tblSirket\"";
                    var SirketList = await contex.kurumSirket.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    sirketBilgisi.AddRange(SirketList);
                    teminatSirketBilgisi.AddRange(SirketList);
                    teminatSirketBilgisi.Add(new clsKurumSirket { id = -1, ad = "Hepsi"});

                    Sql = "select \"kurum_id\" id,\"kurumAdi\" ad from \"tblKurumlar\"";
                    var KurumList = await contex.kurumSirket.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    kurumBilgisi.AddRange(KurumList);
                    teminatKurumBilgisi.AddRange(KurumList);
                    teminatKurumBilgisi.Add(new clsKurumSirket { id = -1, ad = "Hepsi" });

                    Sql = "select \"tip_id\" id,\"TipAdi\" ad from \"tblTeminatTip\"";
                    var TipList = await contex.kurumSirket.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    teminatTipBilgisi.AddRange(TipList);
                    teminatTipBilgisi.Add(new clsKurumSirket { id = -1, ad = "Hepsi" });


                    Sql = "select * from \"tblBankaBilgisi\"";
                    var BankaList = await contex.tblBankaBilgisi.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    bankaBilgisi.AddRange(BankaList);
                    bankaBilgisi.Add(new clsBanka { banka_id = -1, BankaAdi = "Hepsi" });

                    Sql = "select \"tur_id\" id,\"TurAdi\" ad from \"tblTeminatTur\"";
                    var TurList = await contex.kurumSirket.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                    teminatTurBilgisi.AddRange(TurList);
                    teminatTurBilgisi.Add(new clsKurumSirket { id = -1, ad = "Hepsi" });

                }

            }
            cmbCikanSirket.DataSource = sirketBilgisi;
            cmbGidenKurum.DataSource = kurumBilgisi;

            cmbTeminatVerilenKurum.DataSource = teminatKurumBilgisi;
            cmbTeminatCikaranKurum.DataSource = teminatSirketBilgisi;
            cmbTeminatTipi.DataSource = teminatTipBilgisi;
            cmbTeminatTur.DataSource = teminatTurBilgisi;

            cmbBankaBilgisi.DataSource = bankaBilgisi;

            kurulusUpdate = false;

            await nullText();

        }
        public async Task nullText()
        {
            txtEvrakKonusu.Text = "Evrak Konusu Belirtiniz.";
            txtUnvan.Text = "Unvan Bilgisi Giriniz.";
            cmbCikanSirket.Text = "Yazının Çıkacağı Şirketi Seçiniz";
            cmbGidenKurum.Text = "Yazının Gideceği Kurumu Seçiniz";
            cmbTeminatVerilenKurum.Text = "Teminat Verilen Kurumu Seçiniz";
            cmbTeminatCikaranKurum.Text = "Teminatın Çıktığı Kurumu Seçiniz";
            cmbTeminatTipi.Text = "Teminat Tipini Seçiniz";
            txtMektupNumarasi.Text = "Mektup Numarasını Giriniz";
            txtTeminatTutari.Text = "Teminat Tutarını Giriniz";
            cmbBankaBilgisi.Text = "Banka Seçimi Yapınız";
            txtBankaUnvani.Text = "Banka Unvanı Giriniz";
            txtTeminatHk.Text = "Teminat Hakkında Bilgi Giriniz";
            cmbTeminatTur.Text = "Teminat Türünü Seçiniz";
        }

        private async void btnTeminatKaydet_Click(object sender, EventArgs e)
        {
            using (var contex = new context())
            {
                contex.tblVerilenTeminatlar.Add(teminat);
                await contex.SaveChangesAsync();
            }
            txtMektupNumarasi.Clear();
            txtTeminatTutari.Clear();

            MessageBox.Show("Kayıt Tamamlandı");

            await nullText();
        }

        private async void btnBankaKaydet_Click(object sender, EventArgs e)
        {
            using (var contex = new context())
            {
                contex.tblBankaBilgisi.Add(banka);
                await contex.SaveChangesAsync();
            }

            MessageBox.Show("Kayıt Tamamlandı");

            kurulusUpdate = true;
            await UpdateForm();
            banka.banka_id = 0;

        }

        private async void btnTeminatSorgula_Click(object sender, EventArgs e)
        {
            String Query = " Where \"tblVerilenTeminatlar\".\"MektupNo\" like '%" + txtMektupNumarasi.Text + "%'"; ;
            String basTar = dateTeminatTarihi.Value.ToShortDateString();
            String bitTar = dateTeminatBitTar.Value.ToShortDateString();

            if (!(dateTeminatTarihi == null) && !(dateTeminatBitTar == null))
            {
                Query += " and \"tblVerilenTeminatlar\".\"Tarih\" between '"+basTar+"' and '"+bitTar+"' ";
            }           
            if (!(Convert.ToInt32(cmbBankaBilgisi.SelectedValue) == -1))
            {
                Query += " and \"tblVerilenTeminatlar\".\"banka_id\" = "+cmbBankaBilgisi.SelectedValue+"";
            }
            if (!(Convert.ToInt32(cmbTeminatCikaranKurum.SelectedValue) == -1))
            {
                Query += " and \"tblVerilenTeminatlar\".\"sirket_id\" = " + cmbTeminatCikaranKurum.SelectedValue + "";
            }
            if (!(Convert.ToInt32(cmbTeminatVerilenKurum.SelectedValue) == -1))
            {
                Query += " and \"tblVerilenTeminatlar\".\"kurum_id\" = " + cmbTeminatVerilenKurum.SelectedValue + "";
            }
            if (!(Convert.ToInt32(cmbTeminatTipi.SelectedValue) == -1))
            {
                Query += " and \"tblVerilenTeminatlar\".\"tip_id\" = " + cmbTeminatTipi.SelectedValue + "";
            }
            if (!(Convert.ToInt32(cmbTeminatTur.SelectedValue) == -1))
            {
                Query += " and \"tblVerilenTeminatlar\".\"tur_id\" = " + cmbTeminatTipi.SelectedValue + "";
            }
            if(chkTumunuListele.CheckState == CheckState.Checked)
            {
                Query = "";
            }

            gridTeminatTablosu.DataSource = query.TeminatBilgisiAl(Query);

            await nullText();
        }
    }
}
