namespace QuizzBankBE.DTOs
{
    public class UserDTO
    {
        public int Iduser { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public DateTime? Dob { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public int? Gender { get; set; }

        public DateTime Createdat { get; set; }

        public DateTime Updatedat { get; set; }

        public string Email { get; set; }
    }
}
