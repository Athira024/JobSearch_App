using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearch_App.Models
{
    public class EmployeeSearchCls
    {
        public EmployeeSearchCls()
        {
            inserte = new employeeList();
            selectemployee = new List<employeeList>();
        }
        public employeeList inserte { get; set; }
        public List<employeeList> selectemployee { get; set; }
    }
    public class employeeList
    {
        public int JS_Id { get; set; }
        public string JS_Name { get; set; }
        public int JS_Age { get; set; }
        public string JS_Phone { get; set; }
        public string JS_Email { get; set; }
        public string JS_Address { get; set; }
        public string JS_Qualification { get; set; }
        public string JS_Experience { get; set; }
        public string JS_Skills { get; set; }
        public string JS_Photo { get; set; }
    }
}