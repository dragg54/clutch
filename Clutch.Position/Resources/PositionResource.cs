using clutch_position.Data;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace clutch_position.Resources
{
    public class PositionResource
    {
        public int Id { get; set; }

        public string PositionName { get; set; }

        public string PositionDescription { get; set; }

        public string PositionStatus { get; set; }

        public int Salary { get; set; }
    }
}
