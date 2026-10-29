using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearch_App.Models
{
    public class LoginCls
    {
        [Required(ErrorMessage = "Enter the username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter the password")]
        public string Pwd { get; set; }
        public string Logmsg { get; set; }
    }
}