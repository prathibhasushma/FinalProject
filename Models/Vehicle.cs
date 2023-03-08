using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vehicle_Id { get; set; }

        public double vehicle_capacity { get; set; }

        public double available_seats { get; set; }
    }
}
