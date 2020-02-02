using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using PresentationLayer.Controllers;
using PresentationLayer.Other;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Web.Mvc;
using PresentationLayer.Helpers;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /SysAdmin/Student/
        [GridAction]
        public ActionResult Index()
        {
            List<StudentVM> viewModels = new List<StudentVM>();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.GetAll(SessionHelper.SchoolId).OrderByDescending(s => s.StudentId);
            foreach (Entities.Student entity in entites)
            {
                viewModels.Add(CommanMethods.getStudentViewModel(entity));
            }

            return View(new GridModel<StudentVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Student/Details/5
        public ActionResult Details(int id)
        {
            StudentVM viewModel = new StudentVM();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.FindBy(a => a.StudentId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Student entity = entites.FirstOrDefault();
                viewModel = CommanMethods.getStudentViewModel(entity);
            }
            return View(viewModel);
        }

        /// <summary>
        /// Method for select
        /// </summary>
        /// <returns>return action result</returns>
        [GridAction]
        public ActionResult Select()
        {
            string mode = Request.QueryString["Grid-mode"];

            if (!string.IsNullOrEmpty(mode))
            {
                return this.RedirectToAction("Create");
            }
            else
            {
                List<StudentVM> viewModels = new List<StudentVM>();

                StudentBAL balObject = new StudentBAL();
                IQueryable<Entities.Student> entites = balObject.GetAll(SessionHelper.SchoolId).OrderByDescending(s => s.StudentId);

                foreach (Entities.Student entity in entites)
                {
                    viewModels.Add(CommanMethods.getStudentViewModel(entity));
                }

                return this.View("Index", new GridModel<StudentVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Student/Create
        public ActionResult Create()
        {
            StudentVM viewModel = new StudentVM();
        
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            //ReligionBAL religionBAL = new ReligionBAL();
            //viewModel.Religions = from obj in religionBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };

            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Student/Create
        [HttpPost]
        public ActionResult Create(StudentVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Insert(viewModel);
                    return RedirectToAction("StudentFeeTranscation", "Student", new { area = "SysAdmin" });
                }
                else
                {
                    //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                   // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };

                    return View(viewModel);
                }
            }
            catch
            {
                //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
               //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };

                return View(viewModel);
            }
        }

        private void Insert(StudentVM viewModel)
        {
            StudentBAL balObject = new StudentBAL();
            int studentId = balObject.AddStudent(getStudentEntity(viewModel), SessionHelper.SchoolId);

            Helpers.SessionHelper.StudentId = studentId;
        }

        //
        // GET: /SysAdmin/Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentVM viewModel = new StudentVM();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.FindBy(a => a.SrNo == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Student entity = entites.FirstOrDefault();
                viewModel.SrNo = entity.SrNo;
                viewModel.StudentId = entity.StudentId;
                viewModel.RegisterId = entity.RegisterId;
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
                viewModel.ClassName = entity.ClassName;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.Title = entity.Title.Trim();
                viewModel.FirstName = entity.FirstName;
                viewModel.MiddleName = entity.MiddleName;
                viewModel.LastName = entity.LastName;
                viewModel.MotherName = entity.MotherName;
                viewModel.Gender = entity.Gender;
                viewModel.Nationality = entity.Nationality.Trim();
                viewModel.ReligionCastId = entity.ReligionCastId;
                viewModel.ReligionId = entity.ReligionId;
                viewModel.CastId = entity.CastId;
                viewModel.ReligionName = entity.ReligionName.Trim();
                viewModel.CastName = entity.CastName.Trim();
                viewModel.ReserveCategory = entity.ReserveCategory;
                viewModel.DateOfBirth = entity.DateOfBirth;
                viewModel.PlaceOfBirth = entity.PlaceOfBirth.Trim();
                viewModel.AdharcardNo = entity.AdharcardNo;
                viewModel.DateOfAdmission = entity.DateOfAdmission;
                viewModel.MotherTounge = entity.MotherTounge;
                viewModel.UStudentId = entity.UStudentId;
                viewModel.LastSchoolAttended = entity.LastSchoolAttended;
                viewModel.Progress = entity.Progress;
                viewModel.Conduct = entity.Conduct;
                viewModel.DateOfLeavingSchool = entity.DateOfLeavingSchool;
                viewModel.LastSchoolClass = entity.LastSchoolClass;
                viewModel.LastSchoolTCNo = entity.LastSchoolTCNo;
                viewModel.ClassInWhichStudingAndSinceWhen = entity.ClassInWhichStudingAndSinceWhen;
                viewModel.ReasonForLeavingSchool = entity.ReasonForLeavingSchool;
                viewModel.RemarkOnTC = entity.RemarkOnTC;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModel.PrevSchoolDateofLiving = entity.PrevSchoolDateofLiving;
            }
            //if (viewModel.AcademicYearId != null && viewModel.AcademicYearId > 0)
            {
                ReligionBAL religionBAL = new ReligionBAL();
                viewModel.Religions = from obj in religionBAL.GetAll().Where(c =>  c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
            }


            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Student entity = new Entities.Student();
                    entity.SrNo = (int)viewModel.SrNo;
                    entity.StudentId = viewModel.StudentId;

                    entity.RegisterId = viewModel.RegisterId;
                    entity.ClassDivisionId = viewModel.ClassDivisionId;
                    entity.Title = viewModel.Title;
                    entity.FirstName = viewModel.FirstName;
                    entity.MiddleName = viewModel.MiddleName;
                    entity.LastName = viewModel.LastName;
                    entity.MotherName = viewModel.MotherName;
                    entity.Gender = viewModel.Gender;
                    entity.Nationality = viewModel.Nationality;
					ReligionCastBAL religionCastBAL = new ReligionCastBAL();
                    entity.ReligionCastId = religionCastBAL.FindBy(cd => cd.ReligionId == viewModel.ReligionId && cd.CastId == viewModel.CastId).FirstOrDefault().ReligionCastId;

                    entity.DateOfBirth = viewModel.DateOfBirth;
                    entity.PlaceOfBirth = viewModel.PlaceOfBirth;
                    entity.AdharcardNo = viewModel.AdharcardNo;
                    entity.DateOfAdmission = viewModel.DateOfAdmission;
                    entity.MotherTounge = viewModel.MotherTounge;
                    entity.UStudentId = viewModel.UStudentId;
                    entity.LastSchoolAttended = viewModel.LastSchoolAttended;
                    entity.Progress = viewModel.Progress;
                    entity.Conduct = viewModel.Conduct;
                    entity.DateOfLeavingSchool = viewModel.DateOfLeavingSchool;
                    entity.LastSchoolClass = viewModel.LastSchoolClass;
                    entity.LastSchoolTCNo = viewModel.LastSchoolTCNo;
                    entity.ClassInWhichStudingAndSinceWhen = viewModel.ClassInWhichStudingAndSinceWhen;
                    entity.ReasonForLeavingSchool = viewModel.ReasonForLeavingSchool;
                    entity.RemarkOnTC = viewModel.RemarkOnTC;
                    entity.Status = viewModel.Status;
                    entity.TCPrinted = viewModel.TCPrinted;
                    entity.Remark = viewModel.Remark;
                    entity.PrevSchoolDateofLiving = viewModel.PrevSchoolDateofLiving;
                    StudentBAL balObject = new StudentBAL();
                    balObject.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                   // if (viewModel.AcademicYearId != null && viewModel.AcademicYearId > 0)
                    {
                        ReligionBAL religionBAL = new ReligionBAL();
                        viewModel.Religions = from obj in religionBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                    }
                    return View(viewModel);
                }
            }
            catch
            {
               // if (viewModel.AcademicYearId != null && viewModel.AcademicYearId > 0)
                {
                    ReligionBAL religionBAL = new ReligionBAL();
                    viewModel.Religions = from obj in religionBAL.GetAll().Where(c =>  c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                }
                return View();
            }
        }


        //
        // POST: /SysAdmin/Student/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                StudentBAL balObject = new StudentBAL();
                balObject.Delete(id);

                StudentAddressBAL studentAddressBAL = new StudentAddressBAL();
                studentAddressBAL.Delete(id);

                StudentParentBAL studentParentBAL = new StudentParentBAL();
                studentParentBAL.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /// <summary>
        /// method for get classes list
        /// </summary>
        /// <returns>classes list</returns>
        public JsonResult GetClassesList()
        {
            ClassBAL classBAL = new ClassBAL();
            var classesList = from obj in classBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
            return this.Json(classesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get divisions list
        /// </summary>
        /// <returns>divisions list</returns>
        public JsonResult GetDivisionsList(int ClassId)
        {
            ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> classDivisions = classDivisionBAL.FindBy(cd => cd.ClassId == ClassId);
            var divisionIds = from classDivisionObj in classDivisions select classDivisionObj.DivisionId;
            DivisionBAL divisionsBAL = new DivisionBAL();
            var divisions = divisionsBAL.GetAll().Where(d => d.Status == true);
            var divisionesList = from obj in divisions from obj1 in divisionIds where obj.DivisionId == (obj1) select new SelectListItem() { Text = obj.DivisionName, Value = obj.DivisionId.ToString() };
            return this.Json(divisionesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get religions list
        /// </summary>
        /// <returns>religions list</returns>
        public JsonResult GetReligionsList()
        {
            ReligionBAL religionBAL = new ReligionBAL();
            var religionsList = from obj in religionBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
            return this.Json(religionsList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get casts list
        /// </summary>
        /// <returns>casts list</returns>
        public JsonResult GetCastsList(int ReligionId)
        {
            ReligionCastBAL religionCastBAL = new ReligionCastBAL();
            IQueryable<Entities.ReligionCast> religionCasts = religionCastBAL.FindBy(cd => cd.ReligionId == ReligionId);
            var castIds = from religionCastObj in religionCasts select religionCastObj.CastId;
            CastBAL castBAL = new CastBAL();
            var casts = castBAL.GetAll().Where(d => d.Status == true);
            var castsList = from obj in casts from obj1 in castIds where obj.CastId == (obj1) select new SelectListItem() { Text = obj.CastName, Value = obj.CastId.ToString() };
            return this.Json(castsList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get Reserve Category
        /// </summary>
        /// <param name="ReligionId">Religion Id</param>
        /// <param name="CastId">Cast Id</param>
        /// <returns>return string</returns>
        public JsonResult GetReserveCategory(int ReligionId, int CastId)
        {
            return this.Json(PresentationLayer.Other.CommanMethods.GetReserveCategory(ReligionId, CastId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult StudentFeeTranscation()
        {
            //Helpers.SessionHelper.StudentId = 1;
            StudentFeeTranscationVM studentFeeTranscationVM = new StudentFeeTranscationVM();
            studentFeeTranscationVM.StudentId = (int)Helpers.SessionHelper.StudentId;
            StudentBAL studentObject = new StudentBAL();
            IQueryable<Entities.Student> students = studentObject.FindBy(s => s.StudentId == studentFeeTranscationVM.StudentId);
            if (students != null)
            {
                Entities.Student student = students.FirstOrDefault();
                studentFeeTranscationVM.StudentFullNameWithTitle = string.Concat(student.Title.Trim(), " ", student.FirstName, " ", student.MiddleName, " ", student.LastName).Trim();
            }
            
            return View(studentFeeTranscationVM);
        }

        public ActionResult GetStudentTransactionSub()
        {
            int studentId = (int)Helpers.SessionHelper.StudentId;
            List<StudentFeeTranscationSubVM> studentFeeTranscationSubVMs = new List<StudentFeeTranscationSubVM>();
            StudentLedgerBAL studentLedgerBAL = new StudentLedgerBAL();
            IQueryable<Entities.StudentLedger> studentLedgers = studentLedgerBAL.GetAll().Where(a => a.StudentId == studentId).OrderBy(a => a.StudentLedgerId);
            if (studentLedgers != null && studentLedgers.Count() > 0)
            {
                IEnumerable<IGrouping<int, Entities.StudentLedger>> studentLedgersGroupByFeeId = studentLedgers.GroupBy(a => a.FeeHeadId);
                if (studentLedgersGroupByFeeId != null && studentLedgersGroupByFeeId.Count() > 0)
                {
                    foreach (IGrouping<int, Entities.StudentLedger> studentLedgersGroupItem in studentLedgersGroupByFeeId)
                    {
                        Entities.StudentLedger studentLedger = studentLedgersGroupItem.LastOrDefault();
                        StudentFeeTranscationSubVM studentTransactionSubVM = new StudentFeeTranscationSubVM();
                        studentTransactionSubVM.StudentTransactionSubId = 0;
                        studentTransactionSubVM.FeeHeadId = studentLedgersGroupItem.Key;
                        FeeHeadBAL feeHeadBAL = new FeeHeadBAL();
                        studentTransactionSubVM.FeeHeadName = feeHeadBAL.FindBy(f => f.FeeHeadId == studentLedgersGroupItem.Key).FirstOrDefault().FeeHeadName;
                        studentTransactionSubVM.Cr = 0;
                        studentTransactionSubVM.Dr = 0;
                        studentTransactionSubVM.Balance = studentLedger.HeadBalance;
                        FeeHeadBAL balObject = new FeeHeadBAL();
                        studentTransactionSubVM.Fees = from obj in balObject.GetAll() select new SelectListItem() { Text = obj.FeeHeadName, Value = obj.FeeHeadId.ToString() };
                        studentFeeTranscationSubVMs.Add(studentTransactionSubVM);
                    }
                }
            }
            return PartialView("_GetStudentFeeTranscationSub", studentFeeTranscationSubVMs);
        }

        [HttpPost]
        public ActionResult StudentFeeTranscation(StudentFeeTranscationVM studentFeeTranscationVM, FormCollection formCollection)
        {
            StudentBAL studentObject = new StudentBAL();
            IQueryable<Entities.Student> students = studentObject.FindBy(s => s.StudentId == studentFeeTranscationVM.StudentId);
            if (students != null)
            {
                Entities.Student student = students.FirstOrDefault();
                studentFeeTranscationVM.ClassDivisionId = student.ClassDivisionId;
            }


            TryUpdateModel<StudentFeeTranscationVM>(studentFeeTranscationVM);

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                Entities.StudentTransaction entity = new Entities.StudentTransaction();
                entity.ReceiptNo = studentFeeTranscationVM.ReceiptNo;
                entity.StudentTransactionId = studentFeeTranscationVM.StudentTransactionId;
                entity.ClassDivisionId = studentFeeTranscationVM.ClassDivisionId;
                entity.StudentId = studentFeeTranscationVM.StudentId;
                entity.TransactionDate = studentFeeTranscationVM.TransactionDate;
                entity.Status = true;
                entity.Remark = studentFeeTranscationVM.Remark;
                entity.ReceiptTotal = studentFeeTranscationVM.ReceiptTotal;
                entity.BankName = studentFeeTranscationVM.BankName;
                entity.ChequeNumber = studentFeeTranscationVM.ChequeNumber;
                entity.StudentTransactionSubList = new List<Entities.StudentTransactionSub>();

                string itemFeeId = Convert.ToString(formCollection["item.FeeHeadId"]);
                string itemCr = Convert.ToString(formCollection["item.Cr"]);
                string itemDr = Convert.ToString(formCollection["item.Dr"]);

                if (!string.IsNullOrEmpty(itemFeeId))
                {
                    string[] strArrFeeId = itemFeeId.Split(',');
                    string[] strArrCr = itemCr.Split(',');
                    string[] strArrDr = itemDr.Split(',');
                    if (itemFeeId.Length > 0)
                    {
                        for (int i = 0; i < strArrFeeId.Length; i++)
                        {
                            Entities.StudentTransactionSub entitySub = new Entities.StudentTransactionSub();
                            entitySub.FeeHeadId = Convert.ToInt32(strArrFeeId[i]);
                            entitySub.Cr = Convert.ToDecimal(strArrCr[i]);
                            entitySub.Dr = Convert.ToDecimal(strArrDr[i]);
                            entity.StudentTransactionSubList.Add(entitySub);
                        }
                    }
                }

                StudentTransactionBAL balObject = new StudentTransactionBAL();
                balObject.Add(entity,SessionHelper.SchoolId );
                PresentationLayer.Helpers.SessionHelper.ReportIndex = 2;
                return Redirect(@"~\Report.aspx");
                //return RedirectToAction("Index", "Student", new { area = "SysAdmin" });
            }

            return View();
        }

        [HttpGet]
        public ActionResult StudentSearch()
        {
            StudentVM viewModel = new StudentVM();
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            viewModel.Status = true;
            return View(viewModel);
        }

        public ActionResult _GetStudentDetails(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId, bool Status)
        {
            List<StudentVM> viewModels = new List<StudentVM>();
            List<Entities.Student> studentEntites = new List<Entities.Student>();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.SearchStudents(RegisterId, ClassId, DivisionId, ReligionId, CastId, Status, SessionHelper.SchoolId);
            foreach (Entities.Student entity in entites)
            {
                viewModels.Add(CommanMethods.getStudentViewModel(entity));
            }
            return PartialView("_GetStudentDetails", new GridModel<StudentVM> { Data = viewModels });
        }

        [HttpPost]
        public ActionResult ExportStudentDetails(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId, bool Status)
        {
            //List<StudentVM> viewModels = new List<StudentVM>();
            DataTable dataTableExport = new DataTable();
            dataTableExport.Columns.Add(new DataColumn("S.No", typeof(System.Int64)));
            dataTableExport.Columns.Add(new DataColumn("Student Name", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Mother's Name", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Class", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Division", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("GR No.", typeof(System.Int64)));
            dataTableExport.Columns.Add(new DataColumn("Birth Date", typeof(System.DateTime)));
            dataTableExport.Columns.Add(new DataColumn("birthplace", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("date of admition", typeof(System.DateTime)));
            dataTableExport.Columns.Add(new DataColumn("admition class", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Religion", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Subcast", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Category", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Aadhar card no", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("parents contact no", typeof(System.Int64)));
            dataTableExport.Columns.Add(new DataColumn("Address", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Bank Name", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Bank account holder name", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Account No", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Branch", typeof(System.String)));
            dataTableExport.Columns.Add(new DataColumn("Ifsc Code", typeof(System.String)));

            List<Entities.Student> studentEntites = new List<Entities.Student>();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.SearchStudents(RegisterId, ClassId, DivisionId, ReligionId, CastId, Status, SessionHelper.SchoolId);
            StudentParentBAL balStudentParent = new StudentParentBAL();
            IQueryable<Entities.StudentParent> studentParentEntities = balStudentParent.GetAll();
            long srNo = new long();
            foreach (Entities.Student entity in entites)
            {
                srNo += 1;
                DataRow dr = dataTableExport.NewRow();
                dr["S.No"] = srNo;
                string name = string.Empty;
                if (!string.IsNullOrEmpty(entity.FirstName))
                {
                    name = name + entity.FirstName;
                }
                if (!string.IsNullOrEmpty(entity.MiddleName))
                {
                    name = name + " " + entity.MiddleName;
                }
                if (!string.IsNullOrEmpty(entity.LastName))
                {
                    name = name + " " + entity.LastName;
                }
                dr["Student Name"] = name;
                dr["Mother's Name"] = entity.MotherName;
                dr["Class"] = entity.ClassName;
                dr["Division"] = entity.DivisionName;
                dr["GR No."] = entity.RegisterId;
                dr["Birth Date"] = entity.DateOfBirth;
                dr["birthplace"] = entity.PlaceOfBirth;
                if (entity.DateOfAdmission != null)
                {
                    dr["date of admition"] = entity.DateOfAdmission;
                }
                if (entity.LastSchoolAttended != null)
                {
                    dr["admition class"] = entity.LastSchoolAttended;
                }
                dr["Religion"] = entity.ReligionName;
                dr["Subcast"] = entity.ClassName;
                dr["Category"] = entity.ReserveCategory;
                dr["Aadhar card no"] = entity.AdharcardNo;

                IQueryable<Entities.StudentParent> studentParentEntityList = studentParentEntities.Where(sp => sp.StudentId == entity.StudentId);
                if (studentParentEntityList != null && studentParentEntityList.Count() > 0)
                {
                    Entities.StudentParent studentParentEntity = studentParentEntityList.FirstOrDefault();
                    dr["parents contact no"] = studentParentEntity.MobileNumber;
                    dr["Address"] = studentParentEntity.Address;
                    dr["Bank Name"] = studentParentEntity.BankName;
                    name = string.Empty;
                    if (!string.IsNullOrEmpty(studentParentEntity.FirstName))
                    {
                        name = name + studentParentEntity.FirstName;
                    }
                    if (!string.IsNullOrEmpty(studentParentEntity.MiddleName))
                    {
                        name = name + " " + studentParentEntity.MiddleName;
                    }
                    if (!string.IsNullOrEmpty(studentParentEntity.LastName))
                    {
                        name = name + " " + studentParentEntity.LastName;
                    }
                    dr["Bank account holder name"] = name;
                    dr["Account No"] = studentParentEntity.AccountNo;
                    dr["Branch"] = studentParentEntity.Branch;
                    dr["Ifsc Code"] = studentParentEntity.IFSCCode;
                }
                dataTableExport.Rows.Add(dr);
            }
            if (dataTableExport != null && dataTableExport.Rows.Count > 0)
            {
                bool blnError = false;

                try
                {
                    // Declare Instance
                    Microsoft.Office.Interop.Excel.Application excelApp;
                    Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                    Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                    Microsoft.Office.Interop.Excel.Range excelCellrange;

                    // Start Excel and get Application object.
                    excelApp = new Microsoft.Office.Interop.Excel.Application();

                    // for making Excel visible
                    excelApp.Visible = false;
                    excelApp.DisplayAlerts = false;

                    // Creation a new Workbook
                    excelworkBook = excelApp.Workbooks.Add(Type.Missing);

                    // Workk sheet
                    excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                    excelSheet.Name = "StudentsExportData";

                    // column headings
                    for (int i = 0; i < dataTableExport.Columns.Count; i++)
                    {
                        excelSheet.Cells[1, (i + 1)] = dataTableExport.Columns[i].ColumnName;
                    }

                    // rows
                    for (int i = 0; i < dataTableExport.Rows.Count; i++)
                    {
                        // to do: format datetime values before printing
                        for (int j = 0; j < dataTableExport.Columns.Count; j++)
                        {
                            excelSheet.Cells[(i + 2), (j + 1)] = dataTableExport.Rows[i][j];
                        }
                    }

                    // check fielpath
                    excelApp.Visible = false;
                    excelApp.UserControl = false;
                    excelworkBook.SaveAs("d:\\StudentsExportData.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    excelworkBook.Close();
                    excelApp.Quit();
                }
                catch (Exception)
                {
                    blnError = true;
                    //throw;
                }
                if (!blnError)
                {
                    ViewBag.Error = "Successfully uploaded file !!!";
                }
                else
                {
                    ViewBag.Error = "Some thing is wrong, Please contact to admin.";
                }
            }

            return RedirectToAction("StudentSearch");
        }

        [HttpGet]
        public ActionResult StudentLedger()
        {
            StudentVM viewModel = new StudentVM();

            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            viewModel.Status = true;
            return View(viewModel);
        }

        public ActionResult _GetStudentLedger(int RegisterId, int ClassId, int DivisionId, int ReligionId, int CastId)
        {
            StudentLedgerBAL studentLedgerBAL = new StudentLedgerBAL();
            IQueryable<Entities.StudentLedger> entities = studentLedgerBAL.getStudentLedger(RegisterId, ClassId, DivisionId, ReligionId, CastId, SessionHelper.SchoolId);

            List<StudentLedgerVM> studentLedgerVMs = new List<StudentLedgerVM>();
            foreach (Entities.StudentLedger entity in entities)
            {
                StudentLedgerVM studentLedgerVM = new StudentLedgerVM();
                studentLedgerVM.StudentLedgerId = entity.StudentLedgerId;
                studentLedgerVM.StudentId = entity.StudentId;
                studentLedgerVM.StudentFullNameWithTitle = entity.StudentFullNameWithTitle;
                studentLedgerVM.TransactionDate = entity.TransactionDate;
                studentLedgerVM.FeeHeadId = entity.FeeHeadId;
                studentLedgerVM.FeeHeadName = entity.FeeHeadName;
                studentLedgerVM.Cr = entity.Cr;
                studentLedgerVM.Dr = entity.Dr;
                studentLedgerVM.HeadBalance = entity.HeadBalance;
                studentLedgerVM.Balance = entity.Balance;
                studentLedgerVM.ReceiptNo = entity.ReceiptNo;
                studentLedgerVM.BankName = entity.BankName;
                studentLedgerVM.ChequeNumber = entity.ChequeNumber;
                studentLedgerVM.Status = entity.Status;
                studentLedgerVM.Remark = entity.Remark;
                studentLedgerVMs.Add(studentLedgerVM);
            }

            return PartialView("_GetStudentLedger", new GridModel<StudentLedgerVM> { Data = studentLedgerVMs });
        }

        public ActionResult StudentLedgerGrid(StudentLedgerSearchVM viewModel)
        {
            if (!ModelState.IsValid)
            {
               // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
               // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                return View(viewModel);
            }

            StudentLedgerBAL studentLedgerBAL = new StudentLedgerBAL();
            IQueryable<Entities.StudentLedger> entities = studentLedgerBAL.getStudentLedger( 0, 0, 0, 0, 0, SessionHelper.SchoolId);
            List<StudentLedgerVM> studentLedgerVMs = new List<StudentLedgerVM>();
            foreach (Entities.StudentLedger entity in entities)
            {
                StudentLedgerVM studentLedgerVM = new StudentLedgerVM();
                studentLedgerVM.StudentLedgerId = entity.StudentLedgerId;
                studentLedgerVM.StudentId = entity.StudentId;
                studentLedgerVM.StudentFullNameWithTitle = entity.StudentFullNameWithTitle;
                studentLedgerVM.TransactionDate = entity.TransactionDate;
                studentLedgerVM.FeeHeadId = entity.FeeHeadId;
                studentLedgerVM.FeeHeadName = entity.FeeHeadName;
                studentLedgerVM.Cr = entity.Cr;
                studentLedgerVM.Dr = entity.Dr;
                studentLedgerVM.HeadBalance = entity.HeadBalance;
                studentLedgerVM.Balance = entity.Balance;
                studentLedgerVM.ReceiptNo = entity.ReceiptNo;
                studentLedgerVM.BankName = entity.BankName;
                studentLedgerVM.ChequeNumber = entity.ChequeNumber;
                studentLedgerVM.Status = entity.Status;
                studentLedgerVM.Remark = entity.Remark;
                studentLedgerVMs.Add(studentLedgerVM);
            }

            return View("StudentLedgerGrid", new GridModel<StudentLedgerVM> { Data = studentLedgerVMs });
        }

        private Entities.Student getStudentEntity(StudentVM viewModel)
        {
            Entities.Student entity = new Entities.Student();
            entity.StudentId = viewModel.StudentId;
            entity.RegisterId = viewModel.RegisterId;
            //entity.ClassDivisionId = viewModel.ClassDivisionId;
            ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
            entity.ClassDivisionId = classDivisionBAL.FindBy(cd => cd.ClassId == viewModel.ClassId && cd.DivisionId == viewModel.DivisionId).FirstOrDefault().ClassDivisionId;
            entity.Title = viewModel.Title;
            entity.FirstName = viewModel.FirstName;
            entity.MiddleName = viewModel.MiddleName;
            entity.LastName = viewModel.LastName;
            entity.MotherName = viewModel.MotherName;
            entity.Gender = viewModel.Gender;
            entity.Nationality = viewModel.Nationality;
            ReligionCastBAL religionCastBAL = new ReligionCastBAL();
            entity.ReligionCastId = religionCastBAL.FindBy(cd => cd.ReligionId == viewModel.ReligionId && cd.CastId == viewModel.CastId).FirstOrDefault().ReligionCastId;
            entity.DateOfBirth = viewModel.DateOfBirth;
            entity.PlaceOfBirth = viewModel.PlaceOfBirth;
            entity.AdharcardNo = viewModel.AdharcardNo;
            entity.DateOfAdmission = viewModel.DateOfAdmission;
            entity.MotherTounge = viewModel.MotherTounge;
            entity.UStudentId = viewModel.UStudentId;
            entity.LastSchoolAttended = viewModel.LastSchoolAttended;
            entity.Progress = viewModel.Progress;
            entity.Conduct = viewModel.Conduct;
            entity.DateOfLeavingSchool = viewModel.DateOfLeavingSchool;
            entity.LastSchoolClass = viewModel.LastSchoolClass;
            entity.LastSchoolTCNo = viewModel.LastSchoolTCNo;
            entity.ClassInWhichStudingAndSinceWhen = viewModel.ClassInWhichStudingAndSinceWhen;
            entity.ReasonForLeavingSchool = viewModel.ReasonForLeavingSchool;
            entity.RemarkOnTC = viewModel.RemarkOnTC;
            entity.Status = true;
            entity.Remark = viewModel.Remark;
            entity.PrevSchoolDateofLiving = viewModel.PrevSchoolDateofLiving;

            return entity;
        }

        public JsonResult GetStudentDetailsByRegisterId(int RegisterId)
        {
            return this.Json(CommanMethods.GetStudentDetailsByRegisterId(RegisterId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentImport()
        {
   

            return View();
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            //string path = @"D:\UploadTemplate";
            string fullPath = Path.Combine(Server.MapPath("~/UploadTemplate"), file);

            byte[] filedata = System.IO.File.ReadAllBytes(fullPath);
            string contentType = MimeMapping.GetMimeMapping(fullPath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = file,
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        [HttpPost]
        public ActionResult Importexcel()
        {
            if (Request.Files["FileUpload"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName).ToLower();
                string connString = "";
                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
                //string[] validFileTypes = { ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads"), Request.Files["FileUpload"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["FileUpload"].SaveAs(path1);
                    DataTable dt = new DataTable();
                    if (extension == ".csv")
                    {
                        dt = ConvertCSVtoDataTable(path1);
                        //ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook  
                    else if (extension.Trim() == ".xls")
                    {
                       connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", path1);
                        dt = ConvertXSLXtoDataTable(path1, connString);
                        //ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                        //connString = string.Format( ConfigurationManager.AppSettings["oldbConnection"].ToString()+"",path1);
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                        dt = ConvertXSLXtoDataTable(path1, connString);

                        //ViewBag.Data = dt;
                    }


                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<Entities.Student> studentEntities = new List<Entities.Student>();
                        List<Entities.StudentParent> studentParentEnties = new List<Entities.StudentParent>();

                        StreamWriter errorLog;
                        if (!System.IO.File.Exists("d:\\UploadErrorLog.txt"))
                        {
                            errorLog = new StreamWriter("d:\\UploadErrorLog.txt");
                        }
                        else
                        {
                            errorLog = System.IO.File.AppendText("d:\\UploadErrorLog.txt");
                            errorLog.WriteLine("---------------------------------------------------------------" + DateTime.Now + "---------------------------------------------------------------");
                        }
                        //errorLog.WriteLine("Date Time " + DateTime.Now);

                        int intExcelRow = 1; // 1st row for fields
                        //int intExcelErrorRow = 1;
                        bool blnError = false;
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                intExcelRow += 1;

                                Entities.Student studentEntity = new Entities.Student();
                                Entities.StudentParent studentParentEntity = new Entities.StudentParent();

                                studentEntity.StudentId = 0;

                                studentEntity.RegisterId = Convert.ToInt32(row["GRNo"]);
                                studentEntity.ClassName = Convert.ToString(row["Class"]);
                                studentEntity.DivisionName = Convert.ToString(row["Division"]);
                                //studentEntity.ClassId = Convert.ToInt32(row["ClassId"]);
                                //studentEntity.DivisionId = Convert.ToInt32(row["DivisionId"]);
                                studentEntity.ClassDivisionId = CommanMethods.GetClassDivisionId(studentEntity.ClassName, studentEntity.DivisionName);

                                if (studentEntity.ClassDivisionId == 0)
                                {
                                    blnError = true;
                                    errorLog.WriteLine("Row No.: " + intExcelRow.ToString() + " Invalid Class<->Division relation for Class:" + studentEntity.ClassName + " Division:" + studentEntity.DivisionName + " Please add in Master->ClassDivision...");
                                    continue;
                                }
                                //studentEntity.Title = Convert.ToString(row["Title"]).Trim();
                                studentEntity.FirstName = Convert.ToString(row["FirstName"]).Trim();
                                if (row["MiddleName"] != null)
                                {
                                    studentEntity.MiddleName = Convert.ToString(row["MiddleName"]);
                                }
                                studentEntity.LastName = Convert.ToString(row["LastName"]).Trim();
                                studentEntity.MotherName = Convert.ToString(row["MotherName"]).Trim();
                                studentEntity.Gender = Convert.ToString(row["Gender"]).Trim();
                                studentEntity.Nationality = Convert.ToString(row["Nationality"]).Trim();

                                studentEntity.ReligionName = Convert.ToString(row["Religion"]).Trim();
                                studentEntity.CastName = Convert.ToString(row["CastName"]).Trim();
                                //studentEntity.ReligionId = 
                                //studentEntity.CastId = Convert.ToInt32(row["CastId"]);
                                studentEntity.ReligionCastId = CommanMethods.GetReligionCastId(studentEntity.ReligionName, studentEntity.CastName);
                                if (studentEntity.ReligionCastId == 0)
                                {
                                    blnError = true;
                                    errorLog.WriteLine("Row No.: " + intExcelRow.ToString() + " Invalid Caste<->Religion relation for Religion:" + studentEntity.ReligionName + " Caste:" + studentEntity.CastName + " Please add in Master->ReligionCaste...");
                                    continue;
                                }
                                //studentEntity.ReserveCategory = Convert.ToString(row["ReserveCategory"]).Trim();
                                studentEntity.DateOfBirth = Convert.ToDateTime(row["BirthDate"]);
                                studentEntity.PlaceOfBirth = Convert.ToString(row["BirthPlace"]).Trim();
                                studentEntity.AdharcardNo = Convert.ToString(row["AadharCardNo"]).Trim();
                                studentEntity.DateOfAdmission = Convert.ToDateTime(row["DateOfAdmission"]);
                                if (row["AdmissionClass"] != null)
                                {
                                    studentEntity.LastSchoolAttended = Convert.ToString(row["AdmissionClass"]);
                                }
                                studentEntity.UStudentId = Convert.ToString(row["StudentId"]).Trim();

                                studentEntity.Status = true;
                                studentEntity.MotherTounge = "";
                               
                                studentParentEntity.FirstName = Convert.ToString(row["ParentFirstName"]).Trim();
                                if (row["ParentMiddleName"] != null)
                                {
                                    studentParentEntity.MiddleName = Convert.ToString(row["ParentMiddleName"]);
                                }
                                studentParentEntity.LastName = Convert.ToString(row["ParentLastName"]).Trim();
                                studentParentEntity.Gender = Convert.ToString(row["ParentGender"]).Trim();
                                studentParentEntity.CountryId = Convert.ToInt32(row["CountryId"]);
                                studentParentEntity.StateId = Convert.ToInt32(row["StateId"]);
                                studentParentEntity.DistrictId = Convert.ToInt32(row["DistrictId"]);
                                studentParentEntity.CityId = Convert.ToInt32(row["CityId"]);
                                if (row["ParentContactNo"] != null)
                                {
                                    studentParentEntity.MobileNumber = Convert.ToInt64(row["ParentContactNo"]);
                                }
                                studentParentEntity.Address = Convert.ToString(row["Address"]);
                                if (row["BankName"] != null)
                                {
                                    studentParentEntity.BankName = Convert.ToString(row["BankName"]);
                                }
                                if (row["AccountNo"] != null)
                                {
                                    studentParentEntity.AccountNo = Convert.ToString(row["AccountNo"]);
                                }
                                if (row["Branch"] != null)
                                {
                                    studentParentEntity.Branch = Convert.ToString(row["Branch"]);
                                }
                                if (row["IfscCode"] != null)
                                {
                                    studentParentEntity.IFSCCode = Convert.ToString(row["IfscCode"]);
                                }
                                studentParentEntity.Status = true;



                                StudentBAL balObject = new StudentBAL();
                                int studentId = balObject.AddStudent(studentEntity, SessionHelper.SchoolId);

                                studentParentEntity.StudentId = studentId;
                                StudentParentBAL studentParentBALObject = new StudentParentBAL();
                                IQueryable<Entities.StudentParent> StudentParentEntityes = studentParentBALObject.FindBy(s => s.StudentId == studentId);
                                if (StudentParentEntityes != null && StudentParentEntityes.Count() > 0)
                                {
                                    studentParentBALObject.Edit(studentParentEntity);
                                }
                                else
                                {
                                    studentParentBALObject.Add(studentParentEntity);
                                }

                                studentEntities.Add(studentEntity);
                                studentParentEnties.Add(studentParentEntity);
                            }
                            catch (Exception ex)
                            {
                                if (!blnError)
                                {
                                    blnError = true;
                                }
                                errorLog.WriteLine("Row No: " + intExcelRow.ToString() + " Message: " + ex.Message);

                            }
                        }

                        //// check fielpath
                        errorLog.Close();

                        if (!blnError)
                        {
                            TempData["Error"] = "Successfully uploaded file !!!";
                        }
                        else
                        {
                            TempData["Error"] = "Some records uploaded and Some error found, Please check error file. Error file name :- d:\\UploadErrorLog.txt";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Something is wrong.";
                    }
                }
                else
                {
                    TempData["Error"] = "Please upload file in .csv format only.";
                }
            }

            return RedirectToAction("StudentImport");
        }



        public ActionResult StudentExport()
        {
            //ExportVM viewm = new ExportVM();
            StudentVM viewModel = new StudentVM();
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            viewModel.Status = true;

            return View(viewModel);

        }


        [HttpPost]
        public ActionResult Export2Excel()
        {

            ExportVM evm = new ExportVM();
            List<ExportVM> viewModels = new List<ExportVM>();
            ExportBAL balObject = new ExportBAL();
            IQueryable<Entities.Export> entites = balObject.getExportRecords( SessionHelper.SchoolId);

            var gv = new GridView();
            gv.DataSource = entites;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StudentExportList.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();



            TempData["Message"] = "File Exported Successfully!!";

            return RedirectToAction("StudentExport");


        }
        private static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return dt;
        }

        private static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                oleda.Fill(ds);

                dt = ds.Tables[0];

            }
            catch
            {
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }

        public ActionResult StudentAttendance()
        {
            StudentAttendanceVM viewModel = new StudentAttendanceVM();
            
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            viewModel.DateOfAttendance = DateTime.Today;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentAttendance(FormCollection formCollection, int ClassId, int DivisionId, string dateAttendance, String[] students, String[] days, String[] remarks) //string entities )
        {
            if (students.Length > 0)
            {
                List<Entities.StudentAttendance> entities = new List<Entities.StudentAttendance>();
                Entities.StudentAttendance entity;
                DateTime dt = Convert.ToDateTime(dateAttendance);

                for (int i = 0; i < students.Length; i++)
                {
                    entity = new Entities.StudentAttendance();
                    entity.ClassId = ClassId;
                    entity.DivisionId = DivisionId;
                    entity.DateOfAttendance = dt;
                    int id = 0;
                    if (students[i] != null)
                    {
                        int.TryParse(students[i], out id);
                    }

                    entity.StudentId = id;

                    decimal attendancedays = 0;
                    if (days[i] != null)
                    {
                        decimal.TryParse(days[i], out attendancedays);
                    }
                    entity.AttendanceInDays = attendancedays;

                    string absentremark = string.Empty;
                    if (remarks[i] != null)
                    {
                        absentremark = Convert.ToString(remarks[i]);
                    }
                    entity.AbsentRemark = absentremark;

                    entities.Add(entity);
                }

                StudentAttendanceBAL studentAttendaceBAL = new StudentAttendanceBAL();
                studentAttendaceBAL.AddStudentAttendance(entities);

                StudentAttendanceVM viewModel = new StudentAttendanceVM();

                ClassBAL classBAL = new ClassBAL();
                viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
                TempData["Error"] = "Successfully Submitted.";
                return View(viewModel);
            }
            else
            {
                StudentAttendanceVM viewModel = new StudentAttendanceVM();
                ClassBAL classBAL = new ClassBAL();
                viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
                
                TempData["Error"] = "Something wrong, Records not valid.";
                return View(viewModel);
            }
        }

        public ActionResult _GetStudentAttendance( int ClassId, int DivisionId, DateTime DateOfAttendance)
        {
            List<StudentAttendanceVM> viewModels = new List<StudentAttendanceVM>();
            List<Entities.StudentAttendance> studentEntites = new List<Entities.StudentAttendance>();
            StudentAttendanceBAL balObject = new StudentAttendanceBAL();
            IQueryable<Entities.StudentAttendance> entites = balObject.GetAllStudentAttendance( ClassId, DivisionId, DateOfAttendance);
            foreach (Entities.StudentAttendance entity in entites)
            {
                StudentAttendanceVM viewModel = new StudentAttendanceVM();
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
                viewModel.StudentId = entity.StudentId;
                viewModel.RegisterId = entity.RegisterId;
                viewModel.StudentFullNameWithTitle = entity.StudentFullNameWithTitle;
                viewModel.DateOfAttendance = entity.DateOfAttendance;
                viewModel.AttendanceInDays = entity.AttendanceInDays;
                viewModel.AbsentRemark = entity.AbsentRemark;

                viewModels.Add(viewModel);
            }
            return PartialView("_GetStudentAttendance", viewModels);

        }

        public ActionResult GetStudentAttendance(int ClassId, int DivisionId)
        {
            List<StudentVM> viewModels = new List<StudentVM>();
            List<Entities.Student> studentEntites = new List<Entities.Student>();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.SearchStudents(0, ClassId, DivisionId, 0, 0, true, SessionHelper.SchoolId);
            foreach (Entities.Student entity in entites)
            {
                //viewModels.Add(CommanMethods.getStudentViewModel(entity));
                StudentAttendanceVM viewModel = new Models.StudentAttendanceVM();

                viewModel.RegisterId = entity.RegisterId;
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
                viewModel.DateOfAttendance = DateTime.Today;
            }
            int totalRecords = viewModels.Count();

            var jsonData = new
            {

                records = totalRecords,
                rows = viewModels
            };
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }
    }
}