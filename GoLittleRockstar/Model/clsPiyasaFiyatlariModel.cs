using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using GoLittleRockstar.Functions;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(Tarih))]
    public class clsPiyasaFiyatlariModel : restApi.SistemYonu
    {
        [JsonProperty("date")]
        public DateTime Tarih { get; set; }
        [JsonProperty("mcp")]
        public decimal? PTF { get; set; }
        [JsonProperty("smp")]
        public decimal? SMF { get; set; }
        [JsonProperty("smpDirection")]
        public String? SistemYonu { get; set; }
    }
}
