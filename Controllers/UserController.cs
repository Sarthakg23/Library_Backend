using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    public class UserController : ApiController
    {
        DAL d = new DAL();

        [Route("api/Users")]
        [HttpGet]
        public HttpResponseMessage getAllUsers()
        {           
            if (d.getAllUsers().Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Record Doesnot exist.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, d.getAllUsers());
            }
        }

        [Route("api/User/SignUp")]
        [HttpPost]
        public HttpResponseMessage signUp(user_data user)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email);
            if (user_obj == null)
            {
                
                try
                {
                   
                        if (ModelState.IsValid)
                        {
                            entities.user_data.Add(user);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "Record added successfully. Your user ID is " + user.user_id);
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
                return Request.CreateResponse(HttpStatusCode.Found, "Record already exist.");
            }
        }

        //[Route("api/User/AddAdmin")]
        //[HttpPost]

        //public HttpResponseMessage addAdmin(user_data user)
        //{
        //    Library_ManagementEntities entities = new Library_ManagementEntities();
        //    user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email);
        //    if (user_obj == null)
        //    {

        //        try
        //        {
        //            if (user_obj.user_type == "admin")
        //            {
        //                entities.user_data.Add(user);
        //                entities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, "Record added successfully.");
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, "Go to the home page and create user account.");
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
        //        }
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Found, "Record already exist.");
        //    }
        //}


        [Route("api/User/SignIn")]
        [HttpPost]
        public HttpResponseMessage signIn(user_data user)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email && user1.user_password==user.user_password);
            UserModel um = new UserModel(user_obj.user_id, user_obj.user_name, user_obj.user_email, user_obj.user_password, user_obj.user_gender, user_obj.user_type, user_obj.user_age, user_obj.user_DOB, user_obj.user_address, user_obj.user_contact);
            if (user_obj != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, um);
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
                return Request.CreateResponse(HttpStatusCode.NotFound, "Record doesnot exist");
            }
        }
        //getuserbyemail 
        //model with dob password 

        [Route("api/GetUserByEmail")]
        [HttpPost]
        public HttpResponseMessage getUserByMail(user_data user_email)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            user_data user = entities.user_data.FirstOrDefault(user2 => user2.user_email == user_email.user_email);
            UserModel um = new UserModel(user.user_id, user.user_name, user.user_email, user.user_password, user.user_gender, user.user_type, user.user_age, user.user_DOB, user.user_address, user.user_contact);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "This user doesnot exist");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        entities.user_data.Add(user);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, um);
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

        [Route("api/User/ForgotPassword")]
        [HttpPut]
        public HttpResponseMessage forgotPassword(ForgotPasswordDataModel user)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email==user.user_email && user1.user_DOB == user.user_DOB);

            if (user_obj != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        user_obj.user_password = user.user_password;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Password has been changed. Use this password to login");
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
                return Request.CreateResponse(HttpStatusCode.NotFound, "This user does not exist");
            }
        }

        [Route("api/User/UpdatePassword")]
        [HttpPut]
        public HttpResponseMessage updatePassword(UserDataModel user)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_id==user.user_id );
            if (user_obj != null)
            {
               
                try
                {
                    if (ModelState.IsValid)
                    {
                        user_obj.user_password = user.user_password;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Password has been updated.");
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
                return Request.CreateResponse(HttpStatusCode.NotFound, "This user does not exist");
            }
        }
    }
}