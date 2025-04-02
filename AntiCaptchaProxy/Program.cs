using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Models;
using AntiCaptchaProxy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static AntiCaptchaProxy.LocalhostMiddlware;

var builder = WebApplication.CreateBuilder(args);

var jwtAuthConfig = builder.Configuration.GetSection("Authentication:Jwt");
var jwtAuthEnabled = jwtAuthConfig.GetValue<bool>("Enabled");

var pgConfig = builder.Configuration.GetSection("Database:PostgreSQL");
var pgEnabled = pgConfig.GetValue<bool>("Enabled");

var AllowAllCors = "AllowAllCors";

// Add services to the container.

builder.Services.AddSingleton<IAntiCaptchaService, AntiCaptchaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllCors,
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    if (jwtAuthEnabled)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidIssuer = jwtAuthConfig.GetValue<string>("Issuer"),
            ValidAudience = jwtAuthConfig.GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthConfig.GetValue<string>("Key") ?? string.Empty))
        };
    }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
if (pgEnabled)
{
    builder.Services.AddDbContext<ProxyStatsDb>(options => options.UseNpgsql(pgConfig.GetValue<string>("ConnectionString")));
}
else
{
    builder.Services.AddDbContext<ProxyStatsDb>(options => options.UseInMemoryDatabase("anti_captcha_stats"));
}
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseMiddleware<LocalhostMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowAllCors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
