using Microsoft.Extensions.Logging;

namespace UnitTest;

[TestFixture]
public class LoggingBuilderTest {

    [Test]
    public void _01_CanBuildLoggingFactory() {
        var target = new LoggingBuilder();
        var factory = target.BuildLoggerFactory();
        NotNull(factory);
        var logger = factory.CreateLogger("LoggingBuilderTest");
        Console.WriteLine(logger.GetType());
        logger.LogInformation("_01_CanBuildLoggingFactory");
    }

}
