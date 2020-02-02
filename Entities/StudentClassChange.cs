using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentClassChange
    {
        public int SrNo { get; set; }

        public int PreviousClassDivisionId { get; set; }

        public int CurrentClassDivisionId { get; set; }

        public int StudentId { get; set; }

        public int RegisterId { get; set; }

        public string StudentFullNameWithTitle { get; set; }

        public string Remark { get; set; }

        public int[] SelectedStudent { get; set; }

    }
}
