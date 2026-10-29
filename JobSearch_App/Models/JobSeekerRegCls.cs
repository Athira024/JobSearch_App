using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearch_App.Models
{
    public class CheckBoxListHelper
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class JobSeekerRegCls
    {
        public List<CheckBoxListHelper> MyQual { get; set; } 
        public string[] selectedQual { get; set; } 
        [Required(ErrorMessage = "Enter the name")]
        public string JS_Name { get; set; }
        [Range(18, 40, ErrorMessage = "Enter the age")]
        public int JS_Age { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid number")]
        public string JS_Phone { get; set; }
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string JS_Email { get; set; }
        [Required(ErrorMessage = "Enter the address")]
        public string JS_Address { get; set; }
        public string JS_Qualification { get; set; }
        public string JS_Experience { get; set; }
        public string JS_Skills { get; set; }
        public string JS_Photo { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
        [Compare("Pwd", ErrorMessage = "Password mismatch")]
        public string Cpwd { get; set; }
        public string JSmsg { get; set; }
    }
}