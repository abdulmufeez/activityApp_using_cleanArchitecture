using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Services;
using Domain;
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

            services.AddAuthentication();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}