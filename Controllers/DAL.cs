using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LibraryManagement.Controllers
{
    public class DAL
    {
        public DAL()
        {

        }
        public List<UserModel> getAllUsers()
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            List<UserModel> list = new List<UserModel>();
            foreach (user_data user in entities.user_data)
            {
                UserModel um = new UserModel(user.user_id, user.user_name, user.user_email, user.user_password, user.user_gender, user.user_type, user.user_age, user.user_DOB, user.user_address, user.user_contact);
                list.Add(um);
            }
            return list;
        }

        public List<BookModel> getAllBooks()
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            List<BookModel> list = new List<BookModel>();
            foreach (BOOK book in entities.BOOKs)
            {
                BookModel bm = new BookModel(book.ID, book.TITLE, book.AUTHOR, book.PUBLISHER, book.ISBN, book.GENERE, book.B_IMAGE, book.RACK_NO, book.E_BOOK, book.LANG, book.TOTAL_COPIES, book.AVAILABLE_COPIES, book.YOP);
                list.Add(bm);
            }
            return list;
        }
    }
}