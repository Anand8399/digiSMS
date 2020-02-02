using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;
namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentParentVM
    {
        [Key]
        [Required]
        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        public int StudentId { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resource))]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "MiddleName", ResourceType = typeof(Resource))]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        [StringLength(1)]
        public string Gender { get; set; }

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

        [Display(Name = "CurrentCityId", ResourceType = typeof(Resource))]
        public int CurrentCityId { get; set; }

        [Display(Name = "CurrentPinCode", ResourceType = typeof(Resource))]
        public int? CurrentPinCode { get; set; }

        [Display(Name = "MobileNumber", ResourceType = typeof(Resource))]
        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered mobile number not valid.")]
        public long MobileNo { get; set; }

        [Display(Name = "ContactNumber", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        [Display(Name = "Occupation", ResourceType = typeof(Resource))]
        [StringLength(10)]
        public string Occupation { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string CompanyName { get; set; }

        [Display(Name = "CompanyAddress", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string CompanyAddress { get; set; }

        [Display(Name = "CompanyContactNo", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string CompanyContactNo { get; set; }

        [Display(Name = "BankName", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string BankName { get; set; }

        [Display(Name = "AccountNo", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string AccountNo { get; set; }

        [Display(Name = "Branch", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string Branch { get; set; }

        [Display(Name = "IFSCCode", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string IFSCCode { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Countries { get; set; }

        [Display(Name = "StudentFullNameWithTitle", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }
    }
}