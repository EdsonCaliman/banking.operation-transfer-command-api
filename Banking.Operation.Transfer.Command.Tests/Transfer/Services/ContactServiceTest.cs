using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Flurl.Http.Testing;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Tests.Transfer.Services
{
    public class ContactServiceTest
    {
        private IContactService _contactService;
        private ContactApiParameters _contactApiParameters;

        [SetUp]
        public void SetUp()
        {
            _contactApiParameters = new ContactApiParameters();

            _contactService = new ContactService(_contactApiParameters);
        }

        [Test]
        public async Task ShouldGetOneWithSuccess()
        {
            var Id = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            _contactApiParameters.Url = "https://api.com";

            using var httpTest = new HttpTest();

            httpTest.RespondWith($"{{'Id':'{Id}', 'Name': 'Test' }}", 200);

            var contact = await _contactService.GetOne(Id, clientId);

            Assert.AreEqual("Test", contact.Name);
            Assert.AreEqual(Id, contact.Id);
            httpTest.ShouldHaveCalled("https://api.com/*")
                .WithVerb(HttpMethod.Get);
        }
    }
}
