using Library_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Management.Controllers
{
    public class RequestController : ApiController
    {
        DAL d = new DAL();

        [HttpGet]
        public HttpResponseMessage getAllRequests()
        {
            if (d.getAllRequests().Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Requests!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllRequests());
        }

        [HttpGet]
        [Route("api/request/user/{userid}")]
        public HttpResponseMessage getAllRequestsByUserId(int userid)
        {
            if (d.getAllRequestsByUser(userid).Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Requests!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllRequestsByUser(userid));
        }



        [HttpPut]
        [Route("api/request/cancel/{id}")]
        public HttpResponseMessage cancelRequest(int id)
        {
            if (d.cancelRequest(id) == false)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Request Id!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Request Cancelled");
        }


        [HttpPut]
        [Route("api/request/approve/{id}")]
        public HttpResponseMessage approveRequest(int id)
        {
            if (d.approveRequest(id) == false)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Request Id!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Request Approved");
        }

        [HttpPost]
        public HttpResponseMessage generateRequest(Request req)
        {
            if(d.generateRequest(req)==true)
                return Request.CreateResponse(HttpStatusCode.OK, "Request Generated");
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Request Not Generated!");
        }

    }
}
