using clutch_position.Data;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace clutch_position.Resources
{
    public class PositionResource
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
