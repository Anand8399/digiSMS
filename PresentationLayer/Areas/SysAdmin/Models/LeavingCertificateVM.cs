using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class LeavingCertificateVM
    {
        [Display(Name = "RegisterId", ResourceType = typeof(Resource))]
        public int? RegisterId { get; set; }

         [Display(Name = "ClassDivisionId", ResourceType = typeof(Resource))]
        public int ClassDivisionId { get; set; }


        [Display(Name = "ClassId", ResourceType = typeof(Resource))]
        public int ClassId { get; set; }


        [Display(Name = "DivisionId", ResourceType = typeof(Resource))]
        public int DivisionId { get; set; }

        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        public int StudentId { get; set; }


        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Divisions { get; set; }

        [Display(Name = "ClassName", ResourceType = typeof(Resource))]
        public string ClassName { get; set; }

        [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public string DivisionName { get; set; }

        [Display(Name = "StudentFullNameWithTitle", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }
    }
}