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
    public class CastDAL : IGenericRepository<Cast>
    {
        public IQueryable<Cast> GetAll()
        {
            try
            {
                List<Cast> entites = new List<Cast>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT CastId, LTRIM(RTRIM([Cast])) as CastName, Status, Remark FROM CastDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Cast entity = new Cast();
                            entity.CastId = Convert.ToInt32(dr["CastId"]);
                            entity.CastName = Convert.ToString(dr["CastName"]);
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

        public IQueryable<Cast> FindBy(System.Linq.Expressions.Expression<Func<Cast, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Cast entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [CastDetails]([AcademicYearId],[Cast],[Status],[Remark]) VALUES({0}, '{1}','{2}', '{3}');", entity.AcademicYearId, entity.CastName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@Cast", entity.CastName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_CastDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [CastDetails] WHERE CastId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Cast entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [CastDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[Cast] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE CastId  = {4};", entity.AcademicYearId, entity.CastName, entity.Status, entity.Remark, entity.CastId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@CastId", entity.CastId);
                parameterlist.Add("@Cast", entity.CastName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);



                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_CastDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Cast> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
