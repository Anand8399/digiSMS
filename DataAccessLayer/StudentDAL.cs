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
    public class StudentDAL : IGenericRepository<Student>
    {
        public IQueryable<Student> GetAll(int schoolId)
        {
            try
            {
                List<Student> entites = new List<Student>();
                String SqlQuery="select * from viewGetAllStudentDetails";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";

                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
 
                        if (dr != null)
                        {
                            Student entity = new Student();
                            entity.SrNo = Convert.ToInt32(dr["SrNo"]);
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.RegisterId = Convert.ToInt32(dr["RegisterId"]);
                            entity.ClassDivisionId = Convert.ToInt32(dr["ClassDivisionId"]);
                            entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                            entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                            entity.ClassName = Convert.ToString(dr["Class"]).Trim();
                            entity.DivisionName = Convert.ToString(dr["Division"]).Trim();
                            entity.Title = Convert.ToString(dr["Title"]).Trim();
                            entity.FirstName = Convert.ToString(dr["FirstName"]).Trim();
                            entity.MiddleName = Convert.ToString(dr["MiddleName"]).Trim();
                            entity.LastName = Convert.ToString(dr["LastName"]).Trim();
                            entity.MotherName = Convert.ToString(dr["MotherName"]).Trim();
                            entity.Gender = Convert.ToString(dr["Gender"]).Trim();
                            entity.Nationality = Convert.ToString(dr["Nationality"]).Trim();
                            entity.ReligionCastId = Convert.ToInt32(dr["ReligionCastId"]);
                            entity.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                            entity.CastId = Convert.ToInt32(dr["CastId"]);
                            entity.ReligionName = Convert.ToString(dr["Religion"]).Trim();
                            entity.CastName = Convert.ToString(dr["Cast"]).Trim();
                            entity.ReserveCategory = Convert.ToString(dr["ReserveCategory"]).Trim();
                            entity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                            entity.PlaceOfBirth = Convert.ToString(dr["PlaceOfBirth"]).Trim();
                            entity.AdharcardNo = Convert.ToString(dr["AdharcardNo"]).Trim();
                            entity.DateOfAdmission = Convert.ToDateTime(dr["DateOfAdmission"]);
                            entity.MotherTounge = Convert.ToString(dr["MotherTounge"]).Trim();
                            entity.UStudentId = Convert.ToString(dr["UStudentId"]).Trim();
                            
                            entity.LastSchoolClass = Convert.ToString(dr["LastSchoolClass"]).Trim();
                            entity.LastSchoolTCNo = Convert.ToString(dr["LastSchoolTCNo"]).Trim();
                            entity.LastSchoolAttended = Convert.ToString(dr["LastSchoolAttended"]).Trim();
                            entity.Progress = Convert.ToString(dr["Progress"]).Trim();
                            entity.Conduct = Convert.ToString(dr["Conduct"]).Trim();
                            if (dr["DateOfLeavingSchool"] != DBNull.Value)
                            {
                                entity.DateOfLeavingSchool = Convert.ToDateTime(dr["DateOfLeavingSchool"]);
                            }
                            entity.ClassInWhichStudingAndSinceWhen = Convert.ToString(dr["ClassInWhichStudingAndSinceWhen"]).Trim();
                            entity.ReasonForLeavingSchool = Convert.ToString(dr["ReasonForLeavingSchool"]).Trim();
                            entity.RemarkOnTC = Convert.ToString(dr["RemarkOnTC"]).Trim();
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            entity.Remark = dr["Remark"].ToString().Trim();
                            entity.TCPrinted = Convert.ToBoolean(dr["TCPrinted"].ToString());
                            if (dr["TCNo"] != null && !string.IsNullOrEmpty(dr["TCNo"].ToString()))
                            {
                                entity.TCNo = Convert.ToInt32(dr["TCNo"].ToString());
                            }
                           
                            entity.LastSchoolClass = Convert.ToString(dr["LastSchoolClass"]).Trim();
                            entity.LastSchoolTCNo = Convert.ToString(dr["LastSchoolTCNo"]).Trim();
                            if (dr["PrevSchoolDateofLiving"] != DBNull.Value)
                            {
                                entity.PrevSchoolDateofLiving = Convert.ToDateTime(dr["PrevSchoolDateofLiving"]);
                            }
                            
                            
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

        public IQueryable<Student> FindBy(System.Linq.Expressions.Expression<Func<Student, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }


        public void Add(Student entity)
        {
            try
            {
                Hashtable parameterlist = new Hashtable();
                            parameterlist.Add("@RegisterId", entity.RegisterId);                            
                            parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);
                            parameterlist.Add("@Title", entity.Title.Trim());
                            parameterlist.Add("@FirstName", entity.FirstName.Trim());
                            parameterlist.Add("@MiddleName", entity.MiddleName.Trim());
                            parameterlist.Add("@LastName", entity.LastName.Trim());
                            parameterlist.Add("@MotherName", entity.MotherName.Trim());
                            parameterlist.Add("@Gender", entity.Gender);
                            parameterlist.Add("@Nationality", entity.Nationality);
                            parameterlist.Add("@ReligionCastId", entity.ReligionCastId);
                            parameterlist.Add("@DateOfBirth", entity.DateOfBirth);
                            parameterlist.Add("@PlaceOfBirth", entity.PlaceOfBirth);
                            parameterlist.Add("@AdharcardNo", entity.AdharcardNo.Trim());
                            parameterlist.Add("@DateOfAdmission", entity.DateOfAdmission);
                            parameterlist.Add("@MotherTounge", entity.MotherTounge);
                            parameterlist.Add("@UStudentId", entity.UStudentId);
                           
                            parameterlist.Add("@LastSchoolAttended", entity.LastSchoolAttended.Trim());
                            parameterlist.Add("@Progress", entity.Progress.Trim());
                            parameterlist.Add("@Conduct", entity.Conduct.Trim());
                            parameterlist.Add("@DateOfLeavingSchool", entity.DateOfLeavingSchool);
                            parameterlist.Add("@LastSchoolClass", entity.LastSchoolClass);
                            parameterlist.Add("@LastSchoolTCNo", entity.LastSchoolTCNo);
                            parameterlist.Add("@ClassInWhichStudingAndSinceWhen", entity.ClassInWhichStudingAndSinceWhen.Trim());
                            parameterlist.Add("@ReasonForLeavingSchool", entity.ReasonForLeavingSchool.Trim());
                            parameterlist.Add("@RemarkOnTC", entity.RemarkOnTC.Trim());
                            parameterlist.Add("@Status", entity.Status == true ? 1 :0);
                            parameterlist.Add("@Remark", entity.Remark.Trim());
                            parameterlist.Add("@PrevSchoolDateofLiving", entity.PrevSchoolDateofLiving);
                            parameterlist.Add("@OUT_StudentId", null);
                            Hashtable outparameterlist = new Hashtable();
                            int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentInsert", parameterlist, ref outparameterlist);
                            int studentTransactionId = 0;
                            if (outparameterlist != null && outparameterlist.Count > 0)
                            {
                                foreach (string parametername in outparameterlist.Keys)
                                {
                                    if (parametername.StartsWith("@OUT_"))
                                    {
                                        studentTransactionId = Convert.ToInt32(outparameterlist[parametername]);
                                    }
                                }
                            }
                //string queryString = string.Format(" INSERT INTO [StudentDetails]("
                //            + " [AcademicYearId]"
                //            + ",[RegisterId]"
                //            + ",[ClassDivisionId]"
                //            + ",[Title]"
                //            + ",[FirstName]"
                //            + ",[MiddleName]"
                //            + ",[LastName]"
                //            + ",[MotherName]"
                //            + ",[Gender]"
                //            + ",[Nationality]"
                //            + ",[ReligionCastId]"
                //            + ",[DateOfBirth]"
                //            + ",[PlaceOfBirth]"
                //            + ",[DateOfAdmission]"
                //            + ",[LastSchoolAttended]"
                //            + ",[Progress]"
                //            + ",[Conduct]"
                //            + ",[DateOfLeavingSchool]"
                //            + ",[ClassInWhichStudingAndSinceWhen]"
                //            + ",[ReasonForLeavingSchool]"
                //            + ",[RemarkOnTC]"
                //            + ",[Status]"
                //            + ",[Remark]"
                //            + ") VALUES({0}, {1},{2}, '{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',{21},'{22}');",
                            
                //            entity.AcademicYearId, entity.RegisterId, entity.ClassDivisionId, entity.Title, entity.FirstName, entity.MiddleName, entity.LastName, entity.MotherName,
                //            entity.Gender, entity.Nationality, entity.ReligionCastId, entity.DateOfBirth, entity.PlaceOfBirth, entity.DateOfAdmission, entity.LastSchoolAttended,
                //            entity.Progress, entity.Conduct, entity.DateOfLeavingSchool, entity.ClassInWhichStudingAndSinceWhen, entity.ReasonForLeavingSchool, entity.RemarkOnTC,
                //            entity.Status == true ? 1 : 0, entity.Remark);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
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
                string queryString = string.Format(" DELETE FROM [StudentDetails] WHERE StudentId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Student entity)
        {
            try
            {
                //string queryString = string.Format(" UPDATE [StudentDetails] "
                //            + " SET "
                //            + "  [AcademicYearId] = '{0}'"
                //            + ", [RegisterId] = '{1}'"
                //            + ", [ClassDivisionId] = '{2}'"
                //            + ", [Title] = '{3}'"
                //            + ", [FirstName] = '{4}'"
                //            + ", [MiddleName] = '{5}'"
                //            + ", [LastName] = '{6}'"
                //            + ", [MotherName] = '{7}'"
                //            + ", [Gender] = '{8}'"
                //            + ", [Nationality] = '{9}'"
                //            + ", [ReligionCastId] = '{10}'"
                //            + ", [DateOfBirth] = '{11}'"
                //            + ", [PlaceOfBirth] = '{12}'"
                //            + ", [DateOfAdmission] = '{13}'"
                //            + ", [LastSchoolAttended] = '{14}'"
                //            + ", [Progress] = '{15}'"
                //            + ", [Conduct] = '{16}'"
                //            + ", [DateOfLeavingSchool] = '{17}'"
                //            + ", [ClassInWhichStudingAndSinceWhen] = '{18}'"
                //            + ", [ReasonForLeavingSchool] = '{19}'"
                //            + ", [RemarkOnTC] = '{20}'"
                //            + ", [Status] = '{21}'"
                //            + ", [Remark] = '{22}'"
                //            + ", [AdharcardNo] = '{23}'"
                //            + " WHERE StudentId  = {24};",
                //            entity.AcademicYearId, entity.RegisterId, entity.ClassDivisionId,
                //            string.IsNullOrEmpty(entity.Title) ? entity.Title : entity.Title.Trim(), 
                //            entity.FirstName.Trim(), 
                //            string.IsNullOrEmpty(entity.MiddleName) ? entity.MiddleName : entity.MiddleName.Trim(), 
                //            entity.LastName.Trim(),
                //            string.IsNullOrEmpty(entity.MotherName)? entity.MotherName:   entity.MotherName.Trim(),
                //            entity.Gender, entity.Nationality, entity.ReligionCastId, entity.DateOfBirth, entity.PlaceOfBirth.Trim(), entity.DateOfAdmission, 
                //            string.IsNullOrEmpty(entity.LastSchoolAttended) ? entity.LastSchoolAttended : entity.LastSchoolAttended.Trim(),
                //            string.IsNullOrEmpty(entity.Progress) ? entity.Progress : entity.Progress.Trim(), 
                //            string.IsNullOrEmpty(entity.Conduct) ? entity.Conduct : entity.Conduct.Trim(), 
                //            entity.DateOfLeavingSchool, 
                //            string.IsNullOrEmpty(entity.ClassInWhichStudingAndSinceWhen) ? entity.ClassInWhichStudingAndSinceWhen : entity.ClassInWhichStudingAndSinceWhen.Trim(), 
                //            string.IsNullOrEmpty(entity.ReasonForLeavingSchool) ? entity.ReasonForLeavingSchool : entity.ReasonForLeavingSchool.Trim(), 
                //            string.IsNullOrEmpty(entity.RemarkOnTC) ? entity.RemarkOnTC : entity.RemarkOnTC.Trim(),
                //            entity.Status, 
                //            string.IsNullOrEmpty(entity.Remark) ? entity.Remark : entity.Remark.Trim(), 
                //            string.IsNullOrEmpty(entity.AdharcardNo) ? entity.AdharcardNo : entity.AdharcardNo.Trim(), 
                //            entity.StudentId);
                //int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@SrNo", entity.SrNo);
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@RegisterId", entity.RegisterId);
                parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);
                parameterlist.Add("@Title", entity.Title);
                parameterlist.Add("@FirstName", entity.FirstName);
                parameterlist.Add("@MiddleName", entity.MiddleName);
                parameterlist.Add("@LastName", entity.LastName);
                parameterlist.Add("@MotherName", entity.MotherName);
                parameterlist.Add("@Gender", entity.Gender);
                parameterlist.Add("@Nationality", entity.Nationality);
                parameterlist.Add("@ReligionCastId", entity.ReligionCastId);
                parameterlist.Add("@DateOfBirth", entity.DateOfBirth);
                parameterlist.Add("@PlaceOfBirth", entity.PlaceOfBirth);
                parameterlist.Add("@AdharcardNo", entity.AdharcardNo);
                parameterlist.Add("@DateOfAdmission", entity.DateOfAdmission);
                parameterlist.Add("@MotherTounge", entity.MotherTounge);
                parameterlist.Add("@UStudentId", entity.UStudentId);
                parameterlist.Add("@LastSchoolAttended", entity.LastSchoolAttended);
                parameterlist.Add("@Progress", entity.Progress);
                parameterlist.Add("@Conduct", entity.Conduct);
                parameterlist.Add("@DateOfLeavingSchool", entity.DateOfLeavingSchool);
                parameterlist.Add("@LastSchoolClass", entity.LastSchoolClass);
                parameterlist.Add("@LastSchoolTCNo", entity.LastSchoolTCNo);
                parameterlist.Add("@ClassInWhichStudingAndSinceWhen", entity.ClassInWhichStudingAndSinceWhen);
                parameterlist.Add("@ReasonForLeavingSchool", entity.ReasonForLeavingSchool);
                parameterlist.Add("@RemarkOnTC", entity.RemarkOnTC);
                parameterlist.Add("@Status", entity.Status == true ? 1 : 0);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@PrevSchoolDateofLiving", entity.PrevSchoolDateofLiving);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Add new student record
        /// </summary>
        /// <param name="entity">Student entity</param>
        /// <returns>Student Id</returns>
        public int AddStudent(Student entity, int SchoolId)
        {
            try
            {
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@StudentId", entity.StudentId);
                parameterlist.Add("@SchoolId", SchoolId);
                parameterlist.Add("@RegisterId", entity.RegisterId);
                parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);
                parameterlist.Add("@Title", entity.Title);
                parameterlist.Add("@FirstName", entity.FirstName);
                parameterlist.Add("@MiddleName", entity.MiddleName);
                parameterlist.Add("@LastName", entity.LastName);
                parameterlist.Add("@MotherName", entity.MotherName);
                parameterlist.Add("@Gender", entity.Gender);
                parameterlist.Add("@Nationality", entity.Nationality);
                parameterlist.Add("@ReligionCastId", entity.ReligionCastId);
                parameterlist.Add("@DateOfBirth", entity.DateOfBirth);
                parameterlist.Add("@PlaceOfBirth", entity.PlaceOfBirth);
                parameterlist.Add("@AdharcardNo", entity.AdharcardNo);
                parameterlist.Add("@DateOfAdmission", entity.DateOfAdmission);
                parameterlist.Add("@MotherTounge", entity.MotherTounge);
                parameterlist.Add("@UStudentId", entity.UStudentId);
                parameterlist.Add("@LastSchoolAttended", entity.LastSchoolAttended);
                parameterlist.Add("@Progress", entity.Progress);
                parameterlist.Add("@Conduct", entity.Conduct);
                parameterlist.Add("@DateOfLeavingSchool", entity.DateOfLeavingSchool);
                parameterlist.Add("@LastSchoolClass", entity.LastSchoolClass);
                parameterlist.Add("@LastSchoolTCNo", entity.LastSchoolTCNo);
                parameterlist.Add("@ClassInWhichStudingAndSinceWhen", entity.ClassInWhichStudingAndSinceWhen);
                parameterlist.Add("@ReasonForLeavingSchool", entity.ReasonForLeavingSchool);
                parameterlist.Add("@RemarkOnTC", entity.RemarkOnTC);
                parameterlist.Add("@Status", entity.Status == true ? 1 : 0);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@PrevSchoolDateofLiving", entity.PrevSchoolDateofLiving);
                parameterlist.Add("@OUT_StudentId", null);
                Hashtable outparameterlist = new Hashtable();
                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_StudentInsert", parameterlist, ref outparameterlist);
                int studentId = 0;
                if (outparameterlist != null && outparameterlist.Count > 0)
                {
                    foreach (string parametername in outparameterlist.Keys)
                    {
                        if (parametername.StartsWith("@OUT_"))
                        {
                            studentId = Convert.ToInt32(outparameterlist[parametername]);
                        }
                    }
                }
                return studentId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<StudentClassChange> GetStudentClassChange(int ClassDivisionId, int schoolId)
        {
            try
            {
                List<StudentClassChange> entities = new List<StudentClassChange>();
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassDivisionId", ClassDivisionId);
                parameterlist.Add("@SchoolId", schoolId);
                //ExecuteProcedureReader
                DataSet ds = CommanMethodsForSQL.ExecuteProcedureReader("sp_GetStudentByClassDivision", parameterlist);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            StudentClassChange entity = new StudentClassChange();
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.StudentFullNameWithTitle = Convert.ToString(dr["StudentName"]);
                            entity.RegisterId = Convert.ToInt32(dr["RegisterId"]);
                            entities.Add(entity);
                        }
                    }
                }

                return entities.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public void StudentChangeClassDivision(StudentClassChange entity,int schoolId)
        {
            try
            {
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@SchoolId", schoolId);
                parameterlist.Add("@PreviousClassDivisionId", entity.PreviousClassDivisionId);
                parameterlist.Add("@CurrentClassDivisionId", entity.CurrentClassDivisionId);
                parameterlist.Add("@Status", 1);
                parameterlist.Add("@CreatedBy", 0);
                parameterlist.Add("@CreatedDate", DateTime.Today.Date);

                DataTable dt = new DataTable();
                dt.Columns.Add("StudentId", typeof(int));
                dt.Columns.Add("Remark", typeof(string));
                for (int i = 0; i < entity.SelectedStudent.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["StudentId"] = entity.SelectedStudent[i];
                    dr["Remark"] = entity.Remark;
                    dt.Rows.Add(dr);
                }
                parameterlist.Add("@ClassChangeDataType", dt);

                
                int effetedRows = CommanMethodsForSQL.StudentChangeClassDivision("sp_StudentChangeClassDivision", parameterlist);

                if (effetedRows > 0) { }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Student> SearchStudents(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId, bool status, int schoolId)
        {
            try
            {
                List<Student> entites = new List<Student>();
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@SchoolId", schoolId);
                parameterlist.Add("@RegisterId", RegisterId);
                parameterlist.Add("@ClassId", ClassId);
                parameterlist.Add("@DivisionId", DivisionId);
                parameterlist.Add("@ReligionId", ReligionId);
                parameterlist.Add("@CastId", CastId);
                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_SearchStudents", parameterlist);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            Student entity = new Student();
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            if (entity.Status == status)
                            {
                                entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                                entity.RegisterId = Convert.ToInt32(dr["RegisterId"]);
                                entity.ClassDivisionId = Convert.ToInt32(dr["ClassDivisionId"]);
                                entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                                entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                                entity.ClassName = Convert.ToString(dr["Class"]).Trim();
                                entity.DivisionName = Convert.ToString(dr["Division"]).Trim();
                                entity.Title = Convert.ToString(dr["Title"]).Trim();
                                entity.FirstName = Convert.ToString(dr["FirstName"]).Trim();
                                entity.MiddleName = Convert.ToString(dr["MiddleName"]).Trim();
                                entity.LastName = Convert.ToString(dr["LastName"]).Trim();
                                entity.MotherName = Convert.ToString(dr["MotherName"]).Trim();
                                entity.Gender = Convert.ToString(dr["Gender"]).Trim();
                                entity.Nationality = Convert.ToString(dr["Nationality"]).Trim();
                                entity.ReligionCastId = Convert.ToInt32(dr["ReligionCastId"]);
                                entity.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                                entity.CastId = Convert.ToInt32(dr["CastId"]);
                                entity.ReligionName = Convert.ToString(dr["Religion"]).Trim();
                                entity.CastName = Convert.ToString(dr["Cast"]).Trim();
                                entity.ReserveCategory = Convert.ToString(dr["ReserveCategory"]).Trim();
                                entity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                                entity.PlaceOfBirth = Convert.ToString(dr["PlaceOfBirth"]).Trim();
                                entity.AdharcardNo = Convert.ToString(dr["AdharcardNo"]).Trim();
                                entity.DateOfAdmission = Convert.ToDateTime(dr["DateOfAdmission"]);
                                entity.LastSchoolAttended = Convert.ToString(dr["LastSchoolAttended"]).Trim();
                                entity.Progress = Convert.ToString(dr["Progress"]).Trim();
                                entity.Conduct = Convert.ToString(dr["Conduct"]).Trim();
                                if (dr["DateOfLeavingSchool"] != DBNull.Value)
                                {
                                    entity.DateOfLeavingSchool = Convert.ToDateTime(dr["DateOfLeavingSchool"]);
                                }
                                entity.ClassInWhichStudingAndSinceWhen = Convert.ToString(dr["ClassInWhichStudingAndSinceWhen"]).Trim();
                                entity.ReasonForLeavingSchool = Convert.ToString(dr["ReasonForLeavingSchool"]).Trim();
                                entity.RemarkOnTC = Convert.ToString(dr["RemarkOnTC"]).Trim();

                                entity.Remark = dr["Remark"].ToString().Trim();
                                entites.Add(entity);
                            }
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

        public void UpdateTCDetails(int StudentId)
        {
            Hashtable parameterlist = new Hashtable();
            parameterlist.Add("@StudentId", StudentId);
            CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_UpdateStudentTCStatus", parameterlist);
        }

        public long GetMaxTCNo()
        {
            Hashtable parameterlist = new Hashtable();
            long returnValue = 0;
            DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_GetStudentMaxTCNo", parameterlist);
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                if (dr != null)
                {
                    returnValue = Convert.ToInt32(dr[0]);
                }
            }

            return returnValue;
        }

        public IQueryable<Student> getStudentsWithBalance(int ClassId, int DivisionId, int ReligionId, int CastId)
        {
            try
            {
                List<Student> entites = new List<Student>();
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassId", ClassId);
                parameterlist.Add("@DivisionId", DivisionId);
                parameterlist.Add("@ReligionId", ReligionId);
                parameterlist.Add("@CastId", CastId);
                DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_StudentLedgerBalance", parameterlist);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (dr != null)
                        {
                            Student entity = new Student();
                            //entity.StudentFullNameWithTitle = string.Concat(Convert.ToString(dr["Title"]).Trim(), " ", Convert.ToString(dr["FirstName"]).Trim(), " ", Convert.ToString(dr["MiddleName"]).Trim(), " ", Convert.ToString(dr["LastName"]).Trim()).Trim();
                            entity.StudentId = Convert.ToInt32(dr["StudentId"]);
                            entity.RegisterId = Convert.ToInt32(dr["RegisterId"]);
                            entity.ClassDivisionId = Convert.ToInt32(dr["ClassDivisionId"]);
                            entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                            entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                            entity.ClassName = Convert.ToString(dr["Class"]).Trim();
                            entity.DivisionName = Convert.ToString(dr["Division"]).Trim();
                            entity.Title = Convert.ToString(dr["Title"]).Trim();
                            entity.FirstName = Convert.ToString(dr["FirstName"]).Trim();
                            entity.MiddleName = Convert.ToString(dr["MiddleName"]).Trim();
                            entity.LastName = Convert.ToString(dr["LastName"]).Trim();
                            entity.MotherName = Convert.ToString(dr["MotherName"]).Trim();
                            entity.Gender = Convert.ToString(dr["Gender"]).Trim();
                            entity.Nationality = Convert.ToString(dr["Nationality"]).Trim();
                            entity.ReligionCastId = Convert.ToInt32(dr["ReligionCastId"]);
                            entity.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                            entity.CastId = Convert.ToInt32(dr["CastId"]);
                            entity.ReligionName = Convert.ToString(dr["Religion"]).Trim();
                            entity.CastName = Convert.ToString(dr["Cast"]).Trim();
                            entity.ReserveCategory = Convert.ToString(dr["ReserveCategory"]).Trim();
                            entity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                            entity.PlaceOfBirth = Convert.ToString(dr["PlaceOfBirth"]).Trim();
                            entity.AdharcardNo = Convert.ToString(dr["AdharcardNo"]).Trim();
                            entity.DateOfAdmission = Convert.ToDateTime(dr["DateOfAdmission"]);
                            entity.LastSchoolAttended = Convert.ToString(dr["LastSchoolAttended"]).Trim();
                            entity.Progress = Convert.ToString(dr["Progress"]).Trim();
                            entity.Conduct = Convert.ToString(dr["Conduct"]).Trim();
                            if (dr["DateOfLeavingSchool"] != DBNull.Value)
                            {
                                entity.DateOfLeavingSchool = Convert.ToDateTime(dr["DateOfLeavingSchool"]);
                            }
                            entity.ClassInWhichStudingAndSinceWhen = Convert.ToString(dr["ClassInWhichStudingAndSinceWhen"]).Trim();
                            entity.ReasonForLeavingSchool = Convert.ToString(dr["ReasonForLeavingSchool"]).Trim();
                            entity.RemarkOnTC = Convert.ToString(dr["RemarkOnTC"]).Trim();
                            if (dr["balance"] != DBNull.Value)
                            {
                                entity.Balance = Convert.ToDecimal(dr["balance"].ToString());
                            }
                            if (dr["MobileNumber"] != DBNull.Value)
                            {
                                entity.MobileNumber = Convert.ToInt64(dr["MobileNumber"].ToString());
                            }
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
        public IQueryable<Student> GetAll()
        {
            //throw new NotImplementedException();
            return GetAll(0);
        }
    }
}
