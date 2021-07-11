using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2.Models
{
    public class signinModel
    {
        [Required]
        public string user_email { get; set; }
        [Required]
        public string user_password { get; set; }

        signinModel() { }

        signinModel(string user_email, string user_password)
        {
            this.user_email = user_email;
            this.user_password = user_password;
        }
    }
}