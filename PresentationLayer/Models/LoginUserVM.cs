using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Models
{
    public class LoginUserVM
    {
        [Required(ErrorMessage = "Select Role")]
        [Display(Name = "RoleId", ResourceType = typeof(Resource))]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Enter User Id")]
        [Display(Name = "UserId", ResourceType = typeof(Resource))]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Roles { get; set; }
    }
}