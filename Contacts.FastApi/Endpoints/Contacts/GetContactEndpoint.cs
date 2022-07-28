using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.Contacts.ContactById)]
public class GetContactEndpoint : GetEntityEndpointBase<Person>
{
    public GetContactEndpoint(IReadRepository<Person> repo) : base(repo) { }
}