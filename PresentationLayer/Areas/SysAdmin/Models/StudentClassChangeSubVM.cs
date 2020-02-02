using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;


namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentClassChangeSubVM
    {
        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        public int StudentId { get; set; }

        [Display(Name = "RegisterId", ResourceType = typeof(Resource))]
        public int RegisterId { get; set; }

        [Display(Name = "StudentFullNameWithTitle", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }

        public bool CheckedValues { get; set; }
    }
}