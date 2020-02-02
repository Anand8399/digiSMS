using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;




namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentAttendanceVM
    {

        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        public int StudentId { get; set; }

        [Display(Name = "StudentFullNameWithTitle", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }

        [Required]
        [Display(Name = "RegisterId", ResourceType = typeof(Resource))]
        public int RegisterId { get; set; }


        [Required]
        [Display(Name = "ClassId", ResourceType = typeof(Resource))]
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "DivisionId", ResourceType = typeof(Resource))]
        public int DivisionId { get; set; }

        [Required]
        [Display(Name = "ClassDivisionId", ResourceType = typeof(Resource))]
        public int ClassDivisionId { get; set; }

        [Required]
        [Display(Name = "Month", ResourceType = typeof(Resource))]
        //[DisplayFormat(DataFormatString = "{0:MMM-yyyy}")] 
        public DateTime DateOfAttendance { get; set; }
        [Display(Name = "AttendanceInDays", ResourceType = typeof(Resource))]
        public decimal AttendanceInDays { get; set; }
        [Display(Name = "AbsentRemark", ResourceType = typeof(Resource))]
        public string AbsentRemark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

    }
}