using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project2.Models
{
    public class BookModel
    {
        public int ID;
        public string TITLE;
        public string AUTHOR;
        public string PUBLISHER;
        public string ISBN;
        public string GENERE;
        public string B_IMAGE;
        public int? RACK_NO;
        public string E_BOOK;
        public string LANG;
        public int TOTAL_COPIES;
        public int? AVAILABLE_COPIES;
        public DateTime YOP;
        public BookModel(int ID, string TITLE, string AUTHOR, string PUBLISHER, string ISBN, string GENERE, string B_IMAGE, int? RACK_NO, string E_BOOK, string LANG, int TOTAL_COPIES, int? AVAILABLE_COPIES, DateTime YOP)
        {
            this.ID = ID;
            this.TITLE = TITLE;
            this.AUTHOR = AUTHOR;
            this.PUBLISHER = PUBLISHER;
            this.ISBN = ISBN;
            this.GENERE = GENERE;
            this.B_IMAGE = B_IMAGE;
            this.RACK_NO = RACK_NO;
            this.E_BOOK = E_BOOK;
            this.LANG = LANG;
            this.TOTAL_COPIES = TOTAL_COPIES;
            this.AVAILABLE_COPIES = AVAILABLE_COPIES;
            this.YOP = YOP;
        }
    }
}