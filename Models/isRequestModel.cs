using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2.Models
{
    public class isRequestModel
    {
        [Required]
        public Nullable<int> book_id { set; get; }
        [Required]
        public Nullable<int> user_id { set; get; }

        public isRequestModel() { }

        public isRequestModel(int? book_id,int? user_id)
        {
            this.book_id = book_id;
            this.user_id = user_id;
        }
    }

    
}