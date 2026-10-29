using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearch_App.Models
{
    public class JobApplicationCls
    {
        public JobApplicationCls()
        {
            selectjob = new List<jobList>();
            insertse = new jobList();
        }
        public jobList insertse { set; get; }
        public List<jobList> selectjob { set; get; }     
    }
    public class AppCls
    {
        public int JA_Id { get; set; }
        public int JS_Id { get; set; }
        public int Comp_Id { get; set; }
        public int Job_Id { get; set; }
        public System.DateTime JA_Date { get; set; }
        public string JA_Resume { get; set; }
        public int JA_Status { get; set; }
        public string JAMsg { get; set; }
    }
}