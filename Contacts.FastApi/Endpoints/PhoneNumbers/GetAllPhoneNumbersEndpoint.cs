using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.PhoneNumbers;

[HttpGet(Constants.Endpoints.PhoneNumbers.AllPhoneNumbers)]
public class GetAllPhoneNumbersEndpoint : GetAllEntitiesEndpointBase<PhoneNumber>
{
    public GetAllPhoneNumbersEndpoint(IReadRepository<PhoneNumber> repo) : base(repo) { }
}