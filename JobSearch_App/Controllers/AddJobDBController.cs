using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch_App.Models;

namespace JobSearch_App.Controllers
{
    public class AddJobDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: AddJobDB
        public ActionResult AddJob_Pageload()
        {

            return View();
        }
        public ActionResult AddJob_Click(AddJobCls clsobj)
        {
            if (ModelState.IsValid)
            {
                int compid = Convert.ToInt32(Session["uid"]);
                dbobj.sp_JobInsert(compid, clsobj.Job_Title, clsobj.Job_Skills, clsobj.Job_Qualification, clsobj.Job_Experience, clsobj.Job_Salary, 
                    clsobj.Job_Location, clsobj.Job_StartDate, clsobj.Job_EndDate, "Open");
                clsobj.JobMsg = "Successfully inserted";
                return View("AddJob_Pageload", clsobj);
            }
            return View("AddJob_Pageload", clsobj);
        }
    }
}