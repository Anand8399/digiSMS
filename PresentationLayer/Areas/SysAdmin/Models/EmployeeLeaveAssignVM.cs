using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;




namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class EmployeeLeaveAssignVM
    {
        
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public decimal LeavesInDays { get; set; }
        public decimal BalanceLeaves { get; set; }
        public decimal NewLeaves { get; set; }
        public string Remark { get; set; }

    }
}