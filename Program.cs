using System.Diagnostics; // This is where Stopwatch lives
using System.Text.Json;    // This is where JsonSerializer lives

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Railway configuration
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Add logging to see what's happening
var logger = app.Logger;
logger.LogInformation($"Application starting on port {port}");

// Static files configuration
app.UseDefaultFiles();
app.UseStaticFiles();

// API routes with logging
app.MapGet("/api/health", (ILogger<Program> logger) =>
{
    logger.LogInformation("Health check endpoint called");
    return Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
});

// API routes
//app.MapGet("/api/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapGet("/api/info", () => new
{
    message = "Welcome to my ASP.NET Minimal API!",
    version = "1.0.0",
    environment = app.Environment.EnvironmentName
});

// Don't use a catch-all fallback for now
// Just let 404s be 404s until we confirm everything else works

app.Run();