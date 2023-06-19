using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class Question
{
    public int Idquestions { get; set; }

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Questionstype { get; set; } = null!;

    public string? Generalfeedback { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime Updatedat { get; set; }

    public float DefaultMark { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<QuestionVersion> QuestionVersions { get; set; } = new List<QuestionVersion>();

    public virtual ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();

    public virtual User UpdatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Keyword> Keywords { get; set; } = new List<Keyword>();
}
