using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class User
{
    public int Iduser { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateTime? Dob { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public int? Gender { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Question> QuestionCreatedbyNavigations { get; set; } = new List<Question>();

    public virtual ICollection<Question> QuestionUpdatedbyNavigations { get; set; } = new List<Question>();

    public virtual ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();

    public virtual ICollection<UserCategory> UserCategoryAddByNavigations { get; set; } = new List<UserCategory>();

    public virtual ICollection<UserCategory> UserCategoryUsers { get; set; } = new List<UserCategory>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
