using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class Mail
    {
        public string to;
        public string subject;
        public string message;

        public Mail(string to, string subject, string message)
        {
            this.to = to;
            this.subject = subject;
            this.message = message;
        }

        public Mail()
        {

        }
    }
}