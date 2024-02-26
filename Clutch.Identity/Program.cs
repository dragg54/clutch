using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Numerics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using clutch_identity.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using clutch_identity.Services;

var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = builder.Configuration.GetSection("JWTSettings:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("JWTSettings:Key").Get<string>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

builder.Services.AddAuthorization(options => options.AddPolicy("Admin",
     policy => policy.RequireRole("Admin")));


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

//Inject services    
builder.Services.AddScoped<UserService, IUserService>();

builder.Services.AddDbContext<UserDBContext>(options =>
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseMiddleware<ErrorHandlingMiddleware>();

if (builder.Environment.IsProduction())
{
    Log.Information("DI for context in production");
    var db = app.Services.GetRequiredService<UserDBContext>();
    await db.Database.MigrateAsync();

}


app.Run();
