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
    public class ReligionController : Controller
    {
        //
        // GET: /SysAdmin/Religion/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<ReligionVM> viewModels = new List<ReligionVM>();
            ReligionBAL balObject = new ReligionBAL();
            IQueryable<Entities.Religion> entites = balObject.GetAll();

            foreach (Entities.Religion entity in entites)
            {
                ReligionVM viewModel = new ReligionVM();
                viewModel.ReligionId = entity.ReligionId;
                viewModel.ReligionName = entity.ReligionName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<ReligionVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Religion/Details/5
        public ActionResult Details(int id)
        {
            ReligionVM viewModel = new ReligionVM();
            ReligionBAL balObject = new ReligionBAL();
            IQueryable<Entities.Religion> entites = balObject.FindBy(a => a.ReligionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Religion entity = entites.FirstOrDefault();
                viewModel.ReligionId = entity.ReligionId;
                viewModel.ReligionName = entity.ReligionName;
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
                List<ReligionVM> viewModels = new List<ReligionVM>();
                ReligionBAL balObject = new ReligionBAL();
                IQueryable<Entities.Religion> entites = balObject.GetAll();

                foreach (Entities.Religion entity in entites)
                {
                    ReligionVM viewModel = new ReligionVM();
                    viewModel.ReligionId = entity.ReligionId;
                    viewModel.ReligionName = entity.ReligionName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<ReligionVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Religion/Create
        public ActionResult Create()
        {
            ReligionVM viewModel = new ReligionVM();
            //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
            //viewModel.AcademicYears = from obj in academicYearBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Religion/Create
        [HttpPost]
        public ActionResult Create(ReligionVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Religion entity = new Entities.Religion();
                    entity.ReligionId = viewModel.ReligionId;
                    entity.ReligionName = viewModel.ReligionName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ReligionBAL balObject = new ReligionBAL();
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
        // GET: /SysAdmin/Religion/Edit/5
        public ActionResult Edit(int id)
        {
            ReligionVM viewModel = new ReligionVM();
            ReligionBAL balObject = new ReligionBAL();
            IQueryable<Entities.Religion> entites = balObject.FindBy(a => a.ReligionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Religion entity = entites.FirstOrDefault();
                viewModel.ReligionId = entity.ReligionId;
                viewModel.ReligionName = entity.ReligionName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Religion/Edit/5
        [HttpPost]
        public ActionResult Edit(ReligionVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Religion entity = new Entities.Religion();
                    entity.ReligionId = viewModel.ReligionId;
                    entity.ReligionName = viewModel.ReligionName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ReligionBAL balObject = new ReligionBAL();
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
        // POST: /SysAdmin/Religion/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ReligionBAL balObject = new ReligionBAL();
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