using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpPost(Constants.Endpoints.Entities.Contact)]
public class PostContactEndpoint : PostEntityEndpointBase<Contact>
{
    public PostContactEndpoint(IRepository<Contact> repo) : base(repo) { }
}