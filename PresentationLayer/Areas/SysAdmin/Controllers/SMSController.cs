using BusinessAccessLayer;
using PresentationLayer.Areas.SysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace PresentationLayer.Areas.SysAdmin.Controllers
{
    public class SMSController : Controller
    {
        //
        // GET: /SysAdmin/SMS/
        public ActionResult Index()
        {
            StudentVM viewModel = new StudentVM();

            ClassBAL classBAL = new ClassBAL();
            viewModel.Classes = from obj in classBAL.GetAll() where obj.Status == true select new SelectListItem() { Text = obj.ClassName, Value = obj.ClassId.ToString() };
           
            //ReligionBAL religionBAL = new ReligionBAL();
            //viewModel.Religions = from obj in religionBAL.GetAll().Where(c => c.Status == true) select new SelectListItem() { Text = obj.ReligionName, Value = obj.ReligionId.ToString() };

            viewModel.Status = true;
            return View(viewModel);
        }

        //
        // POST: /SMS/
        [HttpPost]
        public ActionResult Index(FormCollection formCollection, string message, string assignChkBx)
        {
            //string remark = string.Empty; string assignChkBx = string.Empty;
            //if (formCollection["Remark"] != null && formCollection["Remark"] != "")
            //{
            //    remark = formCollection["Remark"].ToString();
            //}            
            //if (formCollection["assignChkBx"] != null && formCollection["assignChkBx"] != "")
            //{
            //    assignChkBx = Convert.ToString(formCollection["assignChkBx"]);
            //}
            if(assignChkBx.EndsWith(","))
            {
                assignChkBx = assignChkBx.Remove(assignChkBx.Length - 1, 1);
            }
            // Read SMS provider link from config

            string SMSLink = ConfigurationManager.AppSettings["SMSLink"].ToString();

            SMSLink = SMSLink.Replace("message=[MESSAGE]", "message=" + message);
            SMSLink = SMSLink.Replace("numbers=[MOB_NUMBERS]", "numbers=" + assignChkBx);

            /*
            string sms = "http://www.envoyersms.in/sendSMS?username=endemo";
            sms += "&message=" + message + "&sendername=ENINFO&smstype=TRANS&numbers=" + assignChkBx + "&apikey=82a02280-406d-4337-972a-41e8087be26c";
            // enter sms code..
            */
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(SMSLink);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();

                //parse the response
                //"[{\"responseCode\":\"Message SuccessFully Submitted\"},{\"msgid\":\"2244765\"}]"

                respStreamReader.Close();
                myResp.Close();
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Connection can't be succefull !!!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult _GetStudentDetails( int ClassId, int DivisionId, int ReligionId, int CastId, bool Status)
        {
            List<StudentVM> viewModels = new List<StudentVM>();
            List<Entities.Student> studentEntites = new List<Entities.Student>();
            StudentBAL balObject = new StudentBAL();
            IQueryable<Entities.Student> entites = balObject.getStudentsWithBalance(ClassId, DivisionId, ReligionId, CastId);
            foreach (Entities.Student entity in entites)
            {
                viewModels.Add(PresentationLayer.Other.CommanMethods.getStudentViewModel(entity));
            }
            return PartialView("_GetStudentDetails", new Telerik.Web.Mvc.GridModel<StudentVM> { Data = viewModels });
        }
	}
}