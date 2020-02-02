using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StateBAL
    {
        public IQueryable<State> GetAll()
        {
            StateDAL dalObject = new StateDAL();
            IQueryable<State> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<State> FindBy(System.Linq.Expressions.Expression<Func<State, bool>> predicate)
        {
            StateDAL dalObject = new StateDAL();
            IQueryable<State> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(State entity)
        {
            StateDAL dalObject = new StateDAL();
            dalObject.Add(entity);
        }

        public void Edit(State entity)
        {
            StateDAL dalObject = new StateDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            StateDAL dalObject = new StateDAL();
            dalObject.Delete(id);
        }
    }
}
