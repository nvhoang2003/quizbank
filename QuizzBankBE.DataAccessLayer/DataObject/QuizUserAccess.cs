using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class QuizUserAccess
{
    public int IdquizUserAccess { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public int AddBy { get; set; }

    public DateTime? AddAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual UserCourse AddByNavigation { get; set; } = null!;

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual UserCourse User { get; set; } = null!;
}
