using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clutch.Employee.Position.Response;

namespace Clutch.Employee.Position.Client
{
    public interface IPositionClient
    {
        Task<EmployeePositionResponse> GetEmployeePositionResource(string id);
    }
}