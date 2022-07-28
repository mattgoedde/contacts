using Contacts.Domain.Base;

namespace Contacts.FastApi.Contracts.Requests;

public class GetEntityRequest<T> : Request
    where T : notnull, EntityBase, new()
{
    public int Id { get; init; } = default!;
}