using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class Route_
    {
        [Key]
        public double Route_id { get; set; }


        [ForeignKey("Vehicle")]
        public int Vehicle_Id { get; set; }
        public Vehicle? Vehicle { get; set; }


        public string? routes { get; set; }
    }
}
