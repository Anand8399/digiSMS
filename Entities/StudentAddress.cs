using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentAddress
    {
        public int StudentId { get; set; }
        public string CurrentAddress { get; set; }
        public int CurrentCountryId { get; set; }
        public int CurrentStateId { get; set; }
        public int CurrentDistrictId { get; set; }
        public int CurrentCityId { get; set; }
        public int? CurrentPinCode { get; set; }
        public string PermentAddress { get; set; }
        public int PermentCountryId { get; set; }
        public int PermentStateId { get; set; }
        public int PermentDistrictId { get; set; }

        public int PermentCityId { get; set; }
        public int? PermentPinCode { get; set; }
        public bool Status { get; set; }
        public string Remark { get; set; }

        public string StudentFullName { get; set; }
    }
}
