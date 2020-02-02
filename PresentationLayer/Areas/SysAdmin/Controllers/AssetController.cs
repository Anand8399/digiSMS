using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using PresentationLayer.Helpers;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class AssetController : Controller
    {
        //
        // GET: /SysAdmin/Asset/
        public ActionResult Index()
        {
            List<AssetVM> viewModels = new List<AssetVM>();
            AssetBAL balObject = new AssetBAL();
            IQueryable<Entities.Asset> entites = balObject.GetAll(SessionHelper.SchoolId);

            foreach (Entities.Asset entity in entites)
            {
                AssetVM viewModel = new AssetVM();
                viewModel.SrNo = entity.SrNo;
                viewModel.AssetName = entity.AssetName;
                viewModel.TypeofAsset = entity.TypeofAsset;
                viewModel.Quantity = entity.Quantity;
                viewModel.PricePerItem = entity.PricePerItem;
                viewModel.Total = entity.Total;
                viewModel.PurchaseDate = entity.PurchaseDate;
                viewModel.Condition = entity.Condition;
                viewModel.AssesmentYear = entity.AssesmentYear;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;

                viewModels.Add(viewModel);
            }
            return View(new GridModel<AssetVM> { Data = viewModels });
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
                List<AssetVM> viewModels = new List<AssetVM>();
                AssetBAL balObject = new AssetBAL();
                IQueryable<Entities.Asset> entites = balObject.GetAll(SessionHelper.SchoolId);

                foreach (Entities.Asset entity in entites)
                {
                    AssetVM viewModel = new AssetVM();
                    viewModel.SrNo = entity.SrNo;
                    viewModel.AssetName = entity.AssetName;
                    viewModel.TypeofAsset = entity.TypeofAsset;
                    viewModel.Quantity = entity.Quantity;
                    viewModel.PricePerItem = entity.PricePerItem;
                    viewModel.Total = entity.Total;
                    viewModel.PurchaseDate = entity.PurchaseDate;
                    viewModel.Condition = entity.Condition;
                    viewModel.AssesmentYear = entity.AssesmentYear;
                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    viewModels.Add(viewModel);
                }
                return View(new GridModel<AssetVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/State/Create
        public ActionResult Create()
        {
            AssetVM viewModel = new AssetVM();
            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/State/Create
        [HttpPost]
        public ActionResult Create(AssetVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Asset entity = new Entities.Asset();
                    entity.SrNo = viewModel.SrNo;
                    entity.AssetName = viewModel.AssetName;
                    entity.TypeofAsset = viewModel.TypeofAsset;
                    entity.Quantity = viewModel.Quantity;
                    entity.PricePerItem = viewModel.PricePerItem;
                    entity.Total = viewModel.Total;
                    entity.PurchaseDate = viewModel.PurchaseDate;
                    entity.Condition = viewModel.Condition;
                    entity.AssesmentYear = viewModel.AssesmentYear;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark == null ? string.Empty : viewModel.Remark;

                    AssetBAL balObject = new AssetBAL();
                    balObject.Add(entity, SessionHelper.SchoolId);

                    TempData["Message"] = "Asset entry updated successfully !!!";
                }
                else
                {
                    TempData["Error"] = "Some problem while updating asset entry !!!";
                }
            }
            catch
            {
                TempData["Error"] = "Some problem while updating asset entry !!!";
            }            
            return View(viewModel);
        }


        public ActionResult Edit(int id)
        {
           AssetVM viewModel = new AssetVM();
           AssetBAL balObject = new AssetBAL();
            IQueryable<Entities.Asset> entites = balObject.FindBy(a => a.SrNo == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Asset entity = entites.FirstOrDefault();
                viewModel.SrNo = entity.SrNo;
                viewModel.AssetName = entity.AssetName;
                viewModel.TypeofAsset = entity.TypeofAsset;
                viewModel.Quantity = entity.Quantity;
                viewModel.PricePerItem = entity.PricePerItem;
                viewModel.Total = entity.Total;
                viewModel.PurchaseDate = entity.PurchaseDate;
                 viewModel.Condition = entity.Condition;
                 viewModel.AssesmentYear = entity.AssesmentYear;
                 viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AssetVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Asset entity = new Entities.Asset();
                    entity.SrNo = viewModel.SrNo;
                    entity.AssetName = viewModel.AssetName;
                    entity.TypeofAsset = viewModel.TypeofAsset;
                    entity.Quantity = viewModel.Quantity;
                    entity.PricePerItem = viewModel.PricePerItem;
                    entity.Total = viewModel.Total;
                    entity.PurchaseDate = viewModel.PurchaseDate;
                    entity.Condition = viewModel.Condition;
                    entity.AssesmentYear = viewModel.AssesmentYear;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;
                    AssetBAL balObject = new AssetBAL();
                    balObject.Edit(entity);
                    TempData["Message"] = "Asset entry updated successfully !!!";
                }
                else
                {
                    TempData["Error"] = "Some problem while updating asset entry !!!";
                }
            }
            catch
            {
                TempData["Error"] = "Some problem while updating asset entry !!!";
            }

            return View(viewModel);
        }
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