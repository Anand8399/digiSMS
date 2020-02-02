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
    public class CastController : Controller
    {
        //
        // GET: /SysAdmin/Cast/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<CastVM> viewModels = new List<CastVM>();
            CastBAL balObject = new CastBAL();
            IQueryable<Entities.Cast> entites = balObject.GetAll();

            foreach (Entities.Cast entity in entites)
            {
                CastVM viewModel = new CastVM();
                viewModel.CastId = entity.CastId;
                viewModel.CastName = entity.CastName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<CastVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Cast/Details/5
        public ActionResult Details(int id)
        {
            CastVM viewModel = new CastVM();
            CastBAL balObject = new CastBAL();
            IQueryable<Entities.Cast> entites = balObject.FindBy(a => a.CastId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Cast entity = entites.FirstOrDefault();
                viewModel.CastId = entity.CastId;
                viewModel.CastName = entity.CastName;
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
                List<CastVM> viewModels = new List<CastVM>();
                CastBAL balObject = new CastBAL();
                IQueryable<Entities.Cast> entites = balObject.GetAll();

                foreach (Entities.Cast entity in entites)
                {
                    CastVM viewModel = new CastVM();
                    viewModel.CastId = entity.CastId;
                    viewModel.CastName = entity.CastName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<CastVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Cast/Create
        public ActionResult Create()
        {
            CastVM viewModel = new CastVM();
           // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
           // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Cast/Create
        [HttpPost]
        public ActionResult Create(CastVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Cast entity = new Entities.Cast();
                    entity.CastId = viewModel.CastId;
                    entity.CastName = viewModel.CastName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    CastBAL balObject = new CastBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
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

        //
        // GET: /SysAdmin/Cast/Edit/5
        public ActionResult Edit(int id)
        {
            CastVM viewModel = new CastVM();
            CastBAL balObject = new CastBAL();
            IQueryable<Entities.Cast> entites = balObject.FindBy(a => a.CastId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Cast entity = entites.FirstOrDefault();
                viewModel.CastId = entity.CastId;
                viewModel.CastName = entity.CastName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Cast/Edit/5
        [HttpPost]
        public ActionResult Edit(CastVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Cast entity = new Entities.Cast();
                    entity.CastId = viewModel.CastId;
                    entity.CastName = viewModel.CastName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    CastBAL balObject = new CastBAL();
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
        // POST: /SysAdmin/Cast/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                CastBAL balObject = new CastBAL();
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