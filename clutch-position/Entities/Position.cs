using clutch_position.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clutch_position.Entities
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Column("positionName")]

        public string PositionName { get; set; }

        [Column("positionDescription")]
        public string PositionDescription { get; set; }

        [Column("positionStatus")]
        public PositionStatus PositionStatus { get; set; }

        [Column("salary")]
        public int Salary { get; set; }
    }
}
