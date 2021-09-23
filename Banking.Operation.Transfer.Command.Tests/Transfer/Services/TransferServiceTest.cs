using AutoFixture;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Microsoft.Extensions.Logging;
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
        private Mock<IBalanceService> _balanceService;
        private Mock<ITransactionService> _transactionService;
        private Mock<ILogger<TransferService>> _logger;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _transferRepository = new Mock<ITransferRepository>();
            _clientService = new Mock<IClientService>();
            _contactService = new Mock<IContactService>();
            _balanceService = new Mock<IBalanceService>();
            _transactionService = new Mock<ITransactionService>();
            _logger = new Mock<ILogger<TransferService>>();
            _fixture = new Fixture();

            _transferService = new TransferService(
                _logger.Object,
                _transferRepository.Object, 
                _clientService.Object,
                _contactService.Object, 
                _balanceService.Object,
                _transactionService.Object);
        }

        [Test]
        public async Task ShouldSaveTransaction()
        {
            var client = _fixture.Create<ClientDto>();
            var contactClient = _fixture.Create<ClientDto>();
            var contact = _fixture.Create<ContactDto>();
            var balance = new BalanceDto { Value = 100 };
            var requestTransactionDto = new RequestTransferDto { ContactId = contact.Id, Value = 10 };
            _clientService.Setup(c => c.GetOne(client.Id)).Returns(Task.FromResult(client));
            _clientService.Setup(c => c.GetByAccount(contact.Account)).Returns(Task.FromResult(contactClient));
            _contactService.Setup(c => c.GetOne(client.Id, contact.Id)).Returns(Task.FromResult(contact));
            _balanceService.Setup(b => b.GetBalance(client.Id)).Returns(Task.FromResult(balance));

            var transactionDto = await _transferService.Save(client.Id, requestTransactionDto);

            Assert.IsNotNull(transactionDto);
            _transferRepository.Verify(c => c.Add(It.IsAny<TransferEntity>()));
        }
    }
}
