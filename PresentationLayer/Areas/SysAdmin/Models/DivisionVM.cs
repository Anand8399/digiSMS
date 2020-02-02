using PresentationLayer.LocalResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class DivisionVM
    {
        [Key]
        [Required]
        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public string DivisionName { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }
    }
}