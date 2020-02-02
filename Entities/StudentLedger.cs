using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentLedger
    {
        public int StudentLedgerId { get; set; }

        public int StudentId { get; set; }

        public DateTime TransactionDate { get; set; }   

        public int FeeHeadId { get; set; }

        public decimal Cr { get; set; }

        public decimal Dr { get; set; }

        public decimal HeadBalance { get; set; }

        public decimal Balance { get; set; }

        public int ReceiptNo { get; set; }

        public string BankName { get; set; }

        public string ChequeNumber { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string StudentFullNameWithTitle { get; set; }

        public string FeeHeadName { get; set; }
    }
}
