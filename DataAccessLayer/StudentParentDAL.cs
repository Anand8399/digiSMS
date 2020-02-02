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
    public class StudentParentDAL : IGenericRepository<StudentParent>
    {
        public IQueryable<StudentParent> GetAll()
        {
            try
            {
                List<StudentParent> entites = new List<StudentParent>();
                //DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT StudentParentDetails.* FROM StudentParentDetails;");
                Hashtable parameterlist = new Hashtable();
                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_GetAllStudentParentDetails", parameterlist);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            StudentParent entity = new StudentParent();
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.Title = Convert.ToString(dr["Title"]);
                            entity.FirstName = Convert.ToString(dr["FirstName"]);
                            entity.MiddleName = Convert.ToString(dr["MiddleName"]);
                            entity.LastName = Convert.ToString(dr["LastName"]);
                            entity.Gender = Convert.ToString(dr["Gender"]);
                            entity.Address = Convert.ToString(dr["Address"]);
                            entity.CountryId = Convert.ToInt32(dr["CountryId"]);
                            entity.StateId = Convert.ToInt32(dr["StateId"]);
                            entity.DistrictId = Convert.ToInt32(dr["DistrictId"]);
                            entity.CityId = Convert.ToInt32(dr["CityId"]);
                            entity.PinCode = Convert.ToInt32(dr["PinCode"]);
                            entity.MobileNumber = Convert.ToInt64(dr["MobileNumber"]);
                            entity.ContactNo = Convert.ToString(dr["ContactNo"]);
                            entity.Occupation = Convert.ToString(dr["Occupation"]);
                            entity.CompanyName = Convert.ToString(dr["CompanyName"]);
                            entity.CompanyAddress = Convert.ToString(dr["CompanyAddress"]);
                            entity.CompanyContactNo = Convert.ToString(dr["CompanyContactNo"]);
                            entity.BankName = Convert.ToString(dr["BankName"]);
                            entity.AccountNo = Convert.ToString(dr["AccountNo"]);
                            entity.Branch = Convert.ToString(dr["Branch"]);
                            entity.IFSCCode = Convert.ToString(dr["IFSCCode"]);
                            entity.StudentFullName = Convert.ToString(dr["StudentFullName"]);
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

        public IQueryable<StudentParent> FindBy(System.Linq.Expressions.Expression<Func<StudentParent, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(StudentParent entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [StudentParentDetails]("
                    
                //            + " [StudentId]"
                //            + ", [Title]"
                //            + ", [FirstName]"
                //            + ", [MiddleName]"
                //            + ", [LastName]"
                //            + ", [Gender]"
                //            + ", [Address]"
                //            + ", [CountryId]"
                //            + ", [StateId]"
                //            + ", [DistrictId]"
                //            + ", [CityId]"
                //            + ", [PinCode]"
                //            + ", [MobileNumber]"
                //            + ", [ContactNo]"
                //            + ", [Occupation]"
                //            + ", [CompanyName]"
                //            + ", [CompanyAddress]"
                //            + ", [CompanyContactNo]"
                //            + ", [Status]"
                //            + ", [Remark]"
                //            + ", [BankName]"
                //            + ", [AccountNo]"
                //            + ", [Branch]"
                //            + ", [IFSCCode]"
                //            + ") VALUES({0}, '{1}','{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}');"
                //            , entity.StudentId, entity.Title, entity.FirstName, entity.MiddleName, entity.LastName, entity.Gender, entity.Address, entity.CountryId
                //            , entity.StateId, entity.DistrictId, entity.CityId, entity.PinCode, entity.MobileNumber, entity.ContactNo, entity.Occupation, entity.CompanyName
                //            ,entity.CompanyAddress, entity.CompanyContactNo, entity.Status == true ? 1 : 0, entity.Remark, entity.BankName, entity.AccountNo, entity.Branch, entity.IFSCCode);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@Title", entity.Title);
                parameterlist.Add("@FirstName", entity.FirstName);
                parameterlist.Add("@MiddleName", entity.MiddleName);
                parameterlist.Add("@LastName", entity.LastName);
                parameterlist.Add("@Gender", entity.Gender);
                parameterlist.Add("@Address", entity.Address);
                parameterlist.Add("@CountryId", entity.CountryId);
                parameterlist.Add("@StateId", entity.StateId);
                parameterlist.Add("@DistrictId", entity.DistrictId);
                parameterlist.Add("@CityId", entity.CityId);
                parameterlist.Add("@PinCode", entity.PinCode);
                parameterlist.Add("@MobileNumber", entity.MobileNumber);
                parameterlist.Add("@ContactNo", entity.ContactNo);
                parameterlist.Add("@Occupation", entity.Occupation);
                parameterlist.Add("@CompanyName", entity.CompanyName);
                parameterlist.Add("@CompanyAddress", entity.CompanyAddress);
                parameterlist.Add("@CompanyContactNo", entity.CompanyContactNo);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@BankName", entity.BankName);
                parameterlist.Add("@AccountNo", entity.AccountNo);
                parameterlist.Add("@Branch", entity.Branch);
                parameterlist.Add("@IFSCCode", entity.IFSCCode);
               
               

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentParentDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [StudentParentDetails] WHERE StudentId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(StudentParent entity)
        {
            try
            {
                //string queryString = string.Format(" UPDATE [StudentParentDetails] "
                //            + " SET  [Title] = '{0}'"
                //            + ", [FirstName] = '{1}'"
                //            + ", [MiddleName] = '{2}'"
                //            + ", [LastName] = '{3}'"
                //            + ", [Gender] = '{4}'"
                //            + ", [Address] = '{5}'"
                //            + ", [CountryId] = '{6}'"
                //            + ", [StateId] = '{7}'"
                //            + ", [DistrictId] = '{8}'"
                //            + ", [CityId] = '{9}'"
                //            + ", [PinCode] = '{10}'"
                //            + ", [MobileNumber] = '{11}'"
                //            + ", [ContactNo] = '{12}'"
                //            + ", [Occupation] = '{13}'"
                //            + ", [CompanyName] = '{14}'"
                //            + ", [CompanyAddress] = '{15}'"
                //            + ", [CompanyContactNo] = '{16}'"
                //            + ", [Status] = {17}"
                //            + ", [Remark] = '{18}'"
                //            + ", [BankName]= '{19}'"
                //            + ", [AccountNo]= '{20}'"
                //            + ", [Branch]= '{21}'"
                //            + ", [IFSCCode]= '{22}'"
                //            + " WHERE  [StudentId] = {23};"
                //            , entity.Title, entity.FirstName, entity.MiddleName, entity.LastName, entity.Gender, entity.Address, entity.CountryId
                //            , entity.StateId, entity.DistrictId, entity.CityId, entity.PinCode, entity.MobileNumber, entity.ContactNo, entity.Occupation, entity.CompanyName
                //            , entity.CompanyAddress, entity.CompanyContactNo, entity.Status == true ? 1 :0, entity.Remark
                //            , entity.BankName, entity.AccountNo, entity.Branch, entity.IFSCCode, entity.StudentId);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
              
                parameterlist.Add("@Title", entity.Title);
                parameterlist.Add("@FirstName", entity.FirstName);
                parameterlist.Add("@MiddleName", entity.MiddleName);
                parameterlist.Add("@LastName", entity.LastName);
                parameterlist.Add("@Gender", entity.Gender);
                parameterlist.Add("@Address", entity.Address);
                parameterlist.Add("@CountryId", entity.CountryId);
                parameterlist.Add("@StateId", entity.StateId);
                parameterlist.Add("@DistrictId", entity.DistrictId);
                parameterlist.Add("@CityId", entity.CityId);
                parameterlist.Add("@PinCode", entity.PinCode);
                parameterlist.Add("@MobileNumber", entity.MobileNumber);
                parameterlist.Add("@ContactNo", entity.ContactNo);
                parameterlist.Add("@Occupation", entity.Occupation);
                parameterlist.Add("@CompanyName", entity.CompanyName);
                parameterlist.Add("@CompanyAddress", entity.CompanyAddress);
                parameterlist.Add("@CompanyContactNo", entity.CompanyContactNo);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@BankName", entity.BankName);
                parameterlist.Add("@AccountNo", entity.AccountNo);
                parameterlist.Add("@Branch", entity.Branch);
                parameterlist.Add("@IFSCCode", entity.IFSCCode);
                parameterlist.Add("@StudentId", entity.StudentId);



                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentParentDetailsUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<StudentParent> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
