using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class AcademicYearVM
    {
        [Key]
        [Required]
        public int AcademicYearId { get; set; }


       [Display(Name = "AcademicYearName", ResourceType = typeof(Resource))]
        [StringLength(10)]
        public string AcademicYearName { get; set; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(Resource))]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "EndDate", ResourceType = typeof(Resource))]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }
    }
}