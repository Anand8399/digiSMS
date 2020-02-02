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
    public class StudentAttendanceDAL : IGenericRepository<StudentAttendance>
    {
        public IQueryable<StudentAttendance> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentAttendance> FindBy(System.Linq.Expressions.Expression<Func<StudentAttendance, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(StudentAttendance entity)
        {
            try
            {
                Hashtable parameterlist = new Hashtable();
                
                parameterlist.Add("@ClassId", entity.ClassId);
                parameterlist.Add("@DivisionId", entity.DivisionId);
                parameterlist.Add("@DateOfAttendance", entity.DateOfAttendance);
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@AttendanceInDays", entity.AttendanceInDays);
                parameterlist.Add("@AbsentRemark", entity.AbsentRemark);

                Hashtable outparameterlist = new Hashtable();
                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("StudentAttendanceInsert", parameterlist);
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void AddStudentAttendance(List<StudentAttendance> entities)
        {
            try
            {
                Hashtable parameterlist;

                foreach (var entity in entities)
                {
                    parameterlist = new Hashtable();

                    parameterlist.Add("@ClassId", entity.ClassId);
                    parameterlist.Add("@DivisionId", entity.DivisionId);
                    parameterlist.Add("@DateOfAttendance", entity.DateOfAttendance);
                    parameterlist.Add("@StudentId", entity.StudentId);
                    parameterlist.Add("@AttendanceInDays", entity.AttendanceInDays);
                    parameterlist.Add("@AbsentRemark", entity.AbsentRemark);

                    Hashtable outparameterlist = new Hashtable();
                    int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentAttendanceInsert", parameterlist);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(StudentAttendance entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentAttendance> GetAllStudentAttendance(int ClassId, int DivisionId, DateTime DateOfAttendance)
        {
            try
            {
                List<StudentAttendance> entites = new List<StudentAttendance>();

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassId", ClassId);
                parameterlist.Add("@DivisionId", DivisionId);
                parameterlist.Add("@DateOfAttendance", DateOfAttendance);

                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_StudentAttendanceGet", parameterlist);


                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            StudentAttendance entity = new StudentAttendance();

                            entity.Id = Convert.ToInt32(dr["Id"]);
                            entity.ClassDivisionId = Convert.ToInt32(dr["ClassDivisionId"]);
                            entity.ClassId = ClassId;
                            entity.DivisionId = DivisionId;

                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.RegisterId = Convert.ToInt32(dr["RegisterId"]);
                            entity.StudentFullNameWithTitle = Convert.ToString(dr["StudentFullName"]);
                            if (dr["DateOfAttendance"] != DBNull.Value)
                            {
                                entity.DateOfAttendance = Convert.ToDateTime(dr["DateOfAttendance"]);
                            }
                            entity.AttendanceInDays = Convert.ToDecimal(dr["AttendanceInDays"]);
                            entity.AbsentRemark = Convert.ToString(dr["AbsentRemark"]).Trim();
                            entity.Status = Convert.ToBoolean(dr["Status"]);

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
        public IQueryable<StudentAttendance> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
