using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Contacts.Domain.Models;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Contracts.Responses;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Base.Get;

public abstract class PutEntityEndpointBase<T> : Endpoint<PutEntityRequest<T>, EntityResponse<T>>
    where T : notnull, EntityBase, new()
{

    private readonly IRepository<T> _repo;
    public PutEntityEndpointBase(IRepository<T> repo)
    {
        _repo = repo;
    }

    public override async Task HandleAsync(PutEntityRequest<T> req, CancellationToken ct)
    {
        try
        {
            T entity = await _repo.Update(req.Entity.Id, req.Entity, ct);
            if (entity is not null)
                await SendOkAsync(new EntityResponse<T>()
                {
                    Entity = entity,
                    Success = true
                }, ct);
            else
                await SendErrorsAsync(400, ct);
        }
        catch
        {
            await SendErrorsAsync(500, ct);
        }
    }
}