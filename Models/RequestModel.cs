using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class RequestModel
    {
        public int? request_id;
        public string request_status;
        public int reIssue_id;
        public DateTime request_date;
        public DateTime request_approve_date;

        public RequestModel( int? request_id, string request_status, int reIssue_id, DateTime request_date, DateTime request_approve_date)
        {
            this.request_id = request_id;
            this.request_status = request_status;
            this.reIssue_id = reIssue_id;
            this.request_date = request_date;
            this.request_approve_date = request_approve_date;
        }
    }
}