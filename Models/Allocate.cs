using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject1.Models
{
    public class Allocate
    {
        [Key]
        public int allocateid { get; set; }
        [ForeignKey("Employee")]
        public int Employee_Id { get; set; }

        public Employee? Employee { get; set; }

        [ForeignKey("Vehicle")]
        public int Vehicle_Id { get; set; }

        public Vehicle? Vehicle { get; set; }

        [ForeignKey("Route_")]

        public double Route_id { get; set; }

        public Route_? Route_ { get; set; }  
    }
}
