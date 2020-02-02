using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;


namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class FeeHeadVM
    {
        [Key]
        [Required]
        public int FeeHeadId { get; set; }


        [Required]
        [Display(Name = "FeeHeadName", ResourceType = typeof(Resource))]
        public string FeeHeadName { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }


    }
}