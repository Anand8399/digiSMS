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
    public class StudentTransactionDAL : IGenericRepository<StudentTransaction>
    {
        public IQueryable<StudentTransaction> GetAll(int SchoolId)
        {
            try
            {
                List<StudentTransaction> entites = new List<StudentTransaction>();

                String SqlQuery = "select * from viewGetAllStudentTransaction";
                if (SchoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + SchoolId;

                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        if (dr != null)
                        {
                            StudentTransaction entity = new StudentTransaction();
                            entity.StudentTransactionId = Convert.ToInt32(dr["StudentTransactionId"]);
                            entity.ClassDivisionId = Convert.ToInt32(dr["ClassDivisionId"]);
                            entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                            entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                            entity.ClassName = Convert.ToString(dr["Class"]);
                            entity.DivisionName = Convert.ToString(dr["Division"]);
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.StudentFullNameWithTitle = Convert.ToString(dr["StudentFullName"]);
                            entity.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            entity.ReceiptNo = Convert.ToInt32(dr["ReceiptNo"]);
                            entity.ReceiptTotal = Convert.ToDecimal(dr["ReceiptTotal"]);
                            entity.BankName = dr["BankName"].ToString();
                            entity.ChequeNumber = dr["ChequeNumber"].ToString();
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            entity.Remark = dr["Remark"].ToString();
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

        public IQueryable<StudentTransaction> FindBy(System.Linq.Expressions.Expression<Func<StudentTransaction, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(StudentTransaction entity)
        {
        }
        public void Add(StudentTransaction entity, int schoolId)
        {

            String SqlQuery = "select max(StudentTransactionId) As ReceiptNo from  StudentTransaction";
            if (schoolId != 0)
                SqlQuery = SqlQuery + " where SchoolId=" + schoolId +";";

            DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    if (dr != null)
                    {
                        if (dr["ReceiptNo"] != DBNull.Value)
                        {
                            entity.ReceiptNo = Convert.ToInt32(dr["ReceiptNo"]) + 1;
                        }
                        else
                            entity.ReceiptNo = 1;
                    }                        
                    else
                        entity.ReceiptNo = 1;   
                }
            }   
            try
            {
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@SchoolId", schoolId);
                parameterlist.Add("@TransactionDate", entity.TransactionDate);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@ReceiptNo", entity.ReceiptNo);
                parameterlist.Add("@BankName", entity.BankName);
                parameterlist.Add("@ChequeNumber", entity.ChequeNumber);
                parameterlist.Add("@ReceiptTotal", entity.ReceiptTotal);
                parameterlist.Add("@UserId", 0);
                parameterlist.Add("@OUT_StudentTransactionId", null);
                Hashtable outparameterlist = new Hashtable();
                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentTransactionInsert", parameterlist, ref outparameterlist);
                int studentTransactionId = 0;
                if (outparameterlist != null && outparameterlist.Count > 0)
                {
                    foreach (string parametername in outparameterlist.Keys)
                    {
                        if (parametername.StartsWith("@OUT_"))
                        {
                            studentTransactionId = Convert.ToInt32(outparameterlist[parametername]);
                        }
                    }
                }
                if (studentTransactionId > 0 && entity.StudentTransactionSubList != null && entity.StudentTransactionSubList.Count > 0)
                {
                    foreach (Entities.StudentTransactionSub studentTransactionSub in entity.StudentTransactionSubList)
                    {
                        parameterlist = new Hashtable();
                        parameterlist.Add("@StudentTransactionId", studentTransactionId);
                        parameterlist.Add("@FeeHead", studentTransactionSub.FeeHeadId);
                        parameterlist.Add("@Cr", studentTransactionSub.Cr);
                        parameterlist.Add("@DR", studentTransactionSub.Dr);
                        parameterlist.Add("@Balance", studentTransactionSub.Balance);
                        parameterlist.Add("@Remark", studentTransactionSub.Remark);
                        parameterlist.Add("@StudentId", entity.StudentId);
                        parameterlist.Add("@TransactionDate", entity.TransactionDate);
                        parameterlist.Add("@ReceiptNo", entity.ReceiptNo);
                        parameterlist.Add("@BankName", entity.BankName);
                        parameterlist.Add("@ChequeNumber", entity.ChequeNumber);
                        parameterlist.Add("@UserId", 0);
                        parameterlist.Add("@OUT_StudentTransactionSubId", null);
                        outparameterlist = new Hashtable();
                        effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentTransactionSubInsert", parameterlist, ref outparameterlist);
                    }
                }

                //string queryString = string.Format(" INSERT INTO [StudentTransaction]([AcademicYearId],[ClassDivisionId],[StudentId],[TransactionDate],[Status],[Remark]) VALUES({0}, {1},{2}, '{3}', {4}, '{5}');", 
                //    entity.AcademicYearId, entity.ClassDivisionId, entity.StudentId,entity.TransactionDate, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int Id)
        {
            try
            {
                string queryString = string.Format(" DELETE FROM [StudentTransaction] WHERE StudentTransactionId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(StudentTransaction entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [StudentTransaction] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[StudentTransaction] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE StudentTransactionId  = {4};", entity.AcademicYearId, entity.StudentTransactionName, entity.Status, entity.Remark, entity.StudentTransactionId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<StudentTransactionSub> GetAllStudentTransactionSubByTransactionId(long StudentTransactionId)
        {
            try
            {
                List<StudentTransactionSub> entites = new List<StudentTransactionSub>();

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentTransactionId", StudentTransactionId);
                
                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_GetAllStudentTransactionSub", parameterlist);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            StudentTransactionSub entity = new StudentTransactionSub();
                            entity.StudentTransactionId = Convert.ToInt32(dr["StudentTransactionId"]);
                            entity.StudentTransactionSubId = Convert.ToInt32(dr["StudentTransactionSubId"]);
                            entity.FeeHeadId = Convert.ToInt32(dr["FeeHead"]);
                            entity.FeeHeadName = Convert.ToString(dr["FeeHeadName"]);
                            entity.Cr = Convert.ToDecimal(dr["Cr"]);
                            entity.Dr = Convert.ToDecimal(dr["Dr"]);
                            entity.Balance = Convert.ToDecimal(dr["Balance"]);
                            entity.Remark = dr["Remark"].ToString();
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
        public IQueryable<StudentTransaction> GetAll()
        {
            return GetAll(0);
        }
    }
}
