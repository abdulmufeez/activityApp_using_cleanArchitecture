using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(config.GetValue<string>("clientAppUrl"));
                });
            });
            // adding mediator as a service and telling it to find where the handlers are,just like in typeof()
            services.AddMediatR(typeof(List.Handler));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }

        public static async Task<WebApplication> UseAutoMigrateAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await SeedData.SeedActivitiesData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }
            return app;
        }
    }
}