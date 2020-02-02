using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class UserBAL
    {
        public IQueryable<User> FindBy(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            UserDAL dalObject = new UserDAL();
            IQueryable<User> results = dalObject.FindBy(predicate);
            return results;
        }

        public IQueryable<User> GetAll()
        {
            UserDAL dalObject = new UserDAL();
            IQueryable<User> results = dalObject.GetAll();
            return results;
        }

        public void ChangePassword(User entity)
        {
            UserDAL dalObject = new UserDAL();
            dalObject.ChangePassword(entity);            
        }
    }
}
