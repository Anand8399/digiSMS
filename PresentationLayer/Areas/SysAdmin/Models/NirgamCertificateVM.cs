using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class NirgamCertificateVM
    {
        [Display(Name = "RegisterId", ResourceType = typeof(Resource))]
        public int? RegisterId { get; set; }


        public int ClassDivisionId { get; set; }


        [Display(Name = "ClassId", ResourceType = typeof(Resource))]
        public int ClassId { get; set; }


        [Display(Name = "DivisionId", ResourceType = typeof(Resource))]
        public int DivisionId { get; set; }

        [Display(Name = "StudentId", ResourceType = typeof(Resource))]
        public int StudentId { get; set; }


        public IEnumerable<System.Web.Mvc.SelectListItem> Classes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Divisions { get; set; }

        [Display(Name = "ClassName", ResourceType = typeof(Resource))]
        public string ClassName { get; set; }

        [Display(Name = "DivisionName", ResourceType = typeof(Resource))]
        public string DivisionName { get; set; }

        //[Display(Name = "Student Full Name")]
        //public string StudentFullNameWithTitle { get; set; }

        //[Display(Name = "Name Of Mother")]
        //public string NameOfMother { get; set; }

        // [Display(Name = "Name Of Mother")]
        //public string ReligionAndCast { get; set; }

        // [Display(Name = "Place Of Birth")]
        //public string PlaceOfBirth { get; set; }

        // [Display(Name = "Date Of Birth ")]
        //public DateTime  DateOfBirth { get; set; }

        // [Display(Name = "Last School Attended")]
        //public string LastSchoolAttended { get; set; }

        // [Display(Name = "Date Of Admission")]
        //public DateTime DateOfAdmission { get; set; }

        // [Display(Name = "Name Of Mother")]
        //public string MotherTounge { get; set; }

        // [Display(Name = "Admission Given In Class")]
        //public string AdmissionGivenInClass { get; set; }

        // [Display(Name = "Certificate No ")]
        //public string CertificateNo { get; set; }

        // [Display(Name = "Remark ")]
        //public bool Remark { get; set; }
    }
}