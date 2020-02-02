using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class ReligionCastVM
    {
        [Key]
        [Required]
        public int ReligionCastId { get; set; }

        [Required]
        [Display(Name = "ReligionId", ResourceType = typeof(Resource))]
        public int ReligionId { get; set; }

        [Required]
        [Display(Name = "CastId", ResourceType = typeof(Resource))]
        public int CastId { get; set; }

        [Required]
        [Display(Name = "ReserveCategory", ResourceType = typeof(Resource))]
        public string ReserveCategory { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Religions { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Casts { get; set; }
        [Display(Name = "ReligionName", ResourceType = typeof(Resource))]
        public string ReligionName { get; set; }

        [Display(Name = "CastName", ResourceType = typeof(Resource))]
        public string CastName { get; set; }
    }
}