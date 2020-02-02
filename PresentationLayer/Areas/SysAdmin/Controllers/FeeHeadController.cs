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
    public class FeeHeadController : Controller
    {
        //
        // GET: /SysAdmin/FeeHead/
        [GridAction]
        public ActionResult Index()
        {
            List<FeeHeadVM> viewModels = new List<FeeHeadVM>();
            FeeHeadBAL balObject = new FeeHeadBAL();
            IQueryable<Entities.FeeHead> entites = balObject.GetAll();

            foreach (Entities.FeeHead entity in entites)
            {
                FeeHeadVM viewModel = new FeeHeadVM();
                viewModel.FeeHeadId = entity.FeeHeadId;
                 viewModel.FeeHeadName = entity.FeeHeadName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<FeeHeadVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/FeeHead/Details/5
        public ActionResult Details(int id)
        {
            FeeHeadVM viewModel = new FeeHeadVM();
            FeeHeadBAL balObject = new FeeHeadBAL();
            IQueryable<Entities.FeeHead> entites = balObject.FindBy(a => a.FeeHeadId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.FeeHead entity = entites.FirstOrDefault();
                viewModel.FeeHeadId = entity.FeeHeadId;
                viewModel.FeeHeadName = entity.FeeHeadName;
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
                List<FeeHeadVM> viewModels = new List<FeeHeadVM>();
                FeeHeadBAL balObject = new FeeHeadBAL();
                IQueryable<Entities.FeeHead> entites = balObject.GetAll();

                foreach (Entities.FeeHead entity in entites)
                {
                    FeeHeadVM viewModel = new FeeHeadVM();
                    viewModel.FeeHeadId = entity.FeeHeadId;
                    viewModel.FeeHeadName = entity.FeeHeadName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<FeeHeadVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/FeeHead/Create
        public ActionResult Create()
        {
            FeeHeadVM viewModel = new FeeHeadVM();
            //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
            //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/FeeHead/Create
        [HttpPost]
        public ActionResult Create(FeeHeadVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.FeeHead entity = new Entities.FeeHead();
                    entity.FeeHeadId = viewModel.FeeHeadId;
                    entity.FeeHeadName = viewModel.FeeHeadName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    FeeHeadBAL balObject = new FeeHeadBAL();
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
               // AcademicYearBAL academicYearBAL = new AcademicYearBAL();
                //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/FeeHead/Edit/5
        public ActionResult Edit(int id)
        {
            FeeHeadVM viewModel = new FeeHeadVM();
            FeeHeadBAL balObject = new FeeHeadBAL();
            IQueryable<Entities.FeeHead> entites = balObject.FindBy(a => a.FeeHeadId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.FeeHead entity = entites.FirstOrDefault();
                viewModel.FeeHeadId = entity.FeeHeadId;
                viewModel.FeeHeadName = entity.FeeHeadName.Trim();
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/FeeHead/Edit/5
        [HttpPost]
        public ActionResult Edit(FeeHeadVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.FeeHead entity = new Entities.FeeHead();
                    entity.FeeHeadId = viewModel.FeeHeadId;
                    entity.FeeHeadName = viewModel.FeeHeadName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    FeeHeadBAL balObject = new FeeHeadBAL();
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
        // POST: /SysAdmin/FeeHead/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                FeeHeadBAL balObject = new FeeHeadBAL();
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