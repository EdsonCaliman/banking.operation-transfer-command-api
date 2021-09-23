using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public class BalanceService : IBalanceService
    {
        private const string _balanceRelativeUrl = "/{0}/balance";
        private readonly BalanceApiParameters _balanceApiParameters;

        public BalanceService(BalanceApiParameters balanceApiParameters)
        {
            _balanceApiParameters = balanceApiParameters;
        }

        public async Task<BalanceDto> GetBalance(Guid clientId)
        {
            var finalBalanceRelativeUrl = string.Format(_balanceRelativeUrl, clientId);

            return await _balanceApiParameters
                .Url
                .AppendPathSegment(finalBalanceRelativeUrl)
                .GetJsonAsync<BalanceDto>();
        }
    }
}
