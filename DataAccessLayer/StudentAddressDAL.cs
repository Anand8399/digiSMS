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
    public class StudentAddressDAL : IGenericRepository<StudentAddress>
    {
        public IQueryable<StudentAddress> GetAll()
        {
            try
            {
                List<StudentAddress> entites = new List<StudentAddress>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT StudentAddressDetails.* FROM StudentAddressDetails;");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            StudentAddress entity = new StudentAddress();
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.CurrentAddress = dr["CurrentAddress"].ToString();
                            entity.CurrentCountryId = Convert.ToInt32(dr["CurrentCountryId"]);
                            entity.CurrentStateId = Convert.ToInt32(dr["CurrentStateId"]);
                            entity.CurrentDistrictId = Convert.ToInt32(dr["CurrentDistrictId"]);
                            entity.CurrentCityId = Convert.ToInt32(dr["CurrentCityId"]);
                            entity.CurrentPinCode = Convert.ToInt32(dr["CurrentPinCode"]);
                            entity.PermentAddress = dr["PermentAddress"].ToString();
                            entity.PermentCountryId = Convert.ToInt32(dr["PermentCountryId"]);
                            entity.PermentStateId = Convert.ToInt32(dr["PermentStateId"]);
                            entity.PermentDistrictId = Convert.ToInt32(dr["PermentDistrictId"]);
                            entity.PermentCityId = Convert.ToInt32(dr["PermentCityId"]);
                            entity.PermentPinCode = Convert.ToInt32(dr["PermentPinCode"]);
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

        public IQueryable<StudentAddress> FindBy(System.Linq.Expressions.Expression<Func<StudentAddress, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(StudentAddress entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [StudentAddressDetails]("
                //    +" [StudentId]"
                //    +", [CurrentAddress]"
                //    +", [CurrentCountryId]"
                //    +", [CurrentStateId]"
                //    +", [CurrentDistrictId]"
                //    +", [CurrentCityId]"
                //    +", [CurrentPinCode]"
                //    +", [PermentAddress]"
                //    +", [PermentCountryId]"
                //    +", [PermentStateId]"
                //    +", [PermentDistrictId]"
                //    +", [PermentCityId]"                    
                //    +", [PermentPinCode]"
                //    +", [Status]"
                //    +", [Remark]"
                //    + ") VALUES({0}, '{1}','{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}');"
                //    , entity.StudentId, entity.CurrentAddress, entity.CurrentCountryId, entity.CurrentStateId, entity.CurrentDistrictId, entity.CurrentCityId, entity.CurrentPinCode
                //    , entity.PermentAddress, entity.PermentCountryId, entity.PermentStateId, entity.PermentDistrictId, entity.PermentCityId, entity.PermentPinCode
                //    ,entity.Status, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@CurrentAddress", entity.CurrentAddress);
                parameterlist.Add("@CurrentCountryId", entity.CurrentCountryId);
                parameterlist.Add("@CurrentStateId", entity.CurrentStateId);
                parameterlist.Add("@CurrentDistrictId", entity.CurrentDistrictId);
                parameterlist.Add("@CurrentCityId", entity.CurrentCityId);
                parameterlist.Add("@CurrentPinCode", entity.CurrentPinCode);
                parameterlist.Add("@PermentAddress", entity.PermentAddress);
                parameterlist.Add("@PermentCountryId", entity.PermentCountryId);
                parameterlist.Add("@PermentStateId", entity.PermentStateId);
                parameterlist.Add("@PermentDistrictId", entity.PermentDistrictId);
                parameterlist.Add("@PermentCityId", entity.PermentCityId);
                parameterlist.Add("@PermentPinCode", entity.PermentPinCode);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentAddressDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [StudentAddressDetails] WHERE StudentId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(StudentAddress entity)
        {
            try
            {
                //string queryString = string.Format(" UPDATE [StudentAddressDetails] " +
                //        " SET [CurrentAddress] ='{0}'"
                //    +", [CurrentCountryId]='{1}'"
                //    +", [CurrentStateId]='{2}'"
                //    +", [CurrentDistrictId]='{3}'"
                //    +", [CurrentCityId]='{4}'"
                //    +", [CurrentPinCode]='{5}'"
                //    +", [PermentAddress]='{6}'"
                //    +", [PermentCountryId]='{7}'"
                //    +", [PermentStateId]='{8}'"
                //    +", [PermentDistrictId]='{9}'"
                //    +", [PermentCityId]='{10}'"
                //    +", [PermentPinCode]='{11}'"
                //     + ",[Status] = '{12}'" 
                //      + ",[Remark] = '{13}'"
                //   + " WHERE StudentId  = {14};"
                //   , entity.CurrentAddress, entity.CurrentCountryId, entity.CurrentStateId, entity.CurrentDistrictId, entity.CurrentCityId, entity.CurrentPinCode
                //    , entity.PermentAddress, entity.PermentCountryId, entity.PermentStateId, entity.PermentDistrictId, entity.PermentCityId, entity.PermentPinCode
                //    , entity.Status, entity.Remark, entity.StudentId);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@CurrentAddress", entity.CurrentAddress);
                parameterlist.Add("@CurrentCountryId", entity.CurrentCountryId);
                parameterlist.Add("@CurrentStateId", entity.CurrentStateId);
                parameterlist.Add("@CurrentDistrictId", entity.CurrentDistrictId);
                parameterlist.Add("@CurrentCityId", entity.CurrentCityId);
                parameterlist.Add("@CurrentPinCode", entity.CurrentPinCode);
                parameterlist.Add("@PermentAddress", entity.PermentAddress);
                parameterlist.Add("@PermentCountryId", entity.PermentCountryId);
                parameterlist.Add("@PermentStateId", entity.PermentStateId);
                parameterlist.Add("@PermentDistrictId", entity.PermentDistrictId);
                parameterlist.Add("@PermentCityId", entity.PermentCityId);
                parameterlist.Add("@PermentPinCode", entity.PermentPinCode);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);


                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentAddressDetailsUpdate", parameterlist);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<StudentAddress> GetAll(Int64 StudentId)
        {
            try
            {
                List<StudentAddress> entites = new List<StudentAddress>();
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", StudentId);
                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_GetAllStudentAddressDetails", parameterlist);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            StudentAddress entity = new StudentAddress();
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.CurrentAddress = dr["CurrentAddress"].ToString();
                            entity.CurrentCountryId = Convert.ToInt32(dr["CurrentCountryId"]);
                            entity.CurrentStateId = Convert.ToInt32(dr["CurrentStateId"]);
                            entity.CurrentDistrictId = Convert.ToInt32(dr["CurrentDistrictId"]);
                            entity.CurrentCityId = Convert.ToInt32(dr["CurrentCityId"]);
                            entity.CurrentPinCode = Convert.ToInt32(dr["CurrentPinCode"]);
                            entity.PermentAddress = dr["PermentAddress"].ToString();
                            entity.PermentCountryId = Convert.ToInt32(dr["PermentCountryId"]);
                            entity.PermentStateId = Convert.ToInt32(dr["PermentStateId"]);
                            entity.PermentDistrictId = Convert.ToInt32(dr["PermentDistrictId"]);
                            entity.PermentCityId = Convert.ToInt32(dr["PermentCityId"]);
                            entity.PermentPinCode = Convert.ToInt32(dr["PermentPinCode"]);
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            entity.Remark = dr["Remark"].ToString();
                            entity.StudentFullName = dr["StudentFullName"].ToString();

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
        public IQueryable<StudentAddress> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
