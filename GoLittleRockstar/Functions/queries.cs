﻿using GoLittleRockstar.Model;
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
                var Sql = "Select "+
                                "a.\"Tarih\", " +
                                " b.\"tarifeAdi\", " +	                            
	                            "a.\"tarifeFiyat\", "+
                                "a.\"otvFiyat\" " +
                                "from " +
                                "\"tblBotasTarife\" a, "+
	                            "\"tblBotasTarifeTipi\" b " +
                                "where a.tarife_id = b.tarife_id "+
                                "and a.\"Tarih\" between '"+baslangicTarihi+"' " +
                                "and '"+bitisTarihi+"' "+
                                "order by b.\"tarifeAdi\", a.\"Tarih\"";

                return contex.botasTarifeSource.FromSqlRaw(Sql).ToList();
            }
        }
    }
}