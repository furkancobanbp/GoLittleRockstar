using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(kisi_id))]
    public class clsKisi
    {
        public int kisi_id { get; set; }
        public String KisiAdi { get; set; }
    }
    [PrimaryKey(nameof(calisma_id))]
    public class clsCalismaDonemi
    {
        public int calisma_id { get; set; }
        public String CalismaAdi { get; set; }
    }
    [PrimaryKey(nameof(islem_id))]
    public class clsIslemTuru
    {
        public int islem_id { get; set; }
        public String islemAdi { get; set; }
    }
    [PrimaryKey(nameof(Tarih), nameof(kisi_id), nameof(islem_id))]
    public class clsGirisYukle
    {
        public DateTime Tarih { get; set; }
        public int kisi_id { get; set; }
        public int calisma_id { get; set; }
        public int islem_id { get; set; }
    }
    [Keyless]
    public class clsGirisKontrol
    {
        public String KisiAdi { get; set; }
        public String islemAdi { get; set; }
        public int count { get; set; }
        
    }
    [Keyless]
    public class TumGirisTablo
    {
        public DateTime Tarih { get; set; }
        public String KisiAdi { get; set; }
        public String islemAdi { get; set; }
        public String CalismaAdi { get; set; }
    }
    [Keyless]
    public class OtoSiraVericiSinif
    {
        public String KisiAdi { get; set; }
        public int count { get; set; }
    }
}
