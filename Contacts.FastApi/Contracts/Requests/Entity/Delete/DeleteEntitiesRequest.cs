using Contacts.Domain.Base;

namespace Contacts.FastApi.Contracts.Requests;

public class DeleteEntitiesRequest<T> : Request
    where T : notnull, EntityBase, new()
{
    public T[] Entities { get; init; } = default!;
}