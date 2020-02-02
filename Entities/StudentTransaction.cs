using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentTransaction
    {
        public int StudentTransactionId { get; set; }

 

        public int ClassDivisionId { get; set; }

        public int StudentId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ReceiptNo { get; set; }

        public string BankName { get; set; }

        public string ChequeNumber { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public decimal ReceiptTotal { get; set; }

        public List<StudentTransactionSub> StudentTransactionSubList  { get; set; }

        public int ClassId { get; set; }

        public int DivisionId { get; set; }

        public string ClassName { get; set; }

        public string DivisionName { get; set; }

        public string StudentFullNameWithTitle { get; set; }
        
    }
}
