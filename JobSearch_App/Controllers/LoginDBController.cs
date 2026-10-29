using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch_App.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace JobSearch_App.Controllers
{
    public class LoginDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: LoginDB
        public ActionResult Login_Pageload()
        {
            return View();
        }
        public ActionResult CompanyHome()
        {
            return View();
        }
        public ActionResult JobSeekerHome()
        {
            UpdateExpiredJobs();
            var obj = new JobSearchCls();
            var jobs = dbobj.sp_SelectAllJob().ToList();
            foreach (var item in jobs)
            {
                var jobcls = new jobList();
                jobcls.Job_Id = item.Job_Id;
                jobcls.Comp_Id = item.Comp_Id;
                jobcls.Comp_Name = item.Comp_Name;
                jobcls.Job_Title = item.Job_Title;
                jobcls.Job_Skills = item.Job_Skills;
                jobcls.Job_Qualification = item.Job_Qualification;
                jobcls.Job_Experience = item.Job_Experience;
                jobcls.Job_Salary = item.Job_Salary;
                jobcls.Job_Location = item.Job_Location;
                jobcls.Job_StartDate = item.Job_StartDate;
                jobcls.Job_EndDate = item.Job_EndDate;
                jobcls.Job_Status = item.Job_Status;
                obj.selectjob.Add(jobcls);
            }
            return View(obj);
        }
        private void UpdateExpiredJobs()
        {
            DateTime today = DateTime.Today;
            var expiredJobs = dbobj.Job_Tab.Where(x => x.Job_EndDate < today && x.Job_Status == "Open").ToList();
            foreach (var job in expiredJobs)
            {
                job.Job_Status = "Closed";
            }
            if (expiredJobs.Count > 0)
            {
                dbobj.SaveChanges();
            }
        }
        public ActionResult Login_Click(LoginCls clsobj)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.sp_CountLoginId(clsobj.Username, clsobj.Pwd).First();
                if (val == 1)
                {
                    var uid = dbobj.sp_LoginId(clsobj.Username, clsobj.Pwd).FirstOrDefault();
                    Session["uid"] = uid;
                    var lt = dbobj.sp_LoginType(clsobj.Username, clsobj.Pwd).FirstOrDefault();
                    if (lt == "Company")
                    {
                        return RedirectToAction("CompanyHome");
                    }
                    else if (lt == "JobSeeker")
                    {
                        return RedirectToAction("JobSeekerHome");
                    }
                }
                else
                {
                    ModelState.Clear();
                    clsobj.Logmsg = "Invalid username and password";
                    return View("Login_Pageload", clsobj);
                }
            }
            else
            {
                ModelState.Clear();
                return View("Login_Pageload", clsobj);
            }
            return View("Login_Pageload", clsobj);

        }

        public ActionResult searchjob_click(JobSearchCls clsobj)
        {
            string qry = "";
            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Comp_Name))
            {
                qry += " and Comp_Name like '%" + clsobj.insertse.Comp_Name + "%'";
            }
            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Title))
            {
                qry += " and Job_Title like '%" + clsobj.insertse.Job_Title + "%'";
            }
            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Skills))
            {
                qry += " and Job_Skills like '%" + clsobj.insertse.Job_Skills + "%'";
            }
            var jobResult = getdata(clsobj, qry);
            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Experience) && int.TryParse(clsobj.insertse.Job_Experience.Trim(), out int searchExp))
            {
                jobResult.selectjob = jobResult.selectjob .Where(j => MatchExperience(j.Job_Experience, searchExp)).ToList();
            }
            return View("JobSeekerHome", jobResult);
        }
        private bool MatchExperience(string dbExperience, int searchExp)
        {
            if (string.IsNullOrEmpty(dbExperience)) return false;

            var numbers = System.Text.RegularExpressions.Regex.Matches(dbExperience, @"\d+")
                            .Cast<System.Text.RegularExpressions.Match>()
                            .Select(m => int.Parse(m.Value))
                            .ToList();

            if (numbers.Count >= 2)
            {
                return searchExp >= numbers[0] && searchExp <= numbers[1]; 
            }
            else if (numbers.Count == 1)
            {
                return searchExp >= numbers[0];
            }
            return false;
        }
        private JobSearchCls getdata(JobSearchCls clsobj, string qry)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_JobSearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var joblist = new JobSearchCls();
                while (dr.Read())
                {
                    var jobcls = new jobList();
                    jobcls.Job_Id = Convert.ToInt32(dr["Job_Id"].ToString());
                    jobcls.Comp_Id = Convert.ToInt32(dr["Comp_Id"].ToString());
                    jobcls.Comp_Name= dr["Comp_Name"].ToString();
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
        public ActionResult MyApplications()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login_Pageload");
            }
            int userId = Convert.ToInt32(Session["uid"]);
            var obj = new UserAppliedJobs();
            var result = dbobj.sp_User_JobView(userId).ToList();
            foreach (var item in result)
            {
                var jobView = new UserJobView
                {
                    Comp_Name = item.Comp_Name,
                    Job_Title = item.Job_Title,
                    Job_Location = item.Job_Location,
                    JA_Date = item.JA_Date
                };
                obj.appliedJobList.Add(jobView);
            }

            return View(obj);
        }
    }
}