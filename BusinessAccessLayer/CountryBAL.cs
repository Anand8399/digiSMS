using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class CountryBAL
    {
        public IQueryable<Country> GetAll()
        {
            CountryDAL dalObject = new CountryDAL();
            IQueryable<Country> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Country> FindBy(System.Linq.Expressions.Expression<Func<Country, bool>> predicate)
        {
            CountryDAL dalObject = new CountryDAL();
            IQueryable<Country> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Country entity)
        {
            CountryDAL dalObject = new CountryDAL();
            dalObject.Add(entity);
        }

        public void Edit(Country entity)
        {
            CountryDAL dalObject = new CountryDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            CountryDAL dalObject = new CountryDAL();
            dalObject.Delete(id);
        }
    }
}
