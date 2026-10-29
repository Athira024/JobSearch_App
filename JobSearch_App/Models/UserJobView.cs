using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearch_App.Models
{
    public class UserAppliedJobs
    {
        public List<UserJobView> appliedJobList { get; set; } = new List<UserJobView>();
    }
    public class UserJobView
    {      
        public string Comp_Name { get; set; }
        public string Job_Title { get; set; }
        public string Job_Location { get; set; }
        public DateTime JA_Date { get; set; }      
    }
}