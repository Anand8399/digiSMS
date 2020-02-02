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
    public class ClassDivisionController : Controller
    {
        //
        // GET: /SysAdmin/ClassDivision/
        [GridAction]
        public ActionResult Index()
        {
            List<ClassDivisionVM> viewModels = new List<ClassDivisionVM>();
            ClassDivisionBAL balObject = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> entites = balObject.GetAll();


            foreach (Entities.ClassDivision entity in entites)
            {
                ClassDivisionVM viewModel = new ClassDivisionVM();
                viewModel.ClassDivisionId = entity.ClassDivisionId;

                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;

                viewModel.ClassName = entity.ClassName;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<ClassDivisionVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/ClassDivision/Details/5
        public ActionResult Details(int id)
        {
            ClassDivisionVM viewModel = new ClassDivisionVM();
            ClassDivisionBAL balObject = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> entites = balObject.FindBy(a => a.ClassDivisionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.ClassDivision entity = entites.FirstOrDefault();
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
				viewModel.ClassName = entity.ClassName;
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
                List<ClassDivisionVM> viewModels = new List<ClassDivisionVM>();
                ClassDivisionBAL balObject = new ClassDivisionBAL();
                IQueryable<Entities.ClassDivision> entites = balObject.GetAll();

                foreach (Entities.ClassDivision entity in entites)
                {
                    ClassDivisionVM viewModel = new ClassDivisionVM();
                    viewModel.ClassDivisionId = entity.ClassDivisionId;
                    viewModel.ClassId = entity.ClassId;
                    viewModel.DivisionId = entity.DivisionId;
                    viewModel.ClassName = entity.ClassName;
                    viewModel.DivisionName = entity.DivisionName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<ClassDivisionVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/ClassDivision/Create
        public ActionResult Create()
        {
            ClassDivisionVM viewModel = new ClassDivisionVM();
            
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/ClassDivision/Create
        [HttpPost]
        public ActionResult Create(ClassDivisionVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.ClassDivision entity = new Entities.ClassDivision();                    
                    entity.ClassDivisionId = viewModel.ClassDivisionId;
                    entity.ClassId = viewModel.ClassId;
                    entity.DivisionId = viewModel.DivisionId;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ClassDivisionBAL balObject = new ClassDivisionBAL();
                    balObject.Add(entity);
                    //this.TempData["AlertMessage"] = "Successfully Save !!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ClassBAL classBAL = new ClassBAL();
                    viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };

                     return View(viewModel);
                }
            }
            catch
            {
                ClassBAL classBAL = new ClassBAL();
                viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/ClassDivision/Edit/5
        public ActionResult Edit(int id)
        {
            ClassDivisionVM viewModel = new ClassDivisionVM();
            ClassDivisionBAL balObject = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> entites = balObject.FindBy(a => a.ClassDivisionId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.ClassDivision entity = entites.FirstOrDefault();
                viewModel.ClassDivisionId = entity.ClassDivisionId;
                viewModel.ClassId = entity.ClassId;
                viewModel.DivisionId = entity.DivisionId;
				viewModel.ClassName = entity.ClassName;
                viewModel.DivisionName = entity.DivisionName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/ClassDivision/Edit/5
        [HttpPost]
        public ActionResult Edit(ClassDivisionVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.ClassDivision entity = new Entities.ClassDivision();
                    entity.ClassDivisionId = viewModel.ClassDivisionId;
                    entity.ClassId = viewModel.ClassId;
                    entity.ClassName = viewModel.ClassName;
                    entity.DivisionId = viewModel.DivisionId;
                    entity.DivisionName = viewModel.DivisionName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ClassDivisionBAL balObject = new ClassDivisionBAL();
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
        // POST: /SysAdmin/ClassDivision/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ClassDivisionBAL balObject = new ClassDivisionBAL();
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
        public JsonResult GetDivisionsList()
        {
            DivisionBAL divisionsBAL = new DivisionBAL();
            var divisionesList = from obj in divisionsBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.DivisionName, Value = obj.DivisionId.ToString() };
            return this.Json(divisionesList, JsonRequestBehavior.AllowGet);
        }
	}
}