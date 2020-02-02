using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
   public class AccountBAL
    {
       public IQueryable<Account> GetAll(int schoolId)
        {
            AccountDAL dalObject = new AccountDAL();
            IQueryable<Account> results = dalObject.GetAll(schoolId);
            return results;
        }

       public IQueryable<Account> GetAll()
       {
           AccountDAL dalObject = new AccountDAL();
           IQueryable<Account> results = dalObject.GetAll();
           return results;
       }

       public IQueryable<Account> FindBy(System.Linq.Expressions.Expression<Func<Account, bool>> predicate)
        {
            AccountDAL dalObject = new AccountDAL();
            IQueryable<Account> results = dalObject.FindBy(predicate);
            return results;
        }

       public void Add(Account entity, int schoolId)
       {
           AccountDAL dalObject = new AccountDAL();
           dalObject.Add(entity, schoolId);
       }

       public void Add(Account entity)
        {
            AccountDAL dalObject = new AccountDAL();
            dalObject.Add(entity);
        }

       public void Edit(Account entity)
        {
            AccountDAL dalObject = new AccountDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            AccountDAL dalObject = new AccountDAL();
            dalObject.Delete(id);
        }
    }
}
