using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzBankBE.DataAccessLayer.Entity.Interface
{
    internal interface IAuditedEntityBase
    {
        int? CreateBy { get; set; }
        DateTime? CreateDate { get; set; }
        int? UpdateBy { get; set; }
        DateTime? UpdateDate { get; set; }
        int IsDeleted { get; set; }
    }
}
