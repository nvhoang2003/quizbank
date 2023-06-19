using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class Course
{
    public int Courseid { get; set; }

    public string Fullname { get; set; } = null!;

    public string Shortname { get; set; } = null!;

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
