using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class DivisionBAL
    {
        public IQueryable<Division> GetAll()
        {
            DivisionDAL dalObject = new DivisionDAL();
            IQueryable<Division> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Division> FindBy(System.Linq.Expressions.Expression<Func<Division, bool>> predicate)
        {
            DivisionDAL dalObject = new DivisionDAL();
            IQueryable<Division> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Division entity)
        {
            DivisionDAL dalObject = new DivisionDAL();
            dalObject.Add(entity);
        }

        public void Edit(Division entity)
        {
            DivisionDAL dalObject = new DivisionDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            DivisionDAL dalObject = new DivisionDAL();
            dalObject.Delete(id);
        }
    }
}
