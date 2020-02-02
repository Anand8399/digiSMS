using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Models
{
    public class ChangePasswordVM
    {
        [Required]
        [Display(Name = "UserId", ResourceType = typeof(Resource))]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10)]
        [Display(Name = "OldPassword", ResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resource))]
        public string NewPassword { get; set; }

        //[Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        [StringLength(10)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Resource))]
        public string ConfirmNewPassword { get; set; } 

        public IEnumerable<System.Web.Mvc.SelectListItem> UserList { get; set; }
    }
}