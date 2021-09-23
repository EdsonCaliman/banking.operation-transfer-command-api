using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Enums;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public class TransactionService : ITransactionService
    {
        private const string _transactionRelativeUrl = "/{0}/transactions";
        private readonly TransactionApiParameters _transactionApiParameters;

        public TransactionService(TransactionApiParameters transactionApiParameters)
        {
            _transactionApiParameters = transactionApiParameters;
        }

        public async Task Post(Guid clientId, TransactionType type, decimal value)
        {
            var transaction = new TransactionDto(type, value);
            var finalTransactionRelativeUrl = string.Format(_transactionRelativeUrl, clientId);

            await _transactionApiParameters
                .Url
                .AppendPathSegment(finalTransactionRelativeUrl)
                .PostJsonAsync(transaction);
        }
    }
}
