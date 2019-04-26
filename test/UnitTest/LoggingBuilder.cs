using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UnitTest {

    public class LoggingBuilder {

        private ServiceProvider provider;

        private readonly IServiceCollection services = new ServiceCollection();

        public ILoggerFactory BuildLoggerFactory() {
            if (provider == null) {
                services.AddLogging(logging => {
                    logging.AddConsole();
                });
                provider = services.BuildServiceProvider();
            }
            return provider.GetService<ILoggerFactory>();
        }

    }

}
