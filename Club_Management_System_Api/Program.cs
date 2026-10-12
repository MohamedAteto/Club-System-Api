using System.Reflection;
using System.Text;
using Club_Management_System_Api.Data;
using Club_Management_System_Api.Mapping;
using Club_Management_System_Api.Reposatories.Implementation;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // register the repository and service classes and the auto-mapper

        builder.Services.AddScoped<IMemberRepo, MemberRepo>();
        builder.Services.AddScoped<IMemberShipRepo, MemberShipRepo>();
        builder.Services.AddScoped<IFacilityRepo, FacilityRepo>();
        builder.Services.AddScoped<IBookingRepo, BookingRepo>();
        builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<IunitOfWork, UnitOfWork>();

        builder.Services.AddAutoMapper(typeof(MappingProfiles));


        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Club_System_APi", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token in format **Bearer {your_jwt_token}**",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme ="Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {

                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });

        });




        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]));

        builder.Services.AddAuthentication(op =>

        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        )
        .AddJwtBearer(s =>
        {
            s.RequireHttpsMetadata = false;
            s.SaveToken = true;
            s.TokenValidationParameters = new TokenValidationParameters
            {

                
                   ValidateIssuer = true ,
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,


                   ValidIssuer = builder.Configuration["JWT:Issuer"],
                   ValidAudience = builder.Configuration["JWT:Audience"],
                   IssuerSigningKey = Key
                
            };
            
        }
        );



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

        app.MapControllers();

        app.Run();
    }
}