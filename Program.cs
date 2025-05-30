var builder = WebApplication.CreateBuilder(args);

// Railway configuration
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Static files configuration (order is crucial!)
app.UseDefaultFiles();
app.UseStaticFiles();

// API routes
app.MapGet("/api/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

// Don't use a catch-all fallback for now
// Just let 404s be 404s until we confirm everything else works

app.Run();