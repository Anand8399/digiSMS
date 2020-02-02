﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentFeeTranscationVM
    {
        [Key]
        public int StudentTransactionId { get; set; }


        //[Required]
        [Display(Name = "ClassDivisionId", ResourceType = typeof(Resource))]
        //[Range(1, 100, ErrorMessage = "The Class and Division field is required.")]
        public int ClassDivisionId { get; set; }


        [Required]
        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        [Range(1, int.MaxValue, ErrorMessage = "The Student field is required.")]
        public int StudentId { get; set; }

        [Display(Name = "StudentFullName", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }

        [Required]
        [Display(Name = "TransactionDate", ResourceType = typeof(Resource))]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "ReceiptNo", ResourceType = typeof(Resource))]
        public int ReceiptNo { get; set; }

        [Display(Name = "BankName", ResourceType = typeof(Resource))]
        public string BankName { get; set; }

        [Display(Name = "ChequeNumber", ResourceType = typeof(Resource))]
        public string ChequeNumber { get; set; }

        [Required]
        [Display(Name = "ReceiptTotal", ResourceType = typeof(Resource))]
        [Range(1, double.MaxValue, ErrorMessage = "The Receipt Total field is required.")]
        public decimal ReceiptTotal { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public List<StudentFeeTranscationSubVM> StudentTransactionSubList { get; set; }

        
    }
}