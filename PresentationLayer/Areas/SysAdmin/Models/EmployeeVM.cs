using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class EmployeeVM
    {
        [Key]
        public int EmployeeId { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name = "EmployeeCode", ResourceType = typeof(Resource))]
        public String EmployeeCode { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        public String Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        public String FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "MiddleName", ResourceType = typeof(Resource))]
        public String MiddleName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Catagory", ResourceType = typeof(Resource))]
        public string Category { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }

        [Required]
        [Display(Name = "ContactNo", ResourceType = typeof(Resource))]
        public String ContactNo { get; set; }

        [Required]
        [Display(Name = "DOB", ResourceType = typeof(Resource))]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "JoiningDate", ResourceType = typeof(Resource))]
        public DateTime JoiningDate { get; set; }


    }
}