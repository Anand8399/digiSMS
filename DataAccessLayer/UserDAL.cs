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
    public class UserDAL : IGenericRepository<User>
    {
        public IQueryable<User> GetAll()
        {
            try
            {
                List<User> Users = new List<User>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT UserDetails.SrNo, UserDetails.RoleId, ltrim(rtrim(UserDetails.UserId)) as UserId, ltrim(rtrim(UserDetails.Password)) as Password, UserDetails.UserName, UserDetails.Status, UserDetails.Remark, RoleDetails.Role, SchoolId" +
" FROM UserDetails INNER JOIN RoleDetails ON UserDetails.RoleId = RoleDetails.RoleId;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            User user = new User();
                            user.SrNo = Convert.ToInt32(dr["SrNo"]);
                            user.UserId = dr["UserId"].ToString();
                            user.RoleId = Convert.ToInt16(dr["RoleId"]);
                            user.UserName = dr["UserName"].ToString();
                            user.Status = Convert.ToBoolean(dr["Status"].ToString());
                            user.Remark = dr["Remark"].ToString();
                            user.UserRole = new Role();
                            user.UserRole.RoleName = dr["Role"].ToString();
                            user.Password = Convert.ToString(dr["Password"]);
                            user.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                            Users.Add(user);
                        }
                    }                    
                }

                return Users.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<User> FindBy(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(User entity)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(User entity)
        {
            Hashtable parameterlist = new Hashtable();
            parameterlist.Add("@UserId", entity.UserId);
            parameterlist.Add("@Password", entity.Password);
            CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ChangePassword", parameterlist);
        }
        public IQueryable<User> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
