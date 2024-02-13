using clutch_employee.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace clutch_employee.Resource
{
    public class EmployeeResource
    {
        [JsonProperty("id")]
        public long Id {get; set;}

        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty("positionId")]
        public string PositionId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("employeeStatus")]
        public string EmployeeStatus { get; set; }
    }
}
