using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using clutch_employee.Identity.Requests;
using clutch_employee.Identity.Responses;

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
    }
}