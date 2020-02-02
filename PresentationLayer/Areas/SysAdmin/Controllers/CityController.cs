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
    public class CityController : Controller
    {
        //
        // GET: /SysAdmin/City/
        //
        [GridAction]
        public ActionResult Index()
        {
            List<CityVM> viewModels = new List<CityVM>();
            CityBAL balObject = new CityBAL();
            IQueryable<Entities.City> entites = balObject.GetAll();


            foreach (Entities.City entity in entites)
            {
                CityVM viewModel = new CityVM();
                viewModel.CityId = entity.CityId;
                viewModel.CityName = entity.CityName;
                viewModel.DistrictName = entity.DistrictName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<CityVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/City/Details/5
        public ActionResult Details(int id)
        {
            CityVM viewModel = new CityVM();
            CityBAL balObject = new CityBAL();
            IQueryable<Entities.City> entites = balObject.FindBy(a => a.CityId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.City entity = entites.FirstOrDefault();
                viewModel.CityId = entity.CityId;
                viewModel.CityName = entity.CityName;
                DistrictBAL districtBAL = new DistrictBAL();
                IQueryable<Entities.District> states = districtBAL.GetAll();
                viewModel.DistrictName = districtBAL.FindBy(c => c.DistrictId == entity.DistrictId).FirstOrDefault().DistrictName;
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
                List<CityVM> viewModels = new List<CityVM>();
                CityBAL balObject = new CityBAL();
                IQueryable<Entities.City> entites = balObject.GetAll();

                foreach (Entities.City entity in entites)
                {
                    CityVM viewModel = new CityVM();
                    viewModel.CityId = entity.CityId;
                    viewModel.CityName = entity.CityName;
                    viewModel.DistrictName = entity.DistrictName;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return View(new GridModel<CityVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/City/Create
        public ActionResult Create()
        {
            CityVM viewModel = new CityVM();
            DistrictBAL districtBAL = new DistrictBAL();
            viewModel.Districts = from obj in districtBAL.GetAll() select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/City/Create
        [HttpPost]
        public ActionResult Create(CityVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.City entity = new Entities.City();
                    entity.CityId = viewModel.CityId;
                    entity.DistrictId = viewModel.DistrictId;
                    entity.CityName = viewModel.CityName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    CityBAL balObject = new CityBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    DistrictBAL destrictBAL = new DistrictBAL();
                    viewModel.Districts = from obj in destrictBAL.GetAll() select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };
                    
                    return View(viewModel);
                }
            }
            catch
            {
                DistrictBAL destrictBAL = new DistrictBAL();
                viewModel.Districts = from obj in destrictBAL.GetAll() select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };

                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/City/Edit/5
        public ActionResult Edit(int id)
        {
            CityVM viewModel = new CityVM();
            CityBAL balObject = new CityBAL();
            IQueryable<Entities.City> entites = balObject.FindBy(a => a.CityId == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.City entity = entites.FirstOrDefault();
                viewModel.CityId = entity.CityId;
                viewModel.CityName = entity.CityName;
                viewModel.DistrictId = entity.DistrictId;
                viewModel.DistrictName = entity.DistrictName;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/City/Edit/5
        [HttpPost]
        public ActionResult Edit(CityVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.City entity = new Entities.City();
                    entity.CityId = viewModel.CityId;
                    entity.DistrictId = viewModel.DistrictId;
                    entity.CityName = viewModel.CityName;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    CityBAL balObject = new CityBAL();
                    balObject.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    DistrictBAL destrictBAL = new DistrictBAL();
                    viewModel.Districts = from obj in destrictBAL.GetAll() select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };

                    return View(viewModel);
                }
            }
            catch
            {
                DistrictBAL destrictBAL = new DistrictBAL();
                viewModel.Districts = from obj in destrictBAL.GetAll() select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };
                return View();
            }
        }

      
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                CityBAL balObject = new CityBAL();
                balObject.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

          /// <summary>
        /// method for get state list
        /// </summary>
        /// <returns>District list</returns>
        public JsonResult GetDistrictList()
        {
            DistrictBAL balObject = new DistrictBAL();
            var destrictList = from obj in balObject.GetAll() select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };
            return this.Json(destrictList, JsonRequestBehavior.AllowGet);
        }

	}
}