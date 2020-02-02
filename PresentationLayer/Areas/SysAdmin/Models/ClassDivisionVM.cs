using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class ClassDivisionVM
    {
        [Key]
        [Required]
        public int ClassDivisionId { get; set; }


        [Required]
        [Display(Name = "ClassName", ResourceType = typeof(Resource))]
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public int DivisionId { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Divisions { get; set; }

         [Display(Name = "ClassName", ResourceType = typeof(Resource))]
        public string ClassName { get; set; }

       [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public string DivisionName { get; set; }
        

    }
}