using AutoFixture;
using Banking.Operation.Transfer.Command.Domain.Abstractions.Exceptions;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Banking.Operation.Transfer.Command.Domain.Transfer.Enums;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Banking.Operation.Transfer.Command.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Tests.Transfer.Services
{
    public class TransferServiceTest
    {
        private MockRepository _mockBuilder;
        private ITransferService _transferService;
        private Mock<ITransferRepository> _transferRepository;
        private Mock<IClientService> _clientService;
        private Mock<IContactService> _contactService;
        private Mock<IBalanceService> _balanceService;
        private Mock<ITransactionService> _transactionService;
        private Mock<ILogger<TransferService>> _logger;
        private Mock<IReceiptService> _receiptService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockBuilder = new MockRepository(MockBehavior.Strict);
            _transferRepository =  _mockBuilder.Create<ITransferRepository>();
            _clientService = _mockBuilder.Create<IClientService>();
            _contactService = _mockBuilder.Create<IContactService>();
            _balanceService = _mockBuilder.Create<IBalanceService>();
            _transactionService = _mockBuilder.Create<ITransactionService>();
            _logger = _mockBuilder.Create<ILogger<TransferService>>();
            _receiptService = _mockBuilder.Create<IReceiptService>();
            _fixture = new Fixture();

            _transferService = new TransferService(
                _logger.Object,
                _transferRepository.Object, 
                _clientService.Object,
                _contactService.Object, 
                _balanceService.Object,
                _transactionService.Object,
                _receiptService.Object);
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
            _transactionService.Setup(t => t.Post(client.Id, TransactionType.Debit, requestTransactionDto.Value))
                .Returns(Task.CompletedTask);
            _transactionService.Setup(t => t.Post(contactClient.Id, TransactionType.Credit, requestTransactionDto.Value))
                .Returns(Task.CompletedTask);
            _transferRepository.Setup(t => t.Add(It.IsAny<TransferEntity>())).Returns(Task.CompletedTask);
            _receiptService.Setup(t => t.PublishReceipt(It.IsAny<ReceiptDto>()))
                .Returns(Task.CompletedTask);

            var transactionDto = await _transferService.Save(client.Id, requestTransactionDto);

            Assert.IsNotNull(transactionDto);
            _mockBuilder.VerifyAll();
        }

        [Test]
        public void ShouldNotSaveTransactionWithInvalidClientByAccountFind()
        {
            _logger.MockLog(LogLevel.Error);
            var client = _fixture.Create<ClientDto>();
            var contactClient = _fixture.Create<ClientDto>();
            var contact = _fixture.Create<ContactDto>();
            var balance = new BalanceDto { Value = 100 };
            var requestTransactionDto = new RequestTransferDto { ContactId = contact.Id, Value = 10 };
            _clientService.Setup(c => c.GetOne(client.Id)).Returns(Task.FromResult(client));
            _contactService.Setup(c => c.GetOne(client.Id, contact.Id)).Returns(Task.FromResult(contact));
            _balanceService.Setup(b => b.GetBalance(client.Id)).Returns(Task.FromResult(balance));
            _clientService.Setup(c => c.GetByAccount(contact.Account)).Throws(new Exception());

            Func<Task> action = async () => { await _transferService.Save(client.Id, requestTransactionDto); };
            action.Should().ThrowAsync<BussinessException>();

            _mockBuilder.VerifyAll();
        }

        [Test]
        public void ShouldNotSaveTransactionWithInvalidClientByGetOneFind()
        {
            _logger.MockLog(LogLevel.Error);
            var client = _fixture.Create<ClientDto>();
            var contactClient = _fixture.Create<ClientDto>();
            var contact = _fixture.Create<ContactDto>();
            var balance = new BalanceDto { Value = 100 };
            var requestTransactionDto = new RequestTransferDto { ContactId = contact.Id, Value = 10 };
            _clientService.Setup(c => c.GetOne(client.Id)).Throws(new Exception());

            Func<Task> action = async () => { await _transferService.Save(client.Id, requestTransactionDto); };
            action.Should().ThrowAsync<BussinessException>();

            _mockBuilder.VerifyAll();
        }

        [Test]
        public void ShouldNotSaveTransactionWithInvalidContact()
        {
            _logger.MockLog(LogLevel.Error);
            var client = _fixture.Create<ClientDto>();
            var contactClient = _fixture.Create<ClientDto>();
            var contact = _fixture.Create<ContactDto>();
            var balance = new BalanceDto { Value = 100 };
            var requestTransactionDto = new RequestTransferDto { ContactId = contact.Id, Value = 10 };
            _clientService.Setup(c => c.GetOne(client.Id)).Returns(Task.FromResult(client));
            _contactService.Setup(c => c.GetOne(client.Id, contact.Id)).Throws(new Exception());

            Func<Task> action = async () => { await _transferService.Save(client.Id, requestTransactionDto); };
            action.Should().ThrowAsync<BussinessException>();

            _mockBuilder.VerifyAll();
        }

        [Test]
        public void ShouldNotSaveTransactionWithInsufficientBalance()
        {
            _logger.MockLog(LogLevel.Error);
            var client = _fixture.Create<ClientDto>();
            var contactClient = _fixture.Create<ClientDto>();
            var contact = _fixture.Create<ContactDto>();
            var balance = new BalanceDto { Value = 5 };
            var requestTransactionDto = new RequestTransferDto { ContactId = contact.Id, Value = 10 };
            _clientService.Setup(c => c.GetOne(client.Id)).Returns(Task.FromResult(client));
            _contactService.Setup(c => c.GetOne(client.Id, contact.Id)).Returns(Task.FromResult(contact));
            _balanceService.Setup(b => b.GetBalance(client.Id)).Returns(Task.FromResult(balance));

            Func<Task> action = async () => { await _transferService.Save(client.Id, requestTransactionDto); };
            action.Should().ThrowAsync<BussinessException>();

            _mockBuilder.VerifyAll();
        }
    }
}
