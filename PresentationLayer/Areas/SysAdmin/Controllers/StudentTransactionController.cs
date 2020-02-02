using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using PresentationLayer.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using PresentationLayer.Helpers;


namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class StudentTransactionController : Controller
    {
        //
        // GET: /SysAdmin/StudentTransaction/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<StudentTransactionVM> viewModels = new List<StudentTransactionVM>();
            StudentTransactionBAL balObject = new StudentTransactionBAL();
            IQueryable<Entities.StudentTransaction> entites = balObject.GetAll(SessionHelper.SchoolId);

            foreach (Entities.StudentTransaction entity in entites)
            {
                StudentTransactionVM viewModel = new StudentTransactionVM();
                viewModel.StudentTransactionId = entity.StudentTransactionId;

                viewModel.ClassDivisionId = entity.ClassDivisionId;

                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
                viewModel.ClassName = entity.ClassName;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.StudentId = entity.StudentId;

                viewModel.StudentFullNameWithTitle = entity.StudentFullNameWithTitle;
                viewModel.TransactionDate = entity.TransactionDate;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModel.ReceiptNo = entity.ReceiptNo;
                viewModel.ReceiptTotal = entity.ReceiptTotal;
                viewModel.BankName = entity.BankName;
                viewModel.ChequeNumber = entity.ChequeNumber;
                viewModels.Add(viewModel);
            }

            return View(new GridModel<StudentTransactionVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Division/Details/5
        public ActionResult Details(int id)
        {
            StudentTransactionVM viewModel = new StudentTransactionVM();

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
                List<StudentTransactionVM> viewModels = new List<StudentTransactionVM>();
                StudentTransactionBAL balObject = new StudentTransactionBAL();
                IQueryable<Entities.StudentTransaction> entites = balObject.GetAll(SessionHelper.SchoolId);

                foreach (Entities.StudentTransaction entity in entites)
                {
                    StudentTransactionVM viewModel = new StudentTransactionVM();
                    viewModel.StudentTransactionId = entity.StudentTransactionId;

                    viewModel.ClassDivisionId = entity.ClassDivisionId;

                    viewModel.ClassId = entity.ClassId;
                    viewModel.DivisionId = entity.DivisionId;
                    viewModel.ClassName = entity.ClassName;
                    viewModel.DivisionName = entity.DivisionName;
                    viewModel.StudentId = entity.StudentId;

                    viewModel.StudentFullNameWithTitle = entity.StudentFullNameWithTitle;
                    viewModel.TransactionDate = entity.TransactionDate;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModel.ReceiptNo = entity.ReceiptNo;
                    viewModel.ReceiptTotal = entity.ReceiptTotal;
                    viewModel.BankName = entity.BankName;
                    viewModel.ChequeNumber = entity.ChequeNumber;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<StudentTransactionVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Division/Create
        public ActionResult Create()
        {
            StudentTransactionVM viewModel = new StudentTransactionVM();
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
          

            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Division/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            StudentTransactionVM viewModel = new StudentTransactionVM();
            try
            {                
                if (formCollection["ReceiptNo"] != null)
                {
                    viewModel.ReceiptNo = Convert.ToInt32(formCollection["ReceiptNo"]);
                }
                if (formCollection["StudentTransactionId"] != null && formCollection["StudentTransactionId"] != "")
                {
                    viewModel.StudentTransactionId = Convert.ToInt32(formCollection["StudentTransactionId"]);
                }

                if (formCollection["ClassId"] != null && formCollection["ClassId"] != "")
                {
                    viewModel.ClassId = Convert.ToInt32(formCollection["ClassId"]);
                }
                if (formCollection["DivisionId"] != null && formCollection["DivisionId"] != "")
                {
                    viewModel.DivisionId = Convert.ToInt32(formCollection["DivisionId"]);
                }
                if (formCollection["StudentId"] != null && formCollection["StudentId"] != "")
                {
                    viewModel.StudentId = Convert.ToInt32(formCollection["StudentId"]);
                }
                if (formCollection["TransactionDate"] != null)
                {
                    viewModel.TransactionDate = Convert.ToDateTime(formCollection["TransactionDate"]);
                }
                viewModel.Remark = Convert.ToString(formCollection["Remark"]);
                if (formCollection["ReceiptTotal"] != null && formCollection["ReceiptTotal"]!= "")
                {
                    viewModel.ReceiptTotal = Convert.ToDecimal(formCollection["ReceiptTotal"]);
                }
                viewModel.BankName = Convert.ToString(formCollection["BankName"]);
                viewModel.ChequeNumber = Convert.ToString(formCollection["ChequeNumber"]);
                TryUpdateModel<StudentTransactionVM>(viewModel);
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.StudentTransaction entity = new Entities.StudentTransaction();
                    if (formCollection["ReceiptNo"] != null)
                    {
                        entity.ReceiptNo = Convert.ToInt32(formCollection["ReceiptNo"]);
                    }
                    if (formCollection["StudentTransactionId"] != null && formCollection["StudentTransactionId"] != "")
                    {
                        entity.StudentTransactionId = Convert.ToInt32(formCollection["StudentTransactionId"]);
                    }

                    if (formCollection["ClassId"] != null && formCollection["ClassId"] != "" && formCollection["DivisionId"] != null && formCollection["DivisionId"] != "")
                    {
                        ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                        entity.ClassDivisionId = classDivisionBAL.FindBy(cd => cd.ClassId == Convert.ToInt32(formCollection["ClassId"]) && cd.DivisionId == Convert.ToInt32(formCollection["DivisionId"])).FirstOrDefault().ClassDivisionId;
                    }
                    if (formCollection["StudentId"] != null && formCollection["StudentId"] != "")
                    {
                        entity.StudentId = Convert.ToInt32(formCollection["StudentId"]);
                    }
                    if (formCollection["TransactionDate"] != null)
                    {
                        entity.TransactionDate = Convert.ToDateTime(formCollection["TransactionDate"]);
                    }
                    entity.Status = true;
                    entity.Remark = Convert.ToString(formCollection["Remark"]);
                    if (formCollection["ReceiptTotal"] != null && formCollection["ReceiptTotal"] != "")
                    {
                        entity.ReceiptTotal = Convert.ToDecimal(formCollection["ReceiptTotal"]);
                    }
                    entity.BankName = Convert.ToString(formCollection["BankName"]);
                    entity.ChequeNumber = Convert.ToString(formCollection["ChequeNumber"]);
                    entity.StudentTransactionSubList = new List<Entities.StudentTransactionSub>();
                    //item.StudentTransactionId
                    string itemFeeId = Convert.ToString(formCollection["item.FeeHeadId"]);
                    string itemCr = Convert.ToString(formCollection["item.Cr"]);
                    string itemDr = Convert.ToString(formCollection["item.Dr"]);
                    string itemBalance = Convert.ToString(formCollection["item.Balance"]);

                    if (!string.IsNullOrEmpty(itemFeeId))
                    {

                        string[] strArrFeeId = itemFeeId.Split(',');
                        string[] strArrCr = itemCr.Split(',');
                        string[] strArrDr = itemDr.Split(',');
                        string[] strArrBalance = itemBalance.Split(',');
                        if (itemFeeId.Length > 0)
                        {
                            for (int i = 0; i < strArrFeeId.Length; i++)
                            {
                                Entities.StudentTransactionSub entitySub = new Entities.StudentTransactionSub();
                                entitySub.FeeHeadId = Convert.ToInt32(strArrFeeId[i]);
                                entitySub.Cr = Convert.ToDecimal(strArrCr[i]);
                                entitySub.Dr = Convert.ToDecimal(strArrDr[i]);
                                entitySub.Balance = Convert.ToDecimal(strArrBalance[i]);
                                entity.StudentTransactionSubList.Add(entitySub);
                            }
                        }
                    }

                    StudentTransactionBAL balObject = new StudentTransactionBAL();
                    balObject.Add(entity, SessionHelper.SchoolId);
                    PresentationLayer.Helpers.SessionHelper.ReportIndex = 2;
                    return Redirect(@"~\Report.aspx");
                    //return RedirectToAction("Index");
                }
                else
                {
                    ClassBAL classBAL = new ClassBAL();
                    viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
          
                    return View(viewModel);
                }
            }
            catch
            {
                ClassBAL classBAL = new ClassBAL();
                viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
          
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/Division/Edit/5
        public ActionResult Edit(int id)
        {
            StudentTransactionVM viewModel = new StudentTransactionVM();

            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Division/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentTransactionVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.StudentTransaction entity = new Entities.StudentTransaction();
                    entity.StudentTransactionId = viewModel.StudentTransactionId;
                    entity.ClassDivisionId = viewModel.ClassDivisionId;
                    ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                    entity.ClassDivisionId = classDivisionBAL.FindBy(cd => cd.ClassId == viewModel.ClassId && cd.DivisionId == viewModel.DivisionId).FirstOrDefault().ClassDivisionId;
                    entity.StudentId = viewModel.StudentId;
                    entity.TransactionDate = viewModel.TransactionDate;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;


                    StudentTransactionBAL balObject = new StudentTransactionBAL();
                    balObject.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }


        //
        // POST: /SysAdmin/Division/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                StudentTransactionBAL balObject = new StudentTransactionBAL();
                balObject.Delete(id);
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
            return this.Json(CommanMethods.GetClassesList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get divisions list
        /// </summary>
        /// <returns>divisions list</returns>
        public JsonResult GetDivisionsList(int ClassId)
        {
            return this.Json(CommanMethods.GetDivisionsList(ClassId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get student name
        /// </summary>
        /// <returns>student name</returns>
        public JsonResult GetStudentName(int StudentId)
        {            
            return Json(CommanMethods.GetStudentName(StudentId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get student name
        /// </summary>
        /// <returns>student name</returns>
        public JsonResult GetStudentNameList(int ClassId, int DivisionId)
        {
            return this.Json(CommanMethods.GetStudentNameList(ClassId, DivisionId, SessionHelper.SchoolId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentDetailsByRegisterId(int RegisterId)
        {
            return this.Json(CommanMethods.GetStudentDetailsByRegisterId(RegisterId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentTransactionSubDeails(int StudentId)
        {
            List<StudentTransactionSubVM> studentTransactionSubVMs = new List<StudentTransactionSubVM>();
            StudentLedgerBAL studentLedgerBAL = new StudentLedgerBAL();
            IQueryable<Entities.StudentLedger> studentLedgers = studentLedgerBAL.GetAll().Where(a => a.StudentId == StudentId).OrderBy(a => a.StudentLedgerId);
            if (studentLedgers != null && studentLedgers.Count() > 0)
            {
                IEnumerable<IGrouping<int, Entities.StudentLedger>> studentLedgersGroupByFeeId = studentLedgers.GroupBy(a => a.FeeHeadId);
                if (studentLedgersGroupByFeeId != null && studentLedgersGroupByFeeId.Count() > 0)
                {
                    foreach (IGrouping<int, Entities.StudentLedger> studentLedgersGroupItem in studentLedgersGroupByFeeId)
                    {
                        Entities.StudentLedger studentLedger = studentLedgersGroupItem.LastOrDefault();
                        StudentTransactionSubVM studentTransactionSubVM = new StudentTransactionSubVM();
                        studentTransactionSubVM.StudentTransactionSubId = 0;
                        studentTransactionSubVM.FeeHeadId = studentLedgersGroupItem.Key;
                        FeeHeadBAL feeHeadBAL = new FeeHeadBAL();
                        studentTransactionSubVM.FeeHeadName = feeHeadBAL.FindBy(f => f.FeeHeadId == studentLedgersGroupItem.Key).FirstOrDefault().FeeHeadName;
                        studentTransactionSubVM.Cr = 0;
                        studentTransactionSubVM.Dr = 0;
                        studentTransactionSubVM.Balance = studentLedger.HeadBalance;
                        studentTransactionSubVMs.Add(studentTransactionSubVM);
                    }
                }
            }
            return PartialView("_StudentTransactionSub", new GridModel<StudentTransactionSubVM> { Data = studentTransactionSubVMs });
        }

        public ActionResult GetStudentTransactionSub(int StudentId)
        {
            List<StudentTransactionSubVM> studentTransactionSubVMs = new List<StudentTransactionSubVM>();
            StudentLedgerBAL studentLedgerBAL = new StudentLedgerBAL();
            IQueryable<Entities.StudentLedger> studentLedgers = studentLedgerBAL.GetAll().Where(a => a.StudentId == StudentId).OrderBy(a => a.StudentLedgerId);
            if (studentLedgers != null && studentLedgers.Count() > 0)
            {
                IEnumerable<IGrouping<int, Entities.StudentLedger>> studentLedgersGroupByFeeId = studentLedgers.GroupBy(a => a.FeeHeadId);
                if (studentLedgersGroupByFeeId != null && studentLedgersGroupByFeeId.Count() > 0)
                {
                    foreach (IGrouping<int, Entities.StudentLedger> studentLedgersGroupItem in studentLedgersGroupByFeeId)
                    {
                        Entities.StudentLedger studentLedger = studentLedgersGroupItem.LastOrDefault();
                        StudentTransactionSubVM studentTransactionSubVM = new StudentTransactionSubVM();
                        studentTransactionSubVM.StudentTransactionSubId = 0;
                        studentTransactionSubVM.FeeHeadId = studentLedgersGroupItem.Key;
                        FeeHeadBAL feeHeadBAL = new FeeHeadBAL();
                        studentTransactionSubVM.FeeHeadName = feeHeadBAL.FindBy(f => f.FeeHeadId == studentLedgersGroupItem.Key).FirstOrDefault().FeeHeadName;
                        studentTransactionSubVM.Cr = 0;
                        studentTransactionSubVM.Dr = 0;
                        studentTransactionSubVM.Balance = studentLedger.HeadBalance;
                        FeeHeadBAL balObject = new FeeHeadBAL();
                        studentTransactionSubVM.Fees = from obj in balObject.GetAll() select new SelectListItem() { Text = obj.FeeHeadName, Value = obj.FeeHeadId.ToString() };
                        studentTransactionSubVMs.Add(studentTransactionSubVM);
                    }
                }
            }
            return PartialView("_GetStudentTransactionSub", studentTransactionSubVMs);
        }


       
    }
}