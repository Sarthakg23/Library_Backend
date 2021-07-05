using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Management.Controllers
{
    public class IssueController : ApiController
    {
        DAL d = new DAL();

        [HttpGet]
        public HttpResponseMessage getAllIssues()
        {
            if (d.getAllIssues().Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Issues!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllIssues());
        }
    }
}
