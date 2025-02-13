var builder = WebApplication.CreateBuilder(args);

// Add services to DI container

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapGet("/", () => "Hello World!");

app.Run();
