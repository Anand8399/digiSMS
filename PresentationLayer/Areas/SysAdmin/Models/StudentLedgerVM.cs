using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentLedgerVM
    {
        public int StudentLedgerId { get; set; }

        public int StudentId { get; set; }

        [Display(Name = "Student Full Name")]
        public string StudentFullNameWithTitle { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Fee Head id")]
        public int FeeHeadId { get; set; }

        [Display(Name = "Fee Head")]
        public string FeeHeadName { get; set; }

        [Display(Name = "Credit")]
        public decimal Cr { get; set; }

        [Display(Name = "Debit")]
        public decimal Dr { get; set; }

        [Display(Name = "Head Balance")]
        public decimal HeadBalance { get; set; }

        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        [Display(Name = "Receipt No")]
        public int ReceiptNo { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Cheque No")]
        public string ChequeNumber { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}