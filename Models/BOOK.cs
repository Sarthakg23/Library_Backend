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
    using System.ComponentModel.DataAnnotations;

    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int ID { get; set; }
        [Required]
        public string TITLE { get; set; }
        [Required]
        public string AUTHOR { get; set; }
        [Required]
        public string PUBLISHER { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string GENERE { get; set; }
        [Required]
        public string B_IMAGE { get; set; }
        [Required]
        public Nullable<int> RACK_NO { get; set; }
        [Required]
        public string E_BOOK { get; set; }
        [Required]
        public string LANG { get; set; }
        public int TOTAL_COPIES { get; set; }
        public Nullable<int> AVAILABLE_COPIES { get; set; }
        [Required]
        public System.DateTime YOP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
    }
}
