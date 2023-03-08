using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Employee_Id { get; set; }

        public string? Employee_Name { get; set; }

        public int Age { get; set; }

        public string? Employee_Location { get; set; }

        public double Phone_Number { get; set; }

        [ForeignKey("Vehicle")]
        public int Vehicle_Id { get; set; }

        public Vehicle? Vehicle { get; set; }
    }
}
