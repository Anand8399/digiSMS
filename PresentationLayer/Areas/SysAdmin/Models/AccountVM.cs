 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class AccountVM
    {
        [Key]
        public int SrNo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "NarrationDetails", ResourceType = typeof(Resource))]
        public String NarrationDetails { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "PaymentMode", ResourceType = typeof(Resource))]
        public String PaymentMode { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "TransactionType", ResourceType = typeof(Resource))]
        public String TransactionType { get; set; }

        [Required]
        [Display(Name = "Amount", ResourceType = typeof(Resource))]
        [Range(1, double.MaxValue, ErrorMessage = "Enter Amount")] 
        public decimal Amount { get; set; }

        [Display(Name = "Balance", ResourceType = typeof(Resource))]
        public decimal Balance { get; set; }

        [Required]
        [Display(Name = "TransactionDate", ResourceType = typeof(Resource))]
        public DateTime TransactionDate { get; set; }


        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        [Display(Name = "CustomerName", ResourceType = typeof(Resource))]
        [StringLength(100)]
        public string CustomerName { get; set; }


        [Display(Name = "BankName", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string BankName { get; set; }

        [Display(Name = "ChqDDNumber", ResourceType = typeof(Resource))]
        [StringLength(30)]
        public string ChqDDNumber { get; set; }

        [Display(Name = "ContactNo", ResourceType = typeof(Resource))]
        [StringLength(15)]
        public string ContactNo { get; set; }


        [Display(Name = "FromDate", ResourceType=typeof(Resource))]
        public DateTime FromDate { get; set; }
        [Display(Name = "ToDate", ResourceType = typeof(Resource))]
        public DateTime ToDate { get; set; }
    }
}