using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoleDAL : IGenericRepository<Role>
    {
        public IQueryable<Role> GetAll()
        {
            try
            {
                List<Role> entites = new List<Role>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT RoleDetails.* FROM RoleDetails;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Role entity = new Role();
                            entity.Id = Convert.ToInt32(dr["RoleId"]);
                            entity.RoleName = dr["Role"].ToString();
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

        public IQueryable<Role> FindBy(System.Linq.Expressions.Expression<Func<Role, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Role entity)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Role> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
