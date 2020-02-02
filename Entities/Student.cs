using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Student
    {
        public int SrNo { get; set; }
        public int StudentId { get; set; }
        public int RegisterId { get; set; }
        public int ClassDivisionId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int ReligionCastId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string AdharcardNo { get; set; }
        public DateTime DateOfAdmission { get; set; }

        public string MotherTounge { get; set; }

        public string UStudentId { get; set; }

        public string LastSchoolClass { get; set; }
        public string LastSchoolTCNo { get; set; }

        public string LastSchoolAttended { get; set; }
        public string Progress { get; set; }
        public string Conduct { get; set; }
        public DateTime? DateOfLeavingSchool { get; set; }
        public string ClassInWhichStudingAndSinceWhen { get; set; }
        public string ReasonForLeavingSchool { get; set; }
        public string RemarkOnTC { get; set; }
        public bool Status { get; set; }
        public string Remark { get; set; }

        public int ClassId { get; set; }

        public int DivisionId { get; set; }

        public string ClassName { get; set; }

        public string DivisionName { get; set; }

        public int ReligionId { get; set; }

        public int CastId { get; set; }

        public string ReligionName { get; set; }

        public string CastName { get; set; }

        public string ReserveCategory { get; set; }

        public bool TCPrinted { get; set; }

        public int TCNo { get; set; }

        public DateTime? PrevSchoolDateofLiving { get; set; }

        public decimal Balance { get; set; }
        public long MobileNumber { get; set; }
    }
}
