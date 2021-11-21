
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole();
var startup = new WebTest.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(
    app,
    builder.Environment,
    app.Services.GetService<ILoggerFactory>()
);
app.Run();
