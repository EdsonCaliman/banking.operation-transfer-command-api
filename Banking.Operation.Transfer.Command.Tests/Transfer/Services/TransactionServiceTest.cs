using Banking.Operation.Transfer.Command.Domain.Transfer.Enums;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Flurl.Http.Testing;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Tests.Transfer.Services
{
    public class TransactionServiceTest
    {
        private ITransactionService _transactionService;
        private TransactionApiParameters _transactionApiParameters;

        [SetUp]
        public void SetUp()
        {
            _transactionApiParameters = new TransactionApiParameters();

            _transactionService = new TransactionService(_transactionApiParameters);
        }

        [Test]
        public async Task ShouldPostWithSuccess()
        {
            var clientId = Guid.NewGuid();
            var type = TransactionType.Credit;
            var value = 10;
            _transactionApiParameters.Url = "https://api.com";

            using var httpTest = new HttpTest();

            httpTest.RespondWith(string.Empty, 200);

            await _transactionService.Post(clientId, type, value);

            httpTest.ShouldHaveCalled("https://api.com/*")
                .WithVerb(HttpMethod.Post);
        }
    }
}
