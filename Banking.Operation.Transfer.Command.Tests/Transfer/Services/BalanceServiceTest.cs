using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Flurl.Http.Testing;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Tests.Transfer.Services
{
    public class BalanceServiceTest
    {
        private IBalanceService _balanceService;
        private BalanceApiParameters _balanceApiParameters;

        [SetUp]
        public void SetUp()
        {
            _balanceApiParameters = new BalanceApiParameters();

            _balanceService = new BalanceService(_balanceApiParameters);
        }

        [Test]
        public async Task ShouldGetBalanceWithSuccess()
        {
            var clientId = Guid.NewGuid();
            _balanceApiParameters.Url = "https://api.com";

            using var httpTest = new HttpTest();

            httpTest.RespondWith("{'value': 100 }", 200);

            var balance = await _balanceService.GetBalance(clientId);

            Assert.AreEqual(100, balance.Value);
            httpTest.ShouldHaveCalled("https://api.com/*")
                .WithVerb(HttpMethod.Get);
        }
    }
}
