using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StudentAddressBAL
    {
        public IQueryable<StudentAddress> GetAll()
        {
            StudentAddressDAL dalObject = new StudentAddressDAL();
            IQueryable<StudentAddress> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<StudentAddress> FindBy(System.Linq.Expressions.Expression<Func<StudentAddress, bool>> predicate)
        {
            StudentAddressDAL dalObject = new StudentAddressDAL();
            IQueryable<StudentAddress> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(StudentAddress entity)
        {
            StudentAddressDAL dalObject = new StudentAddressDAL();
            dalObject.Add(entity);
        }

        public void Edit(StudentAddress entity)
        {
            StudentAddressDAL dalObject = new StudentAddressDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            StudentAddressDAL dalObject = new StudentAddressDAL();
            dalObject.Delete(id);
        }

        public IQueryable<StudentAddress> GetAll(Int64 StudentId)
        {
            StudentAddressDAL dalObject = new StudentAddressDAL();
            IQueryable<StudentAddress> results = dalObject.GetAll(StudentId);
            return results;
        }
    }
}
