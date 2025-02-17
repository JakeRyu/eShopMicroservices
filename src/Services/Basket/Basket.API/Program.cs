var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(exceptionHandlerApp =>
{
    exceptionHandlerApp.RegisterServicesFromAssembly(assembly);
    exceptionHandlerApp.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    exceptionHandlerApp.AddOpenBehavior(typeof(LoggingBehaviours<,>));
});

builder.Services.AddCarter();

// --------------------------------------
var app = builder.Build();
// --------------------------------------

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline
app.MapCarter();
 
app.Run();