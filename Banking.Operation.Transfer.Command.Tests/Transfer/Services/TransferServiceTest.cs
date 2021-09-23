using AutoFixture;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Tests.Transfer.Services
{
    public class TransferServiceTest
    {
        private ITransferService _transferService;
        private Mock<ITransferRepository> _transferRepository;
        private Mock<IClientService> _clientService;
        private Mock<IContactService> _contactService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _transferRepository = new Mock<ITransferRepository>();
            _clientService = new Mock<IClientService>();
            _contactService = new Mock<IContactService>();
            _fixture = new Fixture();

            _transferService = new TransferService(_transferRepository.Object, _clientService.Object, _contactService.Object);
        }

        [Test]
        public async Task ShouldSaveTransaction()
        {
            var client = _fixture.Create<ClientDto>();
            var contact = _fixture.Create<ContactDto>();
            var requestTransactionDto = new RequestTransferDto { ContactId = contact.Id, Value = 10 };
            _clientService.Setup(c => c.GetOne(client.Id)).Returns(Task.FromResult(client));
            _contactService.Setup(c => c.GetOne(client.Id, contact.Id)).Returns(Task.FromResult(contact));

            var transactionDto = await _transferService.Save(client.Id, requestTransactionDto);

            Assert.IsNotNull(transactionDto);
            _transferRepository.Verify(c => c.Add(It.IsAny<TransferEntity>()));
        }
    }
}
