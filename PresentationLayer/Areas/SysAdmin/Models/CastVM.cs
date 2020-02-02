using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class CastVM
    {
        [Key]
        [Required]
        public int CastId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "CastName", ResourceType = typeof(Resource))]
        public string CastName { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> AcademicYears { get; set; }
          [Display(Name = "AcademicYear", ResourceType = typeof(Resource))]
        public string AcademicYear { get; set; }
    }
}