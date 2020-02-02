using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class District
    {
        public int DistrictId { get; set; }

        public int StateId { get; set; }

        public string DistrictName { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public string CountryName { get; set; }

        public int CountryId { get; set; }

        public string StateName { get; set; }

    }
}
