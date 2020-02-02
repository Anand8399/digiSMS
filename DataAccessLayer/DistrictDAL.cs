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
    public class DistrictDAL : IGenericRepository<District>
    {
        public IQueryable<District> GetAll()
        {
            try
            {
                List<District> entites = new List<District>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT  DistrictDetails.DistrictId, DistrictDetails.StateId, RTRIM(LTRIM(DistrictDetails.District)) AS District, DistrictDetails.[Status], "
                        + " DistrictDetails.Remark, "
                      + " RTRIM(LTRIM(StateDetails.[State])) AS [State], RTRIM(LTRIM(CountryDetails.Country)) AS Country, StateDetails.CountryId "
                        + " FROM DistrictDetails INNER JOIN "
                      + " StateDetails ON DistrictDetails.StateId = StateDetails.StateId INNER JOIN "
                      + " CountryDetails ON StateDetails.CountryId = CountryDetails.CountryId;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            District entity = new District();
                            entity.DistrictId = Convert.ToInt32(dr["DistrictId"]);
                            entity.StateId = Convert.ToInt32(dr["StateId"]);
                            entity.StateName = dr["State"].ToString();
                            entity.CountryName = dr["Country"].ToString();
                            entity.DistrictName = dr["District"].ToString();
                            entity.CountryId = Convert.ToInt32(dr["CountryId"]);
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

        public IQueryable<District> FindBy(System.Linq.Expressions.Expression<Func<District, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(District entity)
        {
            try
            {

                //string queryString = string.Format(" INSERT INTO [DistrictDetails]([AcademicYearId],[StateId],[District],[Status],[Remark]) VALUES({0}, '{1}','{2}', '{3}', '{4}');", entity.AcademicYearId, entity.StateId, entity.DistrictName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StateId", entity.StateId);
                parameterlist.Add("@District", entity.DistrictName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_DistrictDetailsInsert", parameterlist);

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
                string queryString = string.Format(" DELETE FROM [DistrictDetails] WHERE DistrictId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(District entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [DistrictDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //",[StateId] = '{1}'" +
        //  ",[District] = '{2}'" +
        //  ",[Status] = '{3}'" +
        //  ",[Remark] = '{4}'" +
        //" WHERE DistrictId  = {5};", entity.AcademicYearId, entity.StateId, entity.DistrictName, entity.Status, entity.Remark, entity.DistrictId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StateId", entity.StateId);
                parameterlist.Add("@DistrictName", entity.DistrictName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@DistrictId", entity.DistrictId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_DistrictDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<District> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
