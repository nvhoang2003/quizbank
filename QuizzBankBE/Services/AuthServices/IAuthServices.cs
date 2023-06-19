using QuizzBankBE.DataAccessLayer.DataObject;
using QuizzBankBE.Model;
using QuizzBankBE.DTOs;

namespace QuizzBankBE.Services.AuthServices
{
    public interface IAuthServices
    {
        Task<ServiceResponse<UserDTO>> createUsers(CreateUserDTO createUserDto);

        Task<LoginResponse> login(LoginForm loginFrom);
    }
}
