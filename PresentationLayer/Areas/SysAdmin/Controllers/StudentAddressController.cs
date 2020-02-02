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
    public class StudentAddressController : Controller
    {
        //
        // GET: /SysAdmin/StudentAddress/
        [GridAction]
        public ActionResult Index()
        {
            List<StudentAddressVM> viewModels = new List<StudentAddressVM>();
            StudentAddressBAL balObject = new StudentAddressBAL();
            IQueryable<Entities.StudentAddress> entites = balObject.GetAll();

            CountryBAL countryBAL = new CountryBAL();
            IQueryable<Entities.Country> countries = countryBAL.GetAll();

            StateBAL stateBAL = new StateBAL();
            IQueryable<Entities.State> states = stateBAL.GetAll();

            DistrictBAL DistrictObject = new DistrictBAL();
            IQueryable<Entities.District> districts = DistrictObject.GetAll();

            CityBAL CityObject = new CityBAL();
            IQueryable<Entities.City> cities = CityObject.GetAll();

            StudentBAL studentObject = new StudentBAL();
            IQueryable<Entities.Student> students = studentObject.GetAll();


            foreach (Entities.StudentAddress entity in entites)
            {
                StudentAddressVM viewModel = new StudentAddressVM();
                viewModel.StudentId = entity.StudentId;
                viewModel.CurrentAddress = entity.CurrentAddress;
                viewModel.CurrentCountryId = entity.CurrentCountryId;
                viewModel.CurrentStateId = entity.CurrentStateId;
                viewModel.CurrentDistrictId = entity.CurrentDistrictId;
                viewModel.CurrentCityId = entity.CurrentCityId;
                viewModel.CurrentCountryName = countries.Where(c => c.CountryId == entity.CurrentCountryId).FirstOrDefault().CountryName;
                viewModel.CurrentStateName = states.Where(s => s.StateId == entity.CurrentStateId).FirstOrDefault().StateName;
                viewModel.CurrentDistrictName = districts.Where(c => c.DistrictId == entity.CurrentDistrictId).FirstOrDefault().DistrictName;

                viewModel.CurrentCityName = cities.Where(c => c.CityId == entity.CurrentDistrictId).FirstOrDefault().CityName;

                viewModel.PermentAddress = entity.PermentAddress;
                viewModel.PermentCountryId = entity.PermentCountryId;
                viewModel.PermentStateId = entity.PermentStateId;
                viewModel.PermentDistrictId = entity.PermentDistrictId;
                viewModel.PermentCityId = entity.PermentCityId;

                viewModel.PermentCountryName = countries.Where(c => c.CountryId == entity.PermentCountryId).FirstOrDefault().CountryName;
                viewModel.PermentStateName = states.Where(s => s.StateId == entity.PermentStateId).FirstOrDefault().StateName;
                viewModel.PermentDistrictName = districts.Where(c => c.DistrictId == entity.PermentDistrictId).FirstOrDefault().DistrictName;

                viewModel.PermentCityName = cities.Where(c => c.CityId == entity.PermentCityId).FirstOrDefault().CityName;

                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
                Entities.Student student = students.Where(s => s.StudentId == entity.StudentId).FirstOrDefault();
                viewModel.StudentFullNameWithTitle = string.Concat(student.Title, " ", student.FirstName, " ", student.MiddleName, " ", student.LastName);
                viewModels.Add(viewModel);
            }
            return View(new GridModel<StudentAddressVM> { Data = viewModels });
        }

        //
        // GET: /SysAdmin/Student/Details/5
        public ActionResult Details(int id)
        {
            StudentAddressVM viewModel = new StudentAddressVM();

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
                List<StudentAddressVM> viewModels = new List<StudentAddressVM>();
                StudentAddressBAL balObject = new StudentAddressBAL();
                IQueryable<Entities.StudentAddress> entites = balObject.GetAll();

                CountryBAL countryBAL = new CountryBAL();
                IQueryable<Entities.Country> countries = countryBAL.GetAll();

                StateBAL stateBAL = new StateBAL();
                IQueryable<Entities.State> states = stateBAL.GetAll();

                DistrictBAL DistrictObject = new DistrictBAL();
                IQueryable<Entities.District> districts = DistrictObject.GetAll();

                CityBAL CityObject = new CityBAL();
                IQueryable<Entities.City> cities = CityObject.GetAll();

                StudentBAL studentObject = new StudentBAL();
                IQueryable<Entities.Student> students = studentObject.GetAll();


                foreach (Entities.StudentAddress entity in entites)
                {
                    StudentAddressVM viewModel = new StudentAddressVM();
                    viewModel.StudentId = entity.StudentId;
                    viewModel.CurrentAddress = entity.CurrentAddress;
                    viewModel.CurrentCountryId = entity.CurrentCountryId;
                    viewModel.CurrentStateId = entity.CurrentStateId;
                    viewModel.CurrentDistrictId = entity.CurrentDistrictId;
                    viewModel.CurrentCityId = entity.CurrentCityId;
                    viewModel.CurrentCountryName = countries.Where(c => c.CountryId == entity.CurrentCountryId).FirstOrDefault().CountryName;
                    viewModel.CurrentStateName = states.Where(s => s.StateId == entity.CurrentStateId).FirstOrDefault().StateName;
                    viewModel.CurrentDistrictName = districts.Where(c => c.DistrictId == entity.CurrentDistrictId).FirstOrDefault().DistrictName;
                    viewModel.CurrentCityName = cities.Where(c => c.CityId == entity.CurrentCityId).FirstOrDefault().CityName;

                    viewModel.PermentAddress = entity.PermentAddress;
                    viewModel.PermentCountryId = entity.PermentCountryId;
                    viewModel.PermentStateId = entity.PermentStateId;
                    viewModel.PermentDistrictId = entity.PermentDistrictId;
                    viewModel.PermentCityId = entity.PermentCityId;

                    viewModel.PermentCountryName = countries.Where(c => c.CountryId == entity.PermentCountryId).FirstOrDefault().CountryName;
                    viewModel.PermentStateName = states.Where(s => s.StateId == entity.PermentStateId).FirstOrDefault().StateName;
                    viewModel.PermentDistrictName = districts.Where(c => c.DistrictId == entity.PermentDistrictId).FirstOrDefault().DistrictName;
                    viewModel.PermentCityName = cities.Where(c => c.CityId == entity.PermentCityId).FirstOrDefault().CityName;

                    viewModel.Status = entity.Status;
                    viewModel.Remark = entity.Remark;
                    Entities.Student student = students.Where(s => s.StudentId == entity.StudentId).FirstOrDefault();
                    viewModel.StudentFullNameWithTitle = string.Concat(student.Title, " ", student.FirstName, " ", student.MiddleName, " ", student.LastName);
                    viewModels.Add(viewModel);
                }
                return this.View("Index", new GridModel<StudentAddressVM> { Data = viewModels });
            }
        }

        //
        // GET: /SysAdmin/Student/Create
        public ActionResult Create()
        {
            StudentAddressVM viewModel = new StudentAddressVM();
            CountryBAL countryBAL = new CountryBAL();
            viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };

            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Student/Create
        [HttpPost]
        public ActionResult Create(StudentAddressVM viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Entities.StudentAddress entity = new Entities.StudentAddress();
                    entity.StudentId = viewModel.StudentId;
                    entity.CurrentAddress = viewModel.CurrentAddress;
                    entity.CurrentCountryId = viewModel.CurrentCountryId;
                    entity.CurrentStateId = viewModel.CurrentStateId;
                    entity.CurrentDistrictId = viewModel.CurrentDistrictId;
                    entity.CurrentCityId = viewModel.CurrentCityId;

                    entity.PermentAddress = viewModel.PermentAddress;
                    entity.PermentCountryId = viewModel.PermentCountryId;
                    entity.PermentStateId = viewModel.PermentStateId;
                    entity.PermentDistrictId = viewModel.PermentDistrictId;
                    entity.PermentCityId = viewModel.PermentCityId;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    StudentAddressBAL balObject = new StudentAddressBAL();
                    balObject.Add(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    CountryBAL countryBAL = new CountryBAL();
                    viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
                    return View(viewModel);
                }
            }
            catch
            {
                CountryBAL countryBAL = new CountryBAL();
                viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };
                return View(viewModel);
            }
        }

        //
        // GET: /SysAdmin/Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentAddressVM viewModel = new StudentAddressVM();
            CountryBAL countryBAL = new CountryBAL();
            viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };

            //Get Student Id
            StudentBAL balStudentObject = new StudentBAL();
            IQueryable<Entities.Student> studentEntities = balStudentObject.FindBy(s => s.SrNo == id);
            if (studentEntities != null && studentEntities.Count() >0)
            {
                Entities.Student studentEnity = studentEntities.FirstOrDefault();
                viewModel.StudentId = studentEnity.StudentId;
                viewModel.StudentFullNameWithTitle = string.Concat(studentEnity.Title.Trim(), " ", studentEnity.FirstName.Trim(), " ", studentEnity.MiddleName.Trim(), " ", studentEnity.LastName).Trim();
                viewModel.Status = true;
            }

            StudentAddressBAL balObject = new StudentAddressBAL();
            IQueryable<Entities.StudentAddress> entites = balObject.GetAll(viewModel.StudentId);
            if (entites != null && entites.Count() > 0)
            {
                Entities.StudentAddress entity = entites.FirstOrDefault();
                //viewModel.StudentId = entity.StudentId;
                //viewModel.StudentFullNameWithTitle = entity.StudentFullName;
                viewModel.CurrentAddress = entity.CurrentAddress;
                viewModel.CurrentCountryId = entity.CurrentCountryId;
                viewModel.CurrentStateId = entity.CurrentStateId;
                viewModel.CurrentDistrictId = entity.CurrentDistrictId;
                viewModel.CurrentCityId = entity.CurrentCityId;

                viewModel.PermentAddress = entity.PermentAddress;
                viewModel.PermentCountryId = entity.PermentCountryId;
                viewModel.PermentStateId = entity.PermentStateId;
                viewModel.PermentDistrictId = entity.PermentDistrictId;
                viewModel.PermentCityId = entity.PermentCityId;
                viewModel.CurrentPinCode = entity.CurrentPinCode;
                viewModel.PermentPinCode = entity.PermentPinCode;
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            //else
            //{
            //    viewModel.StudentId = id;
            //    viewModel.StudentFullNameWithTitle = PresentationLayer.Other.CommanMethods.GetStudentName(id);
            //    viewModel.Status = true;
            //}
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentAddressVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.StudentAddress entity = new Entities.StudentAddress();
                    entity.StudentId = viewModel.StudentId;
                    entity.CurrentAddress = viewModel.CurrentAddress;
                    entity.CurrentCountryId = viewModel.CurrentCountryId;
                    entity.CurrentStateId = viewModel.CurrentStateId;
                    entity.CurrentDistrictId = viewModel.CurrentDistrictId;
                    entity.CurrentCityId = viewModel.CurrentCityId;
                    entity.CurrentPinCode = viewModel.CurrentPinCode;

                    entity.PermentAddress = viewModel.PermentAddress;
                    entity.PermentCountryId = viewModel.PermentCountryId;
                    entity.PermentStateId = viewModel.PermentStateId;
                    entity.PermentDistrictId = viewModel.PermentDistrictId;
                    entity.PermentCityId = viewModel.PermentCityId;

                    entity.PermentPinCode = viewModel.PermentPinCode;
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    StudentAddressBAL balObject = new StudentAddressBAL();
                    IQueryable<Entities.StudentAddress> studentAddresses = balObject.FindBy(s => s.StudentId == viewModel.StudentId);
                    if (studentAddresses != null && studentAddresses.Count() > 0)
                    {
                        balObject.Edit(entity);
                    }
                    else
                    {
                        balObject.Add(entity);
                    }
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    CountryBAL countryBAL = new CountryBAL();
                    viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };

                    return View(viewModel);
                }
            }
            catch
            {
                CountryBAL countryBAL = new CountryBAL();
                viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };

                return View();
            }
        }


        //
        // POST: /SysAdmin/Student/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                StudentAddressBAL balObject = new StudentAddressBAL();
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
        /// <returns>state list</returns>
        public JsonResult GetStatesList(int CurrentCountryId)
        {
            StateBAL balObject = new StateBAL();
            var statesList = from obj in balObject.GetAll().Where(c => c.CountryId == CurrentCountryId && c.Status == true) select new SelectListItem() { Text = obj.StateName, Value = obj.StateId.ToString() };
            return this.Json(statesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get District list
        /// </summary>
        /// <returns>District list</returns>
        public JsonResult GetDistrictList(int CurrentStateId)
        {
            DistrictBAL balObject = new DistrictBAL();
            var districtList = from obj in balObject.GetAll().Where(c => c.StateId == CurrentStateId && c.Status == true) select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };
            return this.Json(districtList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get District list
        /// </summary>
        /// <returns>District list</returns>
        public JsonResult GetPermentDistrictList(int PermentStateId)
        {
            DistrictBAL balObject = new DistrictBAL();
            var districtList = from obj in balObject.GetAll().Where(c => c.StateId == PermentStateId && c.Status == true) select new SelectListItem() { Text = obj.DistrictName, Value = obj.DistrictId.ToString() };
            return this.Json(districtList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get state list
        /// </summary>
        /// <returns>state list</returns>
        public JsonResult GetPermentStatesList(int PermentCountryId)
        {
            StateBAL balObject = new StateBAL();
            var statesList = from obj in balObject.GetAll().Where(c => c.CountryId == PermentCountryId && c.Status == true) select new SelectListItem() { Text = obj.StateName, Value = obj.StateId.ToString() };
            return this.Json(statesList, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// method for get cities list
        /// </summary>
        /// <returns>Cities list</returns>
        public JsonResult GetCitiesList(int CurrentDistrictId)
        {

            CityBAL balObject = new CityBAL();
            var cityList = from obj in balObject.GetAll().Where(c => c.DistrictId == CurrentDistrictId && c.Status == true) select new SelectListItem() { Text = obj.CityName, Value = obj.CityId.ToString() };
            return this.Json(cityList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get cities list
        /// </summary>
        /// <returns>Cities list</returns>
        public JsonResult GetPermentCitiesList(int PermentDistrictId)
        {

            CityBAL balObject = new CityBAL();
            var cityList = from obj in balObject.GetAll().Where(c => c.DistrictId == PermentDistrictId && c.Status == true) select new SelectListItem() { Text = obj.CityName, Value = obj.CityId.ToString() };
            return this.Json(cityList, JsonRequestBehavior.AllowGet);
        }
    }
}