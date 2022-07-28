using Contacts.Domain.Base;

namespace Contacts.FastApi.Contracts.Responses;

public class EntityResponse<T> : Response
    where T : notnull, EntityBase, new()
{
    public T Entity { get; init; } = default!;
}