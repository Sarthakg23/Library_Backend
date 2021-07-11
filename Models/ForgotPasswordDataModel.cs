using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project2.Models
{
    public class ForgotPasswordDataModel
    {

        public string user_password;
        public DateTime? user_DOB;
        public string user_email;

        public ForgotPasswordDataModel(string user_password, DateTime? user_DOB, string user_email)
        {
            this.user_password = user_password;
            this.user_DOB = user_DOB;
            this.user_email = user_email;
        }
    }
        
}