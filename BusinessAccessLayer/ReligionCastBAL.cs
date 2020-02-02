using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class ReligionCastBAL
    {
        public IQueryable<ReligionCast> GetAll()
        {
            ReligionCastDAL dalObject = new ReligionCastDAL();
            IQueryable<ReligionCast> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<ReligionCast> FindBy(System.Linq.Expressions.Expression<Func<ReligionCast, bool>> predicate)
        {
            ReligionCastDAL dalObject = new ReligionCastDAL();
            IQueryable<ReligionCast> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(ReligionCast entity)
        {
            ReligionCastDAL dalObject = new ReligionCastDAL();
            dalObject.Add(entity);
        }

        public void Edit(ReligionCast entity)
        {
            ReligionCastDAL dalObject = new ReligionCastDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            ReligionCastDAL dalObject = new ReligionCastDAL();
            dalObject.Delete(id);
        }
    }
}
