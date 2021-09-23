using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public class ContactService : IContactService
    {
        private const string _contactRelativeUrl = "/{0}/contacts/{1}";
        private readonly ContactApiParameters _contactApiParameters;

        public ContactService(ContactApiParameters contactApiParameters)
        {
            _contactApiParameters = contactApiParameters;
        }

        public async Task<ContactDto> GetOne(Guid clientid, Guid id)
        {
            var finalContactRelativeUrl = string.Format(_contactRelativeUrl, clientid, id);

            return await _contactApiParameters
                .Url
                .AppendPathSegment(finalContactRelativeUrl)
                .GetJsonAsync<ContactDto>();
        }
    }
}
