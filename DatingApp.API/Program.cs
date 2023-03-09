using DatingApp.API.Controllers;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using DatingApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using AutoMapper;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

            //builder.Services.AddControllers().newto
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // here we adding database context
        //ConnectionString is a placeHolder that will connect to the db
        builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=(localdb)\\MSSQLlocalDB;Database=DatingApp.db;Trusted_Connection=True;"));

        builder.Services.AddMvc()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
  
        builder.Services.AddScoped<Seed>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IDatingRepository, DatingRepository>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration
                    .GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
        var app = builder.Build();




        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        //we added our additional headers here c:
                        context.Response.AddApplicationError(error.Error.Message);

                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });
        }
      
        using (var serviceScope = app.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;

            var seeder = services.GetRequiredService<Seed>(); //myDependency

            //Use the service
            seeder.SeedUsers();
        }
        
        app.UseCors(x =>
        x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();



        app.Run();
    }
}