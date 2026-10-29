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
    public class SearchEmployeeDBController : Controller
    {
        JobSearchEntities dbobj = new JobSearchEntities();
        // GET: SearchEmployeeDB
        public ActionResult SearchEmp_Pageload()
        {
            var obj = new EmployeeSearchCls();

            var emp = dbobj.JobSeeker_Reg
                            .Where(x => x.JS_Status == "Active")
                            .ToList();

            foreach (var item in emp)
            {
                var empcls = new employeeList();

                empcls.JS_Id = item.JS_Id;
                empcls.JS_Name = item.JS_Name;
                empcls.JS_Age = item.JS_Age;
                empcls.JS_Phone = item.JS_Phone;
                empcls.JS_Email = item.JS_Email;
                empcls.JS_Address = item.JS_Address;
                empcls.JS_Qualification = item.JS_Qualification;
                empcls.JS_Experience = item.JS_Experience;
                empcls.JS_Skills = item.JS_Skills;
                empcls.JS_Photo = item.JS_Photo;

                obj.selectemployee.Add(empcls);
            }

            return View(obj);
        }
        public ActionResult searchemployee_click(EmployeeSearchCls clsobj)
        {
            string qry = "";

            if (clsobj.inserte != null)
            {
                if (!string.IsNullOrWhiteSpace(clsobj.inserte.JS_Qualification))
                {
                    qry += " and JS_Qualification like '%"
                        + clsobj.inserte.JS_Qualification + "%'";
                }

                if (!string.IsNullOrWhiteSpace(clsobj.inserte.JS_Experience))
                {
                    qry += " and JS_Experience like '%"
                        + clsobj.inserte.JS_Experience + "%'";
                }

                if (!string.IsNullOrWhiteSpace(clsobj.inserte.JS_Skills))
                {
                    qry += " and JS_Skills like '%"
                        + clsobj.inserte.JS_Skills + "%'";
                }
            }

            return View("SearchEmp_Pageload", getemployeedata(clsobj, qry));
        }

        // Get employee data
        private EmployeeSearchCls getemployeedata(EmployeeSearchCls clsobj, string qry)
        {
            EmployeeSearchCls employeelist = new EmployeeSearchCls();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var emplist = new EmployeeSearchCls();
                while (dr.Read())
                        {
                            employeeList emp = new employeeList();

                            emp.JS_Id =
                                Convert.ToInt32(dr["JS_Id"]);
                            emp.JS_Name =
                                dr["JS_Name"].ToString();
                            emp.JS_Age =
                                Convert.ToInt32(dr["JS_Age"]);
                            emp.JS_Phone =
                                dr["JS_Phone"].ToString();
                            emp.JS_Email =
                                dr["JS_Email"].ToString();
                            emp.JS_Skills =
                                dr["JS_Skills"].ToString();
                            emp.JS_Qualification =
                                dr["JS_Qualification"].ToString();
                            emp.JS_Experience =
                                dr["JS_Experience"].ToString();
                            emp.JS_Address =
                                dr["JS_Address"].ToString();
                            emp.JS_Photo = 
                                dr["JS_Photo"].ToString();
                    employeelist.selectemployee.Add(emp);
                        }
                con.Close();
                return employeelist;
            }           
        }
    }
}