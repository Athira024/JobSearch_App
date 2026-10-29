using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch_App.Models;

namespace JobSearch_App.Controllers
{
    public class EditJobDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: EditJobDB
        public ActionResult EditJob_Pageload()
        {
            int id = Convert.ToInt32(Session["uid"]);
            var getdata = dbobj.sp_SelectCompJob(id).ToList();
            return View(getdata);
        }
        public ActionResult Job_Update(int id)
        {
            var getdata = dbobj.Job_Tab.FirstOrDefault(j => j.Job_Id == id);
            AddJobCls obj = new AddJobCls
            {
                Job_Id = getdata.Job_Id,
                Job_Title = getdata.Job_Title,
                Job_Skills = getdata.Job_Skills,
                Job_Qualification = getdata.Job_Qualification,
                Job_Experience = getdata.Job_Experience,
                Job_Salary = getdata.Job_Salary,
                Job_Location = getdata.Job_Location,
                Job_StartDate = getdata.Job_StartDate,
                Job_EndDate = getdata.Job_EndDate
            };

            return View(obj);
        }
        public ActionResult Job_Update(AddJobCls obj)
        {
            if (ModelState.IsValid)
            {
                dbobj.sp_JobUpdate(
                    obj.Job_Id,
                    obj.Job_Title,
                    obj.Job_Skills,
                    obj.Job_Qualification,
                    obj.Job_Experience,
                    obj.Job_Salary,
                    obj.Job_Location,
                    obj.Job_StartDate,
                    obj.Job_EndDate
                );
                TempData["msg"] = "Job updated successfully!";
                return RedirectToAction("EditJob_Pageload");
            }
            return View(obj);
        }
    }
}