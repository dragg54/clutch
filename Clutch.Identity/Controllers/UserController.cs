using clutch_identity.Entities;
using clutch_identity.Requests;
using clutch_identity.Response;
using clutch_identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace clutch_identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IUserService userService; 
        public UserController(IConfiguration config, IUserService userService)
        {
            this.config = config;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserActionResponse>> CreateUser(PostUserRequest request)
        {
            await userService.PostUser(request);
            return Ok(new UserActionResponse()
            {
                Message = "User created",
                Data = request,
                StatusCode = System.Net.HttpStatusCode.Created
            });
            //return CreatedAtAction(
            //    nameof(GetUserById),
            //    new {request}
            //    new UserActionResponse()
            //    {
            //        Message = "User created",
            //        Data = request,
            //        StatusCode = System.Net.HttpStatusCode.Created
            //    });
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserActionResponse>> GetUserById(long id)
        {
            var userResource = await userService.GetUser(id);
            return Ok(new UserActionResponse()
            {
                Message = $"User with id {id} found",
                Data = userResource,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpGet]
        public async Task<ActionResult<UserActionResponse>> GetUsers()
        {
            var userResources = await userService.GetUsers();
            return Ok(new UserActionResponse()
            {
                Message = $"Users found",
                Data = userResources,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserActionResponse>> LoginUser(LoginUserRequest request)
        {
            var loginPayload = await userService.LoginUser(request);
            return Ok(new UserActionResponse()
            {
                Message = $"Users with email {request.Email} logged in",
                Data = loginPayload,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<UserActionResponse>> DeleteUser(string id)
        {
            await userService.DeleteUser(id);
            return Ok(new UserActionResponse()
            {
                Message = $"Users with id {id} deleted",
                Data = new {id},
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

    }
}
