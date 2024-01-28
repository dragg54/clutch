using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using clutch_position.Data.Contexts;
using clutch_position.Services;
using Serilog;
using clutch_position.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddDbContext<PositionDbContext>(options =>
{
    Log.Information($"Using {builder.Environment.EnvironmentName} DB");
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("SQLConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();

if (builder.Environment.IsProduction())
{
    Log.Information("DI for context in production");
    var db = app.Services.GetRequiredService<PositionDbContext>();
    await db.Database.MigrateAsync();

}


app.Run();
