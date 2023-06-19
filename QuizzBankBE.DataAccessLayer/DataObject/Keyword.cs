using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class Keyword
{
    public int Idkeywords { get; set; }

    public string Content { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime Updatedat { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
