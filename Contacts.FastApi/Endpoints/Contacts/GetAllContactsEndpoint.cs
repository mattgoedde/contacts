using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.Contacts.AllContacts)]
public class GetAllContactsEndpoint : GetAllEntitiesEndpointBase<Person>
{
    public GetAllContactsEndpoint(IReadRepository<Person> repo) : base(repo) { }
}