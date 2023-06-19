using System.ComponentModel.DataAnnotations;

namespace QuizzBankBE.DTOs
{
    public class CreateUserDTO
    {
        [Required]public string Username { get; set; }

        [Required]public string Password { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public DateTime? Dob { get; set; }

        public string? Address { get; set; }

        [Phone]public string? Phone { get; set; }

        public int? Gender { get; set; }

        [Required][EmailAddress]public string Email { get; set; }
    }
}
