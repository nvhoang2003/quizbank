using QuizzBankBE.DataAccessLayer.Entity.Interface;
using System;
using System.Collections.Generic;
using QuizzBankBE.DataAccessLayer.Entity.Interface;

namespace QuizzBankBE.DataAccessLayer.Entity
{
    public abstract class AuditedEntityBase : IAuditedEntityBase
    {
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int IsDeleted { get; set; } = 0;

    }
}
