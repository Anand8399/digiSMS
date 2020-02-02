using BusinessAccessLayer;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppLib;
using myRes = PresentationLayer.LocalResource.Resource;

namespace PresentationLayer.Controllers
{
    public class LogInController : Controller
    {
        //
        // GET: /LogIn/
        public ActionResult Index()
        {
            LoginUserVM entity = new LoginUserVM();
            //without db
            //entity.UserId = "admin";
            //entity.Password = "a";
            //entity.Roles = new List<SelectListItem> {  new SelectListItem() { Text = "Admin", Value = "Admin" }};
            RoleBAL balObject = new RoleBAL();
            entity.Roles = from obj in balObject.GetAll().Where(r => r.Status == true) select new SelectListItem() { Text = obj.RoleName, Value = obj.Id.ToString() };
            ViewBag.AdminRoleId = entity.Roles.Where(r => r.Text == "Admin").FirstOrDefault().Value;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Index(LoginUserVM entity)
        {
            int iRet = 0;
           
           // iRet = ApplicationCore.VerifyLic(@"C:\inetpub\license.lic");

            if (iRet != 0)
            {
                if (iRet == -1)
                {
                    ViewBag.Error = myRes.Licensedatehaspassed;
                }
                else if (iRet == -4)
                {
                    ViewBag.Error = myRes.Licensecopynotfound;
                }
                else
                    ViewBag.Error = myRes.Thelicensecopyisnotvalid;

            }

            if (ModelState.IsValid && iRet == 0)
            {

                UserBAL balObject = new UserBAL();
                int schoolId = 0;
                //var roles = from obj in roleObject.GetAll().Where(r => r.Status == true) select new SelectListItem() { Text = obj.RoleName, Value = obj.Id.ToString() };
                //var roleEntities = roles.Where(r => r.Value == entity.RoleId.ToString());
                //if (roleEntities != null && roleEntities.Count() > 0)
                //{ 
                //}
                //balObject.OledbConnectionString = ConfigurationManager.ConnectionStrings["OLEDbConnection"].ToString();
                var entites = balObject.GetAll().Where(u => u.RoleId == entity.RoleId && u.UserId.Trim() == entity.UserId.Trim() && u.Password.Trim() == entity.Password.Trim());

                if (entites != null)
                {
                    if (entites.Count() > 0)
                    {
                        Entities.User userEntity = entites.FirstOrDefault();
                        SessionHelper.UserId = entity.UserId;
                        SessionHelper.Username = userEntity.UserName;
                        SessionHelper.Title = userEntity.UserRole.RoleName;
                        SessionHelper.IsAuthenticated = true;

                        // get School Information
                        schoolId = userEntity.SchoolId;
                        SchoolDetailsBAL balObj = new SchoolDetailsBAL();
                        var entity1 = balObj.GetAll().Where(u => u.SchoolId == schoolId);
                        if (entity1 != null)
                        {
                            if (entity1.Count() > 0)
                            {
                                Entities.SchoolDetails userEntity1 = entity1.FirstOrDefault();

                                SessionHelper.SchoolName = userEntity1.SchoolName.Trim();
                                SessionHelper.SchoolId = userEntity1.SchoolId;
                                SessionHelper.LogoPath = userEntity1.LogoPath.Trim();

                            }
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }

               
                ViewBag.Error = myRes.TheUserDoesNotExistOrTheProvidedUsernameOrPasswordIsIncorrect;
            }

            RoleBAL roleObject = new RoleBAL();
            entity.Roles = from obj in roleObject.GetAll().Where(r => r.Status == true) select new SelectListItem() { Text = obj.RoleName, Value = obj.Id.ToString() };
            ViewBag.AdminRoleId = entity.Roles.Where(r => r.Text == "Admin").FirstOrDefault().Value;

            return View(entity);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            this.Session.RemoveAll();
            this.Session.Clear();
            this.Session.Abandon();
            return RedirectToAction("Index", "Login", new { area = "" });
        }

        public ActionResult ChangePassword()
        {
            ChangePasswordVM entity = new ChangePasswordVM();
            entity.UserList = PresentationLayer.Other.CommanMethods.GetUserList();
            return View(entity);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM entity)
        {
            if (ModelState.IsValid)
            {
                bool blnValidUser = PresentationLayer.Other.CommanMethods.GetValidUser(entity.UserId, entity.OldPassword);
                if (!blnValidUser)
                {
                    ViewBag.Error = myRes.TheProvidedUserNameOrPasswordIsIncorrect;
                    entity.UserList = PresentationLayer.Other.CommanMethods.GetUserList();
                    return View(entity);
                }

                UserBAL userBAL = new UserBAL();
                Entities.User user = new Entities.User();
                user.UserId = entity.UserId;
                user.Password = entity.NewPassword;
                userBAL.ChangePassword(user);
                ViewBag.Error = myRes.Passwordchangedsuccessfully;
                //return RedirectToAction("Index");
            }
            entity.UserList = PresentationLayer.Other.CommanMethods.GetUserList();
            return View(entity);
        }
    }
}