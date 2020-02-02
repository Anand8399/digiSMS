using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using PresentationLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class AcademicYearController : BaseController
    {
        //
        // GET: /SysAdmin/AcademicYear/
        [GridAction]
        public ActionResult Index()
        {
            List<AcademicYearVM> viewModels = new List<AcademicYearVM>();
            AcademicYearBAL balObject = new AcademicYearBAL();
            IQueryable<Entities.AcademicYear> entites = balObject.GetAll();
            foreach (Entities.AcademicYear entity in entites)
            {
                AcademicYearVM viewModel = new AcademicYearVM();
                viewModel.AcademicYearId = entity.AcademicYearId;
                viewModel.AcademicYearName = entity.AcademicYearName;
                viewModel.StartDate = entity.StartDate;
                viewModel.EndDate = entity.EndDate;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<AcademicYearVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/AcademicYear/Details/5
        public ActionResult Details(int id)
        {
            AcademicYearVM viewModel = new AcademicYearVM();
            AcademicYearBAL balObject = new AcademicYearBAL();
            IQueryable<Entities.AcademicYear> entites = balObject.FindBy(a => a.AcademicYearId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.AcademicYear entity = entites.FirstOrDefault();
                viewModel.AcademicYearId = entity.AcademicYearId;
                viewModel.AcademicYearName = entity.AcademicYearName;
                viewModel.StartDate = entity.StartDate;
                viewModel.EndDate = entity.EndDate;
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
                List<AcademicYearVM> viewModels = new List<AcademicYearVM>();
                AcademicYearBAL balObject = new AcademicYearBAL();
                IQueryable<Entities.AcademicYear> entites = balObject.GetAll();
                foreach (Entities.AcademicYear entity in entites)
                {
                    AcademicYearVM viewModel = new AcademicYearVM();
                    viewModel.AcademicYearId = entity.AcademicYearId;
                    viewModel.AcademicYearName = entity.AcademicYearName;
                    viewModel.StartDate = entity.StartDate;
                    viewModel.EndDate = entity.EndDate;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<AcademicYearVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/AcademicYear/Create
        public ActionResult Create()
        {
            
            AcademicYearVM viewModel = new AcademicYearVM();
            viewModel.StartDate = DateTime.Today.Date;
            viewModel.EndDate = DateTime.Today.Date;
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/AcademicYear/Create
        [HttpPost]
        public ActionResult Create(AcademicYearVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.AcademicYear entity = new Entities.AcademicYear();
                    entity.AcademicYearId = viewModel.AcademicYearId;
                    entity.AcademicYearName = viewModel.AcademicYearName;
                    entity.StartDate = viewModel.StartDate;
                    entity.EndDate =viewModel.EndDate;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    AcademicYearBAL balObject = new AcademicYearBAL();
                    balObject.Add(entity);
                    //this.TempData["AlertMessage"] = "Successfully Save !!!";
                    return RedirectToAction("Index");
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

        //
        // GET: /SysAdmin/AcademicYear/Edit/5
        public ActionResult Edit(int id)
        {
            AcademicYearVM viewModel = new AcademicYearVM();
            AcademicYearBAL balObject = new AcademicYearBAL();
            IQueryable<Entities.AcademicYear> entites = balObject.FindBy(a => a.AcademicYearId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.AcademicYear entity = entites.FirstOrDefault();
                viewModel.AcademicYearId = entity.AcademicYearId;
                viewModel.AcademicYearName = entity.AcademicYearName;
                viewModel.StartDate = entity.StartDate;
                viewModel.EndDate = entity.EndDate;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/AcademicYear/Edit/5
        [HttpPost]
        public ActionResult Edit(AcademicYearVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.AcademicYear entity = new Entities.AcademicYear();
                    entity.AcademicYearId = viewModel.AcademicYearId;
                    entity.AcademicYearName = viewModel.AcademicYearName;
                    entity.StartDate = viewModel.StartDate;
                    entity.EndDate = viewModel.EndDate;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    AcademicYearBAL balObject = new AcademicYearBAL();
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
        // GET: /SysAdmin/AcademicYear/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    AcademicYearVM viewModel = new AcademicYearVM();
        //    AcademicYearBAL balObject = new AcademicYearBAL();
        //    IQueryable<Entities.AcademicYear> entites = balObject.FindBy(a => a.AcademicYearId == id);
        //    if (entites != null && entites.Count() > 0)
        //    {
        //        Entities.AcademicYear entity = entites.FirstOrDefault();
        //        viewModel.AcademicYearId = entity.AcademicYearId;
        //        viewModel.AcademicYearName = entity.AcademicYearName;
        //        viewModel.StartDate = entity.StartDate;
        //        viewModel.EndDate = entity.EndDate;
        //        viewModel.Status = entity.Status;
        //        viewModel.Remark = entity.Remark;
        //    }
        //    return View(viewModel);
        //}

        //
        // POST: /SysAdmin/AcademicYear/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                AcademicYearBAL balObject = new AcademicYearBAL();
                balObject.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult test()
        {
            return View();

        }
    }
}
