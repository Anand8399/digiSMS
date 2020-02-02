using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentAddressVM
    {
        [Key]
        [Required]
        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "CurrentAddress", ResourceType = typeof(Resource))]
        public string CurrentAddress { get; set; }

        [Required]
        [Display(Name = "CurrentCountryId", ResourceType = typeof(Resource))]
        public int CurrentCountryId { get; set; }

        [Required]
        [Display(Name = "CurrentStateId", ResourceType = typeof(Resource))]
        public int CurrentStateId { get; set; }

        [Required]
        [Display(Name = "CurrentDistrictId", ResourceType = typeof(Resource))]
        public int CurrentDistrictId { get; set; }

        [Required]
        [Display(Name = "CurrentCityId", ResourceType = typeof(Resource))]
        public int CurrentCityId { get; set; }

        [Display(Name = "CurrentPinCode", ResourceType = typeof(Resource))]
        public int? CurrentPinCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "PermentAddress", ResourceType = typeof(Resource))]
        public string PermentAddress { get; set; }

        [Required]
        [Display(Name = "PermentCountryId", ResourceType = typeof(Resource))]
        public int PermentCountryId { get; set; }

        [Required]
        [Display(Name = "PermentStateId", ResourceType = typeof(Resource))]
        public int PermentStateId { get; set; }

        [Required]
        [Display(Name = "PermentDistrictId", ResourceType = typeof(Resource))]
        public int PermentDistrictId { get; set; }

        [Required]
        [Display(Name = "PermentCityId", ResourceType = typeof(Resource))]
        public int PermentCityId { get; set; }

        [Display(Name = "PermentPinCode", ResourceType = typeof(Resource))]
        public int? PermentPinCode { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Countries { get; set; }
        [Display(Name = "CurrentCountryName", ResourceType = typeof(Resource))]
        public string CurrentCountryName { get; set; }
         [Display(Name = "PermentCountryName", ResourceType = typeof(Resource))]
        public string PermentCountryName { get; set; }

        //public IEnumerable<System.Web.Mvc.SelectListItem> CurrentStates { get; set; }
         [Display(Name = "CurrentStateName", ResourceType = typeof(Resource))]
        public string CurrentStateName { get; set; }

        //public IEnumerable<System.Web.Mvc.SelectListItem> PermentStates { get; set; }
         [Display(Name = "PermentStateName", ResourceType = typeof(Resource))]
        public string PermentStateName { get; set; }
         [Display(Name = "CurrentDistrictName", ResourceType = typeof(Resource))]
        public string CurrentDistrictName { get; set; }
        [Display(Name = "PermentDistrictName", ResourceType = typeof(Resource))]
        public string PermentDistrictName { get; set; }
          [Display(Name = "CurrentCityName", ResourceType = typeof(Resource))]
        public string CurrentCityName { get; set; }
         [Display(Name = "PermentCityName", ResourceType = typeof(Resource))]
        public string PermentCityName { get; set; }

        [Display(Name = "StudentFullNameWithTitle", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }
    }
}