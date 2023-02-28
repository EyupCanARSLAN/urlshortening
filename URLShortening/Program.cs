using Business.UrlShortening.Interfaces.UrlShortening;
using Business.UrlShortening.Services;
using Domain.Repository;
using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUrlShorteningRepository, UrlShorteningRepository>();
builder.Services.AddScoped<IUrlShorteningService, UrlShorteningService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapFallback(RedirectDelegate);


static async Task RedirectDelegate(HttpContext httpContext)
{
    var path = httpContext.Request.Path.ToUriComponent().Trim('/');
    httpContext.Response.Redirect($"api/UrlJobs/RedirectShortening?urlShortening={path}"); 
    await Task.CompletedTask;
}

app.Run();
