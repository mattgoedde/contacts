using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Addresses;

[HttpGet(Constants.Endpoints.Addresses.CertainAddresses)]
public class GetAddressesEndpoint : GetEntitiesEndpointBase<Address>
{
    public GetAddressesEndpoint(IReadRepository<Address> repo) : base(repo) { }
}