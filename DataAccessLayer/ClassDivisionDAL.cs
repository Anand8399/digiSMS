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
    public class ClassDivisionDAL : IGenericRepository<ClassDivision>
    {
        public IQueryable<ClassDivision> GetAll()
        {
            try
            {
                List<ClassDivision> entites = new List<ClassDivision>();
                DataTable dataTable = CommanMethodsForSQL.GetDataTable("SELECT ClassDivisionDetails.ClassDivisionId, ClassDivisionDetails.ClassId, "
                + " ClassDivisionDetails.DivisionId, ClassDivisionDetails.Status, ClassDivisionDetails.Remark, "
                + " ClassDetails.Class, DivisionDetails.Division "
                + " FROM         ClassDivisionDetails INNER JOIN "
                + " ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN "
                + " DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId;");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            ClassDivision entity = new ClassDivision();
                            entity.ClassDivisionId = Convert.ToInt32(dr["ClassDivisionId"]);
                            entity.ClassId = Convert.ToInt32(dr["ClassId"]);
                            entity.ClassName = Convert.ToString(dr["Class"]);
                            entity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                            entity.DivisionName = Convert.ToString(dr["Division"]);
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

        public IQueryable<ClassDivision> FindBy(System.Linq.Expressions.Expression<Func<ClassDivision, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(ClassDivision entity)
        {
            try
            {
                //string queryString = string.Format(" INSERT INTO [ClassDivisionDetails] ([AcademicYearId],[ClassId],[DivisionId],[Status],[Remark]) VALUES('{0}', '{1}','{2}', '{3}', '{4}');", entity.AcademicYearId, entity.ClassId, entity.DivisionId, entity.Status, entity.Remark);
               // int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);


                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassId", entity.ClassId);
                parameterlist.Add("@DivisionId", entity.DivisionId);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ClassDivisionDetailsInsert", parameterlist);
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
                string queryString = string.Format(" DELETE FROM [ClassDivisionDetails] WHERE ClassDivisionId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(ClassDivision entity)
        {
            try
            {
        //        string queryString = string.Format(" UPDATE [ClassDivisionDetails] " +
        //" SET [ClassId] = '{0}'" +
        //  ",[DivisionId] = '{1}'" +
        //  ",[Status] = '{2}'" +
        //  ",[Remark] = '{3}'" +
        //  ",[AcademicYearId] = '{4}'" +
        //" WHERE ClassDivisionId  = {5};", entity.ClassId, entity.DivisionId,entity.DivisionName, entity.Status, entity.Remark, entity.AcademicYearId, entity.ClassDivisionId);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);

                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@ClassId", entity.ClassId);
                parameterlist.Add("@DivisionId", entity.DivisionId);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);
                parameterlist.Add("@ClassDivisionId", entity.ClassDivisionId);


                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_ClassDivisionDetailsUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetClassDivisionId(string ClassName, string DivisionName)
        {
            int classDivisionId = 0;
            Hashtable parameterlist = new Hashtable();
            parameterlist.Add("@Class", ClassName);
            parameterlist.Add("@Division", DivisionName);
            DataSet dataSet = CommanMethodsForSQL.ExecuteProcedureReader("sp_GetClassDivisionIdByClassAndDivision", parameterlist);
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                classDivisionId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ClassDivisionId"]);
            }
            return classDivisionId;
        }

        public IQueryable<ClassDivision> GetAll(int SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
