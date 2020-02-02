using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StudentAttendanceBAL
    {


        public void AddStudentAttendance(StudentAttendance entity)
        {
            StudentAttendanceDAL dalObject = new StudentAttendanceDAL();
            dalObject.Add(entity);
        }

       
        public void AddStudentAttendance(List<StudentAttendance> entities)
        {
            StudentAttendanceDAL dalObject = new StudentAttendanceDAL();
            dalObject.AddStudentAttendance(entities);
        }

        public IQueryable<StudentAttendance> GetAllStudentAttendance(int ClassId, int DivisionId, DateTime DateOfAttendance)
        {
            StudentAttendanceDAL dalObject = new StudentAttendanceDAL();
            return dalObject.GetAllStudentAttendance(ClassId, DivisionId, DateOfAttendance);
        }
    }
}
