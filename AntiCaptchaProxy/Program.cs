using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Services;
using static AntiCaptchaProxy.LocalhostMiddlware;

var builder = WebApplication.CreateBuilder(args);

var AllowAllCors = "AllowAllCors";

builder.Services.AddSingleton<IAntiCaptchaService, AntiCaptchaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllCors,
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
