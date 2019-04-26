using System;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace UnitTest {

    [TestFixture]
    public class LoggingBuilderTest {

        [Test]
        public void _01_CanBuildLoggingFactory() {
            var target = new LoggingBuilder();
            var factory = target.BuildLoggerFactory();
            Assert.NotNull(factory);
            var logger = factory.CreateLogger("LoggingBuilderTest");
            Console.WriteLine(logger.GetType());
            logger.LogInformation("_01_CanBuildLoggingFactory");
        }

    }

}
