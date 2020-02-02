using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class SchoolDetailsBAL
    {
        public IQueryable<SchoolDetails> FindBy(System.Linq.Expressions.Expression<Func<SchoolDetails, bool>> predicate)
        {
            SchoolDetailsDAL dalObject = new SchoolDetailsDAL();
            IQueryable<SchoolDetails> results = dalObject.FindBy(predicate);
            return results;
        }

        public IQueryable<SchoolDetails> GetAll()
        {
            SchoolDetailsDAL dalObject = new SchoolDetailsDAL();
            IQueryable<SchoolDetails> results = dalObject.GetAll();
            return results;
        }
    }
}
