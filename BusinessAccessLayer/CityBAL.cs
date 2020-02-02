using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class CityBAL
    {
        public IQueryable<City> GetAll()
        {
            CityDAL dalObject = new CityDAL();
            IQueryable<City> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<City> FindBy(System.Linq.Expressions.Expression<Func<City, bool>> predicate)
        {
            CityDAL dalObject = new CityDAL();
            IQueryable<City> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(City entity)
        {
            CityDAL dalObject = new CityDAL();
            dalObject.Add(entity);
        }

        public void Edit(City entity)
        {
            CityDAL dalObject = new CityDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            CityDAL dalObject = new CityDAL();
            dalObject.Delete(id);
        }
    }
}
