using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(Tarih))]
    public class clsDolarKuru
    {       
        public DateTime Tarih { get; set; }
        [JsonProperty("TP_DK_USD_S_YTL")]
        public decimal? DolarKuru { get; set; }          

    }
}
