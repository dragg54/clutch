using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clutch.Position.Response;
using clutch_employee.Position.Requests;
    
namespace Clutch.Position.Client
{
    public interface IPositionClient
    {
        Task<EmployeePositionResponse> GetEmployeePositionResource(string id);
        Task<EmployeePositionResponse> AmendPositionStatus(AmendPositionStatusRequest request);
    }
}