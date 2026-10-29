using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace JobSearch_App.Models
{
    public class AddJobCls
    {
        public int Job_Id { get; set; }

        public int Comp_Id { get; set; }

        [Required(ErrorMessage = "Enter job title")]
        public string Job_Title { get; set; }

        [Required(ErrorMessage = "Enter job skills")]
        public string Job_Skills { get; set; }

        [Required(ErrorMessage = "Enter qualification")]
        public string Job_Qualification { get; set; }

        [Required(ErrorMessage = "Enter experience")]
        public string Job_Experience { get; set; }

        [Required(ErrorMessage = "Enter salary")]
        public string Job_Salary { get; set; }

        [Required(ErrorMessage = "Enter job location")]
        public string Job_Location { get; set; }

        [Required(ErrorMessage = "Select start date")]
        [DataType(DataType.Date)]
        public DateTime Job_StartDate { get; set; }

        [Required(ErrorMessage = "Select end date")]
        [DataType(DataType.Date)]
        public DateTime Job_EndDate { get; set; }

        public string Job_Status { get; set; }

        public string JobMsg { get; set; }
    }
}