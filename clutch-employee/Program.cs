using clutch_employee.Data.Contexts;
using clutch_employee.Exceptions;
using clutch_employee.Infrastructure.Middlewares;
using clutch_employee.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHttpsRedirection(opt => opt.HttpsPort = 443);
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

});


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
