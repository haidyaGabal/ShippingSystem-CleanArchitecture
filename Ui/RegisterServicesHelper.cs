using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using BL.Mapping;
using BL.Services;
using BL.Services.Shipment;
using DAL;
using DAL.Repositories;
using DAL.UserModels;
using Domains;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Net.Http.Headers;
using Ui.Services;

namespace Ui
{
    public class RegisterServicesHelper
    {
        public static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                // Base URL will be configured in GenericApiClient constructor using appsettings.json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/access-denied";
            options.SlidingExpiration = true;///for create new cookie when exit cookie is expird
            options.Cookie.IsEssential=true; ///browser saved this cookie even if browser has constraint;
            options.ExpireTimeSpan=TimeSpan.FromDays(7);/// for time of cookie
        });

            builder.Services.AddDbContext<ShippingContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            ///DI
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ShippingContext>()
   .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<GenericApiClient>();
            /// Auto Mapper

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            //builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            ///must be declare Repostory 
            
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            builder.Services.AddScoped<IShipingType, ShipingTypeService>();
            builder.Services.AddScoped<ICountry, CounteryService>();
            builder.Services.AddScoped<ICity, CityService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRefershToken, RefershTokenService>();

            builder.Services.AddScoped<IPaymentMethod, PaymentService>();
            builder.Services.AddScoped<IUserReceiver, UserReceiverService>();
            builder.Services.AddScoped<IUserSender, UserSenderService>();
            builder.Services.AddScoped<IShipment, ShipmentService>();
            builder.Services.AddScoped<ITrackingNumber, TrackingNumberCreatorService>();
            builder.Services.AddScoped<ICalculateRate, CalculateRateService>();
            builder.Services.AddScoped<IShipingPackages, ShipingPackageService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IRefreshTokenRetriver, RefreshTokenRetriverService>();







            ///Configuration Logger
            var sinkOptions = new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            };

            Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: sinkOptions
    )
    .CreateLogger();

            builder.Host.UseSerilog();





        }
    }
}
