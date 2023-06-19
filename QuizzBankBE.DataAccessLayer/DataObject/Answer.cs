using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class Answer
{
    public int Idanswers { get; set; }

    public int Questionid { get; set; }

    public string Content { get; set; } = null!;

    public float Fraction { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();
}
