using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class DistrictBAL
    {
        public IQueryable<District> GetAll()
        {
            DistrictDAL dalObject = new DistrictDAL();
            IQueryable<District> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<District> FindBy(System.Linq.Expressions.Expression<Func<District, bool>> predicate)
        {
            DistrictDAL dalObject = new DistrictDAL();
            IQueryable<District> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(District entity)
        {
            DistrictDAL dalObject = new DistrictDAL();
            dalObject.Add(entity);
        }

        public void Edit(District entity)
        {
            DistrictDAL dalObject = new DistrictDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            DistrictDAL dalObject = new DistrictDAL();
            dalObject.Delete(id);
        }
    }
}
