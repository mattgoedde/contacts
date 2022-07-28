using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Contacts.FastApi.Contracts.Requests;
using Contacts.FastApi.Contracts.Responses;
using FastEndpoints;

namespace Contacts.FastApi.Endpoints.Base.Get;

public abstract class PostEntityEndpointBase<T> : Endpoint<PostEntityRequest<T>, EntityResponse<T>>
    where T : notnull, EntityBase, new()
{

    private readonly IRepository<T> _repo;
    public PostEntityEndpointBase(IRepository<T> repo)
    {
        _repo = repo;
    }

    public override async Task HandleAsync(PostEntityRequest<T> req, CancellationToken ct)
    {
        T entity = await _repo.Create(req.Entity, ct);
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