using Contacts.Data.Entities;

namespace Contacts.Data.Repositories;

public interface IRepository<E>
    where E : class, IEntity, new()
{
    public IRepository<E> SetUser(string? user);
    public Task<E> Create(E entity);
    public Task<E?> Read(int id);
    public Task<IEnumerable<E>> Read();
    public Task<IEnumerable<E>> Read(Func<E, bool> predicate);
    public Task<E?> Update(int id, E entity);
    public Task<bool> Delete(int id);
}