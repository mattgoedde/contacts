using Contacts.Domain.Base;

namespace Contacts.Data.Repositories;

public interface IRepository<E> : IReadRepository<E>
    where E : BaseEntity, new()
{
    public IRepository<E> SetUser(string user, CancellationToken cancellationToken = default);
    public Task<E> Create(E entity, CancellationToken cancellationToken = default);
    public Task<E> Update(int id, E entity, CancellationToken cancellationToken = default);
    public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
}