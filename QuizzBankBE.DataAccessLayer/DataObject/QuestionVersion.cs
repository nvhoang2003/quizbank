using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class QuestionVersion
{
    public int IdquestionVersions { get; set; }

    public int QuestionId { get; set; }

    public int? QuestionBankEntryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual QuestionBankEntry? QuestionBankEntry { get; set; }
}
