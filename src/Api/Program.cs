using System.Net.Security;
using Api.Controllers;
using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddHttpClient<IProxmoxService, ProxmoxService>()
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
        }
    );

builder.Services.AddScoped<IHostManagerService, HostManagerService>();
builder.Services.AddScoped<INodeManagerService, NodeManagerService>();
builder.Services.AddScoped<IResourcesManagerService, ResourcesManagerService>();
builder.Services.AddScoped<IStorageManagerService, StorageManagerService>();
builder.Services.AddScoped<IActivityLogsManagerService, ActivityLogsManagerService>();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("Database");

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
app.MapHealthChecks("ping");

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
