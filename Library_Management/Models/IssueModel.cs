using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class IssueModel
    {
        public int ISSUE_ID { get; set; }
        public Nullable<int> REQUEST_ID { get; set; }
        public System.DateTime ISSUE_DATE { get; set; }
        public System.DateTime RETURN_DATE { get; set; }
        public Nullable<float> FINE { get; set; }
        public int REISSUED { get; set; }

        public IssueModel(int iSSUE_ID, int? rEQUEST_ID, DateTime iSSUE_DATE, DateTime rETURN_DATE, float? fINE, int rEISSUED)
        {
            ISSUE_ID = iSSUE_ID;
            REQUEST_ID = rEQUEST_ID;
            ISSUE_DATE = iSSUE_DATE;
            RETURN_DATE = rETURN_DATE;
            FINE = fINE;
            REISSUED = rEISSUED;
        }
    }
}