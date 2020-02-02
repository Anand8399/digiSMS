using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AcademicYear
    {
        public int AcademicYearId { get; set; }

        public string AcademicYearName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }
    }
}
