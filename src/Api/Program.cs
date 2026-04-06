using System.Net.Security;
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
builder
    .Services.AddHttpClient<IProxmoxService, ProxmoxService>()
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
        }
    );

builder.Services.AddScoped<IHostManagerService, HostManagerService>();
builder.Services.AddScoped<IDashboardManagerService, DashboardManagerService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
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

app.UseCors();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
