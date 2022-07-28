using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.People.PersonById)]
public class GetPersonEndpoint : GetEntityEndpointBase<Person>
{
    public GetPersonEndpoint(IReadRepository<Person> repo) : base(repo) { }
}