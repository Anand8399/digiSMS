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
    public class DistrictController : Controller
    {
        //
        // GET: /SysAdmin/District/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<DistrictVM> viewModels = new List<DistrictVM>();
            DistrictBAL balObject = new DistrictBAL();
            IQueryable<Entities.District> entites = balObject.GetAll();

            foreach (Entities.District entity in entites)
            {
                DistrictVM viewModel = new DistrictVM();
                viewModel.DistrictId = entity.DistrictId;
                viewModel.DistrictName = entity.DistrictName;
                viewModel.StateName = entity.StateName;
                viewModel.CountryName = entity.CountryName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<DistrictVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/District/Details/5
        public ActionResult Details(int id)
        {
            DistrictVM viewModel = new DistrictVM();
            DistrictBAL balObject = new DistrictBAL();
            IQueryable<Entities.District> entites = balObject.FindBy(a => a.DistrictId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.District entity = entites.FirstOrDefault();
                viewModel.DistrictId = entity.DistrictId;
                viewModel.DistrictName = entity.DistrictName;
				StateBAL stateBAL = new StateBAL();
                IQueryable<Entities.State> states = stateBAL.GetAll();
                viewModel.StateName = stateBAL.FindBy(c => c.StateId == entity.StateId).FirstOrDefault().StateName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
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
                List<DistrictVM> viewModels = new List<DistrictVM>();
                DistrictBAL balObject = new DistrictBAL();
                IQueryable<Entities.District> entites = balObject.GetAll();


                foreach (Entities.District entity in entites)
                {
                    DistrictVM viewModel = new DistrictVM();
                    viewModel.DistrictId = entity.DistrictId;
                    viewModel.DistrictName = entity.DistrictName;
                    viewModel.StateName = entity.StateName;
                    viewModel.CountryName = entity.CountryName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return View(new GridModel<DistrictVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/District/Create
        public ActionResult Create()
        {
            DistrictVM viewModel = new DistrictVM();
            StateBAL stateBAL = new StateBAL();
            viewModel.States = from obj in stateBAL.GetAll() select new SelectListItem() { Text = obj.StateName, Value = obj.StateId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/District/Create
        [HttpPost]
        public ActionResult Create(DistrictVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.District entity = new Entities.District();
                    entity.DistrictId = viewModel.DistrictId;
                    entity.StateId = viewModel.StateId;
                    entity.DistrictName = viewModel.DistrictName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    DistrictBAL balObject = new DistrictBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                   // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                   // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                    
                    return View(viewModel);
                }
            }
            catch
            {
               // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
               // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/District/Edit/5
        public ActionResult Edit(int id)
        {
            DistrictVM viewModel = new DistrictVM();
            DistrictBAL balObject = new DistrictBAL();
            IQueryable<Entities.District> entites = balObject.FindBy(a => a.DistrictId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.District entity = entites.FirstOrDefault();
                viewModel.DistrictId = entity.DistrictId;
                viewModel.DistrictName = entity.DistrictName;
                viewModel.StateId = entity.StateId;
                viewModel.StateName = entity.StateName;
                viewModel.CountryName = entity.CountryName;
                viewModel.CountryId = entity.CountryId;
                viewModel.StateName = entity.StateName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/District/Edit/5
        [HttpPost]
        public ActionResult Edit(DistrictVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.District entity = new Entities.District();
                    entity.DistrictId = viewModel.DistrictId;
                    entity.StateId = viewModel.StateId;
                    entity.DistrictName = viewModel.DistrictName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    DistrictBAL balObject = new DistrictBAL();
                    balObject.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                   // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                   // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                    
                    return View(viewModel);
                }
            }
            catch
            {
               // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
               // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                
                return View();
            }
        }


        //
        // POST: /SysAdmin/District/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                DistrictBAL balObject = new DistrictBAL();
                balObject.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// method for get countries list
        /// </summary>
        /// <returns>countries list</returns>
        public JsonResult GetCountriesList()
        {
            CountryBAL countryBAL = new CountryBAL();
            var countriesList = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
            return this.Json(countriesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get state list
        /// </summary>
        /// <returns>state list</returns>
        public JsonResult GetStatesList(int CountryId)
        {
            StateBAL balObject = new StateBAL();
            var statesList = from obj in balObject.GetAll().Where(c => c.CountryId == CountryId && c.Status == true) select new SelectListItem() { Text = obj.StateName, Value = obj.StateId.ToString() };
            return this.Json(statesList, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult getall()
        //{
        //    View();
        //}
	}
}