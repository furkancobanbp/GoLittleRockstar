using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(Tarih),nameof(VeriTipi))]
    public class clsGrf
    {
        [JsonProperty("gasDay")]
        public DateTime Tarih { get; set; }
        [JsonProperty("price")]
        public decimal Fiyat { get; set; }
        public int period { get; set; }
        [JsonProperty("periodType")]
        public String VeriTipi { get; set; }
    }
    [Keyless]
    public class clsGrfOrtalama
    {
        public decimal ortalamaFiyat { get; set; }
    }
}
