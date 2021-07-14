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
        dall d = new dall();
        [HttpGet]
        [Route("api/allRequests")]

        public IHttpActionResult requests()
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();

            List<Request> requestlist = new List<Request>();
            requestlist = lb.Requests.ToList();
            if (requestlist.Count == 0) return Content(HttpStatusCode.NotFound, "NOt have any requests");

            List<issueApproveModel> requestModelslist = new List<issueApproveModel>();
            foreach(var k in requestlist)
            {
                BOOK bk = lb.BOOKs.FirstOrDefault(obj => obj.ID == k.book_id);
                issueApproveModel rm = new issueApproveModel(k.request_id, k.user_id, k.book_id, k.request_status, k.reIssue_id, k.request_date, k.request_approve_date,bk.AVAILABLE_COPIES);
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
        public HttpResponseMessage ApproveRequest(int id)
        {
            if (d.approveRequest(id) == false)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Request Id!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Request Approved");

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

        [HttpPost]
        [Route("api/isRequested")]
        public HttpResponseMessage isrequestedOrNot(isRequestModel data)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            if (ModelState.IsValid)
            {
                Request request = lb.Requests.FirstOrDefault(d => d.book_id == data.book_id && d.user_id == data.user_id);
                if (request == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "new request");

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "allready requested");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "some is wrong please try again");
            }


        }

        [HttpPost]
        [Route("api/isapproved")]
        public HttpResponseMessage isapproved(isRequestModel data)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            if (ModelState.IsValid)
            {
                Request request = lb.Requests.FirstOrDefault(d => d.book_id == data.book_id && d.user_id == data.user_id && d.request_status=="pending");
                if (request == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Not");

                }
                else
                {
                    if (request.request_status == "approved")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "yes");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Not");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "some is wrong please try again");
            }


        }

        [HttpPost]
        [Route("api/requestByUserId/{id}")]
        public IHttpActionResult requestByUserId(int id)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            List<Request> rqlist = lb.Requests.ToList();
            if (rqlist.Count==0)
            {
                return Content(HttpStatusCode.NotFound, "No request found");
            }
            List<issueApproveModel> requestModelslist = new List<issueApproveModel>();
            foreach (var k in rqlist)
            {
                BOOK bk = lb.BOOKs.FirstOrDefault(obj => obj.ID == k.book_id);
                issueApproveModel rm = new issueApproveModel(k.request_id, k.user_id, k.book_id, k.request_status, k.reIssue_id, k.request_date, k.request_approve_date, bk.AVAILABLE_COPIES);
                requestModelslist.Add(rm);
            }
            if (requestModelslist.Count == 0) return Content(HttpStatusCode.NotFound, "NOt have any requests");
            return Ok(requestModelslist);
        }


        [HttpDelete]
        [Route("api/deletebyuser/{id}")]
        public IHttpActionResult deletebyuser(int id)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            Request rq = lb.Requests.FirstOrDefault(Id => Id.request_id == id);
            if (rq == null) return Content(HttpStatusCode.NotFound,"request is not found");
            lb.Requests.Remove(rq);
            lb.SaveChanges();
            return Content(HttpStatusCode.OK, "Cancel is successfull");

        }




    }
}
