using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;


namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentClassChangeVM
    {
        [Key]
        [Required]
        public int SrNo { get; set; }


        public int PreviousClassDivisionId { get; set; }

        [Required]
        [Display(Name = "CurrentClassId", ResourceType = typeof(Resource))]
        public int PreviousClassId { get; set; }

        [Required]
        [Display(Name = "CurrentDivisionId", ResourceType = typeof(Resource))]
        public int PreviousDivisionId { get; set; }

        public int CurrentClassDivisionId { get; set; }

        [Required]
        [Display(Name = "NextClassId", ResourceType = typeof(Resource))]
        public int CurrentClassId { get; set; }

        [Required]
        [Display(Name = "NextDivisionId", ResourceType = typeof(Resource))]
        public int CurrentDivisionId { get; set; }

        public List<StudentClassChangeSubVM>  StudentClassChangeSubList { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }
        //[Range(1, Int64.MaxValue, ErrorMessage="Select at list one record.")]
        //public int NoOfSelectedRecords { get; set; }
    }
}