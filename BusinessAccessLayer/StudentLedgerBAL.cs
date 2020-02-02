using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class StudentLedgerBAL
    {
        public IQueryable<StudentLedger> GetAll()
        {
            StudentLedgerDAL dalObject = new StudentLedgerDAL();
            IQueryable<StudentLedger> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<StudentLedger> FindBy(System.Linq.Expressions.Expression<Func<StudentLedger, bool>> predicate)
        {
            StudentLedgerDAL dalObject = new StudentLedgerDAL();
            IQueryable<StudentLedger> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(StudentLedger entity)
        {
            StudentLedgerDAL dalObject = new StudentLedgerDAL();
            dalObject.Add(entity);
        }

        public void Edit(StudentLedger entity)
        {
            StudentLedgerDAL dalObject = new StudentLedgerDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            StudentLedgerDAL dalObject = new StudentLedgerDAL();
            dalObject.Delete(id);
        }

        public IQueryable<StudentLedger> getStudentLedger(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId, int schoolId)
        {
            StudentLedgerDAL dalObject = new StudentLedgerDAL();
            IQueryable<StudentLedger> results = dalObject.getStudentLedger(RegisterId, ClassId, DivisionId, ReligionId, CastId, schoolId);
            return results;
        }
    }
}
