using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLittleRockstar.Model
{
    [Keyless]
    public class clsAna
    {
        public int id { get; set; }
        public String name { get; set; }    
    }
}
    