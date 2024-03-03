using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clutch_employee.Identity.Requests;
using clutch_employee.Identity.Responses;

namespace clutch_employee.Identity.Client
{
    public interface IIdentityClient
    {
        Task<UserResponse> PostUser(PostUserRequest request);
        Task<UserResponse> PutUser(AmendUserRequest request);
        Task<UserResponse> GetUserByEmail(string email);
        Task<UserResponse> GetUserResource(string id);
    }
}