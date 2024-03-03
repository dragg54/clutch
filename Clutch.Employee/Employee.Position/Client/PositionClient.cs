using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clutch.Employee.Position.Response;
using System.Text.Json;
using clutch_employee.Position.Entities;

namespace Clutch.Employee.Position.Client
{
    public class PositionClient : IPositionClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;
        public PositionClient(HttpClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }

        public async Task<EmployeePositionResponse> GetEmployeePositionResource(string id)
        {
            EmployeePositionResponse response = new();
            try
            {
                var url = configuration.GetSection("EmployeePosition")["BaseURL"];
                EmployeePosition employeePosition = await client.GetFromJsonAsync<EmployeePosition>($"{url}/api/Position/{id}");
                if(employeePosition is not null){
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = employeePosition;
                    return response;
                }
                else{
                    response.Message = $"Position with {id} not found";
                    return response;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return response;
            }
        }

    }
}