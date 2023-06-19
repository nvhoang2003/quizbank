using System.ComponentModel.DataAnnotations;

namespace QuizzBankBE.DTOs
{
    public class CreateCourseDTO
    {

        [Required]
        [MaxLength(255)]
        public string Fullname { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Shortname { get; set; } = null!;

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }
    }
}
