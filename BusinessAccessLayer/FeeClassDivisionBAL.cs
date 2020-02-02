using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class FeeClassDivisionBAL
    {
        public IQueryable<FeeClassDivision> GetAll()
        {
            FeeClassDivisionDAL dalObject = new FeeClassDivisionDAL();
            IQueryable<FeeClassDivision> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<FeeClassDivision> FindBy(System.Linq.Expressions.Expression<Func<FeeClassDivision, bool>> predicate)
        {
            FeeClassDivisionDAL dalObject = new FeeClassDivisionDAL();
            IQueryable<FeeClassDivision> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(FeeClassDivision entity)
        {
            FeeClassDivisionDAL dalObject = new FeeClassDivisionDAL();
            dalObject.Add(entity);
        }

        public void Edit(FeeClassDivision entity)
        {
            FeeClassDivisionDAL dalObject = new FeeClassDivisionDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            FeeClassDivisionDAL dalObject = new FeeClassDivisionDAL();
            dalObject.Delete(id);
        }
    }
}
