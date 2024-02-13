using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clutch.Employee.Position.Configs;
using Clutch.Employee.Position.Response;
using Newtonsoft.Json;

namespace Clutch.Employee.Position.Client
{
    public class PositionClient : IPositionClient
    {
        private readonly HttpClient client;
        private readonly EmployeePositionConfiguration configuration;
        public PositionClient(HttpClient client, EmployeePositionConfiguration configuration)
        {
            this.client = client;
        }

        public async Task<EmployeePositionResponse> GetEmployeePositionResource(string id)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync($"{configuration.BaseURL}/{id}");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    EmployeePositionResponse response = JsonConvert.DeserializeObject<EmployeePositionResponse>(jsonResponse);
                    Console.WriteLine("Response:");
                    Console.WriteLine(response);
                    return response;
                }
                else
                {
                    Console.WriteLine(httpResponseMessage);
                    Console.WriteLine($"Failed to retrieve data. Status code: {httpResponseMessage.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}