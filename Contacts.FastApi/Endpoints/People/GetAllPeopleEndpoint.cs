using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpGet(Constants.Endpoints.People.AllPeople)]
public class GetAllPeopleEndpoint : GetAllEntitiesEndpointBase<Person>
{
    public GetAllPeopleEndpoint(IReadRepository<Person> repo) : base(repo) { }
}