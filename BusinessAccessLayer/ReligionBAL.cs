using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class ReligionBAL
    {
        public IQueryable<Religion> GetAll()
        {
            ReligionDAL dalObject = new ReligionDAL();
            IQueryable<Religion> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Religion> FindBy(System.Linq.Expressions.Expression<Func<Religion, bool>> predicate)
        {
            ReligionDAL dalObject = new ReligionDAL();
            IQueryable<Religion> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Religion entity)
        {
            ReligionDAL dalObject = new ReligionDAL();
            dalObject.Add(entity);
        }

        public void Edit(Religion entity)
        {
            ReligionDAL dalObject = new ReligionDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            ReligionDAL dalObject = new ReligionDAL();
            dalObject.Delete(id);
        }
    }
}
