using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace clutch_employee.Position.Entities
{
    public class EmployeePosition
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("positionName")]
        public string PositionName { get; set; }

        [JsonProperty("positionDescription")]
        public string PositionDescription { get; set; }

        [JsonProperty("positionStatus")]
        public string PositionStatus { get; set; }

        [JsonProperty("salary")]
        public int Salary { get; set; }
    }
}
