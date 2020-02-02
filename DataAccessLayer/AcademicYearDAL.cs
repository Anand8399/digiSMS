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

    public class AcademicYearDAL : IGenericRepository<AcademicYear>
    {


        public IQueryable<AcademicYear> GetAll()
        {
            try
            {
                List<AcademicYear> entites = new List<AcademicYear>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT AcademicYearId, LTRIM(RTRIM(AcademicYear)) AS AcademicYear, StartDate, EndDate, Status, Remark "
                + " FROM AcademicYearDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            AcademicYear entity = new AcademicYear();
                            entity.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"]);
                            entity.AcademicYearName = dr["AcademicYear"].ToString();
                            entity.StartDate = Convert.ToDateTime(dr["StartDate"]);
                            entity.EndDate = Convert.ToDateTime(dr["EndDate"]);
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

        public IQueryable<AcademicYear> FindBy(System.Linq.Expressions.Expression<Func<AcademicYear, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(AcademicYear entity)
        {
            try
            {
               //string queryString = string.Format(" INSERT INTO [AcademicYearDetails] ([AcademicYear],[StartDate],[EndDate],[Status],[Remark]) VALUES('{0}', '{1}','{2}', '{3}','{4}');", entity.AcademicYearName, entity.StartDate, entity.EndDate, entity.Status, entity.Remark);
               //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@AcademicYear", entity.AcademicYearName);
                parameterlist.Add("@StartDate", entity.StartDate);
                parameterlist.Add("@EndDate", entity.EndDate);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_AcademicYearInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [AcademicYearDetails] WHERE AcademicYearId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(AcademicYear entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [AcademicYearDetails] " +
        //" SET [AcademicYear] = '{0}'" +
        //  ",[StartDate] = '{1}'" +
        //  ",[EndDate] = '{2}'" +
        //  ",[Status] = '{3}'" +
        //  ",[Remark] = '{4}'" +
        //" WHERE AcademicYearId  = {5};", entity.AcademicYearName, entity.StartDate, entity.EndDate, entity.Status, entity.Remark, entity.AcademicYearId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@AcademicYearName", entity.AcademicYearName);
                parameterlist.Add("@StartDate", entity.StartDate);
                parameterlist.Add("@EndDate", entity.EndDate);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@AcademicYearId", entity.AcademicYearId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_AcademicYearUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<AcademicYear> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
