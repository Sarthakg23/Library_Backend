using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class RequestModel
    {
        public int request_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> book_id { get; set; }
        public string request_status { get; set; }
        public Nullable<int> reIssue_id { get; set; }
        public Nullable<System.DateTime> request_date { get; set; }
        public Nullable<System.DateTime> request_approve_date { get; set; }

        public RequestModel(int request_id, int? user_id, int? book_id, string request_status, int? reIssue_id, DateTime? request_date, DateTime? request_approve_date)
        {
            this.request_id = request_id;
            this.user_id = user_id;
            this.book_id = book_id;
            this.request_status = request_status;
            this.reIssue_id = reIssue_id;
            this.request_date = request_date;
            this.request_approve_date = request_approve_date;
        }
    }
}