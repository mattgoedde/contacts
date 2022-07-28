using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.Contacts.CertainContacts)]
public class GetContactsEndpoint : GetEntitiesEndpointBase<Person>
{
    public GetContactsEndpoint(IReadRepository<Person> repo) : base(repo) { }
}