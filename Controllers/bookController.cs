using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project2.Models;

namespace project2.Controllers
{
    public class bookController : ApiController
    {
        dall d = new dall();

        [Route("api/AllBooks")]
        [HttpGet]
        public HttpResponseMessage getAllBooks()
        {

            if (d.getAllBooks().Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Record Doesnot exist.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllBooks());
            }
        }

        [Route("api/AddBook")]
        [HttpPost]
        public HttpResponseMessage addBook(BOOK book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ISBN == book.ISBN && book1.TITLE == book.TITLE && book1.AUTHOR == book.AUTHOR);
            if (ifBookExist == null)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        entities.BOOKs.Add(book);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Book was added successfully");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "This Book was already added in the database.");

            }


            //if (ifBookExist != null)
            //{
            //    ifBookExist.AVAILABLE_COPIES = book.AVAILABLE_COPIES + ifBookExist.AVAILABLE_COPIES;
            //    ifBookExist.TOTAL_COPIES = book.TOTAL_COPIES + ifBookExist.TOTAL_COPIES;
            //    entities.SaveChanges();
            //    return Request.CreateResponse(HttpStatusCode.OK, "This Book was already added in the database. Total no of copies now available are " + book.AVAILABLE_COPIES );
            //}
            //else
            //{
            //    entities.BOOKs.Add(book);
            //    entities.SaveChanges();
            //    return Request.CreateResponse(HttpStatusCode.OK, "Book was added successfully");
            //}
        }

        [Route("api/UpdateBook")]
        [HttpPut]
        public HttpResponseMessage updateBook(BOOK book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ISBN == book.ISBN && book1.TITLE == book.TITLE && book1.AUTHOR == book.AUTHOR);
            if (ifBookExist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "This book does not exist.");
            }
            else
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        ifBookExist.TITLE = book.TITLE;
                        ifBookExist.AUTHOR = book.AUTHOR;
                        ifBookExist.PUBLISHER = book.PUBLISHER;
                        ifBookExist.ISBN = book.ISBN;
                        ifBookExist.GENERE = book.GENERE;
                        ifBookExist.B_IMAGE = book.B_IMAGE;
                        ifBookExist.RACK_NO = book.RACK_NO;
                        ifBookExist.E_BOOK = book.E_BOOK;
                        ifBookExist.LANG = book.LANG;
                        ifBookExist.TOTAL_COPIES = book.TOTAL_COPIES;
                        ifBookExist.AVAILABLE_COPIES = book.AVAILABLE_COPIES;
                        ifBookExist.YOP = book.YOP;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Book record has been updated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
                }
            }
        }

        [Route("api/DeleteBook")]
        [HttpDelete]
        public HttpResponseMessage deleteBook(BOOK book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ID == book.ID);
            if (ifBookExist != null)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        if (ifBookExist.AVAILABLE_COPIES == ifBookExist.TOTAL_COPIES)
                        {
                            entities.BOOKs.Remove(book);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "Book record has been deleted");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "This Book cannot be deleted as it has been issuedby a user.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "This book does not exist");
            }
        }

        [Route("api/AddCopy")]
        [HttpPost]
        public HttpResponseMessage addCopy(BOOK book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ID == book.ID);
            if (ifBookExist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "This book does not exist. Please add the following book in your db.");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ifBookExist.TOTAL_COPIES = ifBookExist.TOTAL_COPIES + 1;
                        ifBookExist.AVAILABLE_COPIES = ifBookExist.AVAILABLE_COPIES + 1;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Book record has been updated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
                }
            }
        }

        [Route("api/DeleteCopy")]
        [HttpDelete]
        public HttpResponseMessage deleteCopy(BOOK book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ID == book.ID);
            if (ifBookExist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "This book does not exist. Please add the following book in your db.");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (ifBookExist.TOTAL_COPIES >= ifBookExist.AVAILABLE_COPIES && ifBookExist.AVAILABLE_COPIES > 0)
                        {
                            if (ifBookExist.TOTAL_COPIES == 1 && ifBookExist.AVAILABLE_COPIES == 1)
                            {
                                entities.BOOKs.Remove(book);
                                entities.SaveChanges();
                                return Request.CreateResponse(HttpStatusCode.OK, "Book record has been deleted");
                            }
                            else
                            {
                                ifBookExist.TOTAL_COPIES = ifBookExist.TOTAL_COPIES - 1;
                                ifBookExist.AVAILABLE_COPIES = ifBookExist.AVAILABLE_COPIES - 1;
                                entities.SaveChanges();
                                return Request.CreateResponse(HttpStatusCode.OK, "Book record has been updated");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "This Book cannot be deleted as it has been issuedby a user.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
                }
            }
        }

    }
}
