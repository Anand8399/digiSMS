using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Division
    {
        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }
    }
}
