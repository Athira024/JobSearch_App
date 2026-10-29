using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch_App.Models;

namespace JobSearch_App.Controllers
{
    public class JobSeekerRegDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: JobSeekerRegDB
        public ActionResult JSReg_Pageload()
        {
            JobSeekerRegCls obj = new JobSeekerRegCls();
            obj.MyQual = getQualificationData();
            return View(obj);
        }
        public List<CheckBoxListHelper> getQualificationData()
        {
            List<CheckBoxListHelper> sts = new List<CheckBoxListHelper>()
            {
                new CheckBoxListHelper {Value="SSLC",Text="SSLC",IsChecked=false},
                new CheckBoxListHelper {Value="PLUS TWO",Text="PLUS TWO",IsChecked=false},
                new CheckBoxListHelper {Value="BCA",Text="BCA",IsChecked=false},
                new CheckBoxListHelper {Value="MCA",Text="MCA",IsChecked=false},
                new CheckBoxListHelper {Value="BTECH",Text="BTECH",IsChecked=false},
            };
            return sts;
        }
        public ActionResult JSReg_Click(JobSeekerRegCls clsobj, HttpPostedFileBase file, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/JobSeekerPhotos");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~\\JobSeekerPhotos", fname);
                    clsobj.JS_Photo = fullpath;
                }
                var getmaxid = dbobj.sp_MaxIdLogin().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                var quid = string.Join(",", clsobj.selectedQual);
                clsobj.JS_Qualification = quid;
                clsobj.MyQual = getQualificationData();

                dbobj.sp_JobSeekerReg(regid, clsobj.JS_Name, clsobj.JS_Age, clsobj.JS_Phone, clsobj.JS_Email, clsobj.JS_Address, clsobj.JS_Qualification,
                    clsobj.JS_Experience, clsobj.JS_Skills, clsobj.JS_Photo, "Active");
                dbobj.sp_LogInsert(regid, clsobj.Username, clsobj.Pwd, "JobSeeker");
                clsobj.JSmsg = "Successfully inserted";
                return View("JSReg_Pageload", clsobj);
            }
            clsobj.MyQual = getQualificationData();
            return View("JSReg_Pageload", clsobj);
        }
    }
}