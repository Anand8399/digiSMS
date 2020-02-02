using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class ClassDivisionBAL
    {
        public IQueryable<ClassDivision> GetAll()
        {
            ClassDivisionDAL dalObject = new ClassDivisionDAL();
            IQueryable<ClassDivision> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<ClassDivision> FindBy(System.Linq.Expressions.Expression<Func<ClassDivision, bool>> predicate)
        {
            ClassDivisionDAL dalObject = new ClassDivisionDAL();
            IQueryable<ClassDivision> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(ClassDivision entity)
        {
            ClassDivisionDAL dalObject = new ClassDivisionDAL();
            dalObject.Add(entity);
        }

        public void Edit(ClassDivision entity)
        {
            ClassDivisionDAL dalObject = new ClassDivisionDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            ClassDivisionDAL dalObject = new ClassDivisionDAL();
            dalObject.Delete(id);
        }
        public int GetClassDivisionId(string ClassName, string DivisionName)
        {
            ClassDivisionDAL dalObject = new ClassDivisionDAL();
            return dalObject.GetClassDivisionId(ClassName, DivisionName);
        }
    }
}
