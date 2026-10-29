using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearch_App.Models
{
    public class Compclass
    {
        public int Comp_Id { get; set; }
        public string Comp_Name { get; set; }
    }
    public class JobSearchCls
    {
        public JobSearchCls()
        {
            selectjob = new List<jobList>();
            insertse = new jobList();
        }
        public jobList insertse { set; get; }
        public List<jobList> selectjob { set; get; }
    }
    public class jobList
    {
        public int Job_Id { get; set; }
        public int Comp_Id { get; set; }
        public string Comp_Name { get; set; }
        public string Job_Title { get; set; }
        public string Job_Skills { get; set; }
        public string Job_Qualification { get; set; }
        public string Job_Experience { get; set; }
        public string Job_Salary { get; set; }
        public string Job_Location { get; set; }
        public System.DateTime Job_StartDate { get; set; }
        public System.DateTime Job_EndDate { get; set; }
        public string Job_Status { get; set; }

    }
}