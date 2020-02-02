using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ClassDivision
    {
        public int ClassDivisionId { get; set; }

        public int ClassId { get; set; }

        public int DivisionId { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public string ClassName { get; set; }

        public string DivisionName { get; set; }

    }
}
