using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentLedgerSearchVM
    {
        [Display(Name = "General Registration No")]
        public int? RegisterId { get; set; }


        [Display(Name = "Class")]
        public int ClassId { get; set; }

        [Display(Name = "Division")]
        public int DivisionId { get; set; }

        [Display(Name = "Religion")]
        public int ReligionId { get; set; }

        [Required]
        [Display(Name = "Cast")]
        public int CastId { get; set; }


        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Divisions { get; set; }

        [Display(Name = "Class")]
        public string ClassName { get; set; }

        [Display(Name = "Division")]
        public string DivisionName { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Religions { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Casts { get; set; }

        [Display(Name = "Religion")]
        public string ReligionName { get; set; }

        [Display(Name = "Cast")]
        public string CastName { get; set; }

        [Display(Name = "Student full name")]
        public string StudentFullNameWithTitle { get; set; }

    }
}