using spaceTracker.Components;
using spaceTracker.Services;
using Microsoft.EntityFrameworkCore;
using spaceTracker.Data;
using spaceTracker.Data.Seeders;

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

builder.Services.AddScoped<SpaceDevsService>();
builder.Services.AddHttpClient<SpaceDevsService>();

builder.Services.AddTransient<CesiumService>();
builder.Services.AddScoped<SpaceProgramSeeder>();

// Register EF Core with SQLite
builder.Services.AddDbContext<SpaceTrackerDbContext>(options =>
    options.UseSqlite("Data Source=spaceTracker.db"));

builder.Services.AddScoped<SpacecraftSeeder>();
builder.Services.AddScoped<AstronautSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<SpaceProgramSeeder>();
    await seeder.PopulateAsync();

    var spacecraftSeeder = scope.ServiceProvider.GetRequiredService<SpacecraftSeeder>();
    await spacecraftSeeder.PopulateAsync();

    var astronautSeeder = scope.ServiceProvider.GetRequiredService<AstronautSeeder>();
    await astronautSeeder.PopulateAsync();
}

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
