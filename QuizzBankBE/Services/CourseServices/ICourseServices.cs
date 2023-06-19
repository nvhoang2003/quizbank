using QuizzBankBE.DTOs;
using QuizzBankBE.Model.Pagination;
using QuizzBankBE.Model;
using QuizzBankBE.DataAccessLayer.DataObject;

namespace QuizzBankBE.Services.CourseServices
{
    public interface ICourseServices
    {
        Task<ServiceResponse<PageList<CourseDTO>>> getAllCourse(OwnerParameter ownerParameters);
        Task<ServiceResponse<Course>> getCourseByCourseID(int courseID);
    }
}
