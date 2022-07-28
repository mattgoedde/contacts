using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Contacts.Domain.Models;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Contracts.Responses;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Base.Get;

public abstract class PostEntityEndpointBase<T> : Endpoint<EntityRequest<T>, EntityResponse<T>>
    where T : notnull, EntityBase, new()
{

    private readonly IRepository<T> _repo;
    public PostEntityEndpointBase(IRepository<T> repo)
    {
        _repo = repo;
    }

    public override async Task HandleAsync(EntityRequest<T> req, CancellationToken ct)
    {
        try
        {
            T entity = await _repo.Create(req.Entity, ct);
            if (entity is not null)
            {
                EntityResponse<T> res = new()
                {
                    Entity = entity,
                    Success = true
                };
                await SendCreatedAtAsync<GetEntityEndpointBase<T>>(
                    routeValues: entity.Id,
                    responseBody: res,
                    generateAbsoluteUrl: true,
                    cancellation: ct);
            }
            else
            {
                await SendErrorsAsync(400, cancellation: ct);
            }
        }
        catch
        {
            await SendErrorsAsync(500, ct);
        }
    }
}