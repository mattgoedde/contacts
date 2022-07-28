using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Endpoints.Base.Get;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.People;

[HttpDelete(Constants.Endpoints.Entities.Contact)]
public class DeleteContactEndpoint : DeleteEntityEndpointBase<Contact>
{
    public DeleteContactEndpoint(IRepository<Contact> repo) : base(repo) { }
}