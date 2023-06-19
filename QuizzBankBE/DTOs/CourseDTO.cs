using QuizzBankBE.DataAccessLayer.DataObject;
using System.ComponentModel.DataAnnotations;

namespace QuizzBankBE.DTOs
{
    public class CourseDTO
    {
        public int Courseid { get; set; }

        [Required]
        public string Fullname { get; set; } = null!;

        [Required]
        public string Shortname { get; set; } = null!;

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }

        public DateTime? Createdat { get; set; }

        public DateTime? Updatedat { get; set; }
    }
}
