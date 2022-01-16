using Banking.Operation.Transfer.Command.Domain.Abstractions.Exceptions;
using Banking.Operation.Transfer.Command.Domain.Abstractions.Services;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Banking.Operation.Transfer.Command.Domain.Transfer.Enums;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public class TransferService : ITransferService
    {
        private readonly ILogger<TransferService> _logger;
        private readonly ITransferRepository _transferRepository;
        private readonly IClientService _clientService;
        private readonly IContactService _contactService;
        private readonly IBalanceService _balanceService;
        private readonly ITransactionService _transactionService;
        private readonly IReceiptService _receiptService;
        private readonly INotificationService _notificationService;

        public TransferService(
            ILogger<TransferService> logger,
            ITransferRepository transferRepository,
            IClientService clientService,
            IContactService contactService,
            IBalanceService balanceService,
            ITransactionService transactionService,
            IReceiptService receiptService, 
            INotificationService notificationService)
        {
            _logger = logger;
            _transferRepository = transferRepository;
            _clientService = clientService;
            _contactService = contactService;
            _balanceService = balanceService;
            _transactionService = transactionService;
            _receiptService = receiptService;
            _notificationService = notificationService;
        }

        public async Task<ResponseTransferDto> Save(Guid clientId, RequestTransferDto transfer)
        {
            var client = await ValidateClient(clientId);

            var contact = await ValidateContact(client, transfer.ContactId);

            await ValidateBalance(clientId, transfer.Value);

            await MakeTransactions(client, contact, transfer.Value);

            var transferEntity = new TransferEntity(client, contact, transfer.Value);

            await _transferRepository.Add(transferEntity);

            await SendReceipt(client, contact, transferEntity);

            SendNotification(client, contact, transferEntity);

            return new ResponseTransferDto(transferEntity);
        }

        private void SendNotification(ClientDto client, ContactDto contact, TransferEntity transferEntity)
        {
            try
            {
                var message = new MessageDto(
                transferEntity.Id,
                client.Id,
                client.Name,
                contact.Id,
                contact.Name,
                transferEntity.Value,
                transferEntity.CreatedAt,
                transferEntity.CreatedBy
                );

                _notificationService.PublishMessage(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on SendNotification", ex);
                throw new BussinessException("Operation not performed", $"Unable to create the message + {ex.Message}");
            }
        }

        private async Task SendReceipt(ClientDto client, ContactDto contact, TransferEntity transferEntity)
        {
            try
            {
                var receipt = new ReceiptDto(
                transferEntity.Id,
                client.Id,
                client.Name,
                contact.Id,
                contact.Name,
                transferEntity.Value,
                transferEntity.CreatedAt,
                transferEntity.CreatedBy
                );

            await _receiptService.PublishReceipt(receipt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on SendReceipt", ex);
                throw new BussinessException("Operation not performed", $"Unable to create the receipt + {ex.Message}");
            }
        }

        private async Task MakeTransactions(ClientDto client, ContactDto contact, decimal value)
        {
            try
            {
                var contactClient = await _clientService.GetByAccount(contact.Account);

                await _transactionService.Post(client.Id, TransactionType.Debit, value);                

                await _transactionService.Post(contactClient.Id, TransactionType.Credit, value);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on MakeTransactions", ex);
                throw new BussinessException("Operation not performed", "Unable to perform transaction postings");
            }
        }

        private async Task ValidateBalance(Guid clientId, decimal transferValue)
        {
            try
            {
                var balance = await _balanceService.GetBalance(clientId);

                if (balance.Value < transferValue)
                {
                    throw new BussinessException("Operation not performed", "Balance unavailable to carry out the operation");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on ValidateBalance", ex);
                throw new BussinessException("Operation not performed", "Balance unavailable to carry out the operation");
            }
        }

        private async Task<ClientDto> ValidateClient(Guid clientId)
        {
            try
            {
                return await _clientService.GetOne(clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on ValidateClient", ex);
                throw new BussinessException("Operation not performed", "Client not registered");
            }            
        }

        private async Task<ContactDto> ValidateContact(ClientDto client, Guid contactId)
        {
            try
            {
                return await _contactService.GetOne(client.Id, contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on ValidateContact", ex);
                throw new BussinessException("Operation not performed", "Contact not registered");
            }
        }
    }
}
