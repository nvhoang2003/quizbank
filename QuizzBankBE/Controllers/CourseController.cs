using ExcelDataReader;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.DTOs;
using QuizzBankBE.Model.Pagination;
using QuizzBankBE.Model;
using QuizzBankBE.Services.CourseServices;
using QuizzBankBE.DataAccessLayer.DataObject;
using Microsoft.AspNetCore.Authorization;

namespace QuizzBankBE.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowAll")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        private DataContext _dataContext;
        IExcelDataReader reader;
        private IConfiguration _configuration;

        public CourseController(ICourseServices courseServices, DataContext dataContext, IConfiguration configuration)
        {
            _courseServices = courseServices;
            _dataContext = dataContext;
            _configuration = configuration;
        }

        [HttpGet("getOne/{id}")]
        public async Task<ActionResult<ServiceResponse<Course>>> getOne(int id)
        {
            var response = await _courseServices.getCourseByCourseID(id);
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

        [HttpGet("GetAllCourse")]
        public async Task<ActionResult<ServiceResponse<PageList<UserDTO>>>> getAllCourse(
            [FromQuery] OwnerParameter ownerParameters)
        {
            /*  var userIdLogin = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
              if (userIdLogin == null)
              {
                  return new StatusCodeResult(401);
              }
              return Ok();*/

            var courses = await _courseServices.getAllCourse(ownerParameters);
            var metadata = new
            {
                courses.Data.TotalCount,
                courses.Data.PageSize,
                courses.Data.CurrentPage,
                courses.Data.TotalPages,
                courses.Data.HasNext,
                courses.Data.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(courses);
        }
    }
}
