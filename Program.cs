var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/json", () => Results.Json(new { message = "Hello World!" }));
app.MapGet("/json/{name}", (string name) => Results.Json(new { message = $"Hello {name}!" }));

app.Run();
