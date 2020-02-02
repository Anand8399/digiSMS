using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class DistrictVM
    {
        [Key]
        [Required]
        public int DistrictId { get; set; }

        [Required]
        [Display(Name = "StateName", ResourceType = typeof(Resource))]
        [Range(1, 100, ErrorMessage = "The State field is required.")]
        public int StateId { get; set; }

        [Required]
        [Display(Name = "CountryName", ResourceType = typeof(Resource))]
        [Range(1, 100, ErrorMessage = "The Country field is required.")]
        public int CountryId { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name = "DistrictName", ResourceType = typeof(Resource))]
        public string DistrictName { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Countries { get; set; }
          [Display(Name = "CountryName", ResourceType = typeof(Resource))]
        public string CountryName { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> States { get; set; }
          [Display(Name = "StateName", ResourceType = typeof(Resource))]
        public string StateName { get; set; }
    }
}