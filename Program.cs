using spaceTracker.Components;
using spaceTracker.Server.Services; // <-- your WeatherService namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// <-- Register your service here
builder.Services.AddScoped<weatherService>();
builder.Services.AddHttpClient<weatherService>();

builder.Services.AddScoped<RocketLaunchLiveService>();
builder.Services.AddHttpClient<RocketLaunchLiveService>();

builder.Services.AddScoped<n2yoService>();
builder.Services.AddHttpClient<n2yoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
