using BuildingBlocks.Behaviours;
using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(exceptionHandlerApp =>
{
    exceptionHandlerApp.RegisterServicesFromAssembly(assembly);
    exceptionHandlerApp.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    exceptionHandlerApp.AddOpenBehavior(typeof(LoggingBehaviours<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(option => { option.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks();

// -------------------------------------
var app = builder.Build();
// -------------------------------------

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health");

app.Run();