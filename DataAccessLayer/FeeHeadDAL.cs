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
    public class FeeHeadDAL : IGenericRepository<FeeHead>
    {
        public IQueryable<FeeHead> GetAll()
        {
            try
            {
                List<FeeHead> entites = new List<FeeHead>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT FeeHeadDetails.* FROM FeeHeadDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            FeeHead entity = new FeeHead();
                            entity.FeeHeadId = Convert.ToInt32(dr["FeeHeadId"]);
                            entity.FeeHeadName = dr["FeeHead"].ToString();
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

        public IQueryable<FeeHead> FindBy(System.Linq.Expressions.Expression<Func<FeeHead, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(FeeHead entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [FeeHeadDetails]([AcademicYearId],[FeeHead],[Status],[Remark]) VALUES({0}, '{1}',{2}, '{3}');", entity.AcademicYearId, entity.FeeHeadName, entity.Status == true ? 1 : 0, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@FeeHead", entity.FeeHeadName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_FeesHeadDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [FeeHeadDetails] WHERE FeeHeadId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(FeeHead entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [FeeHeadDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[FeeHead] = '{1}'" +
        //  ",[Status] = {2}" +
        //  ",[Remark] = '{3}'" +
        //  " WHERE FeeHeadId  = {4};", entity.AcademicYearId, entity.FeeHeadName, entity.Status == true ? 1 : 0, entity.Remark, entity.FeeHeadId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@FeeHead", entity.FeeHeadName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@FeeHeadId", entity.FeeHeadId);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_FeesHeadDetailsupdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public IQueryable<FeeHead> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
