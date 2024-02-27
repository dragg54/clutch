using clutch_identity.Data.Contexts;
using clutch_identity.Entities;
using clutch_identity.Exceptions;
using clutch_identity.Extensions;
using clutch_identity.Requests;
using clutch_identity.Resources;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace clutch_identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserDBContext userDbContext;
        private readonly IConfiguration config;
        public UserService(UserDBContext userDbContext, IConfiguration config)
        {
            this.userDbContext = userDbContext;
            this.config= config;
        }
        public async Task PostUser(PostUserRequest request)
        {
            try
            {
                var existingUser = userDbContext.Users.SingleOrDefaultAsync(
                user => user.Email == request.Email
                );
                if (existingUser is not null)
                {
                    var msg = "User already exists";
                    throw new DuplicateException(msg);
                }
                var newUser = request.ToAddUserRequest();
                userDbContext.SaveChanges();
            }
            catch (DuplicateException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<UserResource> GetUser(long id)
        {
            try
            {
                var user = await userDbContext.Users.SingleOrDefaultAsync(user => user.Id == id);
                if (user is null)
                {
                    var msg = $"User with id {id} does not exist";
                    throw new NotFoundException(msg);
                }
                return user.ToUserResource();
            }
            catch(NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<UserResource>> GetUsers()
        {
            try
            {
                var users = await userDbContext.Users
                .ToListAsync();
                return users.ToUserResources();
            }
            catch (Exception ex)
            {
                var users = await userDbContext.Users
                .ToListAsync();
                return users.ToUserResources();
            }
        }

        public async void DeleteUser(string id)
        {
            try
            {
                var existingUser = await userDbContext.Users.SingleOrDefaultAsync(
                user => user.Id.Equals(id)
                );
                if (existingUser is null)
                {
                    var msg = $"User with id {id} does not exist";
                    throw new NotFoundException(msg);
                }
                userDbContext.Users.Remove(existingUser);
                userDbContext.SaveChanges();
            }
            catch(NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<string> LoginUser(LoginUserRequest request)
        {
            try
            {
                var existingUser = await userDbContext.Users.SingleOrDefaultAsync(
                user => user.Email.Equals(request.Email));
                if (existingUser is null)
                {
                    var msg = $"User with email {request.Email} does not exist";
                    throw new NotFoundException(msg);
                }
                if (!(UserManager.VerifyPassword(request.Password, existingUser.Password)))
                {
                    var message = "Incorrect password";
                    throw new InvalidRequestException(message);
                }
                return UserManager.GenerateToken(config, request, existingUser);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }
    }

}
