using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using PresentationLayer.Helpers;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /SysAdmin/Employee/k
        public ActionResult Index()
        {
            List<EmployeeVM> viewModels = new List<EmployeeVM>();
            EmployeeBAL balObject = new EmployeeBAL();
            IQueryable<Entities.Employee> entites = balObject.GetAll(SessionHelper.SchoolId);

            foreach (Entities.Employee entity in entites)
            {
                EmployeeVM viewModel = new EmployeeVM();
                viewModel.EmployeeId = entity.EmployeeId;
                viewModel.EmployeeCode = entity.EmployeeCode;
                viewModel.Password = entity.Password;
                viewModel.FirstName = entity.FirstName;
                viewModel.MiddleName = entity.MiddleName;
                viewModel.LastName = entity.LastName;
                viewModel.Category = entity.Category;
                viewModel.Address = entity.Address;
                viewModel.ContactNo = entity.ContactNo;
                viewModel.DOB = entity.DOB;
                viewModel.JoiningDate = entity.JoiningDate;
                

                viewModels.Add(viewModel);
            }
            return View(new GridModel<EmployeeVM> { Data = viewModels });
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
                List<EmployeeVM> viewModels = new List<EmployeeVM>();
                EmployeeBAL balObject = new EmployeeBAL();
                IQueryable<Entities.Employee> entites = balObject.GetAll();

                foreach (Entities.Employee entity in entites)
                {
                    EmployeeVM viewModel = new EmployeeVM();
                    viewModel.EmployeeId = entity.EmployeeId;
                    viewModel.EmployeeCode = entity.EmployeeCode;
                    viewModel.Password = entity.Password;
                    viewModel.FirstName = entity.FirstName;
                    viewModel.MiddleName = entity.MiddleName;
                    viewModel.LastName = entity.LastName;
                    viewModel.Category = entity.Category;
                    viewModel.Address = entity.Address;
                    viewModel.ContactNo = entity.ContactNo;
                    viewModel.DOB = entity.DOB;
                    viewModel.JoiningDate = entity.JoiningDate;
                    
                    viewModels.Add(viewModel);
                }
                return View(new GridModel<EmployeeVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/State/Create
        public ActionResult Create()
        {
            EmployeeVM viewModel = new EmployeeVM();

            return View(viewModel);

        }

        //
        // POST: /SysAdmin/State/Create
        [HttpPost]
        public ActionResult Create(EmployeeVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Employee entity = new Entities.Employee();
                    entity.EmployeeId = viewModel.EmployeeId;
                    entity.EmployeeCode = viewModel.EmployeeCode.Trim();
                    entity.Password = viewModel.Password.Trim();
                    entity.FirstName = viewModel.FirstName.Trim();
                    entity.MiddleName = viewModel.MiddleName.Trim();
                    entity.LastName = viewModel.LastName.Trim();
                    entity.Category = viewModel.Category.Trim();
                    entity.Address = viewModel.Address.Trim();
                    entity.ContactNo = viewModel.ContactNo.Trim();
                    entity.DOB = viewModel.DOB;
                    entity.JoiningDate = viewModel.JoiningDate;
                    

                    EmployeeBAL balObject = new EmployeeBAL();
                    balObject.Add(entity, SessionHelper.SchoolId);
                    TempData["Message"] = "Employee added successfully !!!";
                }
                else
                {
                    TempData["Error"] = "Some problem while adding Employee !!!";
                }
            }
            catch
            {
                TempData["Error"] = "Some problem while adding Employee !!!";
            }

            return View(viewModel);
        }


        public ActionResult Edit(int id)
        {
            EmployeeVM viewModel = new EmployeeVM();
            EmployeeBAL balObject = new EmployeeBAL();
            IQueryable<Entities.Employee> entites = balObject.FindBy(a => a.EmployeeId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Employee entity = entites.FirstOrDefault();
                viewModel.EmployeeId = entity.EmployeeId;
                viewModel.EmployeeCode = entity.EmployeeCode.Trim();
                viewModel.Password = entity.Password.Trim();
                viewModel.FirstName = entity.FirstName.Trim();
                viewModel.MiddleName = entity.MiddleName.Trim();
                viewModel.LastName = entity.LastName.Trim();
                viewModel.Category = entity.Category.Trim();
                viewModel.Address = entity.Address.Trim();
                viewModel.ContactNo = entity.ContactNo.Trim();
                viewModel.DOB = entity.DOB;
                viewModel.JoiningDate = entity.JoiningDate;
                

            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Employee entity = new Entities.Employee();
                    entity.EmployeeId = viewModel.EmployeeId;
                    entity.EmployeeCode = viewModel.EmployeeCode.Trim();
                    entity.Password = viewModel.Password.Trim();
                    entity.FirstName = viewModel.FirstName.Trim();
                    entity.MiddleName = viewModel.MiddleName.Trim();
                    entity.LastName = viewModel.LastName.Trim();
                    entity.Category = viewModel.Category.Trim();
                    entity.Address = viewModel.Address.Trim();
                    entity.ContactNo = viewModel.ContactNo.Trim();
                    entity.DOB = viewModel.DOB;
                    entity.JoiningDate = viewModel.JoiningDate;
                    
                    EmployeeBAL balObject = new EmployeeBAL();
                    balObject.Edit(entity);

                    TempData["Message"] = "Employee added successfully !!!";
                }
                else
                {
                    TempData["Error"] = "Some problem while adding Employee !!!";
                }
            }
            catch
            {
                TempData["Error"] = "Some problem while adding Employee !!!";
            }

            return View(viewModel);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                EmployeeBAL balObject = new EmployeeBAL();
                balObject.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EmployeeLeaveAssign()
        {
            EmployeeLeaveAssignVM viewModel = new EmployeeLeaveAssignVM();
            return View(viewModel);
        }
        public ActionResult _EmployeeLeaveAssign()
        {
            List<EmployeeLeaveAssignVM> viewModels = new List<EmployeeLeaveAssignVM>();
            List<Entities.EmployeeLeaveTransaction> employeeList = new List<Entities.EmployeeLeaveTransaction>();
            EmployeeBAL balObject = new EmployeeBAL();
            IQueryable<Entities.EmployeeLeaveTransaction> entites = balObject.GetAllEmployeeLeaveAssign(SessionHelper.SchoolId);
            foreach (Entities.EmployeeLeaveTransaction entity in entites)
            {
                EmployeeLeaveAssignVM viewModel = new EmployeeLeaveAssignVM();
                viewModel.EmployeeId = entity.EmployeeId;
                viewModel.EmployeeFullName = entity.EmployeeFullName;
                viewModel.BalanceLeaves = entity.BalanceLeaves;
                viewModel.Remark = entity.Remark;

                viewModels.Add(viewModel);
            }
            return PartialView("_EmployeeLeaveAssign", viewModels);


        }

        [HttpPost]
        public ActionResult EmployeeLeaveAssign(FormCollection formCollection, String[] employees, String[] balDays, string[] newDays, String[] remarks) //string entities )
        {
            if (employees.Length > 0)
            {
                List<Entities.EmployeeLeaveTransaction> entities = new List<Entities.EmployeeLeaveTransaction>();
                Entities.EmployeeLeaveTransaction entity;


                for (int i = 0; i < employees.Length; i++)
                {
                    entity = new Entities.EmployeeLeaveTransaction();

                    int id = 0;
                    if (employees[i] != null)
                    {
                        int.TryParse(employees[i], out id);
                    }

                    entity.EmployeeId = id;


                    decimal newDays1 = 0;
                    if (newDays[i] != null)
                    {
                        decimal.TryParse(newDays[i], out newDays1);
                    }

                    entity.LeavesInDays = newDays1;

                    
                    decimal balanceDays = 0;
                    if (balDays[i] != null)
                    {
                        decimal.TryParse(balDays[i], out balanceDays);
                    }
                    entity.BalanceLeaves = balanceDays + newDays1;


                    string remark = string.Empty;
                    if (remarks[i] != null)
                    {
                        remark = Convert.ToString(remarks[i]);
                    }
                    entity.Remark = remark;

                    entities.Add(entity);
                }

                EmployeeBAL employeeBAL = new EmployeeBAL();
                employeeBAL.addLeaves(entities);

                //EmployeeLeaveAssignVM viewModel = new EmployeeLeaveAssignVM();

                TempData["Message"] = "Successfully Submitted.";
               // return View(viewModel);
            }
            else
            {
                //EmployeeLeaveAssignVM viewModel = new EmployeeLeaveAssignVM();
                TempData["Error"] = "Something wrong, Records not valid.";
               // return View(viewModel);
            }

            return View();
        }

        public ActionResult EmployeeLeaveApply()
        {
            EmployeeBAL employeeBAL = new EmployeeBAL();
           
            EmployeeLeaveApplyVM viewModel = new EmployeeLeaveApplyVM();
            viewModel.EmployeeList = from obj in employeeBAL.GetAll() select new SelectListItem() { Text = obj.FirstName.Trim() + " " + obj.MiddleName.Trim() + " " + obj.LastName.Trim(), Value = obj.EmployeeId.ToString() };
            return View(viewModel);

        }

        //
        // POST: /SysAdmin/State/EmployeeLeaveApply
        [HttpPost]
        public ActionResult EmployeeLeaveApply(EmployeeLeaveApplyVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.EmployeeLeaveTransaction entity = new Entities.EmployeeLeaveTransaction();
                    entity.EmployeeId = viewModel.EmployeeId;
                    entity.LeaveFromDate = viewModel.LeaveFromDate;
                    entity.LeaveToDate = viewModel.LeaveToDate;
                    entity.LeaveType = viewModel.LeaveType;

                    entity.Remark = viewModel.Remark == null ? string.Empty : viewModel.Remark;

                    decimal leaveDays = (decimal)(entity.LeaveToDate - entity.LeaveFromDate).TotalDays;
                    if (leaveDays < 0)
                    {
                        TempData["Error"] = "Select valid from date - to date.";
                       // return View(viewModel);
                        return RedirectToAction("EmployeeLeaveApply");
                    }
                    entity.LeavesInDays = leaveDays + 1 ;

                    entity.BalanceLeaves = viewModel.BalanceLeaves - entity.LeavesInDays;


                    EmployeeBAL balObject = new EmployeeBAL();
                    balObject.leaveApply(entity);
                    return RedirectToAction("EmployeeLeaveApply");
                   
                    
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch
            {
                return View(viewModel);
            }
        }

        public JsonResult GetLeaveBalance(int EmployeeId)
        {
            decimal iBal = 0;
            EmployeeBAL employeeBAL = new EmployeeBAL();
            iBal = employeeBAL.getLeaveBalance(EmployeeId);
            return this.Json(iBal, JsonRequestBehavior.AllowGet);
        }

    }
}