using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using clutch_employee.Identity.Requests;
using clutch_employee.Identity.Responses;
using clutch_employee.Position.Entities;

namespace clutch_employee.Identity.Client
{
    public class IdentityClient : IIdentityClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;
        public IdentityClient(HttpClient client, IConfiguration config)
        {
            this.client = client;
            this.config = config;
        }
        public async Task<UserResponse> PostUser(PostUserRequest request)
        {
            try
            {
                string url = config.GetSection("EmployeeIdentity")["BaseURL"];
                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{url}/api/user", content);
                UserResponse userResponse;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    userResponse = new UserResponse
                    {
                        Data = response,
                        Message = response.RequestMessage.ToString(),
                        StatusCode = response.StatusCode,
                    };
                }
                else
                {
                     userResponse = new UserResponse
                    {
                        Message = response.RequestMessage.ToString(),
                        StatusCode = response.StatusCode,
                    };
                }
                return userResponse;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public async Task<UserResponse> PutUser(AmendUserRequest request)
        {
            try
            {
                string url = config.GetSection("EmployeeIdentity")["BaseURL"];
                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{url}/api/user/{request.Id}", content);
                UserResponse userResponse;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    userResponse = new UserResponse
                    {
                        Data = response,
                        Message = response.RequestMessage.ToString(),
                        StatusCode = response.StatusCode,
                    };
                }
                else
                {
                    userResponse = new UserResponse
                    {
                        Message = response.RequestMessage.ToString(),
                        StatusCode = response.StatusCode,
                    };
                }
                return userResponse;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public async Task<UserResponse> GetUserResource(string id)
        {
            UserResponse response = new();
            try
            {
                var url = config.GetSection("User")["BaseURL"];
                EmployeePosition employeePosition = await client.GetFromJsonAsync<EmployeePosition>($"{url}/api/User/{id}");
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

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            UserResponse response = new();
            try
            {
                var url = config.GetSection("User")["BaseURL"];
                EmployeePosition employeePosition = await client.GetFromJsonAsync<EmployeePosition>($"{url}/api/User?Email={email}");
                if (employeePosition is not null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = employeePosition;
                    return response;
                }
                else
                {
                    response.Message = $"Position with email {email} not found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return response;
            }
        }
    }
}