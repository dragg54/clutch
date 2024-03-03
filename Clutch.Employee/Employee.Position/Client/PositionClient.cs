using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clutch.Position.Response;
using System.Text.Json;
using clutch_employee.Position.Entities;
using clutch_employee.Identity.Requests;
using clutch_employee.Identity.Responses;
using clutch_employee.Position.Requests;

namespace Clutch.Position.Client
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
                if (employeePosition is not null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = employeePosition;
                    return response;
                }
                else
                {
                    response.Message = $"Position with {id} not found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return response;
            }
        }

        public async Task<EmployeePositionResponse> AmendPositionStatus(AmendPositionStatusRequest request)
        {
            try
            {
                string url = configuration.GetSection("EmployeePosition")["BaseURL"];
                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PatchAsync($"{url}/api/position/{request.Id}/state", content);
                EmployeePositionResponse positionResponse = null;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    positionResponse = new EmployeePositionResponse
                    {
                        Data = response,
                        Message = response.RequestMessage.ToString(),
                        StatusCode = response.StatusCode,
                    };
                }
                else
                {
                    positionResponse = new EmployeePositionResponse
                    {
                        Message = response.RequestMessage.ToString(),
                        StatusCode = response.StatusCode,
                    };
                }
                return positionResponse;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
    }

}