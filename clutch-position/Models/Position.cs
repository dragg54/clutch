using clutch_position.Data;

namespace clutch_position.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string PositionDescription { get; set; }
        public PositionStatus PositionStatus { get;set; }
    }
}
