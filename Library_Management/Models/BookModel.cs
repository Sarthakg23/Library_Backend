using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string AUTHOR { get; set; }
        public string PUBLISHER { get; set; }
        public string ISBN { get; set; }
        public string GENERE { get; set; }
        public string B_IMAGE { get; set; }
        public Nullable<int> RACK_NO { get; set; }
        public string E_BOOK { get; set; }
        public string LANG { get; set; }
        public int TOTAL_COPIES { get; set; }
        public Nullable<int> AVAILABLE_COPIES { get; set; }
        public System.DateTime YOP { get; set; }

        public BookModel(int iD, string tITLE, string aUTHOR, string pUBLISHER, string iSBN, string gENERE, string b_IMAGE, int? rACK_NO, string e_BOOK, string lANG, int tOTAL_COPIES, int? aVAILABLE_COPIES, DateTime yOP)
        {
            ID = iD;
            TITLE = tITLE;
            AUTHOR = aUTHOR;
            PUBLISHER = pUBLISHER;
            ISBN = iSBN;
            GENERE = gENERE;
            B_IMAGE = b_IMAGE;
            RACK_NO = rACK_NO;
            E_BOOK = e_BOOK;
            LANG = lANG;
            TOTAL_COPIES = tOTAL_COPIES;
            AVAILABLE_COPIES = aVAILABLE_COPIES;
            YOP = yOP;
        }
    }
}