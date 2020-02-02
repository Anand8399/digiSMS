using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentAttendanceSubVM
    {
        public int StudentId { get; set; }

        public decimal Attendance { get; set; }

        public string AbsentRemark { get; set; }

    }
}