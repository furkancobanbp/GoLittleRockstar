using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(IslemBaslangicTarihi), nameof(IslemBitisTarihi), nameof(kisi_id), nameof(islem_id))]
    public class clsGirisDetay
    {
        public DateTime IslemBaslangicTarihi { get; set; }
        public DateTime IslemBitisTarihi { get; set; }
        public int kisi_id { get; set; }
        public int islem_id { get; set; }
        public int calisma_id { get; set; }
        public String Aciklama { get; set; }
    }
    [Keyless]
    public class clsGirisInceleme
    {
        public string? KisiAdi { get; set; }        
        public string? islemAdi { get; set; }
        public int? Saat { get; set; }
        public int? Dakika { get; set; }
    }
    [Keyless]
    public class clsGirisGop
    {
        public string? KisiAdi { get; set; }
        public string? islemAdi { get; set; }
        public int count { get; set; }
    }
}
