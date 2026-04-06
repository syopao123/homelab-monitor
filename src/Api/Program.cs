using Api.Controllers;
using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddScoped<IProxmoxService, ProxmoxService>();
builder.Services.AddScoped<IHostManagerService, HostManagerService>();
builder
    .Services.AddHttpClient("TestProxmoxConnection")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            // This allows the connection even if the SSL cert is self-signed/invalid
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/error");
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    //app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
