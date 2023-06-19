using System.ComponentModel.DataAnnotations;

namespace QuizzBankBE.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(255)]
        public string? Firstname { get; set; }

        [MaxLength(255)]
        public string? Lastname { get; set; }

        public DateTime? Dob { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [Phone]
        [MaxLength(10)]
        public string? Phone { get; set; }

        public int? Gender { get; set; }

        [Required][EmailAddress]public string Email { get; set; }
    }
}
