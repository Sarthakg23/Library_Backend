using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class UserDataModel
    {
        public int? user_id;
        public string user_password;

        public UserDataModel(int? user_id, string user_password)
        {
            this.user_id = user_id;
            this.user_password = user_password;
        }
    }
}