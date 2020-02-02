using BusinessAccessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PresentationLayer.CrystalReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PresentationLayer.Helpers;

namespace PresentationLayer
{
    public partial class Report : System.Web.UI.Page
    {
             

        private string reportName = "Report";
        private bool isPrintExport = false;

        private string reportLang = System.Configuration.ConfigurationManager.AppSettings["Culture"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportDocument rd = new ReportDocument();
                BindReport(rd);
            }
        }

        private void BindReport(ReportDocument rd)
        {
            DateTime fromDate, toDate;
            int studentId = 0;
            switch (PresentationLayer.Helpers.SessionHelper.ReportIndex)
            {
                case 0:
                    studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
                    rd.Load(Server.MapPath("~/CrystalReports/TransferCertificate_" + reportLang + ".rpt"));
                    rd.SetDataSource(LeavingCertificate(studentId));

                    reportName = "TC_" + reportName;
                    break;
                case 1:
                    studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
                    rd.Load(Server.MapPath("~/CrystalReports/BonafideCertificate_" + reportLang + ".rpt"));
                    rd.SetDataSource(BonafideCertificate(studentId));
                    reportName = "Bonafide_" + reportName;
                    break;
                case 2:
                    rd.Load(Server.MapPath("~/CrystalReports/Receipt_" + reportLang + ".rpt"));
                    rd.SetDataSource(FeeReceipt());
                    reportName = "Receipt_" + reportName;
                    break;
                case 3:
                    studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
                    rd.Load(Server.MapPath("~/CrystalReports/NirgamCertificate_" + reportLang + ".rpt"));
                    rd.SetDataSource(NirgamCertificate(studentId));
                    reportName = "Nirgam_" + reportName;
                    break;
                case 4:
                    studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
                    rd.Load(Server.MapPath("~/CrystalReports/BirthCertificate_" + reportLang + ".rpt"));
                   rd.SetDataSource(BirthCertificate(studentId));
                   reportName = "Birth_" + reportName;
                    break;

                case 5:
                    int EmployeeId = 0;
                    EmployeeId = (int)PresentationLayer.Helpers.SessionHelper.TempId;
                    rd.Load(Server.MapPath("~/CrystalReports/EmployeeLeave_" + reportLang + ".rpt"));
                    rd.SetDataSource(LeaveReport(EmployeeId));

                    reportName = "Leave_" + reportName;
                    break;

                case 6:
                     fromDate = PresentationLayer.Helpers.SessionHelper.fromDate;
                     toDate = PresentationLayer.Helpers.SessionHelper.toDate;
                    rd.Load(Server.MapPath("~/CrystalReports/AssetDetails_" + reportLang + ".rpt"));
                    rd.SetDataSource(AssetReport(fromDate,toDate));

                    reportName = "Asset_" + reportName;
                    break;
                case 7:
                     fromDate = PresentationLayer.Helpers.SessionHelper.fromDate;
                     toDate = PresentationLayer.Helpers.SessionHelper.toDate;
                    rd.Load(Server.MapPath("~/CrystalReports/CashBook_" + reportLang + ".rpt"));
                    rd.SetDataSource(CashBookReport(fromDate, toDate));

                    reportName = "CashBook_" + reportName;
                    break;
            }


            CrystalReportViewer1.ReportSource = rd;
            //CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
        }

        private DataSet CashBookReport(DateTime fromDate, DateTime toDate)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";

            DataTable dt = new DataTable("AccountDetails");
            dt.TableName = "AccountDetails";

            dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
            dt.Columns.Add(new DataColumn("NarrationDetails", typeof(string)));
            dt.Columns.Add(new DataColumn("TransactionType", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Total", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));

            AccountBAL balObject = new AccountBAL();
            IQueryable<Entities.Account> entites = balObject.GetAll(SessionHelper.SchoolId);

            if (entites != null && entites.Count() > 0)
            {

                foreach (Entities.Account entity in entites)
                {
                    DataRow dr = dt.NewRow();

                    dr["SrNo"] = entity.SrNo;
                    dr["NarrationDetails"] = entity.NarrationDetails;
                    dr["TransactionType"] = entity.TransactionType;
                    dr["Amount"] = entity.Amount;
                    dr["Total"] = 0;//entity.Total;
                    dr["Remark"] = entity.Remark;
                    dr["TransactionDate"] = entity.TransactionDate;

                    reportName = "_";
                    dt.Rows.Add(dr);
                }
            }
            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;
        }

        private DataSet AssetReport(DateTime fromDate, DateTime toDate)
        {
             DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";

            DataTable dt = new DataTable("AssetDetails");
            dt.TableName = "AssetDetails";

            dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
            dt.Columns.Add(new DataColumn("AssetName", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Total", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));

            dt.Columns.Add(new DataColumn("PurchaseDate", typeof(DateTime)));

            AssetBAL balObject = new AssetBAL();
            IQueryable<Entities.Asset> entites = balObject.GetAll(SessionHelper.SchoolId);

            if (entites != null && entites.Count() > 0)
            {

                foreach (Entities.Asset entity in entites)
                {
                     DataRow dr = dt.NewRow();
                
                    dr["SrNo"] = entity.SrNo;
                    dr["AssetName"] = entity.AssetName;
                    dr["Quantity"] = entity.Quantity;
                    dr["Total"] = entity.Total;
                    dr["Remark"] = entity.Remark;
                    dr["PurchaseDate"] = entity.PurchaseDate;

                   reportName = "_";
                   dt.Rows.Add(dr);
                }

            }
            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;
        }


        private DataSet LeaveReport(int EmployeeId)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";

            DataTable dt = new DataTable("EmployeeLeave");
            dt.TableName = "EmployeeLeave";

            dt.Columns.Add(new DataColumn("EmployeeId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeFullNName", typeof(string)));
            dt.Columns.Add(new DataColumn("LeaveFromDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("LeaveToDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("LeaveType", typeof(string)));
            dt.Columns.Add(new DataColumn("BalanceLeaves", typeof(decimal)));
            dt.Columns.Add(new DataColumn("LeavesInDays", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Reason", typeof(string)));
            dt.Columns.Add(new DataColumn("TransactionType", typeof(string)));
            
            

            EmployeeBAL balObject = new EmployeeBAL();
            IQueryable<Entities.EmployeeLeaveTransaction> entites = balObject.GetAllEmployeeLeaves(SessionHelper.SchoolId).Where(c => c.EmployeeId == EmployeeId);

            if (entites != null && entites.Count() > 0)
            {

                foreach (Entities.EmployeeLeaveTransaction entity in entites)
                {
                //Entities.EmployeeLeaveTransaction entity = entites.FirstOrDefault();
                DataRow dr = dt.NewRow();
                dr["EmployeeId"] = entity.EmployeeId;
                dr["EmployeeFullNName"] = entity.EmployeeFullName;
                dr["LeaveFromDate"] = entity.LeaveFromDate;
                dr["LeaveToDate"] = entity.LeaveToDate;

                string strLeaveType="";
                if (entity.LeaveType == 1)
                    strLeaveType = "Annual Leave";
                else if (entity.LeaveType == 2)
                    strLeaveType = "Sick Leave";
                dr["LeaveType"] = strLeaveType;

                dr["LeavesInDays"] = entity.LeavesInDays;
                dr["BalanceLeaves"] = entity.BalanceLeaves;
                dr["Reason"] = entity.Remark;



                if (entity.TransactionType == 1)
                {
                    dr["TransactionType"] = "Cr";
                }
                else
                    dr["TransactionType"] = "Dr";

                
                
                reportName = entity.EmployeeId + "_" + entity.EmployeeFullName.Trim();
                dt.Rows.Add(dr);
               }
                
            }
            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;
        }

        private DataSet LeavingCertificate(int StudentId)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";

            DataTable dt = new DataTable("LeavingCertificate");
            dt.TableName = "LeavingCertificate";
            dt.Columns.Add(new DataColumn("StudentId", typeof(string)));
            dt.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dt.Columns.Add(new DataColumn("MiddleName", typeof(string)));
            dt.Columns.Add(new DataColumn("LastName", typeof(string)));
            dt.Columns.Add(new DataColumn("MotherName", typeof(string)));
            dt.Columns.Add(new DataColumn("MotherTounge", typeof(string)));
            dt.Columns.Add(new DataColumn("ReligionName", typeof(string)));
            dt.Columns.Add(new DataColumn("CastName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubCast", typeof(string)));
            dt.Columns.Add(new DataColumn("Nationality", typeof(string)));
            dt.Columns.Add(new DataColumn("PlaceOfBirth", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfBirth", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DateOfBirthInWord", typeof(string)));
            dt.Columns.Add(new DataColumn("LastSchool", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfAdmission", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Progress", typeof(string)));
            dt.Columns.Add(new DataColumn("Conduct", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfLeaveingSchool", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ClassInWhichStudingAndSinceWhen", typeof(string)));
            dt.Columns.Add(new DataColumn("Reason", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("RegisterNo", typeof(int)));
            dt.Columns.Add(new DataColumn("TCNo", typeof(string)));
            dt.Columns.Add(new DataColumn("TCPrinted", typeof(bool)));          
            dt.Columns.Add(new DataColumn("AdharcardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfLeaveingSchoolInWords", typeof(string)));
            dt.Columns.Add(new DataColumn("Taluka", typeof(string)));
            dt.Columns.Add(new DataColumn("District", typeof(string)));
            dt.Columns.Add(new DataColumn("state", typeof(string)));
            dt.Columns.Add(new DataColumn("Country", typeof(string)));

            int studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;

            // Fetch the District, State, Country from database ***********
            
            string strDistrict, strState, strCountry, strCity;
            strDistrict = strState = strCountry = strCity = "";

            //balObject.FindBy(s => s.StudentId == studentId );

            StudentParentBAL balObjectAddr = new StudentParentBAL();
            IQueryable<Entities.StudentParent> entites1 = balObjectAddr.FindBy(s => s.StudentId == studentId);
           
            if (entites1 != null && entites1.Count() > 0)
            {
                Entities.StudentParent entity1 = entites1.FirstOrDefault();

                CountryBAL countryBAL = new CountryBAL();
                IQueryable<Entities.Country> countries = countryBAL.GetAll();

                StateBAL stateBAL = new StateBAL();
                IQueryable<Entities.State> states = stateBAL.GetAll();

                DistrictBAL DistrictObject = new DistrictBAL();
                IQueryable<Entities.District> District = DistrictObject.GetAll();
               
                CityBAL CityObject = new CityBAL();
                IQueryable<Entities.City> Cities = CityObject.GetAll();
       

                strCountry = countries.Where(c => c.CountryId == entity1.CountryId).FirstOrDefault().CountryName;
                strState = states.Where(s => s.StateId == entity1.StateId).FirstOrDefault().StateName;
                strDistrict = District.Where(c => c.DistrictId == entity1.DistrictId).FirstOrDefault().DistrictName;
                strCity = Cities.Where(c => c.CityId == entity1.CityId).FirstOrDefault().CityName;

            }
            
            
            // *********

            StudentBAL balObject = new StudentBAL();
            //IQueryable<Entities.Student> entites = balObject.FindBy(s => s.StudentId == studentId );
            IQueryable<Entities.Student> entites = balObject.GetAll(SessionHelper.SchoolId).Where(c => c.StudentId == studentId);
            
            if (entites != null && entites.Count() > 0)
            {
                Entities.Student entity = entites.FirstOrDefault();
                DataRow dr = dt.NewRow();

                dr["StudentId"] = entity.UStudentId;
                dr["FirstName"] = entity.FirstName.Trim();
                dr["MiddleName"] = entity.MiddleName.Trim(); ;
                dr["LastName"] = entity.LastName.Trim(); ;
                dr["MotherName"] = entity.MotherName.Trim(); ;
                dr["ReligionName"] = entity.ReligionName.Trim(); ;
                dr["CastName"] = entity.CastName.Trim();
                dr["SubCast"] = ""; //TODO
                dr["Nationality"] = entity.Nationality.Trim();
                dr["DateOfBirth"] = entity.DateOfBirth; //Date
                dr["PlaceOfBirth"] = entity.PlaceOfBirth;
           
                if (entity.DateOfBirth != null)
                {
                    dr["DateOfBirthInWord"] = DateToText(entity.DateOfBirth);
                }
                else
                {
                    dr["DateOfBirthInWord"] = "";
                }

                dr["LastSchool"] = entity.LastSchoolAttended;
                dr["DateOfAdmission"] = entity.DateOfAdmission; //Date
                if (entity.DateOfAdmission != null)
                {
                    dr["DateOfAdmission"] = entity.DateOfAdmission;//Date
                }
                dr["Progress"] = entity.Progress;
                dr["Conduct"] = entity.Conduct;
                if (entity.DateOfLeavingSchool != null)
                {
                    dr["DateOfLeaveingSchool"] = entity.DateOfLeavingSchool;//Date
                    dr["DateOfLeaveingSchoolInWords"] = DateToText((System.DateTime)entity.DateOfLeavingSchool);
                    
                }
                else
                {
                    dr["DateOfLeaveingSchoolInWords"] = "";
                }


                dr["ClassInWhichStudingAndSinceWhen"] = entity.ClassInWhichStudingAndSinceWhen;
                dr["Reason"] = entity.ReasonForLeavingSchool;
                dr["Remark"] = entity.RemarkOnTC;
                dr["RegisterNo"] = entity.RegisterId;

                dr["TCPrinted"] = entity.TCPrinted;
                if (!entity.TCPrinted)
                {
                    long maxTCNo = balObject.GetMaxTCNo();
                    dr["TCNo"] = FormatedTCNo(Convert.ToString(maxTCNo + 1));

                    //if (isPrintExport)
                      //  UpdateTCDetails(studentId);
                    
                }
                else
                {
                    dr["TCNo"] = FormatedTCNo(Convert.ToString(entity.TCNo));
                    
                }

                if (isPrintExport)
                  UpdateTCDetails(studentId);

                dr["AdharcardNo"] = entity.AdharcardNo;
                dr["MotherTounge"] = entity.MotherTounge;
               
                // ****
                dr["Taluka"] = strCity;
                dr["District"] = strDistrict;
                dr["state"] = strState;
                dr["Country"] = strCountry;               

                dt.Rows.Add(dr);
                reportName = entity.RegisterId + "_" + entity.LastName.Trim() + "_" + entity.FirstName.Trim();
            }

            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;
        }

        private void UpdateTCDetails(int StudentId)
        {
            StudentBAL studentBAL = new StudentBAL();
            studentBAL.UpdateTCDetails(StudentId);
        }

        private DataTable getSchoolDetails()
        {
            DataTable dt = new DataTable("SchoolDetails");
            dt.TableName = "SchoolDetails";  

            dt.Columns.Add(new DataColumn("ManagementName", typeof(string)));
            dt.Columns.Add(new DataColumn("SchoolName", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Taluka", typeof(string)));
            dt.Columns.Add(new DataColumn("District", typeof(string)));
            dt.Columns.Add(new DataColumn("ContactNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("EmailId", typeof(string)));
            dt.Columns.Add(new DataColumn("SchoolReconginationNo", typeof(string)));

            dt.Columns.Add(new DataColumn("Medium", typeof(string)));
            dt.Columns.Add(new DataColumn("UDiscNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Board", typeof(string)));
            dt.Columns.Add(new DataColumn("AffilationNo", typeof(string)));

            dt.Columns.Add(new DataColumn("LogoPath", typeof(string)));

            SchoolDetailsBAL balObject = new SchoolDetailsBAL();
          
            var entites = balObject.GetAll().Where(u => u.SchoolId == SessionHelper.SchoolId);

            if (entites != null && entites.Count() > 0)
            {
                Entities.SchoolDetails entity = entites.FirstOrDefault();
                DataRow dr = dt.NewRow();
                dr["ManagementName"] = entity.ManagementName.Trim();
                dr["SchoolName"] = entity.SchoolName.Trim();
                dr["Address"] = entity.Address.Trim();
                dr["Taluka"] = entity.Taluka.Trim();
                dr["District"] = entity.District.Trim();

                dr["ContactNumber"] = entity.ContactNumber.Trim();
                dr["EmailId"] = entity.EmailId.Trim();
                dr["SchoolReconginationNo"] = entity.SchoolReconginationNo.Trim();
                dr["Medium"] = entity.Medium.Trim();
                dr["UDiscNo"] = entity.UDiscNo.Trim();

                dr["Board"] = entity.Board.Trim();
                dr["AffilationNo"] = entity.AffilationNo.Trim();

                string host = HttpContext.Current.Request.Url.Host;

                dr["LogoPath"] = "http://" + host + entity.LogoPath.Trim();


                dt.Rows.Add(dr);

            }

            return dt;
        }
        private DataSet BonafideCertificate(int StudentId)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";
            
            DataTable dt = new DataTable("BonafideCertificate");
            dt.TableName = "BonafideCertificate";
            dt.Columns.Add(new DataColumn("StudentId", typeof(int)));
            dt.Columns.Add(new DataColumn("StudentFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfBirth", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfBirthInWord", typeof(string)));
            dt.Columns.Add(new DataColumn("Studying", typeof(string)));
            dt.Columns.Add(new DataColumn("Year", typeof(string)));
            dt.Columns.Add(new DataColumn("CastAndSubCast", typeof(string)));
            dt.Columns.Add(new DataColumn("ReserveCategory", typeof(string)));
            dt.Columns.Add(new DataColumn("RegisterNo", typeof(int)));

            int studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
            StudentBAL balObject = new StudentBAL();
            //IQueryable<Entities.Student> entites = balObject.FindBy(s => s.StudentId == studentId );
            IQueryable<Entities.Student> entites = balObject.GetAll(SessionHelper.SchoolId).Where(c => c.StudentId == studentId);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Student entity = entites.FirstOrDefault();
                DataRow dr = dt.NewRow();
                dr["StudentId"] = studentId;
                dr["StudentFullName"] = string.Concat(entity.FirstName.Trim(), " ", entity.MiddleName.Trim(), " ", entity.LastName.Trim()); ;
              
                dr["CastAndSubCast"] = entity.ReligionName + " - " + entity.CastName;
                dr["DateOfBirth"] = entity.DateOfBirth.ToString("dd-MM-yyyy"); //Date
                if (entity.DateOfBirth != null)
                {
                    dr["DateOfBirthInWord"] = DateToText(entity.DateOfBirth);
                }
                else
                {
                    dr["DateOfBirthInWord"] = "";
                }
                dr["Studying"] = entity.ClassName;
                //dr["Year"] = entity.AcademicYear;
                dr["ReserveCategory"] = entity.ReserveCategory;
                dr["RegisterNo"] = entity.RegisterId;
                dt.Rows.Add(dr);
                
                reportName = entity.RegisterId + "_" + entity.LastName.Trim() + "_" + entity.FirstName.Trim();
            }


            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;
        }

        private DataSet FeeReceipt()
        {
            DataSet ds = new DataSet("dsFeeReceipt");
            DataTable dt = new DataTable("Receipt");
            dt.TableName = "Receipt";
            dt.Columns.Add(new DataColumn("StudentTransactionId", typeof(long)));
            dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));
            dt.Columns.Add(new DataColumn("Class", typeof(string)));
            dt.Columns.Add(new DataColumn("Division", typeof(string)));
            dt.Columns.Add(new DataColumn("StudentFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceiptNo", typeof(int)));
            dt.Columns.Add(new DataColumn("BankName", typeof(string)));
            dt.Columns.Add(new DataColumn("ChequeNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceiptTotal", typeof(decimal)));

            StudentTransactionBAL studentTransactionBAL = new StudentTransactionBAL();
            Entities.StudentTransaction entity = studentTransactionBAL.GetAll().LastOrDefault();
            long studentTransactionId = 0;
            if (entity != null)
            {
                studentTransactionId = (long)entity.StudentTransactionId;
                DataRow dr = dt.NewRow();
                dr["StudentTransactionId"] = entity.StudentTransactionId;
                dr["Class"] = entity.ClassName;
                dr["Division"] = entity.DivisionName;
                dr["StudentFullName"] = entity.StudentFullNameWithTitle;
                dr["TransactionDate"] = entity.TransactionDate;//Date
                dr["Remark"] = entity.Remark;
                dr["ReceiptNo"] = entity.ReceiptNo;
                dr["BankName"] = entity.BankName;
                dr["ChequeNumber"] = entity.ChequeNumber;
                dr["ReceiptTotal"] = entity.ReceiptTotal;

                dt.Rows.Add(dr);

                reportName = entity.ReceiptNo + "_" + entity.StudentFullNameWithTitle;
            }
            ds.Tables.Add(dt);
            if (studentTransactionId > 0)
            {
                dt = new DataTable("FeeDetails");
                dt.TableName = "FeeDetails";
                dt.Columns.Add(new DataColumn("StudentTransactionId", typeof(long)));
                dt.Columns.Add(new DataColumn("StudentTransactionSubId", typeof(long)));
                dt.Columns.Add(new DataColumn("FeeHeadName", typeof(string)));
                dt.Columns.Add(new DataColumn("Cr", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Dr", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Balance", typeof(decimal)));

                studentTransactionBAL = new StudentTransactionBAL();
                IQueryable<Entities.StudentTransactionSub> subEntities = studentTransactionBAL.GetAllStudentTransactionSubByTransactionId(studentTransactionId);

                if (subEntities != null && subEntities.Count() > 0)
                {
                    foreach (Entities.StudentTransactionSub subEntity in subEntities)
                    {

                        DataRow dr = dt.NewRow();
                        dr["StudentTransactionId"] = subEntity.StudentTransactionId;
                        dr["StudentTransactionSubId"] = subEntity.StudentTransactionSubId;
                        dr["FeeHeadName"] = subEntity.FeeHeadName;
                        dr["Cr"] = subEntity.Cr;
                        dr["Dr"] = subEntity.Dr;
                        dr["Balance"] = subEntity.Balance;

                        dt.Rows.Add(dr);
                    }

                }
                ds.Tables.Add(getSchoolDetails());
                ds.Tables.Add(dt);
            }

            return ds;
        }

        public string DateToText(DateTime dt)
        {


            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;
            DateTime dtm = new DateTime(1, month, 1);
            string date;


            if (reportLang== "mr-IN")
                date = " "+ convert2Marathi(day) + " " + dtm.ToString("MMMM") + " " + convert2Marathi(year);
            else
                date = convert2Eng(day) + " of " + dtm.ToString("MMMM") + " " + convert2Eng(year);

                 return date;
        }

        public static string convert2Marathi(int number)
        {
            string sDate=string.Empty;

            string[] ordinals = {"", "एक", "दोन", "तीन", "चार", "पाच", "सहा", "सात", "आठ", "नऊ", "दहा", "अकरा", "बारा", "तेरा", "चौदा", "पंधरा", "सोळा", "सतरा", "अठरा", "एकोणवीस", "वीस", 
                                "एकवीस", "बावीस", "तेवीस", "चोवीस", "पंचवीस", "सव्वीस", "सत्तावीस", "अठ्ठावीस", "एकोणतीस","तीस", "एकतीस", "बत्तीस", "तेहेतीस", "चौतीस", "पस्तीस", "छत्तीस", 
                                "सदतीस", "अडतीस", "एकोणचाळीस", "चाळीस","एक्केचाळीस", "बेचाळीस", "त्रेचाळीस", "चव्वेचाळीस", "पंचेचाळीस", "सेहेचाळीस", "सत्तेचाळीस", "अठ्ठेचाळीस", "एकोणपन्नास",
                                " पन्नास", "एक्कावन्न", "बावन्न", "त्रेपन्न", "चोपन्न", "पंचावन्न", "छप्पन्न", "सत्तावन्न", "अठ्ठावन्न", "एकोणसाठ", "साठ", "एकसष्ठ", "बासष्ठ", "त्रेसष्ठ", "चौसष्ठ", "पासष्ठ",
                                "सहासष्ठ", "सदुसष्ठ", "अडुसष्ठ", "एकोणसत्तर", "सत्तर","एक्काहत्तर", "बाहत्तर", "त्र्याहत्तर", "चौर्‍याहत्तर", "पंच्याहत्तर", "शहात्तर", "सत्याहत्तर", "अठ्ठ्याहत्तर", "एकोणऐंशी",
                                "ऐंशी", "एक्क्य़ाऐंशी", "ब्याऐंशी", "ञ्याऐंशी", "चौऱ्याऐंशी", "पंच्याऐंशी", "शहाऐंशी", "सत्त्याऐंशी", "अठ्ठ्याऐंशी", "एकोणनव्वद", "नव्वद", "एक्क्याण्णव", "ब्याण्णव", "त्र्याण्णव", "चौऱ्याण्णव",
                                "पंच्याण्णव", "शहाण्णव", "सत्त्याण्णव", "अठ्ठ्याण्णव", " नव्याण्णव","शंभर"
                             };

            string strNumber = number.ToString();

            if (strNumber.Length < 3)  // for day
                return ordinals[number];

            // for year

            int num1 = int.Parse(strNumber.Substring(0, 2));
            int num2 = int.Parse(strNumber.Substring(2, 2)); // last part of year

            if ((num1 > 10) && (num1 < 20))
                sDate += ordinals[num1] + "शे ";
            else{
            
                sDate += ordinals[int.Parse(strNumber.Substring(0, 1))]+" हजार ";

                if (strNumber.Substring(1, 3) == "100")
                    sDate += "शंभर";
                else
                {
                    int num3 = int.Parse(strNumber.Substring(1, 1));
                    if (num3 > 0)
                        sDate += ordinals[num3] + "शे ";
                }
            }

            sDate += ordinals[num2];
            return sDate;
        }
        public static string convert2Eng(int number)
        {
            string[] eMonth ={"","First","Second","Third","Fourth","Fifth","Sixth","Seventh","Eighth","Ninth","Tenth",
                                "Eleventh","Twelfth","Thirteenth","Fourteenth","Fifteenth","Sixteenth","Seventeenth",
                                "Eighteenth","Nineteenth","Twentieth","Twenty First","Twenty Second","Twenty Third","Twenty Fourth",
                                "Twenty Fifth","Twenty Sixth","Twenty Seventh","Twenty Eighth","Twenty Ninth","Thirtieth","Thirty First"
                               };

            string strNumber = number.ToString();

            if (strNumber.Length < 3)
                return eMonth[number];

            return NumberToText(number, true);

        }

        public static string NumberToText(int number, bool isUK)
        {
            if (number == 0) return "Zero";
            string and = isUK ? "and " : ""; // deals with UK or US numbering
            if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
            "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
            "Six Hundred " + and + "Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Million ", "Billion " };
            num[0] = number % 1000;           // units
            num[1] = number / 1000;
            num[2] = number / 1000000;
            num[1] = num[1] - 1000 * num[2];  // thousands
            num[3] = number / 1000000000;     // billions
            num[2] = num[2] - 1000 * num[3];  // millions
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10;              // ones
                t = num[i] / 10;
                h = num[i] / 100;             // hundreds
                t = t - 10 * h;               // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            isPrintExport = true;
            ReportDocument crystalReport = new ReportDocument();
            BindReport(crystalReport);
            ExportFormatType formatType = ExportFormatType.NoFormat;
            switch (rbFormat.SelectedItem.Value)
            {
                case "Word":
                    formatType = ExportFormatType.WordForWindows;
                    break;
                case "PDF":
                    formatType = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatType = ExportFormatType.Excel;
                    break;
                case "CSV":
                    formatType = ExportFormatType.CharacterSeparatedValues;
                    break;
            }


            crystalReport.ExportToHttpResponse(formatType, Response, true, reportName);
          
            Response.End();
        }

        /*
        private DataTable EntryList(int? AcademicYearId = null, int? ClassId = null, int? DivisionId = null)
        {
            //DataSet ds = new DataSet();
            //ds.DataSetName = "DataSourceForReport";
            DataTable dt = new DataTable("EntryList");
            dt.TableName = "EntryList";
            dt.Columns.Add(new DataColumn("StudentId", typeof(int)));
            dt.Columns.Add(new DataColumn("StudentFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("NameOfMother", typeof(string)));
            dt.Columns.Add(new DataColumn("CastAndSubCast", typeof(string)));
            dt.Columns.Add(new DataColumn("Nationality", typeof(string)));
            dt.Columns.Add(new DataColumn("PlaceOfBirth", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfBirth", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DateOfBirthInWord", typeof(string)));
            dt.Columns.Add(new DataColumn("LastSchool", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfAdmission", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Progress", typeof(string)));
            dt.Columns.Add(new DataColumn("Conduct", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfLeaveingSchool", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ClassInStuding", typeof(string)));
            dt.Columns.Add(new DataColumn("Reason", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("RegisterNo", typeof(int)));
            dt.Columns.Add(new DataColumn("TCNo", typeof(string)));
            dt.Columns.Add(new DataColumn("TCDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("PramanPatraKramank", typeof(int)));
            dt.Columns.Add(new DataColumn("FullFee", typeof(decimal)));

            //int studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
            StudentBAL balObject = new StudentBAL();

            IQueryable<Entities.Student> entites = balObject.FindBy(s => s.AcademicYearId == AcademicYearId);
            if (entites != null && entites.Count() > 0)
            {
                foreach (Entities.Student entity in entites)
                {
                    DataRow dr = dt.NewRow();
                    dr["StudentId"] = entity.StudentId;
                    dr["StudentFullName"] = string.Concat(entity.FirstName.Trim(), " ", entity.MiddleName.Trim(), " ", entity.LastName.Trim()); ;
                    dr["NameOfMother"] = entity.MotherName.Trim();
                    dr["CastAndSubCast"] = entity.ReligionName + " " + entity.CastName;
                    dr["Nationality"] = entity.Nationality;
                    dr["PlaceOfBirth"] = entity.PlaceOfBirth;
                    dr["DateOfBirth"] = entity.DateOfBirth; //Date
                    if (entity.DateOfBirth != null)
                    {
                        dr["DateOfBirthInWord"] = DateToText(entity.DateOfBirth, false, true);
                    }
                    else
                    {
                        dr["DateOfBirthInWord"] = "";
                    }
                    dr["LastSchool"] = entity.LastSchoolAttended;
                    if (entity.DateOfAdmission != null)
                    {
                        dr["DateOfAdmission"] = entity.DateOfAdmission;//Date
                    }
                    dr["Progress"] = entity.Progress;
                    dr["Conduct"] = entity.Conduct;
                    if (entity.DateOfLeavingSchool != null)
                    {
                        dr["DateOfLeaveingSchool"] = entity.DateOfLeavingSchool;//Date
                    }
                    dr["ClassInStuding"] = entity.ClassInWhichStudingAndSinceWhen;
                    dr["Reason"] = entity.ReasonForLeavingSchool;
                    dr["Remark"] = entity.Remark;
                    dr["RegisterNo"] = entity.RegisterId;
                    dr["TCDate"] = DateTime.Today.Date;
                    dr["TCNo"] = this.FormatedTCNo(entity.TCNo.ToString());
                    dr["PramanPatraKramank"] = 0;
                    dr["FullFee"] = 0;
                    dt.Rows.Add(dr);
                }
            }
            //ds.Tables.Add(dt);

            return dt;
        }
        */
        private string FormatedTCNo(string tcNo)
        {
            if (tcNo == "0")
                return " ";

            string fTcNo = string.Empty;
            if (tcNo.Length < 4)
            {
                if (tcNo.Length == 1)
                {
                    fTcNo = "000" + tcNo ;//+ //"N";
                }
                else if (tcNo.Length == 2) { fTcNo = "00" + tcNo;}// + "N"; }
                else if (tcNo.Length == 3) { fTcNo = "0" + tcNo; }//+ "N"; }
            }
            else
            {
                fTcNo = fTcNo;// + "N";
            }
            return fTcNo;
        }


        private DataSet NirgamCertificate(int StudentId)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";
       
            DataTable dt = new DataTable("NirgamCertificate");
            dt.TableName = "NirgamCertificate";
            //dt.Columns.Add(new DataColumn("GeneralRegistrationNo", typeof(int)));
            dt.Columns.Add(new DataColumn("RegisterNo", typeof(int)));
            dt.Columns.Add(new DataColumn("StudentId", typeof(int)));
            dt.Columns.Add(new DataColumn("StudentFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("NameOfMother", typeof(string)));
            dt.Columns.Add(new DataColumn("AdharcardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ReligionAndCast", typeof(string)));
            dt.Columns.Add(new DataColumn("PlaceOfBirth", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfBirth", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DateOfBirthInWord", typeof(string)));
            dt.Columns.Add(new DataColumn("LastSchoolAttended", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfAdmission", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("MotherTounge", typeof(string)));
            dt.Columns.Add(new DataColumn("LastSchoolClass", typeof(string)));
            dt.Columns.Add(new DataColumn("LastSchoolTCNo", typeof(string)));
            dt.Columns.Add(new DataColumn("AdmissionGivenInClass", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfLeavingSchool", typeof(DateTime)));
            //dt.Columns.Add(new DataColumn("Conduct", typeof(string)));
            //dt.Columns.Add(new DataColumn("DateOfLeaveingSchool", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ClassStudingWhileLeavingSchool", typeof(string)));
            dt.Columns.Add(new DataColumn("CertificateNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            
          


            int studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
            StudentBAL balObject = new StudentBAL();

           // IQueryable<Entities.Student> entites = balObject.FindBy(s => s.StudentId == studentId);
            IQueryable<Entities.Student> entites = balObject.GetAll(SessionHelper.SchoolId).Where(c => c.StudentId == studentId);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Student entity = entites.FirstOrDefault();
                DataRow dr = dt.NewRow();
                //dr["GRegistrationNo"] = entity.RegisterId;
                dr["StudentId"] = studentId;
                dr["RegisterNo"] = entity.RegisterId;
                dr["StudentFullName"] = string.Concat(entity.FirstName.Trim(), " ", entity.MiddleName.Trim(), " ", entity.LastName.Trim()); ;
                dr["NameOfMother"] = entity.MotherName.Trim();
                dr["AdharcardNo"] = entity.AdharcardNo.Trim();
                dr["ReligionAndCast"] = entity.ReligionName + " - " + entity.CastName;
                dr["PlaceOfBirth"] = entity.PlaceOfBirth;
                dr["DateOfBirth"] = entity.DateOfBirth; //Date
                if (entity.DateOfBirth != null)
                {
                    dr["DateOfBirthInWord"] = DateToText(entity.DateOfBirth);
                }
                else
                {
                    dr["DateOfBirthInWord"] = "";
                }
                dr["LastSchoolAttended"] = entity.LastSchoolAttended;
                if (entity.DateOfAdmission != null)
                {
                    dr["DateOfAdmission"] = entity.DateOfAdmission;//Date
                }
                dr["MotherTounge"] = entity.MotherTounge;
                dr["LastSchoolClass"] = entity.LastSchoolClass;
                dr["LastSchoolTCNo"] = entity.LastSchoolTCNo;

                dr["AdmissionGivenInClass"] = entity.ClassName; 

                if (entity.DateOfLeavingSchool != null)
                {
                    dr["DateOfLeavingSchool"] = entity.DateOfLeavingSchool;//Date
                }
                dr["ClassStudingWhileLeavingSchool"] = entity.ClassInWhichStudingAndSinceWhen;
                dr["CertificateNo"] = FormatedTCNo(Convert.ToString(entity.TCNo));
                dr["Remark"] = entity.RemarkOnTC;
                
               
                dt.Rows.Add(dr);

                reportName = entity.RegisterId + "_" + entity.LastName.Trim() + "_" + entity.FirstName.Trim();
            }

            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;

        }
        private DataSet BirthCertificate(int StudentId)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "DataSourceForReport";

            DataTable dt = new DataTable("BirthCertificate");
            dt.TableName = "BirthCertificate";
            dt.Columns.Add(new DataColumn("StudentId", typeof(int)));
            dt.Columns.Add(new DataColumn("RegisterNo", typeof(int)));
            dt.Columns.Add(new DataColumn("StudentFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("PlaceOfBirth", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfBirth", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DateOfBirthInWord", typeof(string)));
            int studentId = (int)PresentationLayer.Helpers.SessionHelper.StudentId;
            StudentBAL balObject = new StudentBAL();

            //IQueryable<Entities.Student> entites = balObject.FindBy(s => s.StudentId == studentId);
            IQueryable<Entities.Student> entites = balObject.GetAll(SessionHelper.SchoolId).Where(c => c.StudentId == studentId);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Student entity = entites.FirstOrDefault();
                DataRow dr = dt.NewRow();
                dr["StudentId"] = studentId;
                dr["RegisterNo"] = entity.RegisterId;
                dr["StudentFullName"] = string.Concat(entity.FirstName.Trim(), " ", entity.MiddleName.Trim(), " ", entity.LastName.Trim()); 
                dr["PlaceOfBirth"] = entity.PlaceOfBirth;
                dr["DateOfBirth"] = entity.DateOfBirth; //Date
                dr["DateOfBirthInWord"] = entity.DateOfBirth; 
                 if (entity.DateOfBirth != null)
                {
                    dr["DateOfBirthInWord"] = DateToText(entity.DateOfBirth);
                }
                else
                {
                    dr["DateOfBirthInWord"] = "";
                }
                dt.Rows.Add(dr);

                reportName = entity.RegisterId + "_" + entity.LastName.Trim() + "_" + entity.FirstName.Trim();
            }

            ds.Tables.Add(getSchoolDetails());
            ds.Tables.Add(dt);
            return ds;
        }
    }
}