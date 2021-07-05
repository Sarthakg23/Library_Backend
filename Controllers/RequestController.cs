using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project2.Models;


namespace project2.Controllers
{
    public class RequestController : ApiController
    {
        [HttpGet]
        [Route("api/allRequests")]

        public IHttpActionResult requests()
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();

            List<Request> requestlist = new List<Request>();
            requestlist = lb.Requests.ToList();
            if (requestlist.Count == 0) return Content(HttpStatusCode.NotFound, "NOt have any requests");

            List<requestModel> requestModelslist = new List<requestModel>();
            foreach(var k in requestlist)
            {
                requestModel rm = new requestModel(k.request_id, k.user_id, k.book_id, k.request_status, k.reIssue_id, k.request_date, k.request_approve_date);
                requestModelslist.Add(rm);
            }
            if(requestModelslist.Count==0) return Content(HttpStatusCode.NotFound, "NOt have any requests");
            return Ok(requestModelslist);
        }


        [HttpPost]
        [Route("api/requestBook")]
        public IHttpActionResult Requestbook(Request requestData)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            if (requestData==null)
            {
                return Content(HttpStatusCode.BadRequest, "Please enter the details for request a book");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    lb.Requests.Add(requestData);
                    lb.SaveChanges();
                    return Ok(requestData);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "some internal error occurs");
            }
        }

        [HttpPut]
        [Route("api/approveRequest/{id}")]
        public IHttpActionResult ApproveRequest(int id)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            Request request = lb.Requests.FirstOrDefault(ids => ids.request_id == id);
            if (request == null)
            {
                return Content(HttpStatusCode.BadRequest, "No request found with this request id");
            }

            try
            {
                request.request_status = "approved";
                request.request_approve_date = DateTime.Now;
                ISSUE issue = new ISSUE();
                issue.RETURN_DATE = DateTime.Now.AddDays(10);
                request.reIssue_id = issue.ISSUE_ID;
                lb.SaveChanges();

                requestModel requestmodel = new requestModel(request.request_id, request.user_id, request.book_id, request.request_status, request.reIssue_id, request.request_date, request.request_approve_date);

                return Ok(requestmodel);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
            
        }

        [HttpPut]
        [Route("api/cancelRequest/{id}")]
        public IHttpActionResult CancelRequest(int id)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            Request request = lb.Requests.FirstOrDefault(ids => ids.request_id == id && ids.request_status=="pending");
            if (request == null)
            {
                return Content(HttpStatusCode.BadRequest, "No request found with this request id");
            }

            try
            {
                request.request_status = "canceled";
                request.request_approve_date = DateTime.Now;
                lb.SaveChanges();

                requestModel requestmodel = new requestModel(request.request_id, request.user_id, request.book_id, request.request_status, request.reIssue_id, request.request_date, request.request_approve_date);

                return Ok(requestmodel);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }

    }
}
