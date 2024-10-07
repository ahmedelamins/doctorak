global using Doctorak.Server.Data;
global using Doctorak.Server.DTOs;
global using Doctorak.Server.Models;
global using Doctorak.Server.Services.AuthService;
global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Security.Cryptography;
using Doctorak.Server.Services.EmailService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registering db
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//registering repositories
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService>(provider =>
{
    var apiKey = Environment.GetEnvironmentVariable("ApiKey");
    return new EmailService(apiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
