using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentTransactionSub
    {
        public int StudentTransactionSubId { get; set; }

        public int StudentTransactionId { get; set; }

        public int FeeHeadId { get; set; }

        public decimal Cr { get; set; }

        public decimal Dr { get; set; }

        public decimal Balance { get; set; }

        public string Remark { get; set; }

        public string FeeHeadName { get; set; }
    }
}
