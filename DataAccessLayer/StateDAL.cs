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
    public class StateDAL : IGenericRepository<State>
    {
        public IQueryable<State> GetAll()
        {
            try
            {
                List<State> entites = new List<State>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT     StateDetails.StateId, StateDetails.CountryId, RTRIM(LTRIM(StateDetails.[State])) AS [State], StateDetails.[Status], StateDetails.Remark, "
                      + " RTRIM(LTRIM(CountryDetails.Country)) AS Country "
                        + " FROM         StateDetails INNER JOIN "
                      + " CountryDetails ON StateDetails.CountryId = CountryDetails.CountryId;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            State entity = new State();
                            entity.StateId = Convert.ToInt32(dr["StateId"]);
                            entity.CountryId = Convert.ToInt32(dr["CountryId"]);
                            entity.CountryName = dr["Country"].ToString();
                            entity.StateName = dr["State"].ToString();
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

        public IQueryable<State> FindBy(System.Linq.Expressions.Expression<Func<State, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(State entity)
        {
            try
            {

                //string queryString = string.Format(" INSERT INTO [StateDetails]([AcademicYearId],[CountryId],[State],[Status],[Remark]) VALUES({0}, '{1}','{2}', '{3}', '{4}');", entity.AcademicYearId, entity.CountryId, entity.StateName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@CountryId", entity.CountryId);
                parameterlist.Add("@State", entity.StateName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StateDetailsInsert", parameterlist);

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
                string queryString = string.Format(" DELETE FROM [StateDetails] WHERE StateId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(State entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [StateDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //",[CountryId] = '{1}'" +
        //  ",[State] = '{2}'" +
        //  ",[Status] = '{3}'" +
        //  ",[Remark] = '{4}'" +
        //" WHERE StateId  = {5};", entity.AcademicYearId, entity.CountryId, entity.StateName, entity.Status, entity.Remark, entity.StateId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@CountryId", entity.CountryId);
                parameterlist.Add("@State", entity.StateName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@StateId", entity.StateId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StateDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<State> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
