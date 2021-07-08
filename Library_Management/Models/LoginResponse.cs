using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class LoginResponse
    {
        public UserModel user;
        public string token;

        public LoginResponse(UserModel user, string token)
        {
            this.user = user;
            this.token = token;
        }
    }
}