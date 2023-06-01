using GoLittleRockstar.Functions;
using GoLittleRockstar.Model;
using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar
{
    public partial class controlGirisSira : UserControl
    {


        public List<clsKisi> KisiListe;
        public clsGirisYukle girisYukle;
        public List<clsIslemTuru> islemTur;
        public List<clsCalismaDonemi> calismaDonemi;

        public queries query = new();
        public controlGirisSira()
        {
            InitializeComponent();
        }

        private async void controlGirisSira_Load(object sender, EventArgs e)
        {
            KisiListe = new();
            girisYukle = new();
            islemTur = new();
            calismaDonemi = new();

            using (context context = new())
            {
                var Sql = "select * from \"tblKisi\"";
                var KisiList = await context.tblKisi.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                KisiListe.AddRange(KisiList);

                Sql = "select * from \"tblIslemTur\"";
                var IslemList = await context.tblIslemTur.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                islemTur.AddRange(IslemList);

                Sql = "select * from \"tblCalismaDonemi\"";
                var CalismaDonem = await context.tblCalismaDonemi.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                calismaDonemi.AddRange(CalismaDonem);
            }
            cmbKisi.DataSource = KisiListe;
            cmbIslemTur.DataSource = islemTur;
            cmbCalismaDonem.DataSource = calismaDonemi;

            dateGirisTarih.DataBindings.Add("Value", girisYukle, "Tarih", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbKisi.DataBindings.Add("SelectedValue", girisYukle, "kisi_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbIslemTur.DataBindings.Add("SelectedValue", girisYukle, "islem_id", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private async void btnGirisKaydet_Click(object sender, EventArgs e)
        {
            if ((dateGirisTarih.Value.DayOfWeek == DayOfWeek.Saturday || dateGirisTarih.Value.DayOfWeek == DayOfWeek.Sunday) && chkBayram.CheckState != CheckState.Checked)
            {
                girisYukle.calisma_id = 2;
            }
            else if (chkBayram.CheckState == CheckState.Checked)
            {
                girisYukle.calisma_id = 3;
            }
            else
            {
                girisYukle.calisma_id = 1;
            }

            using (context context = new())
            {
                await context.tblGirisSiraKontrol.AddAsync(girisYukle);
                await context.SaveChangesAsync();
            }
            MessageBox.Show("Kayıt Tamamlandı");

        }

        private void btnIlgiliAySorgula_Click(object sender, EventArgs e)
        {
            DateTime basTar = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime bitTar = basTar.AddMonths(1).AddDays(-1);

            gridHaftaIci.DataSource = query.GirisBilgiAl(basTar, bitTar, 1);
            gridHaftaSonu.DataSource = query.GirisBilgiAl(basTar, bitTar, 2);


        }

        private void btnBayram_Click(object sender, EventArgs e)
        {

            DateTime basTar = new DateTime(year: DateTime.Now.Year, month: 1, day: 1);
            DateTime bitTar = basTar.AddMonths(11).AddDays(30);
            gridBayramTablo.DataSource = query.GirisBilgiAl(basTar, bitTar, 3);

        }

        private void btnTumListe_Click(object sender, EventArgs e)
        {
            gridTumListe.DataSource = query.TumGirisDataAl(dateBasTar.Value, dateBitTar.Value);
        }

        private void btnIkiTarihArasiSorgula_Click(object sender, EventArgs e)
        {

            gridHaftaIci.DataSource = query.GirisBilgiAl(dateBasTar.Value, dateBitTar.Value, 1);
            gridHaftaSonu.DataSource = query.GirisBilgiAl(dateBasTar.Value, dateBitTar.Value, 2);

        }

        private void btnSiraBelirleyici_Click(object sender, EventArgs e)
        {
            string enAzYapan = "";

            int selectedEra = (int)cmbCalismaDonem.SelectedValue;

            if (selectedEra == 3)
            {
                dateBasTar.Value = new DateTime(DateTime.Now.Year, month: 1, day: 1);
                dateBitTar.Value = new DateTime(DateTime.Now.Year, month: 12, day: 31);
            }

            List<OtoSiraVericiSinif> Liste = query.SiraVeriAl(dateBasTar.Value, dateBitTar.Value, selectedEra);
            int minValue = Liste.Min(i => i.count);
            IEnumerable<OtoSiraVericiSinif> minimumDeğerler = Liste.Where(i => i.count == minValue);

            if (minimumDeğerler.Sum(i => i.count) != 0)
            {
                foreach (OtoSiraVericiSinif i in minimumDeğerler)
                {
                    enAzYapan += " " + i.KisiAdi;
                }
                enAzYapan += " <= Sıradaki Kişi/Kişiler";
                MessageBox.Show(enAzYapan);

            }
            else
            {
                MessageBox.Show("Veri Bulunamadı");
            }
        }
    }
}
