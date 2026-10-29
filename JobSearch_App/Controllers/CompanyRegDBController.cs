using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch_App.Models;

namespace JobSearch_App.Controllers
{
    public class CompanyRegDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: CompanyRegDB
        public ActionResult CompReg_Pageload()
        {
            return View();
        }
        public ActionResult CompReg_Click(CompanyRegCls clsobj)
        {
            if (ModelState.IsValid)
            {
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
                dbobj.sp_CompanyReg(regid, clsobj.Comp_Name, clsobj.Comp_Address, clsobj.Comp_Email, clsobj.Comp_Phone);
                dbobj.sp_LogInsert(regid, clsobj.Username, clsobj.Pwd, "Company");
                clsobj.CompRegMsg = "Successfully inserted";
                return View("CompReg_Pageload", clsobj);
            }
            return View("CompReg_Pageload", clsobj);
        }
    }
}