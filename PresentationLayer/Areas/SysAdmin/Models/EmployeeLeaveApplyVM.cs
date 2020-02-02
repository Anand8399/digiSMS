using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;
using myRes=PresentationLayer.LocalResource.Resource;



namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class EmployeeLeaveApplyVM
    {

        [Display(Name = "EmployeeId", ResourceType = typeof(Resource))]
        public int EmployeeId { get; set; }

        
        [Display(Name = "EmployeeFullNName", ResourceType = typeof(Resource))]
        public string EmployeeFullNName { get; set; }

        [Required]
        [Display(Name = "LeaveFromDate", ResourceType = typeof(Resource))]
        public DateTime LeaveFromDate { get; set; }

        [Required]
        [Display(Name = "LeaveToDate", ResourceType = typeof(Resource))]
        public DateTime LeaveToDate { get; set; }

        [Required]
        [Display(Name = "LeaveType", ResourceType = typeof(Resource))]
        public int LeaveType { get; set; }


        [Display(Name = "Reason", ResourceType = typeof(Resource))]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> EmployeeList { get; set; }

        [Display(Name = "LeaveBalance", ResourceType = typeof(Resource))]
        public int BalanceLeaves { get; set; }

    }
}