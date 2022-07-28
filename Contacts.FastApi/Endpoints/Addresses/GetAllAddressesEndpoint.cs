using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Addresses;

[HttpGet(Constants.Endpoints.Addresses.AllAddresses)]
public class GetAllAddressesEndpoint : GetAllEntitiesEndpointBase<Address>
{
    public GetAllAddressesEndpoint(IReadRepository<Address> repo) : base(repo) { }
}