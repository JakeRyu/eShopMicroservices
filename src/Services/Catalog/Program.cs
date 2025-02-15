using BuildingBlocks.Behaviours;

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

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// -------------------------------------
var app = builder.Build();
// -------------------------------------

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();