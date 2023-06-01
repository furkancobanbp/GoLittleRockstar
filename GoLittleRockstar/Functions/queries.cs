using GoLittleRockstar.Model;
using Microsoft.EntityFrameworkCore;
namespace GoLittleRockstar.Functions
{
    public class queries
    {
        public List<clsPiyasaFiyatlariModel> piyasaFiyatCek(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToString("dd-MM-yyyy 00:00:00");
            String bitisTarihi = bitTar.ToString("dd-MM-yyyy 23:59:00");

            using (var contex = new context())
            {
                var Sql = "Select * from \"tblPiyasaFiyatlari\" where \"Tarih\" between '" + baslangicTarihi + "' and '" + bitisTarihi + "' order by \"Tarih\" ASC";
                return contex.tblPiyasaFiyatlari.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsDgpTalimatOzet> dgpOzetCek(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToString("dd-MM-yyyy 00:00:00");
            String bitisTarihi = bitTar.ToString("dd-MM-yyyy 23:59:00");

            using (var contex = new context())
            {
                var Sql = "Select * from \"tblDgpOzet\" where \"Tarih\" between '" + baslangicTarihi + "' and '" + bitisTarihi + "' order by \"Tarih\" ASC ";
                return contex.tblDgpOzet.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsGrf> grfCek(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToString("dd-MM-yyyy 00:00:00");
            String bitisTarihi = bitTar.ToString("dd-MM-yyyy 23:59:00");

            using (var contex = new context())
            {
                var Sql = "Select * from \"tblGazReferans\" where \"Tarih\" between '" + baslangicTarihi + "' and '" + bitisTarihi + "' order by \"Tarih\" ASC";
                return contex.tblGazReferans.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsDolarKuru> DolarKuruGoster(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToShortDateString();
            String bitisTarihi = bitTar.ToShortDateString();

            using (var contex = new context())
            {
                var Sql = "Select * from \"tblDolarKuru\" where \"Tarih\" between '" + baslangicTarihi + "' and '" + bitisTarihi + "' and  " +
                          "\"DolarKuru\" not in(" + 0.00000 + ") order by \"Tarih\" ASC";
                return contex.tblDolarKuru.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsGercekZamanliUretim> uretimGoster(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToString("dd-MM-yyyy 00:00:00");
            String bitisTarihi = bitTar.ToString("dd-MM-yyyy 23:59:00");
            using (var contex = new context())
            {
                var Sql = "Select * from \"tblGercekZamanliUretimler\" where \"Tarih\" between '" + baslangicTarihi + "' and '" + bitisTarihi + "' order by \"Tarih\" ASC ";
                return contex.tblGercekZamanliUretimler.FromSqlRaw(Sql).ToList();
            }
        }


        public List<clsDogalgazTarifeSource> DogalgazTarifeAl(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToShortDateString();
            String bitisTarihi = bitTar.ToShortDateString();

            using (var contex = new context())
            {
                var Sql = "Select " +
                                "a.\"Tarih\", " +
                                " b.\"tarifeAdi\", " +
                                "a.\"tarifeFiyat\", " +
                                "a.\"otvFiyat\" " +
                                "from " +
                                "\"tblBotasTarife\" a, " +
                                "\"tblBotasTarifeTipi\" b " +
                                "where a.tarife_id = b.tarife_id " +
                                "and a.\"Tarih\" between '" + baslangicTarihi + "' " +
                                "and '" + bitisTarihi + "' " +
                                "order by b.\"tarifeAdi\", a.\"Tarih\"";

                return contex.botasTarifeSource.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsTeminatListe> TeminatBilgisiAl(String Query)
        {
            using (var contex = new context())
            {
                var Sql = "select " +
                "\"tblVerilenTeminatlar\".\"Tarih\", " +
                "\"tblVerilenTeminatlar\".\"teminatBilgi\", " +
                "\"tblVerilenTeminatlar\".\"MektupNo\", " +
                "\"tblTeminatTip\".\"TipAdi\", " +
                 "\"tblTeminatTur\".\"TurAdi\", " +
                "\"tblBankaBilgisi\".\"BankaAdi\", " +
                "\"tblKurumlar\".\"kurumAdi\", " +
                "\"tblSirket\".\"sirketAdi\", " +
                "\"tblVerilenTeminatlar\".\"TeminatTutari\" " +
                "from \"tblVerilenTeminatlar\" " +
                "left outer join \"tblBankaBilgisi\" on \"tblVerilenTeminatlar\".banka_id = \"tblBankaBilgisi\".banka_id " +
                "left outer join \"tblKurumlar\" on \"tblVerilenTeminatlar\".kurum_id = \"tblKurumlar\".kurum_id " +
                "left outer join \"tblSirket\" on \"tblVerilenTeminatlar\".sirket_id = \"tblSirket\".sirket_id " +
                "left outer join \"tblTeminatTur\" on \"tblVerilenTeminatlar\".tur_id = \"tblTeminatTur\".tur_id " +
                "left outer join \"tblTeminatTip\" on \"tblVerilenTeminatlar\".tip_id = \"tblTeminatTip\".tip_id " + Query +
                " order by \"tblKurumlar\".\"kurumAdi\" asc";

                return contex.teminatListe.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsGirisKontrol> GirisBilgiAl(DateTime BasTar, DateTime BitTar, int calismaDonemId)
        {
            String BaslangicTarihi = BasTar.ToShortDateString();
            String BitisTarihi = BitTar.ToShortDateString();

            using (var contex = new context())
            {
                var Sql = "select " +
                          "\"tblKisi\".\"KisiAdi\", " +
                          "\"tblIslemTur\".\"islemAdi\", " +
                           "Count(\"tblKisi\".\"KisiAdi\") " +
                            "from \"tblGirisSiraKontrol\" " +
                            "right join \"tblKisi\" on \"tblGirisSiraKontrol\".kisi_id = \"tblKisi\".kisi_id " +
                            "inner join \"tblIslemTur\" on \"tblGirisSiraKontrol\".islem_id = \"tblIslemTur\".islem_id " +
                            "where \"tblGirisSiraKontrol\".\"Tarih\" between '" + BaslangicTarihi + "' and '" + BitisTarihi + "' " +
                            "and \"tblGirisSiraKontrol\".calisma_id = " + calismaDonemId + "" +
                            " group by \"tblKisi\".\"KisiAdi\", \"tblIslemTur\".\"islemAdi\" " +
                            "order by \"tblKisi\".\"KisiAdi\", \"tblIslemTur\".\"islemAdi\" asc";

                return contex.girisKontrol.FromSqlRaw(Sql).ToList();
            }
        }
        public List<TumGirisTablo> TumGirisDataAl(DateTime BasTar, DateTime BitTar)
        {
            String BaslangicTarihi = BasTar.ToShortDateString();
            String BitisTarihi = BitTar.ToShortDateString();

            using (var contex = new context())
            {
                var Sql = "select " +
                            "\"tblGirisSiraKontrol\".\"Tarih\"," +
                            "\"tblKisi\".\"KisiAdi\"," +
                            "\"tblIslemTur\".\"islemAdi\", " +
                            "\"tblCalismaDonemi\".\"CalismaAdi\" " +
                            "from " +
                            "\"tblGirisSiraKontrol\" " +
                            "left join \"tblKisi\" on \"tblGirisSiraKontrol\".kisi_id = \"tblKisi\".kisi_id " +
                            "left join \"tblIslemTur\" on \"tblGirisSiraKontrol\".islem_id = \"tblIslemTur\".islem_id " +
                            "left join \"tblCalismaDonemi\" on \"tblGirisSiraKontrol\".calisma_id = \"tblCalismaDonemi\".calisma_id " +
                            "where \"tblGirisSiraKontrol\".\"Tarih\" between '" + BaslangicTarihi + "' and '" + BitisTarihi + "' " +
                            "order by \"tblGirisSiraKontrol\".\"Tarih\" desc ";

                return contex.TumGirisTablos.FromSqlRaw(Sql).ToList();
            }

        }
        public List<OtoSiraVericiSinif> SiraVeriAl(DateTime BasTar, DateTime BitTar, int calismaDonemId)
        {
            String BaslangicTarihi = BasTar.ToShortDateString();
            String BitisTarihi = BitTar.ToShortDateString();

            using (var contex = new context())
            {
                var Sql = "select \"tblKisi\".\"KisiAdi\", " +
                            "Count(\"tblGirisSiraKontrol\".calisma_id) " +
                            "from \"tblGirisSiraKontrol\" " +
                            "right join \"tblKisi\" " +
                            "on \"tblGirisSiraKontrol\".kisi_id = \"tblKisi\".kisi_id " +
                            "and \"tblGirisSiraKontrol\".calisma_id = " + calismaDonemId + " " +
                            "and \"tblGirisSiraKontrol\".\"Tarih\" " +
                            "between '" + BaslangicTarihi + "' and '" + BitisTarihi + "' " +
                            "group by \"tblKisi\".\"KisiAdi\" " +
                            "order by \"tblKisi\".\"KisiAdi\" asc ";

                return contex.SiraVerici.FromSqlRaw(Sql).ToList();
            }
        }
        public List<clsGirisInceleme> GirisVeriAl(DateTime BasTar, DateTime BitTar, int calismaDonemId)
        {

            using (context contex = new())
            {
                var Sql = "SELECT " +
                            "\"tblKisi\".\"KisiAdi\", " +
                            "\"tblIslemTur\".\"islemAdi\", " +
                            "SUM(EXTRACT " +
                            "(DAY FROM age(\"tblGirisDetayTablo\".\"IslemBitisTarihi\", \"tblGirisDetayTablo\".\"IslemBaslangicTarihi\")) * 24 + " +
                            "EXTRACT " +
                            "(HOUR FROM age(\"tblGirisDetayTablo\".\"IslemBitisTarihi\", \"tblGirisDetayTablo\".\"IslemBaslangicTarihi\"))) AS Saat, " +
                            "SUM(EXTRACT " +
                            "(MINUTE FROM age(\"tblGirisDetayTablo\".\"IslemBitisTarihi\", \"tblGirisDetayTablo\".\"IslemBaslangicTarihi\"))) AS Dakika " +
                            "FROM \"tblGirisDetayTablo\" " +
                            "LEFT JOIN \"tblKisi\" on \"tblGirisDetayTablo\".kisi_id = \"tblKisi\".kisi_id " +
                            "LEFT JOIN \"tblIslemTur\" on \"tblGirisDetayTablo\".islem_id = \"tblIslemTur\".islem_id " +
                            "WHERE \"tblGirisDetayTablo\".calisma_id = " + calismaDonemId + "" +
                            "AND \"tblGirisDetayTablo\".\"IslemBaslangicTarihi\" >= '" + BasTar + "' " +
                            "AND \"tblGirisDetayTablo\".\"IslemBitisTarihi\" <= '" + BitTar + "' " +
                            "AND \"tblGirisDetayTablo\".\"islem_id\" = 2 " +
                            "group by " +
                            "\"tblKisi\".\"KisiAdi\", " +                            
                            "\"tblIslemTur\".\"islemAdi\" "+               
                            "UNION ALL "+
                            "SELECT "+
                            "\"tblKisi\".\"KisiAdi\", "+ 
                            "\"tblIslemTur\".\"islemAdi\", " + 
                            "count(\"tblIslemTur\".islem_id) AS Saat, "+
                            "0 AS Dakika "+
                            "FROM "+ 
                            "\"tblGirisDetayTablo\" "+
                            "LEFT JOIN \"tblKisi\" on \"tblGirisDetayTablo\".kisi_id = \"tblKisi\".kisi_id " +
                            "LEFT JOIN \"tblIslemTur\" on \"tblGirisDetayTablo\".islem_id = \"tblIslemTur\".islem_id " +
                            "WHERE \"tblGirisDetayTablo\".calisma_id = " + calismaDonemId + "" +
                            "AND \"tblGirisDetayTablo\".\"IslemBaslangicTarihi\" >= '" + BasTar + "' " +
                            "AND \"tblGirisDetayTablo\".\"IslemBitisTarihi\" <= '" + BitTar + "' " +
                            "AND \"tblGirisDetayTablo\".\"islem_id\" = 1 " +
                            "group by " +
                            "\"tblKisi\".\"KisiAdi\", " +                            
                            "\"tblIslemTur\".\"islemAdi\" ";
                

                return contex.giris_inceleme.FromSqlRaw(Sql).ToList();
            }

        }
    }
}
