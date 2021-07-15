using Library_Management.Models;
using LibraryManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Library_Management.Controllers
{
    public class UserController : ApiController
    {

        DAL d = new DAL();

        
        [HttpGet]
        public HttpResponseMessage getUser(int id)
        {
            using (Library_ManagementEntities entities = new Library_ManagementEntities())
            {
                user_data user_obj = entities.user_data.FirstOrDefault(user => user.user_id == id);
                if (user_obj == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No user for the ID provided");
                else
                {
                    UserModel um = new UserModel(user_obj.user_id, user_obj.user_name, user_obj.user_email, user_obj.user_password, user_obj.user_gender, user_obj.user_type, user_obj.user_age, user_obj.user_DOB, user_obj.user_address, user_obj.user_contact);
                    return Request.CreateResponse(HttpStatusCode.OK, um);
                }
            }
        }

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

        [Route("api/User/SignIn")]
        [HttpPost]
        public HttpResponseMessage signIn(LoginInput user)
        {
            if (ModelState.IsValid)
            {
                using (Library_ManagementEntities entities = new Library_ManagementEntities())
                { 
                    user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email && user1.user_password == user.user_password);
                    if (user_obj != null)
                    {
                        UserModel um = new UserModel(user_obj.user_id, user_obj.user_name, user_obj.user_email, user_obj.user_password, user_obj.user_gender, user_obj.user_type, user_obj.user_age, user_obj.user_DOB, user_obj.user_address, user_obj.user_contact);
                       try
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, d.CreateJWT(user_obj));

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
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input!");
            }
        }

        [Route("api/User/SignUp")]
        [HttpPost]
        public HttpResponseMessage signUp(user_data user)
        {

            if (ModelState.IsValid)
            {
                Library_ManagementEntities entities = new Library_ManagementEntities();
                user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email);
                if (user_obj == null)
                {

                    try
                    {
                        entities.user_data.Add(user);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Record added successfully. Your user ID is " + user.user_id);
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
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
            }
        }

        [Route("api/GetUserByEmail")]
        [HttpPost]
        public HttpResponseMessage getUserByMail(UserEmail user_email)
        {
            if (ModelState.IsValid)
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

                        return Request.CreateResponse(HttpStatusCode.OK, um);
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid data");
                    }
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
            }

        }

        [Route("api/User/ForgotPassword")]
        [HttpPut]
        public HttpResponseMessage forgotPassword(ForgotPasswordDataModel user)
        {
            if (ModelState.IsValid)
            {
                Library_ManagementEntities entities = new Library_ManagementEntities();
                user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email && user1.user_DOB == user.user_DOB);

                if (user_obj != null)
                {
                    try
                    {
                        user_obj.user_password = user.user_password;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Password has been changed. Use this password to login");
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
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
            }
        }

        [Route("api/User/UpdatePassword")]
        [HttpPut]
        public HttpResponseMessage updatePassword(UserDataModel user)
        {
            if (ModelState.IsValid)
            {
                Library_ManagementEntities entities = new Library_ManagementEntities();
                user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_id == user.user_id);
                if (user_obj != null)
                {
                    try
                    {
                        user_obj.user_password = user.user_password;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Password has been updated.");
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

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid details.");
            }
        }
    }
}