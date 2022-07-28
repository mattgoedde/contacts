using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.PhoneNumbers.PhoneNumberById)]
public class GetPhoneNumberEndpoint : GetEntityEndpointBase<PhoneNumber>
{
    public GetPhoneNumberEndpoint(IReadRepository<PhoneNumber> repo) : base(repo) { }
}