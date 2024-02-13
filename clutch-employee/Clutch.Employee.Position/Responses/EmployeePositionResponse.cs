using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clutch.Employee.Position.Response
{
    public class EmployeePositionResponse
    {
        public int Id { get; set; }

        public string PositionName { get; set; }

        public string PositionDescription { get; set; }

        public string PositionStatus { get; set; }

        public int Salary { get; set; }
    }
}