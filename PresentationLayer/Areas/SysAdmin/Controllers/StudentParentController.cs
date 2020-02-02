using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using PresentationLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class StudentParentController : Controller
    {
        //
        // GET: /SysAdmin/StudentParent/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SysAdmin/Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentParentVM viewModel = new StudentParentVM();
            CountryBAL countryBAL = new CountryBAL();
            viewModel.Countries = from obj in countryBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.CountryName, Value = obj.CountryId.ToString() };

            //Get Student Id
            StudentBAL balStudentObject = new StudentBAL();
            IQueryable<Entities.Student> studentEntities = balStudentObject.FindBy(s => s.SrNo == id);
            if (studentEntities != null && studentEntities.Count() > 0)
            {
                Entities.Student studentEnity = studentEntities.FirstOrDefault();
                viewModel.StudentId = studentEnity.StudentId;
                viewModel.StudentFullNameWithTitle = string.Concat(studentEnity.Title.Trim(), " ", studentEnity.FirstName.Trim(), " ", studentEnity.MiddleName.Trim(), " ", studentEnity.LastName).Trim();
                viewModel.Status = true;
            }

            StudentParentBAL balObject = new StudentParentBAL();
            IQueryable<Entities.StudentParent> entites = balObject.FindBy(a => a.StudentId == viewModel.StudentId);
            if (entites != null && entites.Count() > 0)
            {
                Entities.StudentParent entity = entites.FirstOrDefault();
                viewModel.Title = entity.Title.Trim();
                viewModel.FirstName = entity.FirstName.Trim();
                viewModel.MiddleName = entity.MiddleName.Trim();
                viewModel.LastName = entity.LastName.Trim();
                viewModel.Gender = entity.Gender.Trim();
                viewModel.CurrentAddress = entity.Address.Trim();
                viewModel.CurrentCountryId = entity.CountryId;
                viewModel.CurrentStateId = entity.StateId;
                viewModel.CurrentDistrictId = entity.DistrictId;
                viewModel.CurrentCityId = entity.CityId;
                viewModel.CurrentPinCode = entity.PinCode;
                viewModel.MobileNo = entity.MobileNumber;
                viewModel.ContactNumber = entity.ContactNo;
                viewModel.Occupation = entity.Occupation.Trim();
                viewModel.CompanyName = entity.CompanyName.Trim();
                viewModel.CompanyAddress = entity.CompanyAddress.Trim();
                viewModel.CompanyContactNo = entity.CompanyContactNo.Trim();
                viewModel.BankName = entity.BankName.Trim();
                viewModel.AccountNo = entity.AccountNo.Trim();
                viewModel.Branch = entity.Branch.Trim();
                viewModel.IFSCCode = entity.IFSCCode.Trim();
               // viewModel.StudentFullNameWithTitle = entity.StudentFullName.Trim();
                viewModel.Status = entity.Status;
                viewModel.Remark = entity.Remark;
            }
            //else
            //{
                
            //    viewModel.StudentFullNameWithTitle = PresentationLayer.Other.CommanMethods.GetStudentName(id);
            //    viewModel.Status = true;
            //}
            return View(viewModel);
        }

        //
        // POST: /SysAdmin/Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentParentVM viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Entities.StudentParent entity = new Entities.StudentParent();
                    entity.StudentId = viewModel.StudentId;
                    entity.Title = viewModel.Title;
                    entity.FirstName = viewModel.FirstName;
                    entity.MiddleName = viewModel.MiddleName;
                    entity.LastName = viewModel.LastName;
                    entity.Gender = viewModel.Gender;
                    entity.Address = viewModel.CurrentAddress;
                    entity.CountryId = viewModel.CurrentCountryId;
                    entity.StateId = viewModel.CurrentStateId;
                    entity.DistrictId = viewModel.CurrentDistrictId;
                    entity.CityId = viewModel.CurrentCityId;
                    entity.PinCode = viewModel.CurrentPinCode.HasValue ? Convert.ToInt32(viewModel.CurrentPinCode) : 0;
                    entity.MobileNumber = viewModel.MobileNo;
                    entity.ContactNo = viewModel.ContactNumber;
                    entity.Occupation = viewModel.Occupation;
                    entity.CompanyName = viewModel.CompanyName;
                    entity.CompanyAddress = viewModel.CompanyAddress;
                    entity.CompanyContactNo = viewModel.CompanyContactNo;
                    entity.BankName = string.IsNullOrEmpty(viewModel.BankName) ? string.Empty : viewModel.BankName.Trim();
                    entity.AccountNo = string.IsNullOrEmpty(viewModel.AccountNo) ? string.Empty : viewModel.AccountNo.Trim();
                    entity.Branch = string.IsNullOrEmpty(viewModel.Branch) ? string.Empty : viewModel.Branch.Trim();
                    entity.IFSCCode = string.IsNullOrEmpty(viewModel.IFSCCode) ? string.Empty : viewModel.IFSCCode.Trim();
                    entity.Status = viewModel.Status;
                    entity.Remark = viewModel.Remark;

                    StudentParentBAL balObject = new StudentParentBAL();
                    IQueryable<Entities.StudentParent> StudentParents = balObject.FindBy(s => s.StudentId == viewModel.StudentId);
                    if (StudentParents != null && StudentParents.Count() > 0)
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
    }
}