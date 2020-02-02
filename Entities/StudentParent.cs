using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentParent
    {
        public int StudentId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public int PinCode { get; set; }
        public long MobileNumber { get; set; }
        public string ContactNo { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContactNo { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string IFSCCode { get; set; }
        public string StudentFullName { get; set; }
        public bool Status { get; set; }
        public string Remark { get; set; }
    }
}
