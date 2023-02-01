using DatingApp.API.Data;
using DatingApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// here we adding database context
//ConnectionString is a placeHolder that will connect to the db
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=(localdb)\\MSSQLlocalDB;Database=DatingApp.db;Trusted_Connection=True;"));
builder.Services.AddCors();
// builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => 
x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
