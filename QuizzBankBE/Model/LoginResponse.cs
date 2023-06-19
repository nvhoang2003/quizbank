using System.Data;

namespace QuizzBankBE.Model
{
    public class LoginResponse
    {
        public string accessToken { get; set; }

        public string tokenType { get; set; } = "Bearer";

        public int StatusCode { get; set; }

        public bool Status { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }
}
