using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: OwinStartup(typeof(Library_Management.Startup))]

namespace Library_Management
{
    public class Startup
    {

       
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:44335/", //some string, normally web url,  
                        ValidAudience = "https://localhost:44335/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Library Management Sarthak Goyal"))
                    }
                });
        }
    }
}
