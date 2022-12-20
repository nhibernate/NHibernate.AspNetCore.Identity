
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole();
var startup = new WebTest.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();

if (loggerFactory == null) {
    throw new ApplicationException("Logger Factory is not configured!");
}

startup.Configure(
    app,
    builder.Environment,
    loggerFactory
);
app.Run();
