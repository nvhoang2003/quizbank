using Org.BouncyCastle.Crypto;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.DataAccessLayer.DataObject;
using Microsoft.EntityFrameworkCore;
using QuizzBankBE.Model;
using Org.BouncyCastle.Crypto.Generators;

namespace QuizzBankBE.Services.UserServies
{
    public class UserServices : IUserServices
    {
        private readonly DataContext _dataContext;
        //private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public async Task<ServiceResponse<User>> createUsers(User user)
        {
            var serviceResponse = new ServiceResponse<User>();

            if (await _dataContext.Users.FirstOrDefaultAsync(
                x => x.Username == user.Username || x.Email == user.Email) != null)
            {
                serviceResponse.Status = false;
                serviceResponse.StatusCode = 400;
                serviceResponse.Message = "Tài khoản đã tồn tại !";
                return serviceResponse;
            }
            else
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                var saved = _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = saved.Entity;
            }

            return serviceResponse;
        }
    }
}
