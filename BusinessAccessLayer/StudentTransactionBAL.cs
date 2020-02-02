using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StudentTransactionBAL
    {
        public IQueryable<StudentTransaction> GetAll()
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            IQueryable<StudentTransaction> results = dalObject.GetAll(0);
            return results;
        }

        public IQueryable<StudentTransaction> GetAll(int schoolId)
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            IQueryable<StudentTransaction> results = dalObject.GetAll(schoolId);
            return results;
        }
        public IQueryable<StudentTransaction> FindBy(System.Linq.Expressions.Expression<Func<StudentTransaction, bool>> predicate)
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            IQueryable<StudentTransaction> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(StudentTransaction entity,int SchoolId)
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            dalObject.Add(entity, SchoolId);
        }

        public void Edit(StudentTransaction entity)
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            dalObject.Delete(id);
        }

        public IQueryable<StudentTransactionSub> GetAllStudentTransactionSubByTransactionId(long StudentTransactionId)
        {
            StudentTransactionDAL dalObject = new StudentTransactionDAL();
            return dalObject.GetAllStudentTransactionSubByTransactionId(StudentTransactionId);
        }
    }
}
