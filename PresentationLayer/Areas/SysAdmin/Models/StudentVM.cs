using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;
namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class StudentVM
    {
        public int? SrNo { get; set; }

        [Key]
        [Required]
        public int StudentId { get; set; }


        [Required]
        [Display(Name = "RegisterId", ResourceType = typeof(Resource))]
        public int RegisterId { get; set; }


        [Required]
        [Display(Name = "ClassId", ResourceType = typeof(Resource))]
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "DivisionId", ResourceType = typeof(Resource))]
        public int DivisionId { get; set; }

        [Required]
        [Display(Name = "ClassDivisionId", ResourceType = typeof(Resource))]
        public int ClassDivisionId { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resource))]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "MiddleName", ResourceType = typeof(Resource))]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "MotherName", ResourceType = typeof(Resource))]
        public string MotherName { get; set; }

        [Required]
        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Nationality", ResourceType = typeof(Resource))]
        [StringLength(10)]
        public string Nationality { get; set; }


        [Required]
        [Display(Name = "ReligionId", ResourceType = typeof(Resource))]
        public int ReligionId { get; set; }

        [Required]
        [Display(Name = "CastId", ResourceType = typeof(Resource))]
        public int CastId { get; set; }

        [Required]
        [Display(Name = "ReligionCastId", ResourceType = typeof(Resource))]
        public int ReligionCastId { get; set; }

        [Required]
        [Display(Name = "DateOfBirth", ResourceType = typeof(Resource))]
        //[DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "PlaceOfBirth", ResourceType = typeof(Resource))]
        [StringLength(100)]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "AdharcardNo", ResourceType = typeof(Resource))]
        [StringLength(16)]
        public string AdharcardNo { get; set; }

        [Required]
        [Display(Name = "DateOfAdmission", ResourceType = typeof(Resource))]
        //[DataType(DataType.Date)]
        public DateTime DateOfAdmission { get; set; }

        [Display(Name = "MotherTounge", ResourceType = typeof(Resource))]
        [StringLength(100)]
        public string MotherTounge { get; set; }

        [Display(Name = "UStudentId", ResourceType = typeof(Resource))]
        public string UStudentId { get; set; }


        [StringLength(300)]
        [Display(Name = "LastSchoolAttended", ResourceType = typeof(Resource))]
        public string LastSchoolAttended { get; set; }

        [StringLength(30)]
        [Display(Name = "Progress", ResourceType = typeof(Resource))]
        public string Progress { get; set; }
        [StringLength(30)]
        [Display(Name = "Conduct", ResourceType = typeof(Resource))]
        public string Conduct { get; set; }

        [Display(Name = "PrevSchoolDateofLiving", ResourceType = typeof(Resource))]
        public DateTime? PrevSchoolDateofLiving { get; set; }


        [Display(Name = "LastSchoolClass", ResourceType = typeof(Resource))]
        [StringLength(16)]
        public string LastSchoolClass { get; set; }


        [Display(Name = "LastSchoolTCNo", ResourceType = typeof(Resource))]
        [StringLength(16)]
        public string LastSchoolTCNo { get; set; }


       
        [StringLength(300)]
        [Display(Name = "ClassInWhichStudingAndSinceWhen", ResourceType = typeof(Resource))]
        public string ClassInWhichStudingAndSinceWhen { get; set; }

        [StringLength(300)]
        [Display(Name = "ReasonForLeavingSchool", ResourceType = typeof(Resource))]
        public string ReasonForLeavingSchool { get; set; }

        [StringLength(300)]
        [Display(Name = "RemarkOnTC", ResourceType = typeof(Resource))]
        public string RemarkOnTC { get; set; }

        [Display(Name = "ReserveCategory", ResourceType = typeof(Resource))]
        public string ReserveCategory { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

        [Required]
        [Display(Name = "TCPrinted", ResourceType = typeof(Resource))]
        public bool TCPrinted { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Divisions { get; set; }

        [Display(Name = "ClassName", ResourceType = typeof(Resource))]
        public string ClassName { get; set; }

        [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public string DivisionName { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Religions { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Casts { get; set; }

        [Display(Name = "ReligionName", ResourceType = typeof(Resource))]
        public string ReligionName { get; set; }

        [Display(Name = "CastName", ResourceType = typeof(Resource))]
        public string CastName { get; set; }

        [Display(Name = "StudentFullNameWithTitle", ResourceType = typeof(Resource))]
        public string StudentFullNameWithTitle { get; set; }

        [Display(Name = "ClassDivision", ResourceType = typeof(Resource))]
        public string ClassDivision { get; set; }

        [Display(Name = "ReligionCast", ResourceType = typeof(Resource))]
        public string ReligionCast { get; set; }

        [Display(Name = "DateOfLeavingSchool", ResourceType = typeof(Resource))]
        public DateTime? DateOfLeavingSchool { get; set; }

         [Display(Name = "TCNo", ResourceType = typeof(Resource))]
        public int TCNo { get; set; }
         [Display(Name = "Balance", ResourceType = typeof(Resource))]
         public decimal Balance { get; set; }
         [Display(Name = "MobileNumber", ResourceType = typeof(Resource))]
         public long MobileNumber { get; set; }

    }
}