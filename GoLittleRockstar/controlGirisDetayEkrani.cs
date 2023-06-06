using GoLittleRockstar.Functions;
using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar.Model
{
    public partial class controlGirisDetayEkrani : UserControl
    {
        public List<clsKisi> KisiListe;
        public clsGirisDetay girisYukle;
        public List<clsIslemTuru> islemTur;
        public List<clsCalismaDonemi> calismaDonemi;
        public List<clsIslemTuru> islemTurSira;
        public queries query = new();
        public restApi api = new();
        public controlGirisDetayEkrani()
        {
            InitializeComponent();
        }
        private async void controlGirisDetayEkrani_Load(object sender, EventArgs e)
        {
            KisiListe = new();
            girisYukle = new();
            islemTur = new();
            calismaDonemi = new();
            islemTurSira = new();

            using (context context = new())
            {
                var Sql = "select * from \"tblKisi\"";
                var KisiList = await context.tblKisi.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                KisiListe.AddRange(KisiList);

                Sql = "select * from \"tblIslemTur\"";
                var IslemList = await context.tblIslemTur.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                islemTur.AddRange(IslemList);
                islemTurSira.AddRange(IslemList);

                Sql = "select * from \"tblCalismaDonemi\"";
                var CalismaDonem = await context.tblCalismaDonemi.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                calismaDonemi.AddRange(CalismaDonem);

            }
            cmbKisi.DataSource = KisiListe;
            cmbIslemTur.DataSource = islemTur;
            cmbSiraTur.DataSource = islemTurSira;

            cmbKisi.DataBindings.Add("SelectedValue", girisYukle, "kisi_id", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbIslemTur.DataBindings.Add("SelectedValue", girisYukle, "islem_id", true, DataSourceUpdateMode.OnPropertyChanged);
            txtAciklama.DataBindings.Add("Text", girisYukle, "Aciklama", true, DataSourceUpdateMode.OnPropertyChanged);

        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            DateTime BasTar = new DateTime(dateIslemBaslangicTarihi.Value.Year, dateIslemBaslangicTarihi.Value.Month, dateIslemBaslangicTarihi.Value.Day, (int)numericBaslangicSaat.Value, (int)numericBaslangicDakika.Value, 0, 0);
            DateTime BitTar = new DateTime(dateIslemBitisTarihi.Value.Year, dateIslemBitisTarihi.Value.Month, dateIslemBitisTarihi.Value.Day, (int)numericBitisSaati.Value, (int)numericBitisDakikasi.Value, 0, 0);

            girisYukle.IslemBaslangicTarihi = BasTar;
            girisYukle.IslemBitisTarihi = BitTar;

            if (!(BasTar.DayOfWeek == DayOfWeek.Saturday || BitTar.DayOfWeek == DayOfWeek.Sunday) && chkBayramKontrol.CheckState != CheckState.Checked)
            {
                girisYukle.calisma_id = 1;
            }
            else if (chkBayramKontrol.CheckState == CheckState.Checked)
            {
                girisYukle.calisma_id = 3;
            }
            else
            {
                girisYukle.calisma_id = 2;
            }

            if ((int)cmbIslemTur.SelectedValue == 1)
            {
                girisYukle.Aciklama = "Gün Öncesi Piyasası ve DGP İşlemleri Yapıldı.";
                girisYukle.IslemBaslangicTarihi = BasTar;
                girisYukle.IslemBitisTarihi = BasTar;
            }


            using (context context = new())
            {
                context.tblGirisDetayTablo.Add(girisYukle);
                context.SaveChanges();
            }

            MessageBox.Show("İşlem Tamamlandı");
        }
        private void btnIcındekiAySorgula_Click(object sender, EventArgs e)
        {
            List<clsGirisInceleme> HaftaIci = new List<clsGirisInceleme>();
            List<clsGirisInceleme> HaftaSonu = new List<clsGirisInceleme>();

            DateTime BasTar = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime BitTar = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 0);

            HaftaIci = query.GirisVeriAl(BasTar, BitTar, 1);
            HaftaSonu = query.GirisVeriAl(BasTar, BitTar, 2);

            gridHaftaIci.DataSource = HaftaIci;
            gridHaftaSonu.DataSource = HaftaSonu;
        }
        private void btnAralikSorgula_Click(object sender, EventArgs e)
        {
            List<clsGirisInceleme> HaftaIci = new List<clsGirisInceleme>();
            List<clsGirisInceleme> HaftaSonu = new List<clsGirisInceleme>();

            DateTime BasTar = new DateTime(dateIslemBaslangicTarihi.Value.Year, dateIslemBaslangicTarihi.Value.Month, dateIslemBaslangicTarihi.Value.Day, 0, 0, 0);
            DateTime BitTar = new DateTime(dateIslemBitisTarihi.Value.Year, dateIslemBitisTarihi.Value.Month, dateIslemBitisTarihi.Value.Day, 23, 59, 59);

            HaftaIci = query.GirisVeriAl(BasTar, BitTar, 1);
            HaftaSonu = query.GirisVeriAl(BasTar, BitTar, 2);

            gridHaftaIci.DataSource = HaftaIci;
            gridHaftaSonu.DataSource = HaftaSonu;
        }
        private void btnBayramSorgula_Click(object sender, EventArgs e)
        {
            List<clsGirisInceleme> Bayram = new List<clsGirisInceleme>();

            DateTime BasTar = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            DateTime BitTar = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);

            Bayram = query.GirisVeriAl(BasTar, BitTar, 3);

            gridBayram.DataSource = Bayram;
        }
        private void cmbIslemTur_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAciklama.Enabled = true;

            if (cmbIslemTur.SelectedValue is null || (int)cmbIslemTur.SelectedValue == 1)
            {
                txtAciklama.Enabled = false;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            List<UlkeBilgi> PiyasaBilgi = new();
            List<FiyatBilgi> FiyatData = new();
            PublicationMarketDocument doc = new();

            DateTime BasTar = new DateTime(
                dateIslemBaslangicTarihi.Value.Year,
                dateIslemBaslangicTarihi.Value.Month,
                dateIslemBaslangicTarihi.Value.Day,
                0,
                0,
                0);

            DateTime BitTar = new DateTime(
                dateIslemBitisTarihi.Value.Year,
                dateIslemBitisTarihi.Value.Month,
                dateIslemBitisTarihi.Value.Day,
                1,
                0,
                0);

            using (context context = new())
            {
                var Sql = "select * from \"tblYurtDisiPiyasaBilgi\"";
                var UlkeList = await context.tblYurtDisiPiyasaBilgi.FromSqlRaw(Sql).AsNoTracking().ToListAsync();
                PiyasaBilgi.AddRange(UlkeList);


                foreach (UlkeBilgi i in PiyasaBilgi)
                {
                    string MarketCode = i.ApiKodu;
                    doc = await api.EntsoMarketData(MarketCode, BasTar, BitTar);

                    try
                    {
                        foreach (TimeSeries j in doc.TimeSeries)
                        {
                            foreach (Point k in j.Period.Point)
                            {
                                string dateString = j.Period.TimeInterval.Start.Substring(0, 10);
                                string format = "yyyy-MM-dd";
                                DateTime Date = DateTime.ParseExact(dateString, format, null).AddDays(1);
                                TimeSpan timeValue = new();

                                if (!(j.Period.Resolution == "PT60M"))
                                {
                                    int timeData = k.Position;

                                    if (j.Period.Resolution == "PT15M")
                                    {
                                        timeValue = TimeSpan.FromMinutes(timeData * 15);

                                    }
                                    else if (j.Period.Resolution == "PT30M")
                                    {
                                        timeValue = TimeSpan.FromMinutes(timeData * 30);
                                    }

                                    FiyatBilgi Fiyat = new FiyatBilgi(Date, timeValue, i.piyasa_id, j.Period.Resolution, k.PriceAmount);
                                    FiyatData.Add(Fiyat);

                                }
                                else
                                {
                                    timeValue = TimeSpan.FromHours(k.Position - 1);

                                    FiyatBilgi Fiyat = new FiyatBilgi(Date, timeValue, i.piyasa_id, j.Period.Resolution, k.PriceAmount);
                                    FiyatData.Add(Fiyat);
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine(i.PiyasaAdi + " Ait Veri Alınırken Hata Oluştu");
                        continue;
                    }
                }
                await context.tblYurtDisiElektrikFiyatlari.AddRangeAsync(FiyatData);
                await context.SaveChangesAsync();

                MessageBox.Show("Yurt Dışı Verileri Çekildi");
            }

        }
    }
}

