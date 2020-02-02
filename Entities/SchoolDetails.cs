using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SchoolDetails
    {
        public int SchoolId { get; set; }

        public string ManagementName { get; set; }

        public string SchoolName { get; set; }
        public string Address { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public string SchoolReconginationNo { get; set; }
        public string Medium { get; set; }
        public string UDiscNo { get; set; }
        public string Board { get; set; }
        public string AffilationNo { get; set; }

        public string LogoPath { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }
    }
}
