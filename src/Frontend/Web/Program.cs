using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using Web.Components;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddScoped<LayoutState>(); // For toggling dark mode
builder.Services.AddScoped<ProtectedLocalStorage>(); // For storing app's UI settings
builder.Services.AddScoped<IResourcesService, ResourcesService>();
builder.Services.AddScoped<IActivityLogsService, ActivityLogsService>();

// TODO: Remove these three HttpClients and replace with a single HttpClient only
builder.Services.AddHttpClient<IHostService, HostService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5088/");
});
builder.Services.AddHttpClient<INodeService, NodeService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5088/");
});
builder.Services.AddHttpClient<IHealthMonitorService, HealthMonitorService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5088/");
});

// TODO: Use this HttpClient instead!
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
Console.WriteLine(baseUrl);
builder.Services.AddHttpClient<IHomeLabApiService, HomeLabApiService>(c => { c.BaseAddress = new Uri(baseUrl!);});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
