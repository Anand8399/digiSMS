using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SchoolDetailsDAL : IGenericRepository<SchoolDetails>
    {
        public IQueryable<SchoolDetails> GetAll()
        {
            try
            {
                List<SchoolDetails> entites = new List<SchoolDetails>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT SchoolDetails.* FROM SchoolDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            SchoolDetails entity = new SchoolDetails();
                            entity.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                            entity.ManagementName = dr["ManagementName"].ToString();
                            entity.SchoolName = dr["SchoolName"].ToString();
                            entity.Address = dr["Address"].ToString();
                            entity.Taluka = dr["Taluka"].ToString();
                            entity.District = dr["District"].ToString();
                            entity.ContactNumber = dr["ContactNumber"].ToString();
                            entity.EmailId = dr["EmailId"].ToString();
                            entity.SchoolReconginationNo = dr["SchoolReconginationNo"].ToString();
                            entity.Medium = dr["Medium"].ToString();
                            entity.UDiscNo = dr["UDiscNo"].ToString();
                            entity.Board = dr["Board"].ToString();
                            entity.AffilationNo = dr["AffilationNo"].ToString();
                            entity.LogoPath = dr["LogoPath"].ToString();
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

        public IQueryable<SchoolDetails> FindBy(System.Linq.Expressions.Expression<Func<SchoolDetails, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(SchoolDetails entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(SchoolDetails entity)
        {
            throw new NotImplementedException();
        }
        public IQueryable<SchoolDetails> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
