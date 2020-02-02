using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class CastBAL
    {
        public IQueryable<Cast> GetAll()
        {
            CastDAL dalObject = new CastDAL();
            IQueryable<Cast> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Cast> FindBy(System.Linq.Expressions.Expression<Func<Cast, bool>> predicate)
        {
            CastDAL dalObject = new CastDAL();
            IQueryable<Cast> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Cast entity)
        {
            CastDAL dalObject = new CastDAL();
            dalObject.Add(entity);
        }

        public void Edit(Cast entity)
        {
            CastDAL dalObject = new CastDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            CastDAL dalObject = new CastDAL();
            dalObject.Delete(id);
        }
    }
}
