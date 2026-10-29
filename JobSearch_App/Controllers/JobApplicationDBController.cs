using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch_App.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.IO;

namespace JobSearch_App.Controllers
{
    public class JobApplicationDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: JobApplicationDB
        public ActionResult Application_load(int jid,int cid)
        {
            TempData["cid"] = cid;
            Session["jid"] = jid;
            TempData["jid"] = jid;
            ObjectParameter op = new ObjectParameter("status", typeof(int));
            int userid = Convert.ToInt32(Session["uid"]);
            int jobids = Convert.ToInt32(Session["jid"]);
            dbobj.sp_CountUserAppliedJob(userid, jobids, op);
            int countapply = Convert.ToInt32(op.Value);
            if(countapply==1)
            {
                ViewBag.isapplied = countapply;
            }
            return View(getdata());
        }
        public ActionResult Application_Click(HttpPostedFileBase file, AppCls clsobj)
        {
            if(ModelState.IsValid)
            {
                if(file.ContentLength>0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/ApplicationResume");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~/ApplicationResume", fname);
                    clsobj.JA_Resume = fullpath;
                }
                DateTime currentDate = DateTime.Today;
                clsobj.JA_Date = currentDate;
                clsobj.JS_Id = Convert.ToInt32(Session["uid"]);
                clsobj.Comp_Id = Convert.ToInt32(TempData["cid"]);
                int jobid = Convert.ToInt32(Session["jid"]);
                int status = 1;
                dbobj.sp_PostApplication(clsobj.JS_Id, clsobj.Comp_Id, jobid, clsobj.JA_Date, clsobj.JA_Resume, status);
                return RedirectToAction("JobSeekerHome", "LoginDB");
            }
            else
            {
                return View("Application_load", clsobj);
            }
        }
        private JobApplicationCls getdata()
        {
            using(var con=new SqlConnection(ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetJobDatawithId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jobid", TempData["jid"]);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var joblist = new JobApplicationCls();
                while(dr.Read())
                {
                    var jobcls = new jobList();
                    jobcls.Comp_Id= Convert.ToInt32(dr["Comp_Id"].ToString());
                    jobcls.Job_Title = dr["Job_Title"].ToString();
                    jobcls.Job_Skills = dr["Job_Skills"].ToString();
                    jobcls.Job_Qualification = dr["Job_Qualification"].ToString();
                    jobcls.Job_Experience = dr["Job_Experience"].ToString();
                    jobcls.Job_Salary = dr["Job_Salary"].ToString();
                    jobcls.Job_Location = dr["Job_Location"].ToString();
                    jobcls.Job_StartDate = Convert.ToDateTime(dr["Job_StartDate"].ToString());
                    jobcls.Job_EndDate = Convert.ToDateTime(dr["Job_EndDate"].ToString());
                    joblist.selectjob.Add(jobcls);
                }
                con.Close();
                return joblist;
            }
        }
    }
}