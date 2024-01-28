using clutch_position.Data;
using System.ComponentModel.DataAnnotations;

namespace clutch_position.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        public string PositionName { get; set; }

        public string PositionDescription { get; set; }

        public PositionStatus PositionStatus { get; set; }

        public int Salary { get; set; }
    }
}
