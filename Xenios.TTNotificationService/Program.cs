using Serilog;
using Xenios.TTNotificationService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Ensure logs directory exists
Directory.CreateDirectory("logs");

try
{
    Log.Information("Starting TTNotificationService");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "TTNotificationService terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
