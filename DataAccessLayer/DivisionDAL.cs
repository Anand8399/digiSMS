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
    public class DivisionDAL : IGenericRepository<Division>
    {
        public IQueryable<Division> GetAll()
        {
            try
            {
                List<Division> entites = new List<Division>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT DivisionDetails.DivisionId, DivisionDetails.Division,"
                + " DivisionDetails.Status, DivisionDetails.Remark FROM DivisionDetails ;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Division entity = new Division();
                            entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);

                            entity.DivisionName = dr["Division"].ToString();
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

        public IQueryable<Division> FindBy(System.Linq.Expressions.Expression<Func<Division, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Division entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [DivisionDetails]([AcademicYearId],[Division],[Status],[Remark]) VALUES({0}, '{1}','{2}', '{3}');", entity.AcademicYearId, entity.DivisionName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@Division", entity.DivisionName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_DivisionDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [DivisionDetails] WHERE DivisionId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Division entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [DivisionDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[Division] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE DivisionId  = {4};", entity.AcademicYearId, entity.DivisionName, entity.Status, entity.Remark, entity.DivisionId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@Division", entity.DivisionName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@DivisionId", entity.DivisionId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_DivisionDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Division> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
