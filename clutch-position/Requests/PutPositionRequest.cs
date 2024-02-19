using clutch_position.Data;
using System.ComponentModel.DataAnnotations;

namespace clutch_position.Requests
{
    public class PutPositionRequest
    {

        [Required]
        public string PositionName { get; set; }

        [Required]
        public string PositionDescription { get; set; }

        [Required]
        public string PositionStatus { get; set; }

        [Required]
        public int Salary { get; set; }
    }
}
