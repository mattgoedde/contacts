using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.PhoneNumbers;

[HttpGet(Constants.Endpoints.PhoneNumbers.CertainPhoneNumbers)]
public class GetPhoneNumbersEndpoint : GetEntitiesEndpointBase<PhoneNumber>
{
    public GetPhoneNumbersEndpoint(IReadRepository<PhoneNumber> repo) : base(repo) { }
}