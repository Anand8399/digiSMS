using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class CityVM
    {
        [Key]
        [Required]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "DistrictName", ResourceType = typeof(Resource))]
        [Range(1, 100, ErrorMessage = "The State field is required.")]
        public int DistrictId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        //public IEnumerable<System.Web.Mvc.SelectListItem> AcademicYears { get; set; }

        //public string AcademicYear { get; set; }

        //public IEnumerable<System.Web.Mvc.SelectListItem> Countries { get; set; }

        //public string CountryName { get; set; }

        //public IEnumerable<System.Web.Mvc.SelectListItem> States { get; set; }

        //public string StateName { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Districts { get; set; }
          [Display(Name = "DistrictName", ResourceType = typeof(Resource))]
        public string DistrictName { get; set; }
    }
}