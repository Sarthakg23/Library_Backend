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
                            LoginResponse lr = new LoginResponse(um, CreateJWT(user_obj));
                            return Request.CreateResponse(HttpStatusCode.OK, lr);

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


        private string CreateJWT(user_data user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes("Library Management Sarthak Goyal"));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.user_name),
                new Claim(ClaimTypes.NameIdentifier,user.user_id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
