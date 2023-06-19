using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.DTOs;
using QuizzBankBE.Model.Pagination;
using QuizzBankBE.Model;
using System.Linq;
using QuizzBankBE.DataAccessLayer.DataObject;

namespace QuizzBankBE.Services.CourseServices
{
    public partial class CourseServices : ICourseServices
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CourseServices(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _dataContext = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<PageList<CourseDTO>>> getAllCourse(OwnerParameter ownerParameters)
        {
            var serviceResponse = new ServiceResponse<PageList<CourseDTO>>();
            var courseDTOs = new List<CourseDTO>();
            var dbCourse = await _dataContext.Courses.ToListAsync();
            courseDTOs = dbCourse.Select(u => _mapper.Map<CourseDTO>(u)).ToList();
            serviceResponse.Data = PageList<CourseDTO>.ToPageList(
            courseDTOs.AsEnumerable<CourseDTO>().OrderBy(on => on.Fullname),
            ownerParameters.pageIndex,
            ownerParameters.pageSize);
            return serviceResponse;
        }

        public async Task<ServiceResponse<Course>> getCourseByCourseID(int courseID)
        {
            var serviceresponse = new ServiceResponse<Course>();
            var dbCourse = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Courseid == courseID);
            if (dbCourse == null)
            {
                serviceresponse.Status = false;
                serviceresponse.StatusCode = 400;
                serviceresponse.Message = "course.notFoundwithID";
                return serviceresponse;
            }
            serviceresponse.Data = dbCourse;
            return serviceresponse;
        }
    }
}
