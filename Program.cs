using System.Text;
using ContactManagementV2.Data;
using ContactManagementV2.Middleware;
using ContactManagementV2.Models;
using ContactManagementV2.Repository;
using ContactManagementV2.Repository.interfaces;
using ContactManagementV2.Services.AuthorizeService;
using ContactManagementV2.Services.AuthorizeService.interfaces;
using ContactManagementV2.Services.Cache;
using ContactManagementV2.Services.CategoryService;
using ContactManagementV2.Services.ContactService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace ContactManagementV2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //builder.Logging.ClearProviders();
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Seq(@"http://localhost:5341/")
            .CreateLogger();

        builder.Logging.AddSerilog(logger);

        // Add services to the container.

        var Services = builder.Services;
        {
            Services.AddControllers();
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "ContactManager", Version = "v1" }
                );
                options.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    }
                );

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                },
                                Scheme = "Oauth2",
                                Name = JwtBearerDefaults.AuthenticationScheme,
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    }
                );
            });
            Services.AddScoped<ICacheService, CacheService>();
            Services.AddScoped<IRepository<Contact>, Repository<Contact>>();
            Services.AddScoped<IRepository<Category>, Repository<Category>>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IContactService, ContactService>();
            Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
            );
            Services.AddScoped<IAuthorize, AuthorizeService>();
            Services.AddScoped<ITokenRepository, TokenRepository>();

            Services.AddDbContext<AppAuthDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Auth"))
            );

            Services.AddAutoMapper(typeof(Program));

            Services
                .AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ContactManagement")
                .AddEntityFrameworkStores<AppAuthDbcontext>()
                .AddDefaultTokenProviders();

            Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
            });

            Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                        )
                    }
                );
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<SerilogMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
