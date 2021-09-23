using Banking.Operation.Transfer.Command.Domain.Abstractions.Exceptions;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IClientService _clientService;
        private readonly IContactService _contactService;

        public TransferService(ITransferRepository transferRepository, IClientService clientService, IContactService contactService)
        {
            _transferRepository = transferRepository;
            _clientService = clientService;
            _contactService = contactService;
        }

        public async Task<ResponseTransferDto> Save(Guid clientId, RequestTransferDto transfer)
        {
            var client = await ValidateClient(clientId);

            var contact = await ValidateContact(client, transfer.ContactId);

            var transactionEntity = new TransferEntity(client, contact, transfer.Value);

            await _transferRepository.Add(transactionEntity);

            return new ResponseTransferDto(transactionEntity);
        }

        private async Task<ClientDto> ValidateClient(Guid clientId)
        {
            var client = await _clientService.GetOne(clientId);

            if (client is null)
            {
                throw new BussinessException("Operation not performed", "Client not registered");
            }

            return client;
        }

        private async Task<ContactDto> ValidateContact(ClientDto client, Guid contactId)
        {
            var contact = await _contactService.GetOne(client.Id, contactId);

            if (contact is null)
            {
                throw new BussinessException("Operation not performed", "Contact not registered");
            }

            return contact;
        }
    }
}
