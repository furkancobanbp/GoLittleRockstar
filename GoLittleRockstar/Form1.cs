using GoLittleRockstar.Functions;
using GoLittleRockstar.Model;
using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar
{
    public partial class Form1 : UserControl
    {
        queries query = new queries();
        restApi api = new restApi();
        clsPiyasaFiyatlariModel model = new clsPiyasaFiyatlariModel();
        clsDgpTalimatOzet dgpModel = new clsDgpTalimatOzet();
        List<clsPiyasaFiyatlariModel> fiyatList = new List<clsPiyasaFiyatlariModel>();
        List<clsDgpTalimatOzet> dgpList = new List<clsDgpTalimatOzet>();
        List<clsGrf> grfList = new List<clsGrf>();
        List<clsDolarKuru> dolarList = new List<clsDolarKuru>();
        List<clsAna> sehirListesi = new List<clsAna>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPtfSmfCek_Click(object sender, EventArgs e)
        {
            fiyatList = api.getPtfSmf(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
            if (fiyatList.Count == 0)
            {
                MessageBox.Show("Ýlgili Tarihte Veri Bulunamadý", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool insertOrUpdate = false;
            using (var contex = new context())
            {
                foreach (clsPiyasaFiyatlariModel i in fiyatList)
                {
                    model = (clsPiyasaFiyatlariModel)api.translateToTurkish(i);
                    try
                    {
                        contex.tblPiyasaFiyatlari.Add(model);
                        contex.SaveChanges();
                    }
                    catch
                    {
                        insertOrUpdate = true;
                    }
                    finally
                    {
                        if (insertOrUpdate)
                        {
                            contex.tblPiyasaFiyatlari.Update(model);
                            contex.SaveChanges();
                        }
                        insertOrUpdate = false;
                    }
                }
                MessageBox.Show("Ýþlem Tamamlandý");
            }

        }
        private void btnDgpDurumCek_Click(object sender, EventArgs e)
        {
            dgpList = api.dgpTalimatOzet(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
            if (dgpList.Count == 0)
            {
                MessageBox.Show("Ýlgili Tarihte Veri Bulunamadý", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool insertOrUpdate = false;
            using (var contex = new context())
            {
                foreach (clsDgpTalimatOzet i in dgpList)
                {
                    dgpModel = (clsDgpTalimatOzet)api.translateToTurkish(i);
                    try
                    {
                        contex.tblDgpOzet.Add(dgpModel);
                        contex.SaveChanges();
                    }
                    catch
                    {
                        insertOrUpdate = true;
                    }
                    finally
                    {
                        if (insertOrUpdate)
                        {
                            contex.tblDgpOzet.Update(dgpModel);
                            contex.SaveChanges();
                        }
                        insertOrUpdate = false;
                    }
                }
                MessageBox.Show("Ýþlem Tamamlandý");
            }
        }

        private void btnGrfCek_Click(object sender, EventArgs e)
        {
            grfList = api.getGrf(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
            if (grfList.Count == 0)
            {
                MessageBox.Show("Ýlgili Tarihte Veri Bulunamadý", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool insertOrUpdate = false;
            using (var contex = new context())
            {
                foreach (clsGrf i in grfList)
                {
                    if (i.VeriTipi == "DAILY")
                    {
                        i.VeriTipi = "Günlük";
                    }
                    else if (i.VeriTipi == "MONTHLY")
                    {
                        i.VeriTipi = "AYLIK";
                    }
                    else
                    {
                        i.VeriTipi = "Periyodik";
                    }
                    try
                    {
                        contex.tblGazReferans.Add(i);
                        contex.SaveChanges();
                    }
                    catch
                    {
                        insertOrUpdate = true;
                    }
                    finally
                    {
                        if (insertOrUpdate)
                        {
                            contex.tblGazReferans.Update(i);
                            contex.SaveChanges();
                        }
                        insertOrUpdate = false;
                    }
                }
                MessageBox.Show("Ýþlem Tamamlandý");
            }
        }

        private void btnDolarKuruCek_Click(object sender, EventArgs e)
        {
            dolarList = api.DolarKurAl(dateBaslangicTarihi.Value, dateBitisTarihi.Value).Result;
            if (dolarList.Count == 0)
            {
                MessageBox.Show("Ýlgili Tarihte Veri Bulunamadý", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool insertOrUpdate = false;
            using (var contex = new context())
            {
                foreach (clsDolarKuru i in dolarList)
                {
                    try
                    {
                        if (i.DolarKuru is null)
                            i.DolarKuru = 0;

                        contex.tblDolarKuru.Add(i);
                        contex.SaveChanges();
                    }
                    catch
                    {
                        insertOrUpdate = true;
                    }
                    finally
                    {
                        if (insertOrUpdate)
                        {
                            contex.tblDolarKuru.Update(i);
                            contex.SaveChanges();
                        }
                        insertOrUpdate = false;
                    }
                }
                MessageBox.Show("Ýþlem Tamamlandý");
            }
        }

        private void btnFiyatListele_Click(object sender, EventArgs e)
        {
            gridPiyasaFiyatlari.DataSource = query.piyasaFiyatCek(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
        }

        private void btnDgpListele_Click(object sender, EventArgs e)
        {
            gridDgpDurum.DataSource = query.dgpOzetCek(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
        }

        private void btnGrfListele_Click(object sender, EventArgs e)
        {
            gridGazReferans.DataSource = query.grfCek(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
        }

        private void btnDolarKuruListele_Click(object sender, EventArgs e)
        {
            gridDolarKuru.DataSource = query.DolarKuruGoster(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTarifeKaydet_Click(object sender, EventArgs e)
        {
            if (txtElektrikAmacliKullanim.Text is null || txtProsesAmacliKullanim.Text is null || txtElektrikAmacliKullanim.Text.Trim().Length == 0 || txtProsesAmacliKullanim.Text.Trim().Length == 0 || txtOtv.Text is null || txtOtv.Text.Trim().Length == 0)
            {
                MessageBox.Show("Lütfen Deðer Giriniz");
                return;
            }

            clsDogalgazTarife Tarife = new clsDogalgazTarife();
            List<decimal> TarifeFiyat = new List<decimal>();

            decimal ElektrikUretim = Convert.ToDecimal(txtElektrikAmacliKullanim.Text);
            decimal ProsesAmacli = Convert.ToDecimal(txtProsesAmacliKullanim.Text);
            int tarifeId = 1;

            DateTime Tarih = new DateTime(dateBaslangicTarihi.Value.Year, dateBaslangicTarihi.Value.Month, 1);

            TarifeFiyat.Add(ElektrikUretim);
            TarifeFiyat.Add(ProsesAmacli);

            foreach (decimal i in TarifeFiyat)
            {
                Tarife.Tarih = Tarih;
                Tarife.tarife_id = tarifeId;
                Tarife.tarifeFiyat = i;
                Tarife.otvFiyat = Convert.ToDecimal(txtOtv.Text);

                using (var contex = new context())
                {
                    contex.tblBotasTarife.Add(Tarife);
                    contex.SaveChanges();
                }
                tarifeId++;
            }
            txtElektrikAmacliKullanim.Clear();
            txtProsesAmacliKullanim.Clear();
            txtOtv.Clear();

            MessageBox.Show("Kayýt Tamamlandý", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDogalgazListele_Click(object sender, EventArgs e)
        {
            gridDogalgazTarife.DataSource = query.DogalgazTarifeAl(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
        }

        private void btnGrfOrtalama_Click(object sender, EventArgs e)
        {
            List<clsGrfOrtalama> ort = new List<clsGrfOrtalama>();

            String baslangicTarihi = dateBaslangicTarihi.Value.ToShortDateString();
            String bitisTarihi = dateBitisTarihi.Value.ToShortDateString();

            using (var contex = new context())
            {
                var Sql = "select (AVG(\"Fiyat\")/1000) \"ortalamaFiyat\" from \"tblGazReferans\" where \"Tarih\" between '" + baslangicTarihi + "' and '" + bitisTarihi + "'";
                var Liste = contex.grfOrt.FromSqlRaw(Sql).ToList();
                ort.AddRange(Liste);

            }
            gridGRFOrt.DataSource = ort;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Root> RootData = new List<Root>();
            List<MyForecastData> ForecastData = new List<MyForecastData>();

            using (var contex = new context())
            {
                var Sql = "select id, \"SehirAdi\" name from \"tblSehir\"";
                var Liste = contex.anaSet.FromSqlRaw(Sql).ToList();
                sehirListesi.AddRange(Liste);
            }

            foreach (clsAna i in sehirListesi)
            {
                RootData.Add(api.TahminHavaDurumu(dateBitisTarihi.Value, i.name));               
            }
            foreach(Root i in RootData)
            {
                ForecastData.AddRange(api.ForecastData(i));
            }
            

            using (var contex = new context())
            {
                foreach (MyForecastData i in ForecastData)
                {
                    contex.tblForecastWeatherData.AddRange(i);
                    contex.SaveChanges();
                    contex.ChangeTracker.Clear();
                }                
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<Root> RootData = new List<Root>();
            List<MyForecastData> ForecastData = new List<MyForecastData>();

            using (var contex = new context())
            {
                var Sql = "select id, \"SehirAdi\" name from \"tblSehir\"";
                var Liste = contex.anaSet.FromSqlRaw(Sql).ToList();
                sehirListesi.AddRange(Liste);
            }

            foreach (clsAna i in sehirListesi)
            {
                RootData.Add(api.GecmisHavaDurumu(dateBaslangicTarihi.Value, dateBitisTarihi.Value, i.name));
            }
            foreach (Root i in RootData)
            {
                ForecastData.AddRange(api.ForecastData(i));
            }

            using (var contex = new context())
            {
                foreach (MyForecastData i in ForecastData)
                {
                    contex.tblHistoricWeatherData.AddRange(i);
                    contex.SaveChanges();
                    contex.ChangeTracker.Clear();
                }

            }
        }
    }

}