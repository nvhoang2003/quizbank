using QuizzBankBE.DataAccessLayer.DataObject;
using QuizzBankBE.Model;

namespace QuizzBankBE.Services.UserServies
{
    public interface IUserServices
    {
        Task<ServiceResponse<User>> createUsers(User user);
    }
}
