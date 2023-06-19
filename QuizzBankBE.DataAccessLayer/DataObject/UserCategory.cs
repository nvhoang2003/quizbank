using System;
using System.Collections.Generic;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class UserCategory
{
    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public int Permission { get; set; }

    public int AddBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User AddByNavigation { get; set; } = null!;

    public virtual QuestionCategory Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
