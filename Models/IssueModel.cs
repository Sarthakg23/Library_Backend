using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class IssueModel
    {
        public int? ISSUE_ID;
        public DateTime? ISSUE_DATE;
        public DateTime? RETURN_DATE;
        public double FINE;
        public int REISSUED;

        public IssueModel(int? ISSUE_ID,DateTime ISSUE_DATE, DateTime RETURN_DATE, double FINE, int REISSUED)
        {
            this.ISSUE_ID = ISSUE_ID;
            this.ISSUE_DATE = ISSUE_DATE;
            this.RETURN_DATE = RETURN_DATE;
            this.FINE = FINE;
            this.REISSUED = REISSUED;
        }
    }
}