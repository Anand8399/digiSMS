using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeeLeaveTransaction
    {
        public int TransactionId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeFullName { get; set; }

        public int TransactionType { get; set; }

        public int LeaveType { get; set; }

        public DateTime LeaveFromDate { get; set; }

        public DateTime LeaveToDate { get; set; }

        public decimal LeavesInDays { get; set; }

        public decimal BalanceLeaves { get; set; }

        public string Remark { get; set; }

        public DateTime TransactionDate { get; set; }

    }
}
