using Microsoft.EntityFrameworkCore;

namespace GoLittleRockstar.Model
{
    [PrimaryKey(nameof(id))]
    public class clsDagitimModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
