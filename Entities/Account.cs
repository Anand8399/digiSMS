using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account
    {
        public int SrNo { get; set; }

        public string NarrationDetails { get; set; }

        public string TransactionType { get; set; }
        public string PaymentMode { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Remark { get; set; }

        public string CustomerName { get; set; }
        public string BankName { get; set; }
        public string ChqDDNumber { get; set; }
        public string ContactNo { get; set; }


   
    }
}
