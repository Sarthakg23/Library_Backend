using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project2.Models;

namespace project2.Controllers
{
    
    public class issueController : ApiController
    {

        

        [HttpGet]
        [Route("api/issuelist")]
        public IHttpActionResult Issuedata()
        {
            
            Library_ManagementEntities lb = new Library_ManagementEntities();
            List<ISSUE> issuelist = new List<ISSUE>();
            issuelist = lb.ISSUES.ToList();

            List<IssueModel> issueModelList = new List<IssueModel>();

            if (issuelist.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, "issue are there");
            }

            foreach (var k in issuelist)
            {
                IssueModel im = new IssueModel(k.ISSUE_ID, k.REQUEST_ID, k.ISSUE_DATE, k.RETURN_DATE, k.FINE, k.REISSUED);
                issueModelList.Add(im);
            }
            //IssueModel issueModelList = new IssueModel(issuelist[0].ISSUE_ID, issuelist[0].REQUEST_ID, issuelist[0].ISSUE_DATE, issuelist[0].RETURN_DATE, issuelist[0].FINE, issuelist[0].REISSUED);



            return Ok(issueModelList);
            
        }


        [HttpPost]
        [Route("api/issuebook")]
        public IHttpActionResult bookissue(ISSUE data)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            if (data==null)
            {
                return Content(HttpStatusCode.NotFound, "Please enter values for issue");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    lb.ISSUES.Add(data);
                    lb.SaveChanges();
                    return Ok(data);
                    
                    


                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Please enter valid data");
                }

            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "some error occures");

            }

        }


        [HttpGet]
        [Route("api/issueById/{id}")]
        public IHttpActionResult issuebyid(int id)
        {
           
            Library_ManagementEntities lb = new Library_ManagementEntities();
            ISSUE issuedata= lb.ISSUES.FirstOrDefault(issueid => issueid.ISSUE_ID == id);
            if (issuedata == null)
            {
                return Content(HttpStatusCode.NotFound, "Your id is wrong please check the id");
            }
            IssueModel issueModel = new IssueModel(issuedata.ISSUE_ID, issuedata.REQUEST_ID, issuedata.ISSUE_DATE, issuedata.RETURN_DATE, issuedata.FINE, issuedata.REISSUED);

           


            return Ok(issueModel);
        }




    }
}
