using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class AcademicYearBAL
    {
        public IQueryable<AcademicYear> GetAll()
        {
            AcademicYearDAL dalObject = new AcademicYearDAL();
            IQueryable<AcademicYear> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<AcademicYear> FindBy(System.Linq.Expressions.Expression<Func<AcademicYear, bool>> predicate)
        {
            AcademicYearDAL dalObject = new AcademicYearDAL();
            IQueryable<AcademicYear> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(AcademicYear entity)
        {
            AcademicYearDAL dalObject = new AcademicYearDAL();
            dalObject.Add(entity);
        }

        public void Edit(AcademicYear entity)
        {
            AcademicYearDAL dalObject = new AcademicYearDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            AcademicYearDAL dalObject = new AcademicYearDAL();
            dalObject.Delete(id);
        }
    }
}
