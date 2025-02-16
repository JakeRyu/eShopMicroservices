var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container

// --------------------------------------
var app = builder.Build();
// --------------------------------------

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline

app.Run();