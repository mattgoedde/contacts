using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet("people")]
public class GetAllPeopleEndpoint : GetEntitiesEndpointBase<Person>
{
    public GetAllPeopleEndpoint(IReadRepository<Person> repo) : base(repo)
    {
    }
}