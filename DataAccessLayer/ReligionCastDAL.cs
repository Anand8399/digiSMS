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
    public class ReligionCastDAL : IGenericRepository<ReligionCast>
    {
        public IQueryable<ReligionCast> GetAll()
        {
            try
            {
                List<ReligionCast> entites = new List<ReligionCast>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT     ReligionCastDetails.ReligionCastId, ReligionCastDetails.ReligionId, ReligionCastDetails.CastId, ReligionCastDetails.Status, "
                      + " ReligionCastDetails.Remark, ReligionCastDetails.ReserveCategory, LTRIM(RTRIM(ReligionDetails.Religion)) AS Religion, LTRIM(RTRIM(CastDetails.Cast)) AS Cast "
                        + " FROM         ReligionCastDetails INNER JOIN "
                      + " CastDetails ON ReligionCastDetails.CastId = CastDetails.CastId INNER JOIN "
                      +" ReligionDetails ON ReligionCastDetails.ReligionId = ReligionDetails.ReligionId;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            ReligionCast entity = new ReligionCast();
                            entity.ReligionCastId = Convert.ToInt32(dr["ReligionCastId"]);
                            entity.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                            entity.ReligionName = Convert.ToString(dr["Religion"]);
                            entity.CastId = Convert.ToInt32(dr["CastId"]);
                            entity.CastName = Convert.ToString(dr["Cast"]);
                            entity.ReserveCategory = Convert.ToString(dr["ReserveCategory"]);
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

        public IQueryable<ReligionCast> FindBy(System.Linq.Expressions.Expression<Func<ReligionCast, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(ReligionCast entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [ReligionCastDetails] ([AcademicYearId],[ReligionId],[CastId],[ReserveCategory],[Status],[Remark]) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", entity.AcademicYearId, entity.ReligionId, entity.CastId, entity.ReserveCategory, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString); 
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ReligionId", entity.ReligionId);
                parameterlist.Add("@CastId", entity.CastId);
                parameterlist.Add("@ReserveCategory", entity.ReserveCategory);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ReligionCastDetailsInsert", parameterlist);

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
                string queryString = string.Format(" DELETE FROM [ReligionCastDetails] WHERE ReligionCastId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(ReligionCast entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [ReligionCastDetails] " +
        //" SET [ReligionId] = {0}" +
        //  ",[CastId] = {1}" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //  ",[AcademicYearId] ='{4}'" +
        //  ",[ReserveCategory] = '{5}'" +
        //" WHERE ReligionCastId  = {6};", entity.ReligionId, entity.CastId, entity.Status, entity.Remark, entity.AcademicYearId, entity.ReserveCategory, entity.ReligionCastId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ReligionId", entity.ReligionId);
                parameterlist.Add("@CastId", entity.CastId);
                parameterlist.Add("@ReserveCategory", entity.ReserveCategory);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@ReligionCastId", entity.ReligionCastId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ReligionCastDetailsUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<ReligionCast> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
