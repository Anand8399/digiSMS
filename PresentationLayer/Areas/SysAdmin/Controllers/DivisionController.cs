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
    public class DivisionController : Controller
    {
        //
        // GET: /SysAdmin/Division/
        [GridAction]
        public ActionResult Index()
        {
            List<DivisionVM> viewModels = new List<DivisionVM>();
            DivisionBAL balObject = new DivisionBAL();
            IQueryable<Entities.Division> entites = balObject.GetAll();
            foreach (Entities.Division entity in entites)
            {
                DivisionVM viewModel = new DivisionVM();
                viewModel.DivisionId = entity.DivisionId;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<DivisionVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Division/Details/5
        public ActionResult Details(int id)
        {
            DivisionVM viewModel = new DivisionVM();
            DivisionBAL balObject = new DivisionBAL();
            IQueryable<Entities.Division> entites = balObject.FindBy(a => a.DivisionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Division entity = entites.FirstOrDefault();
                viewModel.DivisionId = entity.DivisionId;
                viewModel.DivisionName = entity.DivisionName;
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
                List<DivisionVM> viewModels = new List<DivisionVM>();
                DivisionBAL balObject = new DivisionBAL();
                IQueryable<Entities.Division> entites = balObject.GetAll();
                foreach (Entities.Division entity in entites)
                {
                    DivisionVM viewModel = new DivisionVM();
                    viewModel.DivisionId = entity.DivisionId;
                    viewModel.DivisionName = entity.DivisionName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<DivisionVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Division/Create
        public ActionResult Create()
        {
            DivisionVM viewModel = new DivisionVM();
			viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Division/Create
        [HttpPost]
        public ActionResult Create(DivisionVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Division entity = new Entities.Division();
                    entity.DivisionId = viewModel.DivisionId;
                    entity.DivisionName = viewModel.DivisionName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    DivisionBAL balObject = new DivisionBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                   // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
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

        //
        // GET: /SysAdmin/Division/Edit/5
        public ActionResult Edit(int id)
        {
            DivisionVM viewModel = new DivisionVM();
            DivisionBAL balObject = new DivisionBAL();
            IQueryable<Entities.Division> entites = balObject.FindBy(a => a.DivisionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Division entity = entites.FirstOrDefault();
                viewModel.DivisionId = entity.DivisionId;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Division/Edit/5
        [HttpPost]
        public ActionResult Edit(DivisionVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Division entity = new Entities.Division();
                    entity.DivisionId = viewModel.DivisionId;
                    entity.DivisionName = viewModel.DivisionName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    DivisionBAL balObject = new DivisionBAL();
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
                DivisionBAL balObject = new DivisionBAL();
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