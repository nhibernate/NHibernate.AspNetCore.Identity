using WebTest;

namespace WebTest {

    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders().AddConsole();
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            var app = builder.Build();
            startup.Configure(
                app,
                builder.Environment,
                app.Services.GetService<ILoggerFactory>()
            );
            app.Run();
        }
    }
}

