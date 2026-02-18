
using ClubManagement.BLL.ExternalServices;
using ClubManagement.BLL.Services.Implementations;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.BLL.Validators;
using ClubManagement.DAL.Data;
using ClubManagement.DAL.Data.Models;
using ClubManagement.DAL.Repositories.Implementations;
using ClubManagement.DAL.Repositories.Interfaces;
using ClubManagement.PL.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UnviClubManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(op =>
                    op.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

            // Register Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders(); // For email confirm, reset password

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IClubService, ClubService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IClubLeaderService, ClubLeaderService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAuthService, AuthService>();


            //builder.Services.AddValidatorsFromAssemblyContaining<AddClubDTOValidator>();
            builder.Services.AddValidatorsFromAssembly(typeof(AddClubDTOValidator).Assembly);



            builder.Services.AddScoped<JWTService>();
            var jwtSettings = builder.Configuration.GetSection("JWT");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenJwtAuth();

            builder.Services.AddCors(builder =>
            {
                builder.AddPolicy("AllowAngularClient", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:4200",
                        "https://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAngularClient");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
