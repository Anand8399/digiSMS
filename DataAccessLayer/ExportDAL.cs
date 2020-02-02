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
   public class ExportDAL
    {

       public IQueryable<Export> getExportRecords(int schoolId)
       {
           try
           {
               List<Export> entites = new List<Export>();

               String strQuery ;

               strQuery = "select * from viewExport where status = 1";
               
               if (schoolId != 0)
                   strQuery = strQuery + " and SchoolId=" + schoolId ;

               strQuery = strQuery + ";";

               DataTable dataTable = CommanMethodsForSQL.GetDataTable(strQuery);
               if (dataTable != null && dataTable.Rows.Count > 0)
               {
                   int iCnt = 1;
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       if (dr != null)
                       {
                           Export entity = new Export();
                           entity.SrNo = Convert.ToInt32(iCnt++);
                           entity.StudentId = Convert.ToString((dr["UStudentId"]));
                           entity.StudentName = Convert.ToString((dr["StudentName"]));
                           entity.MotherName = Convert.ToString((dr["MotherName"]));
                           entity.Class = Convert.ToString((dr["Class"]));
                           entity.Division = Convert.ToString((dr["Division"]));
                           entity.GrNo = Convert.ToInt32((dr["GrNo"]));
                           entity.BirthDate =Convert.ToDateTime((dr["BirthDate"]));
                           entity.BirthPlace = Convert.ToString((dr["BirthPlace"]));
                           entity.DateOfAdmission = Convert.ToDateTime((dr["DateOfAdmission"]));
                           entity.AdmissionClass = Convert.ToString((dr["AdmissionClass"]));
                           entity.Religion = Convert.ToString((dr["Religion"]));
                           entity.Subcaste = Convert.ToString((dr["Subcaste"]));
                           entity.Category = Convert.ToString(dr["Category"]);
                           entity.AadharCardNo = Convert.ToString((dr["AadharCardNo"]));
                           entity.ParentsContactNo = Convert.ToString((dr["ParentsContactNo"]));
                           entity.Address = Convert.ToString((dr["Address"]));
                           entity.BankName = Convert.ToString((dr["BankName"]));
                           entity.BankAccountHolderName = Convert.ToString((dr["Bank account holder name"]));
                           entity.AccountNo = Convert.ToString((dr["AccountNo"]));
                           entity.Branch = Convert.ToString((dr["Branch"]));
                           entity.IFSCCode = Convert.ToString((dr["IFSCCode"]));
                           entites.Add(entity);
                       }
                   }
               }
               else
               {
                   
                       Export entity = new Export();
                       entity.SrNo = 0;
                       entity.StudentId = "";
                       entity.StudentName = "";
                       entity.MotherName = "";
                       entity.Class = "";
                       entity.Division = "";
                       entity.GrNo = Convert.ToInt32(0);
                       entity.BirthDate = Convert.ToDateTime("01-01-2017  12:00:00 AM");
                       entity.BirthPlace = "";
                       entity.DateOfAdmission = Convert.ToDateTime("01-01-2017  12:00:00 AM");
                       entity.AdmissionClass = "";
                       entity.Religion = "";
                       entity.Subcaste = "";
                       entity.Category = "";
                       entity.AadharCardNo = "";
                       entity.ParentsContactNo = "";
                       entity.Address = "";
                       entity.BankName = "";
                       entity.BankAccountHolderName = "";
                       entity.AccountNo = "";
                       entity.Branch = "";
                       entity.IFSCCode = "";
                       entites.Add(entity);
                   }
               

               return entites.AsQueryable();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       
    }
}
