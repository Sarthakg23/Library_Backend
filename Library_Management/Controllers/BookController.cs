using Library_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Management.Controllers
{
    public class BookController : ApiController
    {
        DAL d = new DAL();
        [HttpGet]
        public HttpResponseMessage getAllBooks()
        {
            if (d.getAllBooks().Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Books!");
            else
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllBooks());
        }

        [Route("api/AddBook")]
        [HttpPost]
        public HttpResponseMessage addBook(BOOK book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ISBN == book.ISBN && book1.TITLE == book.TITLE && book1.AUTHOR == book.AUTHOR);
            if (ifBookExist == null)
            {

                //try
                //{
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

                //}
               // catch (Exception e)
               // {
               //     return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
               // }
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
        public HttpResponseMessage updateBook(UpdateRack book)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ID==book.ID);
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
                        ifBookExist.RACK_NO = book.RACK_NO;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Rack has been updated");
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

        [Route("api/DeleteBook/{id}")]
        [HttpDelete]
        public HttpResponseMessage deleteBook(int id)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            BOOK ifBookExist = entities.BOOKs.FirstOrDefault(book1 => book1.ID == id);
            if (ifBookExist != null)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        if (ifBookExist.AVAILABLE_COPIES == ifBookExist.TOTAL_COPIES)
                        {
                            entities.BOOKs.Remove(ifBookExist);
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
        [HttpPost]
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
