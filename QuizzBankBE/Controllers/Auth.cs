using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.DTOs;
using QuizzBankBE.Model;
using QuizzBankBE.Services.AuthServices;
using System.Security.Claims;

namespace QuizzBankBE.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Produces("application/json")]
    public class Auth : ControllerBase
    {
        private readonly IAuthServices _authenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public Auth(IAuthServices authenServices, IHttpContextAccessor httpContextAccessor, DataContext dataContext, IConfiguration configuration)
        {
            _authenServices = authenServices;
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
            _configuration = configuration;
        }

        [HttpPost("registerSingleUser")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> registerSingleUser(
        [FromBody] CreateUserDTO createUserDTO)
        {
            var response = await _authenServices.createUsers(createUserDTO);
            if (response.Status == false){
                return BadRequest(new ProblemDetails
                {
                    Status = response.StatusCode,
                    Title = response.Message
                });
            }

            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> login(
        [FromBody] LoginForm loginForm)
        {
            var response = await _authenServices.login(loginForm);
            if (response.Status == false)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = response.StatusCode,
                    Title = response.Message
                });
            }

            return Ok(response);
        }
    }
}
