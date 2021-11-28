using Microsoft.Extensions.Logging;
using Moq;
using Moq.Language.Flow;
using System;

namespace Banking.Operation.Transfer.Command.Tests.Helpers
{
    static class MockHelper
    {
        public static ISetup<ILogger<T>> MockLog<T>(this Mock<ILogger<T>> logger, LogLevel level)
        {
            return logger.Setup(x => x.Log(level,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }
    }
}
