using QuizzBankBE.Model;
using QuizzBankBE.Model.Pagination;
using QuizzBankBE.DTOs;
using QuizzBankBE.DataAccessLayer.DataObject;

namespace QuizzBankBE.Services.UserServices
{
    public interface IUserServices
    {
        Task<ServiceResponse<PageList<UserDTO>>> getAllUsers(OwnerParameter ownerParameters);
    }
}
