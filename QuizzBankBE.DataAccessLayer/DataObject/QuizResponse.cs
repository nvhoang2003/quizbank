using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class QuizResponse
{
    public int IdquizResponses { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public int QuestionId { get; set; }

    public int ResponsesId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual Answer Responses { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
