using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StudentParentBAL
    {
        public IQueryable<StudentParent> GetAll()
        {
            StudentParentDAL dalObject = new StudentParentDAL();
            IQueryable<StudentParent> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<StudentParent> FindBy(System.Linq.Expressions.Expression<Func<StudentParent, bool>> predicate)
        {
            StudentParentDAL dalObject = new StudentParentDAL();
            IQueryable<StudentParent> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(StudentParent entity)
        {
            StudentParentDAL dalObject = new StudentParentDAL();
            dalObject.Add(entity);
        }

        public void Edit(StudentParent entity)
        {
            StudentParentDAL dalObject = new StudentParentDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            StudentParentDAL dalObject = new StudentParentDAL();
            dalObject.Delete(id);
        }
    }
}
