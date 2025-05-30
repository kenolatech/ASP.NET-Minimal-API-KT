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

app.MapFallback(async context =>
{
    var path = context.Request.Path.Value ?? "";

    // For API routes, return JSON error
    if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.StatusCode = 404;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "API endpoint not found",
                path = path
            })
        );
        return;
    }

    // For paths that look like files (have extensions), just return 404
    // This handles missing images, CSS, JS, etc.
    if (Path.HasExtension(path))
    {
        context.Response.StatusCode = 404;
        return;
    }

    // For the root path specifically, let's not interfere
    // This allows index.html to be served
    if (path == "/" || path == "")
    {
        // Don't handle this - let it 404 naturally if index.html doesn't exist
        context.Response.StatusCode = 404;
        return;
    }

    // For other paths without extensions, show custom 404 page
    // These are likely meant to be routes like "/about" or "/contact"
    context.Response.StatusCode = 404;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(@"
        <!DOCTYPE html>
        <html>
        <head><title>Page Not Found</title></head>
        <body>
            <h1>404 - Page Not Found</h1>
            <p>Sorry, the page you're looking for doesn't exist.</p>
            <a href='/'>Return to Home</a>
        </body>
        </html>
    ");
});

app.Run();