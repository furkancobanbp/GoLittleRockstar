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
        public int kurum_id { get; set; }
        public int sirket_id { get; set; }
        public int tip_id { get; set; }
        public int banka_id { get; set; }
        public String MektupNo { get; set; }
        public decimal TeminatTutari { get; set; }

    }
}
