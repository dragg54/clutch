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
using Serilog;

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
                var existingUser = await userDbContext.Users.SingleOrDefaultAsync(
                user => user.Email == request.Email
                );
                if (existingUser is not null)
                {
                    var msg = "User already exists";
                    Log.Error(msg);
                    throw new DuplicateException(msg);
                }
                var newUser = request.ToAddUserRequest();
                userDbContext.Users.Add(newUser);
                await userDbContext.SaveChangesAsync();
                Log.Information($"user with email {request.Email} created");
            }
            catch (DuplicateException ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
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
                    Log.Error(msg);
                    throw new NotFoundException(msg);
                }
                Log.Information("Resource found for users");
                return user.ToUserResource();
            }
            catch(NotFoundException ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task PutUser(string id, PutUserRequest request){
            try
            {
                var existingUser = await userDbContext.Users.SingleOrDefaultAsync(
                user => user.Id.ToString() == id
                );
                if (existingUser is null)
                {
                    var msg = "User does not exist";
                    Log.Error(msg);
                    throw new NotFoundException(msg);
                }
                request.ToAmendUserRequest(existingUser);
                await userDbContext.SaveChangesAsync();
                Log.Information($"user with email {id} updated");
            }
            catch (DuplicateException ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task DeleteUser(string id)
        {
            try
            {
                var existingUser = await userDbContext.Users.SingleOrDefaultAsync(
                user => user.Id.Equals(id)
                );
                if (existingUser is null)
                {
                    var msg = $"User with id {id} does not exist";
                    Log.Error(msg) ;
                    throw new NotFoundException(msg);
                }
                userDbContext.Users.Remove(existingUser);
                await userDbContext.SaveChangesAsync();
            }
            catch(NotFoundException ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
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
                    Log.Error(msg) ;
                    throw new NotFoundException(msg);
                }
                if (!(UserManager.VerifyPassword(request.Password, existingUser.Password)))
                {
                    var message = "Incorrect password";
                    Log.Error(message) ;
                    throw new InvalidRequestException(message);
                }
                Log.Information("User logged in");
                return UserManager.GenerateToken(config, request, existingUser);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<UserResource>> GetUsers(UserRequestQuery userRequestQuery)
        {
            try
            {
                var query =  userDbContext.Users.AsQueryable();
            if(!String.IsNullOrEmpty(userRequestQuery.FirstName))
            {
                query = query.Where(user => user.FirstName == userRequestQuery.FirstName);
            }
            if(!String.IsNullOrEmpty(userRequestQuery.LastName))
            {
                query = query.Where(user => user.LastName == userRequestQuery.LastName);
            }
            if(!String.IsNullOrEmpty(userRequestQuery.Email))
            {
                query = query.Where(user => user.Email == userRequestQuery.Email);
            }
            return query.ToUserResources();
            }
            catch(Exception exception)
            {
                Log.Error(exception.Message);
                throw new HttpRequestException(exception.Message, exception, HttpStatusCode.InternalServerError);
            }
        }
    }

}
