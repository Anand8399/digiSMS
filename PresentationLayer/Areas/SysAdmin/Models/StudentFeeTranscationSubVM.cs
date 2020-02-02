using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;
namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentFeeTranscationSubVM
    {
        [Key]
        public int StudentTransactionSubId { get; set; }

        [Required]
        [Display(Name = "StudentTransactionId", ResourceType = typeof(Resource))]
        [Range(1, int.MaxValue, ErrorMessage = "The Student Tanx field is required.")]
        public int StudentTransactionId { get; set; }

        [Required]
        [Display(Name = "FeeHeadId", ResourceType = typeof(Resource))]
        [Range(1, 100, ErrorMessage = "The Fee Head field is required.")]
        public int FeeHeadId { get; set; }

        [Required]
        [Display(Name = "Cr", ResourceType = typeof(Resource))]
        [Range(1, double.MaxValue, ErrorMessage = "The Cr field is required.")]
        public decimal Cr { get; set; }

        [Required]
        [Display(Name = "Dr", ResourceType = typeof(Resource))]
        [Range(1, double.MaxValue, ErrorMessage = "The Dr field is required.")]
        public decimal Dr { get; set; }
         [Display(Name = "Balance", ResourceType = typeof(Resource))]
        public decimal Balance { get; set; }
         [Display(Name = "Remark", ResourceType = typeof(Resource))]
        public string Remark { get; set; }

        [Display(Name = "FeeHeadName", ResourceType = typeof(Resource))]
        public string FeeHeadName { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Fees { get; set; }
    }
}