using AutoMapper;
using BL.Mapping;
using DAL;
using DAL.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using WebApi.Services;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
   
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:7155")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();///Required for cookies (refresh token)
                });
            });
            builder.Services.AddControllers();

        
            RegisterServicesHelper.RegisterServices(builder);

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            // Seed 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var dbContext = services.GetRequiredService<ShippingContext>();
                await dbContext.Database.MigrateAsync();
                // Seed data
                await ContextConfig.SeedDataAsync(dbContext, userManager, roleManager);

            }

            app.Run();
        }
    }
}
