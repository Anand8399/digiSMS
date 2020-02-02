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
    public class CityDAL : IGenericRepository<City>
    {
        public IQueryable<City> GetAll()
        {
            try
            {
                List<City> entites = new List<City>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT CityDetails.CityId, CityDetails.City, CityDetails.DistrictId, DistrictDetails.District,DistrictDetails.District, CityDetails.Status, CityDetails.Remark"
+ " FROM CityDetails INNER JOIN DistrictDetails ON CityDetails.DistrictId = DistrictDetails.DistrictId;");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            City entity = new City();
                            entity.CityId = Convert.ToInt32(dr["CityId"]);
                            entity.CityName = dr["City"].ToString();

                            entity.DistrictId = Convert.ToInt32(dr["DistrictId"]);
                            entity.DistrictName = dr["District"].ToString();
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

        public IQueryable<City> FindBy(System.Linq.Expressions.Expression<Func<City, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(City entity)
        {
            try
            {

                //string queryString = string.Format(" INSERT INTO [CityDetails]([DistrictId],[City],[Status],[Remark]) VALUES({0}, '{1}','{2}', '{3}');", entity.DistrictId, entity.CityName, entity.Status, entity.Remark);
                //string s;
                //s = queryString;

                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@DistrictId", entity.DistrictId);
                parameterlist.Add("@City", entity.CityName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_CityDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [CityDetails] WHERE CitytId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(City entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [CityDetails] " +
        //" SET [DistrictId] = {0}" +
        //  ",[City] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE CityId  = {4};", entity.DistrictId, entity.CityName, entity.Status, entity.Remark, entity.CityId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@CityId", entity.CityId);
                parameterlist.Add("@DistrictId", entity.DistrictId);
                parameterlist.Add("@City", entity.CityName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                
                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_CityDetailsUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<City> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
