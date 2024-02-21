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

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
        ValidAudience = builder.Configuration["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();


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
//builder.Services.AddScoped<IdentityService, IdentityService>();
//builder.Services.AddHttpClient<IPositionClient, PositionClient>();

//builder.Services.AddDbContext<IdentityDbContext>(options =>
//{
//    Log.Information($"Using {builder.Environment.EnvironmentName} DB");
//    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
//    if (builder.Environment.IsDevelopment())
//    {
//        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//    }
//    else
//    {
//        connectionString = builder.Configuration.GetConnectionString("SQLConnection");
//        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//    }
//});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseMiddleware<ErrorHandlingMiddleware>();

//if (builder.Environment.IsProduction())
//{
//    Log.Information("DI for context in production");
//    var db = app.Services.GetRequiredService<IdentityDbContext>();
//    await db.Database.MigrateAsync();

//}


app.Run();
