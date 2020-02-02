using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDAL : IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetAll(int schoolId)
        {
            try
            {
                List<Employee> entites = new List<Employee>();

                String SqlQuery = "select * from Employee";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";


                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Employee entity = new Employee();
                            entity.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                            entity.EmployeeCode = Convert.ToString(dr["EmployeeCode"]);
                            entity.Password = Convert.ToString(dr["Password"]);
                            entity.FirstName = Convert.ToString(dr["FirstName"]);
                            entity.MiddleName = Convert.ToString(dr["MiddleName"]);
                            entity.LastName = Convert.ToString(dr["LastName"]);
                            entity.Category = Convert.ToString(dr["Category"]);
                            entity.Address = Convert.ToString(dr["Address"]);
                            entity.ContactNo = Convert.ToString(dr["ContactNo"]);


                            if (dr["DOB"] != DBNull.Value)
                            {
                                entity.DOB = Convert.ToDateTime(dr["DOB"]);
                            }
                            if (dr["JoiningDate"] != DBNull.Value)
                            {
                               entity.JoiningDate = Convert.ToDateTime(dr["JoiningDate"].ToString());
                            }
                      
                            
                            entites.Add(entity);
                        }
                    }
                }

                return entites.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Employee> FindBy(System.Linq.Expressions.Expression<Func<Employee, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Employee entity, int schoolId)
        {
            try
            {
                string queryString = string.Format("INSERT INTO [Employee]([SchoolId],[EmployeeCode],[Password],[FirstName],[MiddleName],[LastName],[Category],[Address],[ContactNo],[DOB],[JoiningDate])" +
                " VALUES( N'{0}', N'{1}',N'{2}',N'{3}', N'{4}', N'{5}', N'{6}',N'{7}', N'{8}', convert(date,'{9}',105), convert(date,'{10}',105)) ;", schoolId, entity.EmployeeCode,entity.Password, entity.FirstName, entity.MiddleName, entity.LastName, entity.Category, entity.Address, entity.ContactNo, entity.DOB, entity.JoiningDate);
                string s = queryString;
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Employee entity)
        {
            Add(entity, 0);
        }

        public void Delete(int Id)
        {
            try
            {
                string queryString = string.Format(" DELETE FROM [Employee] WHERE EmployeeId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Employee entity)
        {
            try
            {

           string queryString = string.Format(" UPDATE [Employee] " +

       " SET [EmployeeCode] = '{0}'" +
          ",[FirstName] = '{1}'" +
          ",[MiddleName] = '{2}'" +
          ",[LastName] = '{3}'" +
          ",[Category]='{4}'" +
          ",[Address]='{5}'" +
          ",[ContactNo]='{6}'" +
          ",[DOB]=convert(date,'{7}',105)" +
           ",[JoiningDate]=convert(date,'{8}',105)" +

        " WHERE EmployeeId  = {9};", entity.EmployeeCode, entity.FirstName, entity.MiddleName, entity.LastName, entity.Category, entity.Address, entity.ContactNo, entity.DOB, entity.JoiningDate, entity.EmployeeId);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
   
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Employee> GetAll(){
            return GetAll(0);
        }

        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaveAssign(int schoolId)
        {
            try
            {
                List<EmployeeLeaveTransaction> entites = new List<EmployeeLeaveTransaction>();

                String SqlQuery = "select * from viewGetEmployeeListForAddLeave";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";


                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            EmployeeLeaveTransaction entity = new EmployeeLeaveTransaction();
                            entity.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);

                            entity.EmployeeFullName = Convert.ToString(dr["FirstName"]).Trim() + " " + Convert.ToString(dr["MiddleName"]).Trim() + " " + Convert.ToString(dr["LastName"]).Trim();

                            entity.BalanceLeaves = 0;
                            if (dr["BalanceLeaves"] != DBNull.Value)
                            {
                                entity.BalanceLeaves = Convert.ToDecimal(dr["BalanceLeaves"]);
                            }


                            entites.Add(entity);
                        }
                    }
                }
                return entites.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void addLeaves(List<EmployeeLeaveTransaction> entities)
        {
            try
            {

                string queryString;
                foreach (var entity in entities)
                {
                    if (entity.LeavesInDays != 0)
                    {
                        queryString = string.Format("INSERT INTO [EmployeeLeaveTransaction]([EmployeeId],[TransactionType],[LeaveType],[LeaveFromDate],[LeaveToDate],[LeavesInDays],[BalanceLeaves],[Remark],[TransactionDate])" +
                        "VALUES('{0}', N'{1}',N'{2}',GETDATE(), GETDATE(), '{3}', '{4}', N'{5}',GETDATE());", entity.EmployeeId, "Cr", 1, entity.LeavesInDays, entity.BalanceLeaves, entity.Remark);


                        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void leaveApply(EmployeeLeaveTransaction entity)
        {
            try
            {



                string queryString;

                    if (entity.LeavesInDays != 0)
                    {
                        queryString = string.Format("INSERT INTO [EmployeeLeaveTransaction]([EmployeeId],[TransactionType],[LeaveType],[LeaveFromDate],[LeaveToDate],[LeavesInDays],[BalanceLeaves],[Remark],[TransactionDate])" +
                        "VALUES('{0}', N'{1}',N'{2}',convert(date,'{3}',105), convert(date,'{4}',105), '{5}', '{6}', N'{7}',GETDATE());", entity.EmployeeId, "Dr", entity.LeaveType, entity.LeaveFromDate, entity.LeaveToDate, entity.LeavesInDays, entity.BalanceLeaves, entity.Remark);


                        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                    }
                }
             
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaveApply(int schoolId)
        {
            try
            {
                List<EmployeeLeaveTransaction> entites = new List<EmployeeLeaveTransaction>();

                String SqlQuery = "select * from viewGetEmployeeListForAddLeave";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";


                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            EmployeeLeaveTransaction entity = new EmployeeLeaveTransaction();
                            entity.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);

                            entity.EmployeeFullName = Convert.ToString(dr["FirstName"]).Trim() + " " + Convert.ToString(dr["MiddleName"]).Trim() + " " + Convert.ToString(dr["LastName"]).Trim();

                            entity.BalanceLeaves = 0;
                            if (dr["BalanceLeaves"] != DBNull.Value)
                            {
                                entity.BalanceLeaves = Convert.ToDecimal(dr["BalanceLeaves"]);
                            }


                            entites.Add(entity);
                        }
                    }
                }
                return entites.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IQueryable<EmployeeLeaveTransaction> GetAllEmployeeLeaves(int schoolId)
        {
            try
            {
                List<EmployeeLeaveTransaction> entites = new List<EmployeeLeaveTransaction>();

                String SqlQuery = "SELECT EmployeeCode, FirstName, MiddleName, LastName, TransactionType,EmployeeLeaveTransaction.EmployeeId as EmpId, LeaveType, LeaveFromDate,LeaveToDate, LeavesInDays, BalanceLeaves,Remark, TransactionDate"+
                                   " FROM Employee INNER JOIN EmployeeLeaveTransaction ON Employee.EmployeeId = EmployeeLeaveTransaction.EmployeeId";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";


                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            EmployeeLeaveTransaction entity = new EmployeeLeaveTransaction();
                            entity.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            entity.EmployeeFullName = Convert.ToString(dr["FirstName"]).Trim() + " " + Convert.ToString(dr["MiddleName"]).Trim() + " " + Convert.ToString(dr["LastName"]).Trim();

                            entity.LeaveType = Convert.ToInt32(dr["LeaveType"]);
                            entity.LeaveFromDate = Convert.ToDateTime(dr["LeaveFromDate"]);
                            entity.LeaveToDate = Convert.ToDateTime(dr["LeaveToDate"]);
                            
                            if (dr["LeavesInDays"] != DBNull.Value)
                            {
                                entity.LeavesInDays = Convert.ToDecimal(dr["LeavesInDays"]);
                            }

                            if (dr["BalanceLeaves"] != DBNull.Value)
                            {
                                entity.BalanceLeaves = Convert.ToDecimal(dr["BalanceLeaves"]);
                            }

                            entity.Remark=Convert.ToString(dr["Remark"]);

                            entity.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);


                            entity.TransactionType = 0;

                            if (Convert.ToString(dr["TransactionType"]).Trim() == "Cr")
                            {
                                entity.TransactionType = 1;
                            }
                            

                            entites.Add(entity);
                        }
                    }
                }
                return entites.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public decimal getLeaveBalance(int employeeId)
        {
            try
            {

                decimal leaveBalance = 0;
                String SqlQuery = "SELECT BalanceLeaves from EmployeeLeaveTransaction where EmployeeId=" + employeeId + ";";

                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {

                            leaveBalance = Convert.ToDecimal(dr["BalanceLeaves"]);
                        }
                    }
                }

                return leaveBalance;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
