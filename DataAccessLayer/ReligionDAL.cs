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
    public class ReligionDAL : IGenericRepository<Religion>
    {
        public IQueryable<Religion> GetAll()
        {
            try
            {
                List<Religion> entites = new List<Religion>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT ReligionDetails.ReligionId, LTRIM(RTRIM(ReligionDetails.Religion)) AS Religion, "
		        + " ReligionDetails.Status, ReligionDetails.Remark"
                + " FROM  ReligionDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Religion entity = new Religion();
                            entity.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                            entity.ReligionName = Convert.ToString(dr["Religion"]);
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

        public IQueryable<Religion> FindBy(System.Linq.Expressions.Expression<Func<Religion, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Religion entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [ReligionDetails] ([AcademicYearId],[Religion],[Status],[Remark]) VALUES({0}, '{1}','{2}','{3}');", entity.AcademicYearId, entity.ReligionName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@Religion", entity.ReligionName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ReligionDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [ReligionDetails] WHERE ReligionId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Religion entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [ReligionDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[Religion] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE [ReligionId]  = {4};", entity.AcademicYearId, entity.ReligionName, entity.Status, entity.Remark, entity.ReligionId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@Religion", entity.ReligionName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@ReligionId", entity.ReligionId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ReligionDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Religion> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
