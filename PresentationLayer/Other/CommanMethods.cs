using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Other
{
    public static class CommanMethods
    {
        /// <summary>
        /// Get student full name with title by student id
        /// </summary>
        /// <param name="StudentId">Student id</param>
        /// <returns>Student name</returns>
        public static string GetStudentName(int StudentId)
        {
            string studentFullName = string.Empty;
            StudentBAL studentObject = new StudentBAL();
            IQueryable< Entities.Student> students = studentObject.FindBy(s => s.StudentId == StudentId);
            if (students != null && students.Count() > 0)
            {
                Entities.Student student = students.FirstOrDefault();
                studentFullName = string.Concat(student.Title.Trim(), " ", student.FirstName.Trim(), " ", student.MiddleName.Trim(), " ", student.LastName).Trim();
            }
            return studentFullName;
        }

        /// <summary>
        /// Get Student name list by class and division
        /// </summary>
        /// <param name="ClassId">Classs id</param>
        /// <param name="DivisionId">Division id</param>
        /// <returns>List of Student name</returns>
        public static IQueryable<SelectListItem> GetStudentNameList(int ClassId, int DivisionId, int schoolId )
        {
            IQueryable<SelectListItem> studentNameList = null;
            if (ClassId > 0 && DivisionId > 0)
            {
                ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                IQueryable<Entities.ClassDivision> classDivisions = classDivisionBAL.FindBy(cd => cd.ClassId == ClassId && cd.DivisionId == DivisionId);
                int ClassDivisionId = 0;
                if (classDivisions != null && classDivisions.Count() > 0)
                {
                    ClassDivisionId = classDivisionBAL.FindBy(cd => cd.ClassId == ClassId && cd.DivisionId == DivisionId).FirstOrDefault().ClassDivisionId;
                }

                if (ClassDivisionId != null && Convert.ToInt32(ClassDivisionId) > 0)
                {
                    StudentBAL studentObject = new StudentBAL();
                    IQueryable<Entities.Student> getStudents = studentObject.GetAll(schoolId).Where(s => s.ClassDivisionId == ClassDivisionId && s.Status == true);
                    studentNameList = from obj in getStudents select new SelectListItem() { Text = string.Concat(obj.FirstName, " ", obj.MiddleName, " ", obj.LastName).Trim(), Value = obj.StudentId.ToString() }; ;
                }
            }
            return studentNameList;
        }

        /// <summary>
        /// Get Student details by register id, exp: AcademicYearId, ClassId, DivisionId, StudentId, StudentName
        /// </summary>
        /// <param name="RegisterId">Register id</param>
        /// <returns>Details as dictionary</returns>
        public static Dictionary<string, string> GetStudentDetailsByRegisterId(int RegisterId)
        {
            Dictionary<string, string> studentDetails = new Dictionary<string, string>();
            if (RegisterId > 0)
            {
                string studentFullName = string.Empty;
                StudentBAL studentObject = new StudentBAL();
                IQueryable<Entities.Student> students = studentObject.FindBy(s => s.RegisterId == RegisterId && s.Status == true);
                
                if (students != null && students.Count() > 0)
                {
                    Entities.Student student = students.FirstOrDefault();
                    studentDetails.Add("ClassId", student.ClassId.ToString());
                    studentDetails.Add("DivisionId", student.DivisionId.ToString());
                    studentDetails.Add("StudentId", student.StudentId.ToString());
                    studentDetails.Add("StudentName", string.Concat(student.Title.Trim(), " ", student.FirstName.Trim(), " ", student.MiddleName.Trim(), " ", student.LastName).Trim());
                    studentDetails.Add("ReligionId", student.ReligionId.ToString());
                    studentDetails.Add("CastId", student.CastId.ToString());
                }
            }
            return studentDetails;
        }

        /// <summary>
        /// method for get Reserve Category
        /// </summary>
        /// <param name="ReligionId">Religion Id</param>
        /// <param name="CastId">Cast Id</param>
        /// <returns>return string</returns>
        public static string GetReserveCategory(int ReligionId, int CastId)
        {
            ReligionCastBAL religionCastBAL = new ReligionCastBAL();
            IQueryable<Entities.ReligionCast> religionCasts = religionCastBAL.FindBy(cd => cd.ReligionId == ReligionId && cd.CastId == CastId);
            if (religionCasts != null && religionCasts.Count() > 0)
            {
                return religionCasts.FirstOrDefault().ReserveCategory;
            }
            return string.Empty;
        }

        public static IQueryable<SelectListItem> GetClassesList()
        {
            IQueryable<SelectListItem> classesList = null;

            ClassBAL classBAL = new ClassBAL();
            classesList = from obj in classBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

            return classesList;
        }

        public static IQueryable<SelectListItem> GetDivisionsList(int ClassId)
        {
            IQueryable<SelectListItem> divisionesList = null;
            if (ClassId > 0)
            {
                ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                IQueryable<Entities.ClassDivision> classDivisions = classDivisionBAL.FindBy(cd => cd.ClassId == ClassId);
                var divisionIds = from classDivisionObj in classDivisions select classDivisionObj.DivisionId;
                DivisionBAL divisionsBAL = new DivisionBAL();
                var divisions = divisionsBAL.GetAll().Where(d => d.Status == true);
                divisionesList = from obj in divisions from obj1 in divisionIds where obj.DivisionId == (obj1) select new SelectListItem() { Text = obj.DivisionName, Value = obj.DivisionId.ToString() };
            }
            return divisionesList;
        }

        public static StudentVM getStudentViewModel(Entities.Student entity)
        {
            StudentVM viewModel = new StudentVM();
            viewModel.SrNo = entity.SrNo;
            viewModel.StudentId = entity.StudentId;
            viewModel.RegisterId = entity.RegisterId;
            viewModel.ClassDivisionId = entity.ClassDivisionId;
            viewModel.ClassId = entity.ClassId;
            viewModel.DivisionId = entity.DivisionId;
            viewModel.ClassName = entity.ClassName;
            viewModel.DivisionName = entity.DivisionName;
            viewModel.Title = entity.Title;
            viewModel.FirstName = entity.FirstName;
            viewModel.MiddleName = entity.MiddleName;
            viewModel.LastName = entity.LastName;
            viewModel.MotherName = entity.MotherName;
            viewModel.Gender = entity.Gender;
            viewModel.Nationality = entity.Nationality;
            viewModel.ReligionCastId = entity.ReligionCastId;
            viewModel.ReligionId = entity.ReligionId;
            viewModel.CastId = entity.CastId;
            viewModel.ReligionName = entity.ReligionName;
            viewModel.CastName = entity.CastName;
            viewModel.ReserveCategory = entity.ReserveCategory;
            viewModel.DateOfBirth = entity.DateOfBirth;
            viewModel.PlaceOfBirth = entity.PlaceOfBirth.Trim();
            viewModel.AdharcardNo = entity.AdharcardNo;
            viewModel.DateOfAdmission = entity.DateOfAdmission;
            viewModel.LastSchoolAttended = entity.LastSchoolAttended;
            viewModel.Progress = entity.Progress;
            viewModel.Conduct = entity.Conduct;
            viewModel.DateOfLeavingSchool = entity.DateOfLeavingSchool;
            viewModel.ClassInWhichStudingAndSinceWhen = entity.ClassInWhichStudingAndSinceWhen;
            viewModel.ReasonForLeavingSchool = entity.ReasonForLeavingSchool;
            viewModel.RemarkOnTC = entity.RemarkOnTC;
            viewModel.Status = entity.Status;
            viewModel.Remark = entity.Remark;
            viewModel.PrevSchoolDateofLiving = entity.PrevSchoolDateofLiving;
            viewModel.StudentFullNameWithTitle = string.Concat(entity.Title.Trim(), " ", entity.FirstName.Trim(), " ", entity.MiddleName.Trim(), " ", entity.LastName.Trim()).Trim();
            viewModel.ClassDivision = string.Concat(entity.ClassName.Trim(), "-", entity.DivisionName.Trim()).Trim();
            viewModel.ReligionCast = string.Concat(entity.ReligionName.Trim(), "-", entity.CastName.Trim()).Trim();
            viewModel.TCNo = entity.TCNo;
            viewModel.TCPrinted = entity.TCPrinted;
            viewModel.Balance = entity.Balance;
            viewModel.MobileNumber = entity.MobileNumber;
            return viewModel;
        }

        public static bool GetStudentTCStatus(int StudentId)
        {           
            if (StudentId > 0)
            {
                string studentFullName = string.Empty;
                StudentBAL studentObject = new StudentBAL();
                IQueryable<Entities.Student> students = studentObject.FindBy(s => s.StudentId == StudentId);

                if (students != null && students.Count() > 0)
                {
                    Entities.Student student = students.FirstOrDefault();
                    return student.TCPrinted;
                }
            }
            return false;
        }

        /// <summary>
        /// Get student full name with title by student id
        /// </summary>
        /// <param name="StudentId">Student id</param>
        /// <returns>Student name</returns>
        public static IQueryable<SelectListItem> GetUserList()
        {
            UserBAL userBAL = new UserBAL();
            IQueryable<SelectListItem> userList = from obj in userBAL.GetAll() select new SelectListItem() { Text = obj.UserName, Value = obj.UserId.ToString() };

            return userList;
        }

        public static bool GetValidUser(string UserId, string Password)
        {
            UserBAL userBAL = new UserBAL();
            IQueryable<Entities.User> users = userBAL.GetAll().Where(w => w.UserId == UserId && w.Password == Password);
            if (users != null && users.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public static int GetClassDivisionId(string ClassName, string DivisionName)
        {
            ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
            return classDivisionBAL.GetClassDivisionId(ClassName, DivisionName);
        }

        public static int GetReligionCastId( string ReligionName, string CastName)
        {
            int religionId = 0;
            int CastId = 0;
            int ReligionCastId = 0;
            ReligionBAL religionBAL = new ReligionBAL();
            IQueryable<Entities.Religion> religionEntities = religionBAL.FindBy(r => r.ReligionName.ToLower() == ReligionName.ToLower());            
            if (religionEntities != null && religionEntities.Count() > 0)
            {
                religionId = religionEntities.FirstOrDefault().ReligionId;
            }

            CastBAL CastBAL = new CastBAL();
            IQueryable<Entities.Cast> CastEntities = CastBAL.FindBy(r => r.CastName.ToLower() == CastName.ToLower());
            
            if (CastEntities != null && CastEntities.Count() > 0)
            {
                CastId = CastEntities.FirstOrDefault().CastId;
            }

            ReligionCastBAL ReligionCastBAL = new ReligionCastBAL();
            IQueryable<Entities.ReligionCast> ReligionCastEntities = ReligionCastBAL.FindBy(r => r.ReligionId == religionId && r.CastId == CastId);            
            if (ReligionCastEntities != null && ReligionCastEntities.Count() > 0)
            {
                ReligionCastId = ReligionCastEntities.FirstOrDefault().ReligionCastId;
            }

            return ReligionCastId;
        }
    }
}