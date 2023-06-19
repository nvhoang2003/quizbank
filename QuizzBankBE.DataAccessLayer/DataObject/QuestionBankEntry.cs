using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class QuestionBankEntry
{
    public int IdquestionBankEntry { get; set; }

    public int QuestionCategoryId { get; set; }

    public virtual QuestionCategory QuestionCategory { get; set; } = null!;

    public virtual ICollection<QuestionVersion> QuestionVersions { get; set; } = new List<QuestionVersion>();

    public virtual ICollection<Quiz> Quizzs { get; set; } = new List<Quiz>();
}
