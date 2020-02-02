using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class FeeHeadBAL
    {
        public IQueryable<FeeHead> GetAll()
        {
            FeeHeadDAL dalObject = new FeeHeadDAL();
            IQueryable<FeeHead> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<FeeHead> FindBy(System.Linq.Expressions.Expression<Func<FeeHead, bool>> predicate)
        {
            FeeHeadDAL dalObject = new FeeHeadDAL();
            IQueryable<FeeHead> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(FeeHead entity)
        {
            FeeHeadDAL dalObject = new FeeHeadDAL();
            dalObject.Add(entity);
        }

        public void Edit(FeeHead entity)
        {
            FeeHeadDAL dalObject = new FeeHeadDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            FeeHeadDAL dalObject = new FeeHeadDAL();
            dalObject.Delete(id);
        }
    }
}
