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
    public class ClassController : Controller
    {
        //
        // GET: /SysAdmin/Class/
        [GridAction]
        public ActionResult Index()
        {
            List<ClassVM> viewModels = new List<ClassVM>();
            ClassBAL balObject = new ClassBAL();
            IQueryable<Entities.Class> entites = balObject.GetAll();

            foreach (Entities.Class entity in entites)
            {
                ClassVM viewModel = new ClassVM();
                viewModel.ClassId = entity.ClassId;
                viewModel.ClassName = entity.ClassName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<ClassVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Class/Details/5
        public ActionResult Details(int id)
        {
            ClassVM viewModel = new ClassVM();
            ClassBAL balObject = new ClassBAL();
            IQueryable<Entities.Class> entites = balObject.FindBy(a => a.ClassId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Class entity = entites.FirstOrDefault();
                viewModel.ClassId = entity.ClassId;
                viewModel.ClassName = entity.ClassName;
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
                List<ClassVM> viewModels = new List<ClassVM>();
                ClassBAL balObject = new ClassBAL();
                IQueryable<Entities.Class> entites = balObject.GetAll();

                foreach (Entities.Class entity in entites)
                {
                    ClassVM viewModel = new ClassVM();
                    viewModel.ClassId = entity.ClassId;
                    viewModel.ClassName = entity.ClassName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<ClassVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Class/Create
        public ActionResult Create()
        {
            ClassVM viewModel = new ClassVM();
            //AcademicYearBAL academicYearBAL = new AcademicYearBAL();
           // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Class/Create
        [HttpPost]
        public ActionResult Create(ClassVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Class entity = new Entities.Class();
                    entity.ClassId = viewModel.ClassId;
                    entity.ClassName = viewModel.ClassName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ClassBAL balObject = new ClassBAL();
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
               // viewModel.AcademicYears = from obj in academicYearBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.AcademicYearName, Value = obj.AcademicYearId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/Class/Edit/5
        public ActionResult Edit(int id)
        {
            ClassVM viewModel = new ClassVM();
            ClassBAL balObject = new ClassBAL();
            IQueryable<Entities.Class> entites = balObject.FindBy(a => a.ClassId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Class entity = entites.FirstOrDefault();
                viewModel.ClassId = entity.ClassId;
                viewModel.ClassName = entity.ClassName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Class/Edit/5
        [HttpPost]
        public ActionResult Edit(ClassVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Class entity = new Entities.Class();
                    entity.ClassId = viewModel.ClassId;
                    entity.ClassName = viewModel.ClassName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ClassBAL balObject = new ClassBAL();
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
                return View(viewModel);
            }
        }


        //
        // POST: /SysAdmin/Class/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ClassBAL balObject = new ClassBAL();
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