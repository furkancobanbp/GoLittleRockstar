using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLittleRockstar.Model
{
    public class clsKurumSirket
    {
        public int id { get; set; }
        public String ad { get; set; }
    }
    [PrimaryKey(nameof(kurum_id))]
    public class clsKurum
    {
        public int kurum_id { get; set; }
        public String kurumAdi { get; set; }
    }
    [PrimaryKey(nameof(sirket_id))]
    public class clsSirket
    {
        public int sirket_id { get; set; }
        public String sirketAdi { get; set; }
    }
    
}
