using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container

// Application services
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(exceptionHandlerApp =>
{
    exceptionHandlerApp.RegisterServicesFromAssembly(assembly);
    exceptionHandlerApp.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    exceptionHandlerApp.AddOpenBehavior(typeof(LoggingBehaviours<,>));
});
builder.Services.AddCarter();
builder.Services.AddMarten(option =>
    {
        option.Connection(builder.Configuration.GetConnectionString("Database")!);
        // Marten by default create schema automatically
        option.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();

// Data services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    // options.InstanceName = "Basket"
});

// Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

// Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
    
// --------------------------------------
var app = builder.Build();
// --------------------------------------

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();