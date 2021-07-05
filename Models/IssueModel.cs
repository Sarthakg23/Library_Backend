using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2.Models
{
    public class IssueModel
    {
        

        public int ISSUE_ID { get; set; }

        [Required]
        public Nullable<int> REQUEST_ID { get; set; }
        [Required]
        public System.DateTime ISSUE_DATE { get; set; }
        [Required]
        public System.DateTime RETURN_DATE { get; set; }
        [Required]
        public Nullable<float> FINE { get; set; }
        [Required]
        public int REISSUED { get; set; }

        public IssueModel() { }

        public IssueModel(int iSSUE_ID, int? rEQUEST_ID, DateTime iSSUE_DATE, DateTime rETURN_DATE, float? fINE, int rEISSUED)
        {
            this.ISSUE_ID = iSSUE_ID;
            this.REQUEST_ID = rEQUEST_ID;
            this.ISSUE_DATE = iSSUE_DATE;
            this.RETURN_DATE = rETURN_DATE;
            this.FINE = fINE;
            this.REISSUED = rEISSUED;
        }
    }
}