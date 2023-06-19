using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.DTOs;
using QuizzBankBE.Model.Pagination;
using QuizzBankBE.Model;
using QuizzBankBE.Services.UserServices;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace QuizzBankBE.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowAll")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private DataContext _dataContext;
        IExcelDataReader reader;
        private IConfiguration _configuration;

        public UserController(IUserServices userServices, DataContext dataContext, IConfiguration configuration)
        {
            _userServices = userServices;
            _dataContext = dataContext;
            _configuration = configuration;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<ServiceResponse<PageList<UserDTO>>>> getAllUsers(
            [FromQuery] OwnerParameter ownerParameters)
        {
          /*  var userIdLogin = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userIdLogin == null)
            {
                return new StatusCodeResult(401);
            }
            return Ok();*/

            var users = await _userServices.getAllUsers(ownerParameters);
            var metadata = new
            {
                users.Data.TotalCount,
                users.Data.PageSize,
                users.Data.CurrentPage,
                users.Data.TotalPages,
                users.Data.HasNext,
                users.Data.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }
    }
}
