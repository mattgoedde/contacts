using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Contacts.Domain.Models;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Contracts.Responses;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Base.Get;

public abstract class DeleteEntityEndpointBase<T> : Endpoint<DeleteEntityRequest<T>, Response>
    where T : notnull, EntityBase, new()
{

    private readonly IRepository<T> _repo;
    public DeleteEntityEndpointBase(IRepository<T> repo)
    {
        _repo = repo;
    }

    public override async Task HandleAsync(DeleteEntityRequest<T> req, CancellationToken ct)
    {
        try
        {
            bool success = await _repo.Delete(req.Entity.Id, ct);
            if (success)
                await SendNoContentAsync(ct);
            else
                await SendErrorsAsync(400, ct);
        }
        catch
        {
            await SendErrorsAsync(500, ct);
        }
    }
}