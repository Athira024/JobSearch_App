using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobSearch_App.Models
{
    public class CompanyRegCls
    {
        [Required(ErrorMessage = "Enter company name")]
        public string Comp_Name { get; set; }
        [Required(ErrorMessage = "Enter company address")]
        public string Comp_Address { get; set; }
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string Comp_Email { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid number")]
        public string Comp_Phone { get; set; }
        [Required(ErrorMessage = "Enter the username")]
        public string Username { get; set; }
        public string Pwd { get; set; }
        [Compare("Pwd", ErrorMessage = "Password mismatch")]
        public string Cpwd { get; set; }
        public string CompRegMsg { get; set; }
    }
}