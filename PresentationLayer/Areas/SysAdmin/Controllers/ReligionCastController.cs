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
    public class ReligionCastController : Controller
    {
        //
        // GET: /SysAdmin/ReligionCast/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<ReligionCastVM> viewModels = new List<ReligionCastVM>();
            ReligionCastBAL balObject = new ReligionCastBAL();
            IQueryable<Entities.ReligionCast> entites = balObject.GetAll();

            //ReligionBAL ReligionBAL = new ReligionBAL();
            //IQueryable<Entities.Religion> Religions = ReligionBAL.GetAll();

            //CastBAL CastBAL = new CastBAL();
            //IQueryable<Entities.Cast> Casts = CastBAL.GetAll();

            foreach (Entities.ReligionCast entity in entites)
            {
                ReligionCastVM viewModel = new ReligionCastVM();
                viewModel.ReligionCastId = entity.ReligionCastId;
                viewModel.ReligionId = entity.ReligionId;
                viewModel.CastId = entity.CastId;
                //viewModel.ReligionName = Religions.Where(c => c.ReligionId == entity.ReligionId).FirstOrDefault().ReligionName;
                //viewModel.CastName = Casts.Where(c => c.CastId == entity.CastId).FirstOrDefault().CastName;
                viewModel.ReligionName = entity.ReligionName;
                viewModel.CastName = entity.CastName;
                viewModel.ReserveCategory = entity.ReserveCategory;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<ReligionCastVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/ReligionCast/Details/5
        public ActionResult Details(int id)
        {
            ReligionCastVM viewModel = new ReligionCastVM();
            ReligionCastBAL balObject = new ReligionCastBAL();
            IQueryable<Entities.ReligionCast> entites = balObject.FindBy(a => a.ReligionCastId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.ReligionCast entity = entites.FirstOrDefault();
                viewModel.ReligionCastId = entity.ReligionCastId;
                viewModel.ReligionId = entity.ReligionId;
                viewModel.CastId = entity.CastId;
                ReligionBAL ReligionBAL = new ReligionBAL();
                CastBAL CastBAL = new CastBAL();
                viewModel.ReligionName = ReligionBAL.FindBy(c => c.ReligionId == entity.ReligionId).FirstOrDefault().ReligionName;
                viewModel.CastName = CastBAL.FindBy(c => c.CastId == entity.CastId).FirstOrDefault().CastName;
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
                List<ReligionCastVM> viewModels = new List<ReligionCastVM>();
                ReligionCastBAL balObject = new ReligionCastBAL();
                IQueryable<Entities.ReligionCast> entites = balObject.GetAll();
                //ReligionBAL ReligionBAL = new ReligionBAL();
                //IQueryable<Entities.Religion> Religions = ReligionBAL.GetAll();
                //CastBAL CastBAL = new CastBAL();
                //IQueryable<Entities.Cast> Casts = CastBAL.GetAll();

                foreach (Entities.ReligionCast entity in entites)
                {
                    ReligionCastVM viewModel = new ReligionCastVM();
                    viewModel.ReligionCastId = entity.ReligionCastId;
                    viewModel.ReligionId = entity.ReligionId;
                    viewModel.CastId = entity.CastId;
                    //viewModel.ReligionName = Religions.Where(c => c.ReligionId == entity.ReligionId).FirstOrDefault().ReligionName;
                    //viewModel.CastName = Casts.Where(c => c.CastId == entity.CastId).FirstOrDefault().CastName;
                    viewModel.ReligionName = entity.ReligionName;
                    viewModel.CastName = entity.CastName;
                    viewModel.ReserveCategory = entity.ReserveCategory;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<ReligionCastVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/ReligionCast/Create
        public ActionResult Create()
        {
            ReligionCastVM viewModel = new ReligionCastVM();

            ReligionBAL ReligionBAL = new ReligionBAL();
            viewModel.Religions = from obj in ReligionBAL.GetAll() select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                    
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/ReligionCast/Create
        [HttpPost]
        public ActionResult Create(ReligionCastVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.ReligionCast entity = new Entities.ReligionCast();
                    entity.ReligionCastId = viewModel.ReligionCastId;
                    entity.ReligionId = viewModel.ReligionId;
                    entity.CastId = viewModel.CastId;
                    entity.ReserveCategory = viewModel.ReserveCategory;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ReligionCastBAL balObject = new ReligionCastBAL();
                    balObject.Add(entity);
                    //this.TempData["AlertMessage"] = "Successfully Save !!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ReligionBAL ReligionBAL = new ReligionBAL();
                    viewModel.Religions = from obj in ReligionBAL.GetAll() select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                    return View(viewModel);
                }
            }
            catch
            {
                ReligionBAL ReligionBAL = new ReligionBAL();
                viewModel.Religions = from obj in ReligionBAL.GetAll() select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/ReligionCast/Edit/5
        public ActionResult Edit(int id)
        {
            ReligionCastVM viewModel = new ReligionCastVM();
            ReligionCastBAL balObject = new ReligionCastBAL();
            IQueryable<Entities.ReligionCast> entites = balObject.FindBy(a => a.ReligionCastId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.ReligionCast entity = entites.FirstOrDefault();
                viewModel.ReligionCastId = entity.ReligionCastId;
                viewModel.ReligionId = entity.ReligionId;
                viewModel.CastId = entity.CastId;
                viewModel.ReligionName = entity.ReligionName;
                viewModel.CastName = entity.CastName;
                viewModel.ReserveCategory = entity.ReserveCategory;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/ReligionCast/Edit/5
        [HttpPost]
        public ActionResult Edit(ReligionCastVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.ReligionCast entity = new Entities.ReligionCast();
                    entity.ReligionCastId = viewModel.ReligionCastId;
                    entity.ReligionId = viewModel.ReligionId;
                    entity.CastId = viewModel.CastId;
                    entity.ReserveCategory = viewModel.ReserveCategory;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    ReligionCastBAL balObject = new ReligionCastBAL();
                    balObject.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    ReligionBAL ReligionBAL = new ReligionBAL();
                    viewModel.Religions = from obj in ReligionBAL.GetAll() select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                    return View(viewModel);
                }
            }
            catch
            {
                ReligionBAL ReligionBAL = new ReligionBAL();
                viewModel.Religions = from obj in ReligionBAL.GetAll() select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
                return View();
            }
        }


        //
        // POST: /SysAdmin/ReligionCast/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ReligionCastBAL balObject = new ReligionCastBAL();
                balObject.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// method for get religions list
        /// </summary>
        /// <returns>religions list</returns>
        public JsonResult GetReligionsList()
        {
            ReligionBAL religionBAL = new ReligionBAL();
            var religionsList = from obj in religionBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };
            return this.Json(religionsList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get casts list
        /// </summary>
        /// <returns>casts list</returns>
        public JsonResult GetCastsList()
        {
            
            CastBAL castBAL = new CastBAL();
            var castsList = from obj in castBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CastName, Value = obj.CastId.ToString() };
            return this.Json(castsList, JsonRequestBehavior.AllowGet);
        }
	}
}