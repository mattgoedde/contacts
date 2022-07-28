using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.Addresses.AddressById)]
public class GetAddressEndpoint : GetEntityEndpointBase<Address>
{
    public GetAddressEndpoint(IReadRepository<Address> repo) : base(repo) { }
}