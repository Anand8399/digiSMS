using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ReligionCast
    {
        public int ReligionCastId { get; set; }

        public int ReligionId { get; set; }

        public int CastId { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }

        public string ReligionName { get; set; }

        public string CastName { get; set; }

        public string ReserveCategory { get; set; }
    }
}
