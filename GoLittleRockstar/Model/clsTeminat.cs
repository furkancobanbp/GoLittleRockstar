using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(Tarih), nameof(MektupNo))]
    public class clsTeminat
    {
        public DateTime Tarih { get; set; }
        public String teminatBilgi { get; set; }
        public int kurum_id { get; set; }
        public int sirket_id { get; set; }
        public int tip_id { get; set; }
        public int tur_id { get; set; }
        public int banka_id { get; set; }
        public String MektupNo { get; set; }
        public decimal TeminatTutari { get; set; }

    }
    [Keyless]
    public class clsTeminatListe 
    {
        public String kurumAdi { get; set; }
        public String sirketAdi { get; set; }
        public String TipAdi { get; set; }
        public String TurAdi { get; set; }
        public String BankaAdi { get; set; }    
        public DateTime Tarih { get; set; }
        public String MektupNo { get; set; }
        public String teminatBilgi { get; set; }
        public decimal TeminatTutari { get; set; }
    }
}
