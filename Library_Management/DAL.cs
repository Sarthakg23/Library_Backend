using Library_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management
{
    public class DAL
    {
        public DAL()
        {

        }

        public List<RequestModel> getAllRequests()
        {
            List<RequestModel> list = new List<RequestModel>();
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                foreach (Request req in entities.Requests)
                {
                    RequestModel r = new RequestModel(req.request_id, req.user_id, req.book_id, req.request_status, req.reIssue_id, req.request_date, req.request_approve_date);
                    list.Add(r);
                }
            }

            return list;
        }

        public Boolean cancelRequest(int id)
        {
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                Request req = entities.Requests.FirstOrDefault(r => r.request_id == id);
                if (req == null)
                    return false;
                else
                {
                    req.request_status = "Cancelled";
                    req.request_approve_date = DateTime.Now;
                    entities.SaveChanges();
                    return true;
                }

            }
        }

        public Boolean approveRequest(int id)
        {
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                Request req = entities.Requests.FirstOrDefault(r => r.request_id == id);
                if (req == null)
                    return false;
                else
                {
                    req.request_status = "Approved";
                    req.request_approve_date = DateTime.Now;
                    entities.SaveChanges();
                    if (req.reIssue_id == null)
                    {
                        ISSUE i = new ISSUE(req.request_id, DateTime.Now, DateTime.Now.AddDays(10), 0, 0);
                        entities.ISSUES.Add(i);
                        BOOK b = entities.BOOKs.FirstOrDefault(book => book.ID == req.book_id);
                        b.AVAILABLE_COPIES = b.AVAILABLE_COPIES - 1;
                        //var values = new Dictionary<string, string> { { "REQUEST_ID", req.request_id.ToString() }, { "ISSUE_DATE", DateTime.Now.ToString() }, { "RETURN_DATE", DateTime.Now.AddDays(10).ToString() }, { "FINE", 0.ToString() }, { "REISSUED", 0.ToString() } };
                        entities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        ISSUE i = entities.ISSUES.FirstOrDefault(issue => issue.ISSUE_ID == req.reIssue_id);
                        i.REISSUED = 1;
                        i.RETURN_DATE = DateTime.Now.AddDays(10);
                        entities.SaveChanges();
                        return true;
                    }
                }

            }
        }

        public List<RequestModel> getAllRequestsByUser(int id)
        {
            List<RequestModel> list = new List<RequestModel>();
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                List<Request> requests = entities.Requests.Where(r => r.user_id == id).ToList();
                foreach (Request req in requests)
                {
                    RequestModel r = new RequestModel(req.request_id, req.user_id, req.book_id, req.request_status, req.reIssue_id, req.request_date, req.request_approve_date);
                    list.Add(r);
                }
            }

            return list;
        }


        public List<IssueModel> getAllIssues()
        {
            List<IssueModel> list = new List<IssueModel>();

            using(Library_ManagementEntities entities=new Library_ManagementEntities())
            {
                foreach(ISSUE i in entities.ISSUES)
                {
                    list.Add(new IssueModel(i.ISSUE_ID, i.REQUEST_ID, i.ISSUE_DATE, i.RETURN_DATE, i.FINE, i.REISSUED));
                }
            }

            return list;
        }


        public Boolean generateRequest(Request req)
        {
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                    entities.Requests.Add(req);
                    entities.SaveChanges();
                    return true;
            }
        }


        public List<IssueModel> getAllIssuesByUser(int userId)
        {
            List<IssueModel> list = new List<IssueModel>();
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                List<Request> requests = entities.Requests.Where(r => r.user_id == userId && r.request_status == "Approved" && r.reIssue_id==null).ToList();
   
                foreach (Request req in requests)
                {
                    ISSUE i=entities.ISSUES.FirstOrDefault(issue => issue.REQUEST_ID == req.request_id);
                    list.Add(new IssueModel(i.ISSUE_ID, i.REQUEST_ID, i.ISSUE_DATE, i.RETURN_DATE, i.FINE, i.REISSUED));
           
                }
            }

            return list;
        }


        public Boolean returnBook(int id)
        {
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                ISSUE i = entities.ISSUES.FirstOrDefault(issue => issue.ISSUE_ID == id);
                Request r = entities.Requests.FirstOrDefault(req => req.request_id == i.REQUEST_ID);
                Request rissue = entities.Requests.FirstOrDefault(req => req.reIssue_id == i.ISSUE_ID);
                BOOK b = entities.BOOKs.FirstOrDefault(book => book.ID == r.book_id);

                if (i != null)
                    entities.ISSUES.Remove(i);
                if (r != null)
                    entities.Requests.Remove(r);
                if (rissue != null)
                    entities.Requests.Remove(rissue);

                
                b.AVAILABLE_COPIES = b.AVAILABLE_COPIES + 1;

            }

            return true;
        }
    }
    }