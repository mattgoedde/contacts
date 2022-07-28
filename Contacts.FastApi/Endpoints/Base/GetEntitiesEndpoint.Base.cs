using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Contracts.Responses;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Base;

public abstract class GetEntitiesEndpointBase<T> : Endpoint<GetEntitiesRequest<T>, EntitiesResponse<T>>
    where T : notnull, EntityBase, new()
{

    private readonly IReadRepository<T> _repo;
    public GetEntitiesEndpointBase(IReadRepository<T> repo)
    {
        _repo = repo;
    }

    public override async Task HandleAsync(GetEntitiesRequest<T> req, CancellationToken ct)
    {
        IEnumerable<T> entities = await _repo.ReadWithoutTracking(ct);
        if (entities is not null && entities.Any())
        {
            EntitiesResponse<T> res = new()
            {
                Entities = entities,
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