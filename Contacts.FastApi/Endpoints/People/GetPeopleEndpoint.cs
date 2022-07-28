using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.People.CertainPeople)]
public class GetPeopleEndpoint : GetEntitiesEndpointBase<Person>
{
    public GetPeopleEndpoint(IReadRepository<Person> repo) : base(repo) { }
}