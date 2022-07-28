using Contacts.Domain.Base;

namespace Contacts.FastApi.Contracts.Requests;

public class EntityRequest<T> : Request
    where T : notnull, EntityBase, new()
{
    public T Entity { get; init; } = default!;
}