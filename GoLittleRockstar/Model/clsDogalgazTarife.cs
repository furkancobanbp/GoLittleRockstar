using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(Tarih), nameof(tarife_id))]
    public class clsDogalgazTarife 
    {
        public DateTime Tarih { get; set; }
        public int tarife_id { get; set; }
        public decimal tarifeFiyat { get; set; }
        public decimal otvFiyat { get; set; }
    }
    [Keyless]
    public class clsDogalgazTarifeSource
    {
        public DateTime Tarih { get; set; }
        public String tarifeAdi { get; set; }
        public decimal tarifeFiyat { get; set; }
        public decimal otvFiyat { get; set; }
    }
    
}
