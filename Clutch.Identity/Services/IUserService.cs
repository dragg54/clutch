using clutch_identity.Entities;
using clutch_identity.Requests;

namespace clutch_identity.Services
{
    public interface IUserService
    {
        void PostUser(PostUserRequest request);
        Task<Users> GetUser(long userId);
        Task<String> LoginUser(LoginUserRequest request);
        void DeleteUser(string userId);
    }
}
