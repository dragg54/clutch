using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace clutch_employee.Position.Entities
{
    public class EmployeePosition
    {
        public int Id { get; set; }

        public string PositionName { get; set; }

        public string PositionDescription { get; set; }

        public string PositionStatus { get; set; }

        public int Salary { get; set; }
    }
}
