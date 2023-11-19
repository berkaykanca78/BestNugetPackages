using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using BaseWebApp.Services.Concrete;
using BaseWebApp.Utilities.EFCore;
using BaseWebApp.Utilities.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Serilog
builder.Host.UseSerilog((builderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .WriteTo.File("logs/app.log", flushToDiskInterval: TimeSpan.FromDays(1)));
#endregion

#region Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Entity Framework 
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(connectionString));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Dependency Injections
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
#endregion

#region Polly
builder.Services.AddHttpClient();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Testing API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
