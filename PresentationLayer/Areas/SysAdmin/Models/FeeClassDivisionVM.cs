using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;



namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class FeeClassDivisionVM
    {
        [Key]
        [Required]
        public int FeeClassDivisionId { get; set; }

        [Required]
        [Display(Name = "FeeHeadId", ResourceType = typeof(Resource))]
        public int FeeHeadId { get; set; }

        [Required]
        [Display(Name = "ClassDivisionId", ResourceType = typeof(Resource))]
        public int ClassDivisionId { get; set; }
        
        [Required]
        [Display(Name = "ClassId", ResourceType = typeof(Resource))]
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "DivisionId", ResourceType = typeof(Resource))]
        public int DivisionId { get; set; }

        [Required]
        [Display(Name = "PeriodInMonthly", ResourceType = typeof(Resource))]
        public int PeriodInMonthly { get; set; }

        [Required]
        [Display(Name = "AmountInMonthly", ResourceType = typeof(Resource))]
        public decimal AmountInMonthly { get; set; }

        [Required]
        [Display(Name = "AmountInYearly", ResourceType = typeof(Resource))]
        public decimal AmountInYearly { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }


        [Display(Name = "FeeHeadName", ResourceType = typeof(Resource))]
        public string FeeHeadName { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Fees { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Divisions { get; set; }

        [Display(Name = "ClassName", ResourceType = typeof(Resource))]
        public string ClassName { get; set; }

        [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public string DivisionName { get; set; }
    }
}