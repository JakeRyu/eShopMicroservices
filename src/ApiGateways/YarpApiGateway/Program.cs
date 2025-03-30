var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var app = builder.Build();

// Configure hte HTTP request pipeline

app.Run();