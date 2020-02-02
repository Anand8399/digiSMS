using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FeeClassDivision
    {

        public int FeeClassDivisionId { get; set; }

        public int FeeHeadId { get; set; }

        public int ClassDivisionId { get; set; }

        public int PeriodInMonthly { get; set; }

        public decimal AmountInMonthly { get; set; }

        public decimal AmountInYearly { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }
        

        public string FeeHeadName { get; set; }

        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

    }
}
