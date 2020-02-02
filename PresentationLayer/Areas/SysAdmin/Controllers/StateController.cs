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
    public class StateController : Controller
    {
        //
        // GET: /SysAdmin/State/
        [GridAction]
        public ActionResult Index()
        {
            List<StateVM> viewModels = new List<StateVM>();
            StateBAL balObject = new StateBAL();
            IQueryable<Entities.State> entites = balObject.GetAll();


            foreach (Entities.State entity in entites)
            {
                StateVM viewModel = new StateVM();
                viewModel.StateId = entity.StateId;
                viewModel.StateName = entity.StateName;
                viewModel.CountryName = entity.CountryName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<StateVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/State/Details/5
        public ActionResult Details(int id)
        {
            StateVM viewModel = new StateVM();
            StateBAL balObject = new StateBAL();
            IQueryable<Entities.State> entites = balObject.FindBy(a => a.StateId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.State entity = entites.FirstOrDefault();
                viewModel.StateId = entity.StateId;
                viewModel.StateName = entity.StateName;
                CountryBAL countryBAL = new CountryBAL();
                viewModel.CountryName = countryBAL.FindBy(c => c.CountryId == entity.CountryId).FirstOrDefault().CountryName;
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
                List<StateVM> viewModels = new List<StateVM>();
                StateBAL balObject = new StateBAL();
                IQueryable<Entities.State> entites = balObject.GetAll();

                CountryBAL countryBAL = new CountryBAL();
                IQueryable<Entities.Country> countries = countryBAL.GetAll();

                foreach (Entities.State entity in entites)
                {
                    StateVM viewModel = new StateVM();
                    viewModel.StateId = entity.StateId;
                    viewModel.StateName = entity.StateName;
                    viewModel.CountryName = countries.Where(c => c.CountryId == entity.CountryId).FirstOrDefault().CountryName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return View(new GridModel<StateVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/State/Create
        public ActionResult Create()
        {
            StateVM viewModel = new StateVM();
            //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
            //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
            //CountryBAL countryBAL = new CountryBAL();
            //viewModel.Countries = from obj in countryBAL.GetAll() select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/State/Create
        [HttpPost]
        public ActionResult Create(StateVM viewModel)
        {
            try
            {                
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.State entity = new Entities.State();
                    entity.StateId = viewModel.StateId;
                    entity.CountryId = viewModel.CountryId;
                    entity.StateName = viewModel.StateName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    StateBAL balObject = new StateBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                    //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                    CountryBAL countryBAL = new CountryBAL();
                    viewModel.Countries = from obj in countryBAL.GetAll() select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
                    return View(viewModel);
                }
            }
            catch
            {
                //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                CountryBAL countryBAL = new CountryBAL();
                viewModel.Countries = from obj in countryBAL.GetAll() select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/State/Edit/5
        public ActionResult Edit(int id)
        {
            StateVM viewModel = new StateVM();
            StateBAL balObject = new StateBAL();
            IQueryable<Entities.State> entites = balObject.FindBy(a => a.StateId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.State entity = entites.FirstOrDefault();
                viewModel.StateId = entity.StateId;
                viewModel.CountryId = entity.CountryId;
                viewModel.CountryName = entity.CountryName;
                viewModel.StateName = entity.StateName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/State/Edit/5
        [HttpPost]
        public ActionResult Edit(StateVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.State entity = new Entities.State();
                    entity.StateId = viewModel.StateId;
                    entity.CountryId = viewModel.CountryId;
                    entity.StateName = viewModel.StateName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    StateBAL balObject = new StateBAL();
                    balObject.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                    //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                    CountryBAL countryBAL = new CountryBAL();
                    viewModel.Countries = from obj in countryBAL.GetAll() select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
                    return View(viewModel);
                }
            }
            catch
            {
                //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                CountryBAL countryBAL = new CountryBAL();
                viewModel.Countries = from obj in countryBAL.GetAll() select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
                return View();
            }
        }


        //
        // POST: /SysAdmin/State/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                StateBAL balObject = new StateBAL();
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
            var countriesList = from obj in countryBAL.GetAll().Where(c =>  c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
            return this.Json(countriesList, JsonRequestBehavior.AllowGet);
        }
	}
}