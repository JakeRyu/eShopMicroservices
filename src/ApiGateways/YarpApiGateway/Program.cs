var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure hte HTTP request pipeline
app.MapReverseProxy();

app.Run();