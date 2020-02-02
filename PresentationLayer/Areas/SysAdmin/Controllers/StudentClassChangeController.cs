using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using myRes = PresentationLayer.LocalResource.Resource;
using PresentationLayer.Helpers;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class StudentClassChangeController : Controller
    {
        //
        // GET: /SysAdmin/StudentClassChange/
        public ActionResult Index()
        {
            StudentClassChangeVM viewModel = new StudentClassChangeVM();
            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection, StudentClassChangeVM viewModel)
        {

            if (formCollection["PreviousClassId"] != null && formCollection["PreviousClassId"] != "")
            {
                viewModel.PreviousClassId = Convert.ToInt32(formCollection["PreviousClassId"]);
            }
            if (formCollection["PreviousDivisionId"] != null && formCollection["PreviousDivisionId"] != "")
            {
                viewModel.PreviousDivisionId = Convert.ToInt32(formCollection["PreviousDivisionId"]);
            }
            if (viewModel.PreviousClassId > 0 && viewModel.PreviousDivisionId > 0)
            {
                ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                IQueryable<Entities.ClassDivision> classDivistions = classDivisionBAL.FindBy(cd => cd.ClassId == viewModel.PreviousClassId && cd.DivisionId == viewModel.PreviousDivisionId);
                if (classDivistions != null && classDivistions.Count() > 0)
                {
                    viewModel.PreviousClassDivisionId = classDivistions.FirstOrDefault().ClassDivisionId;
                }
            }


            if (formCollection["CurrentClassId"] != null && formCollection["CurrentClassId"] != "")
            {
                viewModel.CurrentClassId = Convert.ToInt32(formCollection["CurrentClassId"]);
            }
            if (formCollection["CurrentDivisionId"] != null && formCollection["CurrentDivisionId"] != "")
            {
                viewModel.CurrentDivisionId = Convert.ToInt32(formCollection["CurrentDivisionId"]);
            }
            if (viewModel.CurrentClassId > 0 && viewModel.CurrentDivisionId > 0)
            {
                ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
                IQueryable<Entities.ClassDivision> classDivistions = classDivisionBAL.FindBy(cd => cd.ClassId == viewModel.CurrentClassId && cd.DivisionId == viewModel.CurrentDivisionId);
                if (classDivistions != null && classDivistions.Count() > 0)
                {
                    viewModel.CurrentClassDivisionId = classDivistions.FirstOrDefault().ClassDivisionId;
                }
            }
            string assignChkBx = string.Empty;
            if (formCollection["assignChkBx"] != null && formCollection["assignChkBx"] != "")
            {
                assignChkBx = Convert.ToString(formCollection["assignChkBx"]);
            }
            
            if (!string.IsNullOrEmpty(assignChkBx))
            {
                viewModel.StudentClassChangeSubList = new List<StudentClassChangeSubVM>();
                string[] strArrFeeId = assignChkBx.Split(',');

                if (strArrFeeId.Length > 0)
                {
                    for (int i = 0; i < strArrFeeId.Length; i++)
                    {
                        StudentClassChangeSubVM studentClassChangeSubVM = new StudentClassChangeSubVM();
                        studentClassChangeSubVM.StudentId = Convert.ToInt32(strArrFeeId[i]);
                        viewModel.StudentClassChangeSubList.Add(studentClassChangeSubVM);
                    }
                }
            }

            //if (viewModel.StudentClassChangeSubList != null)
            //{
            //    viewModel.NoOfSelectedRecords = viewModel.StudentClassChangeSubList.Count;
            //}

            TryUpdateModel<StudentClassChangeVM>(viewModel);
                
            if(ModelState.IsValid)
            {
                Entities.StudentClassChange entity = new Entities.StudentClassChange();

                entity.PreviousClassDivisionId = viewModel.PreviousClassDivisionId;

                entity.CurrentClassDivisionId = viewModel.CurrentClassDivisionId;
                entity.Remark = myRes.Studenttransferfrompreviousclass;
                entity.SelectedStudent = new int[viewModel.StudentClassChangeSubList.Count()];
                int i = 0;
                foreach (var item in viewModel.StudentClassChangeSubList)
                {
                    entity.SelectedStudent[i] = item.StudentId;
                    i++;
                }

                StudentBAL balObject = new StudentBAL();
                balObject.StudentChangeClassDivision(entity, SessionHelper.SchoolId);
            }

            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
             return View(viewModel);
        }
        /// <summary>
        /// method for get classes list
        /// </summary>
        /// <returns>classes list</returns>
        public JsonResult GetClassesList()
        {
            ClassBAL classBAL = new ClassBAL();
            var classesList = from obj in classBAL.GetAll() select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
            return this.Json(classesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get divisions list
        /// </summary>
        /// <returns>divisions list</returns>
        public JsonResult GetDivisionsList(int PreviousClassId)
        {
            ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> classDivisions = classDivisionBAL.FindBy(cd => cd.ClassId == PreviousClassId);
            var divisionIds = from classDivisionObj in classDivisions select classDivisionObj.DivisionId;
            DivisionBAL divisionsBAL = new DivisionBAL();
            var divisions = divisionsBAL.GetAll().Where(d => d.Status == true);
            var divisionesList = from obj in divisions from obj1 in divisionIds where obj.DivisionId == (obj1) select new SelectListItem() { Text = obj.DivisionName, Value = obj.DivisionId.ToString() };
            return this.Json(divisionesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get classes list
        /// </summary>
        /// <returns>classes list</returns>
        public JsonResult GetCurrentClassesList()
        {
            ClassBAL classBAL = new ClassBAL();
            var classesList = from obj in classBAL.GetAll() select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
            return this.Json(classesList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for get divisions list
        /// </summary>
        /// <returns>divisions list</returns>
        public JsonResult GetCurrentDivisionsList(int CurrentClassId)
        {
            ClassDivisionBAL classDivisionBAL = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> classDivisions = classDivisionBAL.FindBy(cd => cd.ClassId == CurrentClassId);
            var divisionIds = from classDivisionObj in classDivisions select classDivisionObj.DivisionId;
            DivisionBAL divisionsBAL = new DivisionBAL();
            var divisions = divisionsBAL.GetAll().Where(d => d.Status == true);
            var divisionesList = from obj in divisions from obj1 in divisionIds where obj.DivisionId == (obj1) select new SelectListItem() { Text = obj.DivisionName, Value = obj.DivisionId.ToString() };
            return this.Json(divisionesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _GetStudentDetails( int ClassId, int DivisionId)
        {
            List<StudentClassChangeSubVM> studentClassChangeSubVMs = new List<StudentClassChangeSubVM>();
            int ClassDivisionId = 0;
            ClassDivisionBAL balObject = new ClassDivisionBAL();
            IQueryable<Entities.ClassDivision> ClassDivisions = balObject.FindBy(cd => cd.ClassId == ClassId && cd.DivisionId == DivisionId);
            if (ClassDivisions != null && ClassDivisions.Count() > 0)
            {
                ClassDivisionId = ClassDivisions.FirstOrDefault().ClassDivisionId;
            }
            StudentBAL studentBAL = new StudentBAL();
            IQueryable<Entities.StudentClassChange> studentClassChanges = studentBAL.GetStudentClassChange( ClassDivisionId, SessionHelper.SchoolId);
            if (studentClassChanges != null && studentClassChanges.Count() > 0)
            {
                foreach (Entities.StudentClassChange studentLedgersGroupItem in studentClassChanges)
                {
                    StudentClassChangeSubVM studentTransactionSubVM = new StudentClassChangeSubVM();
                    studentTransactionSubVM.StudentId = studentLedgersGroupItem.StudentId;
                    studentTransactionSubVM.RegisterId = studentLedgersGroupItem.RegisterId;
                    studentTransactionSubVM.StudentFullNameWithTitle = studentLedgersGroupItem.StudentFullNameWithTitle;
                    studentTransactionSubVM.CheckedValues = true;
                    studentClassChangeSubVMs.Add(studentTransactionSubVM);
                }
            }
            return PartialView("_GetStudentDetails", new GridModel<StudentClassChangeSubVM> { Data = studentClassChangeSubVMs });
            //return PartialView("_GetStudentsList", studentClassChanges.AsEnumerable());
        }
    }
}