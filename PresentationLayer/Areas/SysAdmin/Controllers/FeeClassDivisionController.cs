using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class FeeClassDivisionController : Controller
    {
        //
        // GET: /SysAdmin/FeeClassDivision/
        [GridAction]
        public ActionResult Index()
        {
            List<FeeClassDivisionVM> viewModels = new List<FeeClassDivisionVM>();
            FeeClassDivisionBAL balObject = new FeeClassDivisionBAL();
            IQueryable<Entities.FeeClassDivision> entites = balObject.GetAll();


            foreach (Entities.FeeClassDivision entity in entites)
            {
                FeeClassDivisionVM viewModel = new FeeClassDivisionVM();
                viewModel.FeeClassDivisionId = entity.FeeClassDivisionId;
                viewModel.FeeHeadId  = entity.FeeHeadId;
                viewModel.FeeHeadName = entity.FeeHeadName;
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
                viewModel.ClassName = entity.ClassName;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.PeriodInMonthly = entity.PeriodInMonthly;
                viewModel.AmountInMonthly = entity.AmountInMonthly;
                viewModel.AmountInYearly = entity.AmountInYearly;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }

            return View(new GridModel<FeeClassDivisionVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Division/Details/5
        public ActionResult Details(int id)
        {
            FeeClassDivisionVM viewModel = new FeeClassDivisionVM();

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
                List<FeeClassDivisionVM> viewModels = new List<FeeClassDivisionVM>();
                FeeClassDivisionBAL balObject = new FeeClassDivisionBAL();
                IQueryable<Entities.FeeClassDivision> entites = balObject.GetAll();


                foreach (Entities.FeeClassDivision entity in entites)
                {
                    FeeClassDivisionVM viewModel = new FeeClassDivisionVM();
                    viewModel.FeeClassDivisionId = entity.FeeClassDivisionId;
                    viewModel.FeeHeadId = entity.FeeHeadId;
                    viewModel.FeeHeadName = entity.FeeHeadName;
                    viewModel.ClassDivisionId = entity.ClassDivisionId;
                    viewModel.ClassId = entity.ClassId;
                    viewModel.DivisionId = entity.DivisionId;
                    viewModel.ClassName = entity.ClassName;
                    viewModel.DivisionName = entity.DivisionName;
                    viewModel.PeriodInMonthly = entity.PeriodInMonthly;
                    viewModel.AmountInMonthly = entity.AmountInMonthly;
                    viewModel.AmountInYearly = entity.AmountInYearly;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<FeeClassDivisionVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Division/Create
        public ActionResult Create()
        {
            FeeClassDivisionVM viewModel = new FeeClassDivisionVM();

            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
            

            FeeClassDivisionBAL feesClass = new FeeClassDivisionBAL();
            viewModel.Fees = from obj in feesClass.GetAll() select new SelectListItem() { Text = obj.FeeHeadName, Value = obj.FeeHeadId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Division/Create
        [HttpPost]
        public ActionResult Create(FeeClassDivisionVM viewModel)
        {
            try
            {
                //Set default value
                viewModel.PeriodInMonthly = 12;
                viewModel.AmountInMonthly = viewModel.AmountInYearly;

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.FeeClassDivision entity = new Entities.FeeClassDivision();
                    entity.FeeClassDivisionId = viewModel.FeeClassDivisionId;
                    entity.FeeHeadId = viewModel.FeeHeadId;
                    ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                    entity.ClassDivisionId = classDivisionBAL.FindBy(cd => cd.ClassId == viewModel.ClassId && cd.DivisionId == viewModel.DivisionId).FirstOrDefault().ClassDivisionId;
                    entity.PeriodInMonthly = viewModel.PeriodInMonthly;
                    entity.AmountInMonthly = viewModel.AmountInMonthly;
                    entity.AmountInYearly = viewModel.AmountInYearly;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    FeeClassDivisionBAL balObject = new FeeClassDivisionBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    ClassBAL classBAL = new ClassBAL();
                    viewModel.Classes = from obj in classBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
            
                    FeeHeadBAL balObject = new FeeHeadBAL();
                    viewModel.Fees = from obj in balObject.GetAll() select new SelectListItem() { Text = obj.FeeHeadName, Value = obj.FeeHeadId.ToString() };

                    return View(viewModel);
                }
            }
            catch
            {
              
                ClassBAL classBAL = new ClassBAL();
                viewModel.Classes = from obj in classBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
            
                FeeHeadBAL balObject = new FeeHeadBAL();
                viewModel.Fees = from obj in balObject.GetAll() select new SelectListItem() { Text = obj.FeeHeadName, Value = obj.FeeHeadId.ToString() };

                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/Division/Edit/5
        public ActionResult Edit(int id)
        {
            FeeClassDivisionVM viewModel = new FeeClassDivisionVM();
            FeeClassDivisionBAL balObject = new FeeClassDivisionBAL();
            IQueryable<Entities.FeeClassDivision> entites = balObject.FindBy(a => a.FeeClassDivisionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.FeeClassDivision entity = entites.FirstOrDefault();
                viewModel.FeeClassDivisionId = entity.FeeClassDivisionId;
                viewModel.FeeHeadId = entity.FeeHeadId;
                viewModel.FeeHeadName = entity.FeeHeadName;
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
                viewModel.ClassName = entity.ClassName;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.PeriodInMonthly = entity.PeriodInMonthly;
                viewModel.AmountInMonthly = entity.AmountInMonthly;
                viewModel.AmountInYearly = entity.AmountInYearly;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Division/Edit/5
        [HttpPost]
        public ActionResult Edit(FeeClassDivisionVM viewModel)
        {
            try
            {
                //Set default value
                viewModel.PeriodInMonthly = 12;
                viewModel.AmountInMonthly = viewModel.AmountInYearly;

                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.FeeClassDivision entity = new Entities.FeeClassDivision();
                    entity.FeeClassDivisionId = viewModel.FeeClassDivisionId;
                    entity.FeeHeadId = viewModel.FeeHeadId;
                    entity.ClassDivisionId = viewModel.ClassDivisionId;
                    entity.PeriodInMonthly = viewModel.PeriodInMonthly;
                    entity.AmountInMonthly = viewModel.AmountInMonthly;
                    entity.AmountInYearly = viewModel.AmountInYearly;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    FeeClassDivisionBAL balObject = new FeeClassDivisionBAL();
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
                FeeClassDivisionBAL balObject = new FeeClassDivisionBAL();
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

        public JsonResult GetFeeHeadList()
        {
            FeeHeadBAL balObject = new FeeHeadBAL();
            var feeHeadList = from obj in balObject.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.FeeHeadName, Value = obj.FeeHeadId.ToString() };

            return this.Json(feeHeadList, JsonRequestBehavior.AllowGet);
        }
	}
}