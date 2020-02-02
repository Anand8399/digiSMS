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
    public class AccountController : Controller
    {
        //
        // GET: /SysAdmin/Asset/
        public ActionResult Index()
        {
            List<AccountVM> viewModels = new List<AccountVM>();
            AccountBAL balObject = new AccountBAL();
            IQueryable<Entities.Account> entites = balObject.GetAll(SessionHelper.SchoolId);

            foreach (Entities.Account entity in entites)
            {
                AccountVM viewModel = new AccountVM();
                viewModel.SrNo = entity.SrNo;
                viewModel.NarrationDetails = entity.NarrationDetails;
                viewModel.TransactionType = entity.TransactionType;
                viewModel.PaymentMode = entity.PaymentMode;
                viewModel.Amount = entity.Amount;
                viewModel.Balance = entity.Balance;
                viewModel.TransactionDate = entity.TransactionDate;
                viewModel.Remark = entity.Remark;

                viewModel.CustomerName = entity.CustomerName;
                viewModel.BankName = entity.BankName;
                viewModel.ChqDDNumber = entity.ChqDDNumber;
                viewModel.ContactNo = entity.ContactNo;
                viewModels.Add(viewModel);
            }
            return View(new GridModel<AccountVM> { Data = viewModels });
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
                List<AccountVM> viewModels = new List<AccountVM>();
                AccountBAL balObject = new AccountBAL();
                IQueryable<Entities.Account> entites = balObject.GetAll(SessionHelper.SchoolId);

                foreach (Entities.Account entity in entites)
                {
                    AccountVM viewModel = new AccountVM();
                    viewModel.SrNo = entity.SrNo;
                    viewModel.NarrationDetails = entity.NarrationDetails;
                    viewModel.TransactionType = entity.TransactionType;
                    viewModel.PaymentMode = entity.PaymentMode;
                    viewModel.Amount = entity.Amount;
                    viewModel.Balance = entity.Balance;
                    viewModel.TransactionDate = entity.TransactionDate;
                    viewModel.Remark = entity.Remark;

                    viewModel.CustomerName = entity.CustomerName;
                    viewModel.BankName = entity.BankName;
                    viewModel.ChqDDNumber = entity.ChqDDNumber;
                    viewModel.ContactNo = entity.ContactNo;

                    viewModels.Add(viewModel);
                }
                return View(new GridModel<AccountVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/State/Create
        public ActionResult Create()
        {
            AccountVM viewModel = new AccountVM();
            //viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/State/Create
        [HttpPost]
        public ActionResult Create(AccountVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.Account entity = new Entities.Account();
                    entity.SrNo = viewModel.SrNo;
                    entity.NarrationDetails = viewModel.NarrationDetails;
                    entity.TransactionType = viewModel.TransactionType;
                    entity.PaymentMode = viewModel.PaymentMode;
                    entity.Amount = viewModel.Amount;
                    entity.Balance = viewModel.Balance;
                    entity.TransactionDate = DateTime.Now;
                    entity.Remark = viewModel.Remark == null ? string.Empty : viewModel.Remark;

                    entity.CustomerName = viewModel.CustomerName;
                    entity.BankName = viewModel.BankName;
                    entity.ChqDDNumber = viewModel.ChqDDNumber;
                    entity.ContactNo = viewModel.ContactNo;


                    AccountBAL balObject = new AccountBAL();
                    balObject.Add(entity, SessionHelper.SchoolId);
                    TempData["Message"] = "Account entry added successfully !!!";
                }
                else
                {
                    TempData["Error"] = "Some problem while adding account entry !!!";
                }
            }
            catch
            {
                TempData["Error"] = "Some problem while adding account entry !!!";
            }

            return View(viewModel);
        }


        public ActionResult Edit(int id)
        {
            AccountVM viewModel = new AccountVM();
            AccountBAL balObject = new AccountBAL();
            IQueryable<Entities.Account> entites = balObject.FindBy(a => a.SrNo == id);
            if (entites != null && entites.Count() > 0)
            {
                Entities.Account entity = entites.FirstOrDefault();
                viewModel.SrNo = entity.SrNo;
                viewModel.NarrationDetails = entity.NarrationDetails;
                viewModel.TransactionType = entity.TransactionType;
                viewModel.PaymentMode = entity.PaymentMode;
                viewModel.Amount = entity.Amount;
                viewModel.Balance = entity.Balance;
                viewModel.TransactionDate = entity.TransactionDate;
                viewModel.Remark = entity.Remark;

                viewModel.CustomerName = entity.CustomerName;
                viewModel.BankName = entity.BankName;
                viewModel.ChqDDNumber = entity.ChqDDNumber;
                viewModel.ContactNo = entity.ContactNo;

                
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AccountVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.Account entity = new Entities.Account();
                    entity.SrNo = viewModel.SrNo;
                    entity.NarrationDetails = viewModel.NarrationDetails;
                    entity.TransactionType = viewModel.TransactionType;
                    entity.PaymentMode = viewModel.PaymentMode;
                    entity.Amount = viewModel.Amount;
                    entity.Balance = viewModel.Balance;
                    entity.TransactionDate = viewModel.TransactionDate;
                    entity.Remark = viewModel.Remark;
                    entity.CustomerName = viewModel.CustomerName;
                    entity.BankName = viewModel.BankName;
                    entity.ChqDDNumber = viewModel.ChqDDNumber;
                    entity.ContactNo = viewModel.ContactNo;

                    AccountBAL balObject = new AccountBAL();
                    balObject.Edit(entity);
                    TempData["Message"] = "Account updated successfully !!!";
                }
                else
                {
                    TempData["Error"] = "Some problem while updating account entry !!!";
                }
            }
            catch
            {
                TempData["Error"] = "Some problem while updating account entry !!!";
            }

            return View(viewModel);
        }
 public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                AccountBAL balObject = new AccountBAL();
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