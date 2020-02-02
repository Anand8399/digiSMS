using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Other;
using myRes = PresentationLayer.LocalResource.Resource;
using PresentationLayer.Helpers;

namespace PresentationLayer.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeChartVM viewModel = new HomeChartVM();

            HomePageBAL balObject = new HomePageBAL();
          
            IQueryable<Entities.HomePage> entites1 = balObject.getClasswiseMaleFemaleList(SessionHelper.SchoolId);
            if (entites1 != null && entites1.Count() > 0)
            {
                Entities.HomePage entity = entites1.FirstOrDefault();
                viewModel.classList = entity.classList;
                viewModel.classwiseBoysList = entity.classwiseBoysList;
                viewModel.classwiseGirlsList = entity.classwiseGirlsList;

            }

            IQueryable<Entities.HomePage> entites = balObject.getChartData(SessionHelper.SchoolId);
            if (entites != null && entites.Count() > 0)
            {

                Entities.HomePage entity = entites.FirstOrDefault();

                viewModel.CastGeneralCount = entity.CastGeneralCount;
                viewModel.CastNT1Count = entity.CastNT1Count;
                viewModel.CastNT2Count = entity.CastNT2Count;
                viewModel.CastNT3Count = entity.CastNT3Count;
                viewModel.CastNT4Count = entity.CastNT4Count;

                viewModel.CastOBCCount = entity.CastOBCCount;
                viewModel.CastSBCCount = entity.CastSBCCount;
                viewModel.CastSCCount = entity.CastSCCount;
                viewModel.CastSTCount = entity.CastSTCount;
                viewModel.CastVJCount = entity.CastVJCount;
                viewModel.CastVJ1Count = entity.CastVJ1Count;

            }
            return View(viewModel);




        }

        public ActionResult About()
        {
            ViewBag.Message = myRes.Yourapplicationdescriptionpage;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = myRes.Yourcontactpage;

            return View();
        }
    }
}