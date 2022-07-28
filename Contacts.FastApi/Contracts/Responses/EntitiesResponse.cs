using Contacts.Domain.Base;

namespace Contacts.FastApi.Contracts.Responses;

public class EntitiesResponse<T> : Response
    where T : notnull, EntityBase, new()
{
    public IEnumerable<T> Entities { get; init; } = default!;
}