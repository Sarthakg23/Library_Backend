using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Management.Controllers
{
    public class BookController : ApiController
    {
        DAL d = new DAL();
        [HttpGet]
        public HttpResponseMessage getAllBooks()
        {
            if (d.getAllBooks().Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Books!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllBooks());
        }
    }
}
