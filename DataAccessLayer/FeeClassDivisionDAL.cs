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
    public class FeeClassDivisionDAL : IGenericRepository<FeeClassDivision>
    {
        public IQueryable<FeeClassDivision> GetAll()
        {
            try
            {
                List<FeeClassDivision> entites = new List<FeeClassDivision>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT     FeeClassDivisionDetails.FeeClassDivisionId, FeeClassDivisionDetails.FeeHeadId, "
                      + " FeeClassDivisionDetails.ClassDivisionId, FeeClassDivisionDetails.PeriodInMonthly, FeeClassDivisionDetails.AmountInMonthly, "
                      + " FeeClassDivisionDetails.AmountInYearly, FeeClassDivisionDetails.[Status], FeeClassDivisionDetails.Remark, "
                      + " RTRIM(LTRIM(FeeHeadDetails.FeeHead)) AS FeeHead, "
                      + " ClassDivisionDetails.ClassId, ClassDivisionDetails.DivisionId, "
                      + " RTRIM(LTRIM(ClassDetails.Class)) AS Class, RTRIM(LTRIM(DivisionDetails.Division)) AS Division "
+ " FROM         FeeClassDivisionDetails INNER JOIN "
                      + " FeeHeadDetails ON FeeClassDivisionDetails.FeeHeadId = FeeHeadDetails.FeeHeadId INNER JOIN "
                      + " ClassDivisionDetails ON FeeClassDivisionDetails.ClassDivisionId = ClassDivisionDetails.ClassDivisionId INNER JOIN "
                      + " ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN "
                      + " DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            FeeClassDivision entity = new FeeClassDivision();
                            entity.FeeClassDivisionId = Convert.ToInt32(dr["FeeClassDivisionId"]);
                            entity.FeeHeadId = Convert.ToInt32(dr["FeeHeadId"]);
                            entity.ClassDivisionId = Convert.ToInt32( dr["ClassDivisionId"]);
                            entity.PeriodInMonthly = Convert.ToInt32(dr["PeriodInMonthly"]);
                            entity.AmountInMonthly = Convert.ToDecimal(dr["AmountInMonthly"]);
                            entity.AmountInYearly = Convert.ToDecimal(dr["AmountInYearly"]);
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            entity.FeeHeadName = Convert.ToString(dr["FeeHead"]);
                            entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                            entity.ClassName = Convert.ToString(dr["Class"]);
                            entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                            entity.DivisionName = Convert.ToString(dr["Division"]);
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

        public IQueryable<FeeClassDivision> FindBy(System.Linq.Expressions.Expression<Func<FeeClassDivision, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(FeeClassDivision entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [FeeClassDivisionDetails]([AcademicYearId],[FeeHeadId],[ClassDivisionId],[PeriodInMonthly],[AmountInMonthly],[AmountInYearly],[Status],[Remark]) VALUES({0}, {1},{2}, {3},{4},{5},{6},'{7}');", entity.AcademicYearId, entity.FeeHeadId, entity.ClassDivisionId, entity.PeriodInMonthly, entity.AmountInMonthly, entity.AmountInYearly, entity.Status == true ? 1 : 0, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@FeeHeadId", entity.FeeHeadId);
                parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);
                parameterlist.Add("@PeriodInMonthly", entity.PeriodInMonthly);
                parameterlist.Add("@AmountInMonthly", entity.AmountInMonthly);
                parameterlist.Add("@AmountInYearly", entity.AmountInYearly);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);


                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_FeeClassDivisionInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [FeeClassDivisionDetails] WHERE FeeClassDivisionId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(FeeClassDivision entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [FeeClassDivisionDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[FeeHeadId] = {1}" +
        //  ",[ClassDivisionId] = {2}" +
        //  ",[PeriodInMonthly] ={3}" +
        //  ",[AmountInMonthly] = {4}" +
        //  ",[AmountInYearly] = {5}" +
        //  ",[Status] = {6}" +
        //  ",[Remark] = '{7}'" +
        //" WHERE FeeClassDivisionId  = {8};"
        //,entity.AcademicYearId, entity.FeeHeadId, entity.ClassDivisionId, entity.PeriodInMonthly, 
        //entity.AmountInMonthly, entity.AmountInYearly, entity.Status == true ? 1 : 0, entity.Remark, entity.FeeClassDivisionId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@FeeHeadId", entity.FeeHeadId);
                parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);
                parameterlist.Add("@PeriodInMonthly", entity.PeriodInMonthly);
                parameterlist.Add("@AmountInMonthly", entity.AmountInMonthly);
                parameterlist.Add("@AmountInYearly", entity.AmountInYearly);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@FeeClassDivisionId", entity.FeeClassDivisionId);


                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_FeeClassDivisionUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<FeeClassDivision> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
