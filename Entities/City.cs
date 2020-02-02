using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class City
    {
        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public string CityName { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public string DistrictName { get; set; }

    }
}
