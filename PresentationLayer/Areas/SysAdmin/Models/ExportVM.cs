using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class ExportVM
    {

        [Key]
        public int SrNo { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }


        [StringLength(10)]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Class")]
        public string Class { get; set; }

        [Required]
        [Display(Name = "Division")]
        public string Division { get; set; }

        public int GrNo { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Birth Place ")]
        public string BirthPlace { get; set; }

        [Required]
        [Display(Name = "Date Of Admission ")]
        public DateTime DateOfAdmission { get; set; }

        [Required]
        [Display(Name = "Admission Class ")]
        public string AdmissionClass { get; set; }


        [Required]
        [Display(Name = "Religion ")]
        public string Religion { get; set; }



        [Display(Name = "Subcaste ")]
        public string Subcaste { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }


        [Display(Name = "Aadhar Card No ")]
        public string AadharCardNo { get; set; }

        [Required]
        [Display(Name = "Parents Contact ")]
        public string ParentsContactNo { get; set; }


        [Display(Name = "Address ")]
        public string Address { get; set; }


        [Required]
        [Display(Name = "Bank Name ")]
        public string BankName { get; set; }


        [Required]
        [Display(Name = "Bank Account HolderName ")]
        public string BankAccountHolderName { get; set; }

        [Required]
        [Display(Name = "Account No ")]
        public string AccountNo { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Required]
        [Display(Name = "IFSC Code")]
        public string IFSCCode { get; set; }
    }
}