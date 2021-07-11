using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project2.Models
{
    public class MailModel
    {
        public string to;
        public string subject;
        public string message;

        public MailModel(string to, string subject, string message)
        {
            this.to = to;
            this.subject = subject;
            this.message = message;
        }

        public MailModel()
        {

        }
    }
}