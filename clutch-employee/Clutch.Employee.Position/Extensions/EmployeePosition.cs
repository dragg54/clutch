using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clutch.Employee.Position.Response;

namespace Clutch.Employee.Position.Extensions
{
    public class EmployeePosition
    {
        public static EmployeePositionResource ToEmployeeResource(this String jsonResponse)
        {
            return new EmployeePositionResource
            {
                PositionName = jsonResponse.PositionName,
                PositionDescription = jsonResponse.PositionDescription,
                Salary = jsonResponse.Salary,
                PositionStatus = jsonResponse.Empty
            };
        }
    }
}