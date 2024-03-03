using clutch_identity.Entities;
using clutch_identity.Requests;
using clutch_identity.Resources;

namespace clutch_identity.Services
{
    public interface IUserService
    {
        Task PostUser(PostUserRequest request);
        Task<UserResource> GetUser(long userId);
        // Task<List<UserResource>> GetUsers();
        Task<string> LoginUser(LoginUserRequest request);
        Task DeleteUser(string userId);
        Task PutUser(string id, PutUserRequest request);
        Task <List<UserResource>> GetUsers(UserRequestQuery userRequestQuery);
    }
}
