using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StudentBAL
    {
        public IQueryable<Student> GetAll(int schoolId)
        {
            StudentDAL dalObject = new StudentDAL();
            IQueryable<Student> results = dalObject.GetAll(schoolId);
            return results;
        }

        public IQueryable<Student> GetAll()
        {
            StudentDAL dalObject = new StudentDAL();
            IQueryable<Student> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Student> FindBy(System.Linq.Expressions.Expression<Func<Student, bool>> predicate)
        {
            StudentDAL dalObject = new StudentDAL();
            IQueryable<Student> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Student entity)
        {
            StudentDAL dalObject = new StudentDAL();
            dalObject.Add(entity);
        }

        public void Edit(Student entity)
        {
            StudentDAL dalObject = new StudentDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            StudentDAL dalObject = new StudentDAL();
            dalObject.Delete(id);
        }

        /// <summary>
        /// Add new student record
        /// </summary>
        /// <param name="entity">Student entity</param>
        /// <returns>Student Id</returns>
        public int AddStudent(Student entity, int SchoolId)
        {
            StudentDAL dalObject = new StudentDAL();
            return dalObject.AddStudent(entity, SchoolId);
        }

        public IQueryable<StudentClassChange> GetStudentClassChange(int ClassDivisionId, int schoolId)
        {
            StudentDAL dalObject = new StudentDAL();
            return dalObject.GetStudentClassChange(ClassDivisionId, schoolId);
        }

        public void StudentChangeClassDivision(StudentClassChange entity, int schoolId)
        {
            StudentDAL dalObject = new StudentDAL();
            dalObject.StudentChangeClassDivision(entity, schoolId);
        }

        public IQueryable<Student> SearchStudents(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId, bool Status, int schoolId)
        {
            StudentDAL dalObject = new StudentDAL();
            IQueryable<Student> results = dalObject.SearchStudents(RegisterId, ClassId, DivisionId, ReligionId, CastId, Status, schoolId);
            return results;
        }

        public void UpdateTCDetails(int StudentId)
        {
            StudentDAL dalObject = new StudentDAL();
            dalObject.UpdateTCDetails(StudentId);
        }

        public long GetMaxTCNo()
        {
            StudentDAL dalObject = new StudentDAL();
            return dalObject.GetMaxTCNo();
        }

        public IQueryable<Student> getStudentsWithBalance( int ClassId, int DivisionId, int ReligionId, int CastId)
        {
            StudentDAL dalObject = new StudentDAL();
            IQueryable<Student> results = dalObject.getStudentsWithBalance(ClassId, DivisionId, ReligionId, CastId);
            return results;
        }
    }
}
