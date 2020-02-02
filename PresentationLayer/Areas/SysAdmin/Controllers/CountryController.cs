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
    public class CountryController : Controller
    {
        //
        // GET: /SysAdmin/Country/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<CountryVM> viewModels = new List<CountryVM>();
            CountryBAL balObject = new CountryBAL();
            IQueryable<Entities.Country> entites = balObject.GetAll();

            foreach (Entities.Country entity in entites)
            {
                CountryVM viewModel = new CountryVM();
                viewModel.CountryId = entity.CountryId;

                viewModel.CountryName = entity.CountryName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<CountryVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Country/Details/5
        public ActionResult Details(int id)
        {
            CountryVM viewModel = new CountryVM();
            CountryBAL balObject = new CountryBAL();
            IQueryable<Entities.Country> entites = balObject.FindBy(a => a.CountryId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Country entity = entites.FirstOrDefault();
                viewModel.CountryId = entity.CountryId;
				viewModel.CountryName = entity.CountryName;
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
                List<CountryVM> viewModels = new List<CountryVM>();
                CountryBAL balObject = new CountryBAL();
                IQueryable<Entities.Country> entites = balObject.GetAll();
                foreach (Entities.Country entity in entites)
                {
                    CountryVM viewModel = new CountryVM();
                    viewModel.CountryId = entity.CountryId;
                    viewModel.CountryName = entity.CountryName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<CountryVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Country/Create
        public ActionResult Create()
        {
            CountryVM viewModel = new CountryVM();
            //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
           // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Country/Create
        [HttpPost]
        public ActionResult Create(CountryVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Country entity = new Entities.Country();
                    entity.CountryId = viewModel.CountryId;
                    entity.CountryName = viewModel.CountryName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    CountryBAL balObject = new CountryBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                   // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                    return View(viewModel);
                }
            }
            catch
            {
                //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
               // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/Country/Edit/5
        public ActionResult Edit(int id)
        {
            CountryVM viewModel = new CountryVM();
            CountryBAL balObject = new CountryBAL();
            IQueryable<Entities.Country> entites = balObject.FindBy(a => a.CountryId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Country entity = entites.FirstOrDefault();
                viewModel.CountryId = entity.CountryId;
                viewModel.CountryName = entity.CountryName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Country/Edit/5
        [HttpPost]
        public ActionResult Edit(CountryVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Country entity = new Entities.Country();
                    entity.CountryId = viewModel.CountryId;
                    entity.CountryName = viewModel.CountryName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    CountryBAL balObject = new CountryBAL();
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
        // POST: /SysAdmin/Country/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                CountryBAL balObject = new CountryBAL();
                balObject.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
	}
}