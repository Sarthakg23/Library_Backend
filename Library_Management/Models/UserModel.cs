using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class UserModel
    {
        public int? user_id;
        public string user_name;
        public string user_email;
        public string user_password;
        public string user_gender;
        public string user_type;
        public int user_age;
        public DateTime? user_DOB;
        public string user_address;
        public string user_contact;

        public UserModel(int? user_id, string user_name, string user_email, string user_password, string user_gender, string user_type, int user_age, DateTime? user_DOB, string user_address, string user_contact)
        {
            this.user_id = user_id;
            this.user_name = user_name;
            this.user_email = user_email;
            this.user_password = user_password;
            this.user_gender = user_gender;
            this.user_type = user_type;
            this.user_age = user_age;
            this.user_DOB = user_DOB;
            this.user_address = user_address;
            this.user_contact = user_contact;
        }
    }
}