using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public class ClientService : IClientService
    {
        private const string _clientRelativeUrl = "/clients/{0}";
        private readonly ClientApiParameters _clientApiParameters;

        public ClientService(ClientApiParameters clientApiParameters)
        {
            _clientApiParameters = clientApiParameters;
        }

        public async Task<ClientDto> GetByAccount(int account)
        {
            var finalClientRelativeUrl = string.Format(_clientRelativeUrl, account);

            return await _clientApiParameters
                .Url
                .AppendPathSegment(finalClientRelativeUrl)
                .GetJsonAsync<ClientDto>();
        }

        public async Task<ClientDto> GetOne(Guid id)
        {
            var finalClientRelativeUrl = string.Format(_clientRelativeUrl, id);

            return await _clientApiParameters
                .Url
                .AppendPathSegment(finalClientRelativeUrl)
                .GetJsonAsync<ClientDto>();
        }
    }
}
