using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(eic), nameof(olcumZamani))]
    public class clsOsosModel
    {
        
        public String eic { get; set; }        
        public DateTime olcumZamani { get; set; }
        public int period { get; set; }
        public decimal tuketim { get; set; }
        public decimal uretim { get; set; }
        public decimal cekisEnduktif { get; set; }
        public decimal cekisKapasitif { get; set; }
        public decimal verisEnduktif { get; set; }
        public decimal verisKapasitif { get; set; }

    }
}
