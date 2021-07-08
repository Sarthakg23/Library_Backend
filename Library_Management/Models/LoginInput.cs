using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class LoginInput
    {
        [Required]
        [EmailAddress]
        public string user_email;

        [Required]
        [MinLength(7)]
        public string user_password;
    }
}