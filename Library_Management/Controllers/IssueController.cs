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

        [HttpGet]
        [Route("api/issue/user/{userid}")]
        public HttpResponseMessage getAllIssuesByUser(int userid)
        {
            if (d.getAllIssuesByUser(userid).Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Issues!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllIssuesByUser(userid));
        }

        [HttpPut]
        [Route("api/issue/return/{id}")]
        public HttpResponseMessage returnBook(int id)
        {
            if (d.returnBook(id)==false)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Can't Return Contact Admin!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Return Successfull");
        }

        [HttpGet]
        [Route("api/issue/calculateFine")]
        public HttpResponseMessage calculateFine()
        {
            if (d.calculateFine().Count==0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No issue is over due!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.calculateFine());
        }
    }
}
