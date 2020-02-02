using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class ClassBAL
    {
        public IQueryable<Class> GetAll()
        {
            ClassDAL dalObject = new ClassDAL();
            IQueryable<Class> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Class> FindBy(System.Linq.Expressions.Expression<Func<Class, bool>> predicate)
        {
            ClassDAL dalObject = new ClassDAL();
            IQueryable<Class> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Class entity)
        {
            ClassDAL dalObject = new ClassDAL();
            dalObject.Add(entity);
        }

        public void Edit(Class entity)
        {
            ClassDAL dalObject = new ClassDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            ClassDAL dalObject = new ClassDAL();
            dalObject.Delete(id);
        }
    }
}
