using clutch_identity.Data;
using clutch_identity.Entities;
using clutch_identity.Requests;
using clutch_identity.Resources;
using System.Runtime.CompilerServices;

namespace clutch_identity.Extensions
{
    public static class UserExtension
    {
        public static Users ToAddUserRequest(this PostUserRequest request)
        {
            return new Users
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = PasswordManager.HashPassword(request.Password),
                Role = (Role)Enum.Parse(typeof(Role), request.Role),
                ActiveDate = DateTime.Now
            };
        }

        public static UserResource ToUserResource(this Users user)
        {
            return new UserResource
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.ToString(),
                ActiveDate = user.ActiveDate
            };
        }
    }
}
