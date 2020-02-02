using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {

        public int EmployeeId { get; set; }

        public int SchoolId { get; set; }

        public String EmployeeCode { get; set; }

        public String Password { get; set; }

        public String FirstName { get; set; }       
        
        public String MiddleName { get; set; }
        
        public string LastName { get; set; }

        
        public string Category { get; set; }
        public string Address { get; set; }
        
        public String ContactNo { get; set; }
        
        public DateTime DOB { get; set; }

        public DateTime JoiningDate { get; set; }


    }
}
