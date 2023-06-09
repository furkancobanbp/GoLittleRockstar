﻿
using GoLittleRockstar.Model;
using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar
{
    public class context : DbContext
    {
        public DbSet<clsOsosModel> tblOsosVeri { get; set; }
        public DbSet<clsDagitimModel> tblDagitimBolgeleri { get; set; }
        public DbSet<clsPiyasaFiyatlariModel> tblPiyasaFiyatlari { get; set; }
        public DbSet<clsDgpTalimatOzet> tblDgpOzet { get; set; }
        public DbSet<clsGercekZamanliUretim> tblGercekZamanliUretimler { get; set; }
        public DbSet<clsAuf> tblAuf { get; set; }
        public DbSet<clsGrf> tblGazReferans { get; set; }
        public DbSet<clsDolarKuru> tblDolarKuru { get; set; }
        public DbSet<clsDogalgazTarife> tblBotasTarife { get; set; }
        public DbSet<clsDogalgazTarifeSource> botasTarifeSource { get; set; }
        public DbSet<clsGrfOrtalama> grfOrt { get; set; }
        public DbSet<clsEvrak> tblEvrak { get; set; }
        public DbSet<clsKurum> tblKurumlar { get; set; }
        public DbSet<clsSirket> tblSirket { get; set; }
        public DbSet<clsKurumSirket> kurumSirket { get; set; }
        public DbSet<MyForecastData> tblForecastWeatherData { get; set; }
        public DbSet<MyHistoricData> tblHistoricWeatherData { get; set; }
        public DbSet<clsAna> anaSet { get; set; }
        public DbSet<clsSehir> tblSehir { get; set; }
        public DbSet<clsTeminat> tblVerilenTeminatlar { get; set; }
        public DbSet<clsBanka> tblBankaBilgisi { get; set; }
        public DbSet<clsTeminatListe> teminatListe { get; set; }
        public DbSet<clsKisi> tblKisi { get; set; }
        public DbSet<clsCalismaDonemi> tblCalismaDonemi { get; set; }
        public DbSet<clsGirisYukle> tblGirisSiraKontrol { get; set; }
        public DbSet<clsGirisKontrol> girisKontrol { get; set; }
        public DbSet<clsIslemTuru> tblIslemTur { get; set; }
        public DbSet<TumGirisTablo> TumGirisTablos { get; set; }
        public DbSet<OtoSiraVericiSinif> SiraVerici { get; set; }
        public DbSet <clsGirisDetay> tblGirisDetayTablo { get; set; }
        public DbSet <clsGirisInceleme> giris_inceleme { get; set; }
        public DbSet <UlkeBilgi> tblYurtDisiPiyasaBilgi { get; set; }
        public DbSet <FiyatBilgi> tblYurtDisiElektrikFiyatlari { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=CeliklerDb;Username=postgres;Password=Fc123456*");

      
    }
}
