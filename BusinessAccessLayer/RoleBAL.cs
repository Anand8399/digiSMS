using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class RoleBAL
    {
        public IQueryable<Role> FindBy(System.Linq.Expressions.Expression<Func<Role, bool>> predicate)
        {
            RoleDAL dalObject = new RoleDAL();
            IQueryable<Role> results = dalObject.FindBy(predicate);
            return results;
        }

        public IQueryable<Role> GetAll()
        {
            RoleDAL dalObject = new RoleDAL();
            IQueryable<Role> results = dalObject.GetAll();
            return results;
        }
    }
}
