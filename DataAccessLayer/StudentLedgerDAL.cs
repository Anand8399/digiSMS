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
    public class StudentLedgerDAL : IGenericRepository<StudentLedger>
    {
        public IQueryable<StudentLedger> GetAll()
        {
            try
            {
                List<StudentLedger> entites = new List<StudentLedger>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT StudentLedger.* FROM StudentLedger;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            StudentLedger entity = new StudentLedger();
                            entity.StudentLedgerId = Convert.ToInt32(dr["StudentLedgerId"]);
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            entity.FeeHeadId = Convert.ToInt32(dr["FeeHeadId"]);
                            entity.Cr = Convert.ToDecimal(dr["Cr"]);
                            entity.Dr = Convert.ToDecimal(dr["Dr"]);
                            entity.HeadBalance = Convert.ToDecimal(dr["HeadBalance"]);
                            entity.Balance = Convert.ToDecimal(dr["Balance"]);
                            entity.ReceiptNo = Convert.ToInt32(dr["ReceiptNo"]);
                            entity.BankName = Convert.ToString(dr["BankName"]);
                            entity.ChequeNumber = Convert.ToString(dr["ChequeNumber"]);
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

        public IQueryable<StudentLedger> FindBy(System.Linq.Expressions.Expression<Func<StudentLedger, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(StudentLedger entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [StudentLedger]([AcademicYearId],[StudentId],[TransactionDate],[FeeHeadId],[Cr],[Dr],[HeadBalance],[Balance],[ReceiptNo],[BankName],[ChequeNumber],[Status],[Remark],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}');"
                //    , entity.AcademicYearId, entity.StudentId, entity.TransactionDate, entity.FeeHeadId, entity.Cr, entity.Dr, entity.HeadBalance, entity.Balance, entity.Status == true ? 1 : 0, entity.Remark, entity.CreatedBy, entity.CreatedDate, entity.ModifiedBy, entity.ModifiedDate);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@TransactionDate", entity.TransactionDate);
                parameterlist.Add("@FeeHeadId", entity.FeeHeadId);
                parameterlist.Add("@Cr", entity.Cr);
                parameterlist.Add("@Dr", entity.Dr);
                parameterlist.Add("@HeadBalance", entity.HeadBalance);
                parameterlist.Add("@Balance", entity.Balance);
                parameterlist.Add("@ReceiptNo", entity.ReceiptNo);
                parameterlist.Add("@BankName", entity.BankName);
                parameterlist.Add("@ChequeNumber", entity.ChequeNumber);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@CreatedBy", entity.CreatedBy);
                parameterlist.Add("@CreatedDate", entity.CreatedDate);
                parameterlist.Add("@ModifiedBy", entity.ModifiedBy);
                parameterlist.Add("@ModifiedDate", entity.ModifiedDate);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentLedgerInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [StudentLedger] WHERE StudentLedgerId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(StudentLedger entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [StudentLedger] " +
        //" SET [AcademicYearId] = {0}" +
        //",[StudentId] ={1}" + ",[TransactionDate] ='{2}'" + ",[FeeHeadId] ={3}" + ",[Cr] ={4}" + ",[Dr] ={5}" + ",[HeadBalance] ={6}" + ",[Balance]={7}" + 
        //",[Status]={8}" + ",[Remark]='{9}'" + ",[CreatedBy]='{10}'" + ",[CreatedDate]='{11}'" + ",[ModifiedBy]='{12}'" + ",[ModifiedDate]='{13}'" +
        //",[ReceiptNo] ='{14}',[BankName] ='{15},[ChequeNumber] ='{16}" +
        //" WHERE StudentLedgerId  = {17};", entity.AcademicYearId, 
        //entity.StudentId, entity.TransactionDate, entity.FeeHeadId, entity.Cr, entity.Dr, entity.HeadBalance, entity.Balance, 
        //entity.Status == true ? 1 : 0, entity.Remark, entity.CreatedBy, entity.CreatedDate, entity.ModifiedBy, entity.ModifiedDate, 
        //entity.ReceiptNo, entity.BankName, entity.ChequeNumber, entity.StudentLedgerId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);


                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@TransactionDate", entity.TransactionDate);
                parameterlist.Add("@FeeHeadId", entity.FeeHeadId);
                parameterlist.Add("@Cr", entity.Cr);
                parameterlist.Add("@Dr", entity.Dr);
                parameterlist.Add("@HeadBalance", entity.HeadBalance);
                parameterlist.Add("@Balance", entity.Balance);
                parameterlist.Add("@ReceiptNo", entity.ReceiptNo);
                parameterlist.Add("@BankName", entity.BankName);
                parameterlist.Add("@ChequeNumber", entity.ChequeNumber);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@CreatedBy", entity.CreatedBy);
                parameterlist.Add("@CreatedDate", entity.CreatedDate);
                parameterlist.Add("@ModifiedBy", entity.ModifiedBy);
                parameterlist.Add("@ModifiedDate", entity.ModifiedDate);
                parameterlist.Add("@StudentLedgerId", entity.StudentLedgerId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentLedgerUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<StudentLedger> getStudentLedger(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId, int schoolId)
        {
            try
            {
                List<StudentLedger> entites = new List<StudentLedger>();
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@SchoolId", schoolId);
                parameterlist.Add("@RegisterId", RegisterId);
                parameterlist.Add("@ClassId", ClassId);
                parameterlist.Add("@DivisionId", DivisionId);
                parameterlist.Add("@ReligionId", ReligionId);
                parameterlist.Add("@CastId", CastId);
                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_StudentLedger", parameterlist);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            StudentLedger entity = new StudentLedger();
                            entity.StudentLedgerId = Convert.ToInt32(dr["StudentLedgerId"]);
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            entity.FeeHeadId = Convert.ToInt32(dr["FeeHeadId"]);
                            entity.Cr = Convert.ToDecimal(dr["Cr"]);
                            entity.Dr = Convert.ToDecimal(dr["Dr"]);
                            entity.HeadBalance = Convert.ToDecimal(dr["HeadBalance"]);
                            entity.Balance = Convert.ToDecimal(dr["Balance"]);
                            entity.ReceiptNo = Convert.ToInt32(dr["ReceiptNo"]);
                            entity.BankName = Convert.ToString(dr["BankName"]);
                            entity.ChequeNumber = Convert.ToString(dr["ChequeNumber"]);
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            entity.Remark = Convert.ToString(dr["Remark"]);
                            entity.StudentFullNameWithTitle = string.Concat(Convert.ToString(dr["Title"]).Trim(), " ", Convert.ToString(dr["FirstName"]).Trim(), " ", Convert.ToString(dr["MiddleName"]).Trim(), " ", Convert.ToString(dr["LastName"]).Trim()).Trim();
                            entity.FeeHeadName = Convert.ToString(dr["FeeHead"]);
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
        public IQueryable<StudentLedger> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }

    }
}
