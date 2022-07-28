using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Contracts.Responses;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Base;

public abstract class GetEntityEndpointBase<T> : Endpoint<GetEntityRequest<T>, EntityResponse<T>>
    where T : notnull, EntityBase, new()
{

    private readonly IReadRepository<T> _repo;
    public GetEntityEndpointBase(IReadRepository<T> repo)
    {
        _repo = repo;
    }

    public override async Task HandleAsync(GetEntityRequest<T> req, CancellationToken ct)
    {
        T entity = await _repo.ReadWithoutTracking(req.Id, ct);
        if (entity is not null)
        {
            EntityResponse<T> res = new()
            {
                Entity = entity,
                Success = true
            };
            await SendOkAsync(res, ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}