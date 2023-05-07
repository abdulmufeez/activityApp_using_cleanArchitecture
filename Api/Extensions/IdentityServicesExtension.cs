using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Api.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection IdentityServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<DataContext>();
            
            // setting application to accept jwt token and authenticate it
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => 
                {
                    // what to authenticate and how to authenticate
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("TokenKey"))),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
                
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}