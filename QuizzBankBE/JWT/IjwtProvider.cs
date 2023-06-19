using System.Security.Principal;

namespace QuizzBankBE.JWT
{
    public interface IjwtProvider
    {
        string CreateToken(IPrincipal userLogin);
    }
}
