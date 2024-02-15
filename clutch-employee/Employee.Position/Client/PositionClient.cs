using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clutch.Employee.Position.Response;
using Newtonsoft.Json;

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
                HttpResponseMessage httpResponseMessage = await client.GetAsync($"{url}/api/Position/{id}");
                if(httpResponseMessage.IsSuccessStatusCode){
                    var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    var employeePositionResource = JsonConvert.DeserializeObject<EmployeePositionResponse>(jsonResponse);
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = employeePositionResource;
                    return response;
                }
                else{
                    response.StatusCode = httpResponseMessage.StatusCode;
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