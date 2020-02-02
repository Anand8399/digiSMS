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
    public class AccountDAL : IGenericRepository<Account>
    {
        public IQueryable<Account> GetAll(int schoolId)
        {
            try
            {
                List<Account> entites = new List<Account>();

                String SqlQuery = "select * from Account";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";


                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Account entity = new Account();
                            entity.SrNo = Convert.ToInt32(dr["SrNo"]);
                            entity.NarrationDetails = Convert.ToString(dr["NarrationDetails"]);
                            entity.TransactionType = Convert.ToString(dr["TransactionType"]);
                            entity.PaymentMode = Convert.ToString(dr["PaymentMode"]);
                            entity.Amount = Convert.ToDecimal(dr["Amount"]);
                            //entity.Balance = Convert.ToDecimal(dr["Balance"]);
                            entity.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            entity.Remark = dr["Remark"].ToString();

                            entity.CustomerName = dr["CustomerName"].ToString();
                            entity.BankName = dr["BankName"].ToString();
                            entity.ChqDDNumber = dr["ChqDDNumber"].ToString();
                            entity.ContactNo = dr["ContactNo"].ToString();

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

        public IQueryable<Account> FindBy(System.Linq.Expressions.Expression<Func<Account, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Account entity, int schoolId)
        {
            try
            {

                string queryString = string.Format("INSERT INTO [Account]([SchoolId], [NarrationDetails],[TransactionType],[PaymentMode], [Amount], [TransactionDate], [Remark],[CustomerName],[BankName],[ChqDDNumber],[ContactNo])" +
                    "VALUES('{0}', N'{1}',N'{2}', '{3}', '{4}', GETDATE(), N'{5}', N'{6}', N'{7}',N'{8}',N'{9}');", schoolId, entity.NarrationDetails, entity.TransactionType, entity.PaymentMode, entity.Amount, entity.Remark, entity.CustomerName, entity.BankName, entity.ChqDDNumber, entity.ContactNo);


                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Account entity)
        {
            Add(entity, 0);   
        }
       
        public void Delete(int Id)
        {
            try
            {
                string queryString = string.Format(" DELETE FROM [Account] WHERE AccountId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Account entity)
        {

            try
            {

               
                string queryString = string.Format(" UPDATE [Account] " +
                    " SET [NarrationDetails] = N'{0}'" +
                    ",[TransactionType] = N'{1}'" +
                    ",[PaymentMode] = N'{2}'" +
                    ",[Amount] = '{3}'" +
                    //",[Balance] = '{4}'" +
                    ",[TransactionDate]= GETDATE()" +
                    ",[Remark]='{4}'" +
                    ",[CustomerName]=N'{5}'" +
                    ",[BankName]=N'{6}'" +
                    ",[ChqDDNumber]=N'{7}'" +
                    ",[ContactNo]=N'{8}'" +
                     " WHERE SrNo  = {9};", entity.NarrationDetails, entity.TransactionType, entity.PaymentMode, entity.Amount,
                      entity.Remark, entity.CustomerName, entity.BankName, entity.ChqDDNumber,
                     entity.ContactNo, entity.SrNo);
                string s = queryString;
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
      
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public double getAccountBalance()
        {
            double returnValue=0;

             DataTable dataTable = CommanMethodsForSQL.GetDataTable("select Balance from Account where SrNo = (select MAX(SrNo) from Account as t)");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                            returnValue = Convert.ToInt32(dr[0]);
                    }
                }
            return returnValue;
        }

        public IQueryable<Account> GetAll()
        {
            return GetAll(0);
        }
    }
}
