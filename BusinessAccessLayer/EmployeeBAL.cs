using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class EmployeeBAL
    {
        public IQueryable<Employee> GetAll(int schoolId)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            IQueryable<Employee> results = dalObject.GetAll(schoolId);
            return results;
        }

        public IQueryable<Employee> GetAll()
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            IQueryable<Employee> results = dalObject.GetAll();
            return results;
        }
        public IQueryable<Employee> FindBy(System.Linq.Expressions.Expression<Func<Employee, bool>> predicate)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            IQueryable<Employee> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Employee entity, int schoolId)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            dalObject.Add(entity, schoolId);
        }

        public void Add(Employee entity)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            dalObject.Add(entity);
        }

        public void Edit(Employee entity)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            dalObject.Delete(id);
        }


        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaveAssign(int schoolId)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            return dalObject.GetAllEmployeeLeaveApply(schoolId);
        }

        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaveAssign()
        {

            return GetAllEmployeeLeaveAssign(0);
        }

        public void addLeaves(List<EmployeeLeaveTransaction> entities)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            dalObject.addLeaves(entities);
        }

        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaveApply(int schoolId)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            return dalObject.GetAllEmployeeLeaveApply(schoolId);
        }
        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaveApply()
        {

            return GetAllEmployeeLeaveApply(0);
        }

        public void leaveApply(EmployeeLeaveTransaction entity)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            dalObject.leaveApply(entity);
        }

        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaves(int schoolId)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
            IQueryable<EmployeeLeaveTransaction> results = dalObject.GetAllEmployeeLeaves(schoolId);
            return results;
        }

        public decimal getLeaveBalance(int EmpId)
        {
            EmployeeDAL dalObject = new EmployeeDAL();
           return dalObject.getLeaveBalance(EmpId);
        }
    }
}