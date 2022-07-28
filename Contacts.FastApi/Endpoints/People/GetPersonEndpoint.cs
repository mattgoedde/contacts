using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet("person/{id:int}")]
public class GetPersonEndpoint : GetEntityEndpointBase<Person>
{
    public GetPersonEndpoint(IReadRepository<Person> repo) : base(repo)
    {
    }
}