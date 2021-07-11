using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using project2.Models;

namespace project2.Controllers
{
    public class MailController : ApiController
    {
        [HttpPost]

        public HttpResponseMessage sendMail(MailModel mail)
        {
            MailAddress to = new MailAddress(mail.to);
            MailAddress from = new MailAddress("librarymanagment.app@gmail.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = mail.subject;
            message.Body = mail.message;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("librarymanagment.app@gmail.com", "Lm@12315"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required

            try
            {
                client.Send(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Mail Sent");
            }
            catch (SmtpException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
