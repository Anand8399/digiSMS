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
    public class ClassDAL :  IGenericRepository<Class>
    {
        public IQueryable<Class> GetAll()
        {
            try
            {
                List<Class> entites = new List<Class>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT ClassId, ClassDetails.Class, "
                  +  " ClassDetails.Status, ClassDetails.Remark FROM ClassDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Class entity = new Class();
                            entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                            entity.ClassName = dr["Class"].ToString();
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

        public IQueryable<Class> FindBy(System.Linq.Expressions.Expression<Func<Class, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Class entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [ClassDetails] ([AcademicYearId],[Class],[Status],[Remark]) VALUES({0}, '{1}','{2}','{3}');", entity.AcademicYearId, entity.ClassName, entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@Class", entity.ClassName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ClassDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [ClassDetails] WHERE ClassId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Class entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [ClassDetails] " +
        //" SET [AcademicYearId] = {0}" +
        //  ",[Class] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //" WHERE [ClassId]  = {4};", entity.AcademicYearId, entity.ClassName, entity.Status, entity.Remark, entity.ClassId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassId", entity.ClassId);
                parameterlist.Add("@Class", entity.ClassName);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ClassDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Class> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
