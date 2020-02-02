using BusinessAccessLayer;
//using Microsoft.Reporting.WebForms;
using PresentationLayer.Areas.SysAdmin.Models;
using PresentationLayer.Other;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;



namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class ReportsController : Controller
    {

        //
        // GET: /SysAdmin/Report/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LeavingCertificate()
        {
            LeavingCertificateVM leavingCertificateVM = new LeavingCertificateVM();

            ClassBAL classBAL = new ClassBAL();
            leavingCertificateVM.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

            return View(leavingCertificateVM);
        }

        [HttpPost]
        public ActionResult LeavingCertificate(FormCollection frm)
        {
            LeavingCertificateVM leavingCertificateVM = new LeavingCertificateVM();
            if (frm["RegisterId"] != null && !string.IsNullOrEmpty(frm["RegisterId"]))
            {
                leavingCertificateVM.RegisterId = Convert.ToInt32(frm["RegisterId"]);
            }

            if (frm["ClassId"] != null && !string.IsNullOrEmpty(frm["ClassId"]))
            {
                leavingCertificateVM.ClassId = Convert.ToInt32(frm["ClassId"]);
            }
            if (frm["DivisionId"] != null && !string.IsNullOrEmpty(frm["DivisionId"]))
            {
                leavingCertificateVM.DivisionId = Convert.ToInt32(frm["DivisionId"]);
            }
            if (frm["StudentId"] != null && !string.IsNullOrEmpty(frm["StudentId"]))
            {
                leavingCertificateVM.StudentId = Convert.ToInt32(frm["StudentId"]);
            }

            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 0;
                PresentationLayer.Helpers.SessionHelper.StudentId = leavingCertificateVM.StudentId;


                return Redirect(@"~\Report.aspx");
            }
            else
            {
                // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                //  leavingCertificateVM.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };

                return View(leavingCertificateVM);
            }
        }

        [HttpGet]
        public ActionResult BonafideCertificate()
        {
            LeavingCertificateVM leavingCertificateVM = new LeavingCertificateVM();
            ClassBAL classBAL = new ClassBAL();
            leavingCertificateVM.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

            return View(leavingCertificateVM);
        }

        [HttpPost]
        public ActionResult BonafideCertificate(FormCollection frm)
        {
            LeavingCertificateVM leavingCertificateVM = new LeavingCertificateVM();
            if (frm["RegisterId"] != null && !string.IsNullOrEmpty(frm["RegisterId"]))
            {
                leavingCertificateVM.RegisterId = Convert.ToInt32(frm["RegisterId"]);
            }
            if (frm["ClassId"] != null && !string.IsNullOrEmpty(frm["ClassId"]))
            {
                leavingCertificateVM.ClassId = Convert.ToInt32(frm["ClassId"]);
            }
            if (frm["DivisionId"] != null && !string.IsNullOrEmpty(frm["DivisionId"]))
            {
                leavingCertificateVM.DivisionId = Convert.ToInt32(frm["DivisionId"]);
            }
            if (frm["StudentId"] != null && !string.IsNullOrEmpty(frm["StudentId"]))
            {
                leavingCertificateVM.StudentId = Convert.ToInt32(frm["StudentId"]);
            }
            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 1;
                PresentationLayer.Helpers.SessionHelper.StudentId = leavingCertificateVM.StudentId;

                return Redirect(@"~\Report.aspx");
            }
            else
            {
                ClassBAL classBAL = new ClassBAL();
                leavingCertificateVM.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

                return View(leavingCertificateVM);
            }
        }

        [HttpGet]
        public ActionResult StudentLedger()
        {
            StudentLedgerBAL studentLedgerBAL = new StudentLedgerBAL();
            IQueryable<Entities.StudentLedger> entities = studentLedgerBAL.GetAll();

            FeeHeadBAL feeHeadBAL = new FeeHeadBAL();
            IQueryable<Entities.FeeHead> feeHeads = feeHeadBAL.GetAll();

            List<StudentLedgerVM> studentLedgerVMs = new List<StudentLedgerVM>();
            foreach (Entities.StudentLedger entity in entities)
            {
                StudentLedgerVM studentLedgerVM = new StudentLedgerVM();
                studentLedgerVM.StudentLedgerId = entity.StudentLedgerId;
                studentLedgerVM.StudentId = entity.StudentId;
                StudentBAL studentObject = new StudentBAL();
                Entities.Student student = studentObject.FindBy(s => s.StudentId == entity.StudentId).FirstOrDefault();
                if (student != null)
                {
                    studentLedgerVM.StudentFullNameWithTitle = string.Concat(student.Title, " ", student.FirstName, " ", student.MiddleName, " ", student.LastName).Trim();
                }
                studentLedgerVM.TransactionDate = entity.TransactionDate;
                studentLedgerVM.FeeHeadId = entity.FeeHeadId;
                studentLedgerVM.FeeHeadName = feeHeads.Where(fh => fh.FeeHeadId == entity.FeeHeadId).FirstOrDefault().FeeHeadName;
                studentLedgerVM.Cr = entity.Cr;
                studentLedgerVM.Dr = entity.Dr;
                studentLedgerVM.HeadBalance = entity.HeadBalance;
                studentLedgerVM.Balance = entity.Balance;
                studentLedgerVM.ReceiptNo = entity.ReceiptNo;
                studentLedgerVM.BankName = entity.BankName;
                studentLedgerVM.ChequeNumber = entity.ChequeNumber;
                studentLedgerVM.Status = entity.Status;
                studentLedgerVM.Remark = entity.Remark;
                //studentLedgerVM.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                //studentLedgerVM.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                //studentLedgerVM.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                //studentLedgerVM.ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
                studentLedgerVMs.Add(studentLedgerVM);
            }
            return View(new GridModel<StudentLedgerVM> { Data = studentLedgerVMs });
        }

        public JsonResult GetStudentDetailsByRegisterId(int RegisterId)
        {
            return this.Json(CommanMethods.GetStudentDetailsByRegisterId(RegisterId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult _GetStudentDetails(int StudentId)
        {
            List<StudentVM> viewModels = new List<StudentVM>();
            List<Entities.Student> studentEntites = new List<Entities.Student>();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.FindBy(s => s.StudentId == StudentId);
            foreach (Entities.Student entity in entites)
            {
                viewModels.Add(CommanMethods.getStudentViewModel(entity));
            }
            return PartialView("_GetStudentDetails", new GridModel<StudentVM> { Data = viewModels });
        }

        public JsonResult GetStudentTCStatus(int StudentId)
        {
            return this.Json(CommanMethods.GetStudentTCStatus(StudentId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NirgamCertificate()
        {
            NirgamCertificateVM nirgamCertificateVM = new NirgamCertificateVM();
            ClassBAL classBAL = new ClassBAL();
            nirgamCertificateVM.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

            return View(nirgamCertificateVM);
        }

        [HttpPost]
        public ActionResult NirgamCertificate(FormCollection frm)
        {
            NirgamCertificateVM nirgamCertificateVM = new NirgamCertificateVM();
            if (frm["RegisterId"] != null && !string.IsNullOrEmpty(frm["RegisterId"]))
            {
                nirgamCertificateVM.RegisterId = Convert.ToInt32(frm["RegisterId"]);
            }
            if (frm["ClassId"] != null && !string.IsNullOrEmpty(frm["ClassId"]))
            {
                nirgamCertificateVM.ClassId = Convert.ToInt32(frm["ClassId"]);
            }
            if (frm["DivisionId"] != null && !string.IsNullOrEmpty(frm["DivisionId"]))
            {
                nirgamCertificateVM.DivisionId = Convert.ToInt32(frm["DivisionId"]);
            }
            if (frm["StudentId"] != null && !string.IsNullOrEmpty(frm["StudentId"]))
            {
                nirgamCertificateVM.StudentId = Convert.ToInt32(frm["StudentId"]);
            }

            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 3;
                PresentationLayer.Helpers.SessionHelper.StudentId = nirgamCertificateVM.StudentId;

                return Redirect(@"~\Report.aspx");
            }
            else
            {
                ClassBAL classBAL = new ClassBAL();
                nirgamCertificateVM.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

                return View(nirgamCertificateVM);
            }
        }
        [HttpGet]
        public ActionResult BirthCertificate()
        {
            NirgamCertificateVM viewModel = new NirgamCertificateVM();

            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult BirthCertificate(FormCollection frm)
        {
            NirgamCertificateVM nirgamCertificateVM = new NirgamCertificateVM();
            if (frm["RegisterId"] != null && !string.IsNullOrEmpty(frm["RegisterId"]))
            {
                nirgamCertificateVM.RegisterId = Convert.ToInt32(frm["RegisterId"]);
            }

            if (frm["ClassId"] != null && !string.IsNullOrEmpty(frm["ClassId"]))
            {
                nirgamCertificateVM.ClassId = Convert.ToInt32(frm["ClassId"]);
            }
            if (frm["DivisionId"] != null && !string.IsNullOrEmpty(frm["DivisionId"]))
            {
                nirgamCertificateVM.DivisionId = Convert.ToInt32(frm["DivisionId"]);
            }
            if (frm["StudentId"] != null && !string.IsNullOrEmpty(frm["StudentId"]))
            {
                nirgamCertificateVM.StudentId = Convert.ToInt32(frm["StudentId"]);
            }

            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 4;
                PresentationLayer.Helpers.SessionHelper.StudentId = nirgamCertificateVM.StudentId;
                return Redirect(@"~\Report.aspx");
            }
            else
            {
                ClassBAL classBAL = new ClassBAL();
                nirgamCertificateVM.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
                return View(nirgamCertificateVM);
            }
        }
        [HttpGet]
        public ActionResult LeaveReport()
        {
            EmployeeBAL employeeBAL = new EmployeeBAL();


            EmployeeLeaveApplyVM viewModel = new EmployeeLeaveApplyVM();
            viewModel.EmployeeList = from obj in employeeBAL.GetAll() select new SelectListItem() { Text = obj.FirstName.Trim() + " " + obj.MiddleName.Trim() + " " + obj.LastName.Trim(), Value = obj.EmployeeId.ToString() };
            return View(viewModel);


        }

        [HttpGet]
        public ActionResult CashBookReport()
        {
            AccountVM viewModel = new AccountVM();
            return View(viewModel);


        }

        [HttpPost]
        public ActionResult CashBookReport(FormCollection frm)
        {
            AccountVM viewModel = new AccountVM();

            if (frm["FromDate"] != null && !string.IsNullOrEmpty(frm["FromDate"]))
            {
                viewModel.FromDate = Convert.ToDateTime(frm["FromDate"]);
            }
            if (frm["ToDate"] != null && !string.IsNullOrEmpty(frm["ToDate"]))
            {
                viewModel.ToDate = Convert.ToDateTime(frm["ToDate"]);
            }

            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 7;
                PresentationLayer.Helpers.SessionHelper.fromDate = viewModel.FromDate;
                PresentationLayer.Helpers.SessionHelper.toDate = viewModel.ToDate; 
                return Redirect(@"~\Report.aspx");
            }
            else
            {
                return View(viewModel);
            }

        }
        [HttpGet]
        public ActionResult AssetReport()
        {
            AssetVM viewModel = new AssetVM();
            return View(viewModel);


        }

        [HttpPost]
        public ActionResult AssetReport(FormCollection frm)
        {
            AssetVM viewModel = new AssetVM();

            if (frm["FromDate"] != null && !string.IsNullOrEmpty(frm["FromDate"]))
            {
                viewModel.FromDate = Convert.ToDateTime(frm["FromDate"]);
            }
            if (frm["ToDate"] != null && !string.IsNullOrEmpty(frm["ToDate"]))
            {
                viewModel.ToDate = Convert.ToDateTime(frm["ToDate"]);
            }

            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 6;
                PresentationLayer.Helpers.SessionHelper.fromDate = viewModel.FromDate;
                PresentationLayer.Helpers.SessionHelper.toDate = viewModel.ToDate;
                return Redirect(@"~\Report.aspx");
            }
            else
            {
                return View(viewModel);
            }

        }

        [HttpPost]
        public ActionResult LeaveReport(FormCollection frm)
        {
            EmployeeLeaveApplyVM viewModel = new EmployeeLeaveApplyVM();
            if (frm["EmployeeId"] != null && !string.IsNullOrEmpty(frm["EmployeeId"]))
            {
                viewModel.EmployeeId = Convert.ToInt32(frm["EmployeeId"]);
            }


            if (ModelState.IsValid)
            {
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 5;
                PresentationLayer.Helpers.SessionHelper.TempId = viewModel.EmployeeId;
                return Redirect(@"~\Report.aspx");
            }
            else
            {
                EmployeeBAL employeeBAL = new EmployeeBAL();
                viewModel.EmployeeList = from obj in employeeBAL.GetAll() select new SelectListItem() { Text = obj.FirstName.Trim() + " " + obj.MiddleName.Trim() + " " + obj.LastName.Trim(), Value = obj.EmployeeId.ToString() };
                return View(viewModel);
            }

        }
        
        
    }
}
