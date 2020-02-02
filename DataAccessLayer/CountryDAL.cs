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
    public class CountryDAL: IGenericRepository<Country>
    {
        public IQueryable<Country> GetAll()
        {
            try
            {
                List<Country> entites = new List<Country>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT CountryDetails.CountryId, RTRIM(LTRIM(CountryDetails.Country)) AS Country, CountryDetails.[Status], CountryDetails.Remark "
                    + " FROM         CountryDetails ;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Country entity = new Country();
                            entity.CountryId = Convert.ToInt32(dr["CountryId"]);
                            entity.CountryName = dr["Country"].ToString();
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

        public IQueryable<Country> FindBy(System.Linq.Expressions.Expression<Func<Country, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Country entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [CountryDetails]([AcademicYearId],[Country],[Status],[Remark]) VALUES({0}, '{1}','{2}', '{3}');", entity.AcademicYearId, entity.CountryName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@CountryName", entity.CountryName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_CountryInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [CountryDetails] WHERE CountryId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Country entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [CountryDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[Country] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE CountryId  = {4};", entity.AcademicYearId, entity.CountryName, entity.Status, entity.Remark, entity.CountryId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@CountryId", entity.CountryId);
                parameterlist.Add("@CountryName", entity.CountryName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_CountryUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Country> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
