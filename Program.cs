var builder = WebApplication.CreateBuilder(args);

// Railway provides the PORT environment variable
// We need to listen on all interfaces (0.0.0.0) for Railway to route traffic to us
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Enable static file serving - this is crucial!
// This middleware looks for files in the wwwroot folder
app.UseStaticFiles();

// Enable default files (index.html, default.html, etc.)
// This means visiting "/" will automatically serve index.html
app.UseDefaultFiles();

// Your API endpoints can coexist with the static files
app.MapGet("/api/health", () => new { status = "healthy", timestamp = DateTime.UtcNow });

app.MapGet("/api/info", () => new
{
    message = "Welcome to my ASP.NET Minimal API!",
    version = "1.0.0",
    environment = app.Environment.EnvironmentName
});

// This is a fallback - if someone visits a route that doesn't exist,
// we'll redirect them to the home page instead of showing an error
app.MapFallback(async context =>
{
    context.Response.Redirect("/");
});

app.Run();