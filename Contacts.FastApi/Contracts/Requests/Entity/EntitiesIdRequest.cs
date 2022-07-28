using Contacts.Domain.Base;

namespace Contacts.FastApi.Contracts.Requests;

public class EntitiesIdRequest<T> : Request
    where T : notnull, EntityBase, new()
{
    public int[] Ids { get; init; } = default!;
}