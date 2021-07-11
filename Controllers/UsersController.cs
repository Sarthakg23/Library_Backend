using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using project2.Models;

namespace project2.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/Users")]
        [HttpGet]
        public HttpResponseMessage getAllUsers()
        {
            
            Library_ManagementEntities entities = new Library_ManagementEntities();
            List<UserModel> list = new List<UserModel>();
            foreach (user_data user in entities.user_data)
            {
                UserModel um = new UserModel(user.user_id, user.user_name, user.user_email, user.user_password, user.user_gender, user.user_type, user.user_age, user.user_DOB, user.user_address, user.user_contact);
                list.Add(um);

            }
            if (list.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Record Doesnot exist.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, list);
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

        private string CreateJWT(user_data user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes("Library Management Sarthak Goyal"));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Role,user.user_type),
                new Claim(ClaimTypes.SerialNumber,user.user_id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
        public HttpResponseMessage signIn(signinModel user)
        {
            Library_ManagementEntities entities = new Library_ManagementEntities();
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email && user1.user_password == user.user_password);
            //if (user_obj == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "no user found");
            
            if (user_obj != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        
                        return Request.CreateResponse(HttpStatusCode.OK, CreateJWT(user_obj));
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

        [Route("api/userbyid/{id}")]
        [HttpPost]
        public HttpResponseMessage getuserbyid(int id)
        {
            Library_ManagementEntities lb = new Library_ManagementEntities();
            user_data user = lb.user_data.FirstOrDefault(user2 => user2.user_id == id);
            UserModel um = new UserModel(user.user_id, user.user_name, user.user_email, user.user_password, user.user_gender, user.user_type, user.user_age, user.user_DOB, user.user_address, user.user_contact);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "This user doesnot exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, um);

        }










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
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_email == user.user_email && user1.user_DOB == user.user_DOB);

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
            user_data user_obj = entities.user_data.FirstOrDefault(user1 => user1.user_id == user.user_id);
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
