using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentAttendance
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int DivisionId { get; set; }

        public DateTime DateOfAttendance { get; set; }

        public int StudentId { get; set; }

        public decimal AttendanceInDays { get; set; }

        public string AbsentRemark { get; set; }

        public bool Status { get; set; }

        public int RegisterId { get; set; }

        public int ClassDivisionId { get; set; }

        public string StudentFullNameWithTitle { get; set; }
    }
}
