using clutch_identity.Data.Contexts;
using clutch_identity.Exceptions;
using clutch_identity.Requests;
using Microsoft.EntityFrameworkCore;

namespace clutch_identity.Services
{
    public class UserService: IUserService
    {
        private readonly UserDBContext userDbContext;
        public UserService(UserDBContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        public async Task CreateUser(PostUserRequest request)
        {
            var existingUser = userDbContext.Users.SingleOrDefaultAsync(
                user => user.Email == request.Email
                );
            if(existingUser is not null)
            {
                throw new DuplicateUserException();
            }
            var newUser = request.
        }
    }
}
