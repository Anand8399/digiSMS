using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int SrNo { get; set; }

        public int RoleId { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public int SchoolId { get; set; }

        public Role UserRole { get; set; }
    }
}
