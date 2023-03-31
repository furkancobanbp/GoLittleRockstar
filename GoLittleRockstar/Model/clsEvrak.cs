using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(evrak_id), nameof(sirket_id))]
    public class clsEvrak
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int evrak_id { get; set; }
        public int? sirket_id { get; set; }
        public int? kurum_id { get; set; }
        public DateTime? Tarih { get; set; }
        public bool? KepDurum { get; set; }
        public String? Aciklama { get; set; }
    }
}
