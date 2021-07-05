//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            this.ISSUES = new HashSet<ISSUE>();
        }
    
        public int request_id { get; set; }
       

        [Required]

        public Nullable<int> user_id { get; set; }

       
        [Required]
        public Nullable<int> book_id { get; set; }


        [Required]

        public string request_status { get; set; }
        
        public Nullable<int> reIssue_id { get; set; }

        
        

        public Nullable<System.DateTime> request_date { get; set; }
        [Required]

        public Nullable<System.DateTime> request_approve_date { get; set; }
    
        public virtual BOOK BOOK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISSUE> ISSUES { get; set; }
        public virtual user_data user_data { get; set; }
    }
}
