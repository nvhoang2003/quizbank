using Microsoft.EntityFrameworkCore;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.DataAccessLayer.DataObject;
using QuizzBankBE.DTOs;
using QuizzBankBE.Model;
using AutoMapper;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QuizzBankBE.Controllers;
using System.Security.Claims;
using QuizzBankBE.JWT;

namespace QuizzBankBE.Services.AuthServices
{
    public class AuthServices : IAuthServices
    {
        public DataContext _dataContext;
        public IMapper _mapper;
        public IConfiguration _configuration;
        public readonly IjwtProvider _jwtProvider;

        public AuthServices(DataContext dataContext, IMapper mapper, IConfiguration configuration, IjwtProvider jwtProvider)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        public AuthServices()
        {
        }

        private bool VerifyPassword(string password, string storedpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedpassword);
        }

        public async Task<IPrincipal> createPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Iduser + "")
            };
            var identity = new ClaimsIdentity(claims);
            return new ClaimsPrincipal(identity);
        }

        public async Task<ServiceResponse<UserDTO>> createUsers(CreateUserDTO createUserDTO)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();

            if (await _dataContext.Users.FirstOrDefaultAsync(
                x => x.Username == createUserDTO.Username || x.Email == createUserDTO.Email) != null)
            {
                serviceResponse.Status = false;
                serviceResponse.StatusCode = 400;
                serviceResponse.Message = "Tài khoản đã tồn tại !";
                return serviceResponse;
            }
            else
            {
                createUserDTO.Password = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);
                User userRegister = _mapper.Map<User>(createUserDTO);
                var userSaved = _dataContext.Users.Add(userRegister);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Status = true;
                serviceResponse.StatusCode = 200;
                serviceResponse.Message = "Tạo thành công !";
                return serviceResponse;
            }
        }

        public async Task<LoginResponse> login(LoginForm loginForm)
        {
            var serviceResponse = new LoginResponse();
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == loginForm.username);
            if( user != null && VerifyPassword(loginForm.password, user.Password))
            {
                serviceResponse.Status = true;
                serviceResponse.StatusCode = 200;
                IPrincipal userlogin = await createPrincipal(user);
                var token = _jwtProvider.CreateToken(userlogin);
                serviceResponse.Message = "Đăng nhập thành công!" + user.Iduser;
                serviceResponse.accessToken = token;
                return serviceResponse;
            }
            else
            {
                serviceResponse.Status = false;
                serviceResponse.StatusCode = 400;
                serviceResponse.Message = "Tài khoản hoặc mật khẩu không chính xác";
                return serviceResponse;
            }
        }
    }
}
