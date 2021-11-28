using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Flurl.Http.Testing;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Tests.Transfer.Services
{
    public class ClientServiceTest
    {
        private IClientService _clientService;
        private ClientApiParameters _clientApiParameters;

        [SetUp]
        public void SetUp()
        {
            _clientApiParameters = new ClientApiParameters();

            _clientService = new ClientService(_clientApiParameters);
        }

        [Test]
        public async Task ShouldGetByAccountWithSuccess()
        {
            var account = 123;
            _clientApiParameters.Url = "https://api.com";

            using var httpTest = new HttpTest();

            httpTest.RespondWith("{'Account': 123, 'Name': 'Test' }", 200);

            var client = await _clientService.GetByAccount(account);

            Assert.AreEqual("Test", client.Name);
            Assert.AreEqual(account, client.Account);
            httpTest.ShouldHaveCalled("https://api.com/*")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task ShouldGetOneWithSuccess()
        {
            var Id = Guid.NewGuid();
            _clientApiParameters.Url = "https://api.com";

            using var httpTest = new HttpTest();

            httpTest.RespondWith($"{{'Id':'{Id}', 'Name': 'Test' }}", 200);

            var client = await _clientService.GetOne(Id);

            Assert.AreEqual("Test", client.Name);
            Assert.AreEqual(Id, client.Id);
            httpTest.ShouldHaveCalled("https://api.com/*")
                .WithVerb(HttpMethod.Get);
        }
    }
}
