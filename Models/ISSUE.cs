//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ISSUE
    {
        public int ISSUE_ID { get; set; }
        public Nullable<int> REQUEST_ID { get; set; }
        public System.DateTime ISSUE_DATE { get; set; }
        public System.DateTime RETURN_DATE { get; set; }
        public Nullable<float> FINE { get; set; }
        public int REISSUED { get; set; }
    
        public virtual Request Request { get; set; }
    }
}
