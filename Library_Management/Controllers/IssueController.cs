using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Library_Management.Controllers
{
    public class IssueController : ApiController
    {
        DAL d = new DAL();

        
        [HttpGet]
        public HttpResponseMessage getAllIssues()
        {
            if (Request.Headers.Authorization == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            else
            {
                if (d.ValidateCurrentToken(Request.Headers.Authorization.Parameter))
                {
                    if (d.getAllIssues().Count == 0)
                        return Request.CreateResponse(HttpStatusCode.NotFound, "No Issues!");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, d.getAllIssues());
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
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
