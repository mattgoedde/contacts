using Contacts.Domain.Base;

namespace Contacts.Data.Repositories
{
    public interface IReadRepository<E>
    where E : EntityBase, new()
    {
        public Task<E> ReadWithoutTracking(int id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> ReadWithoutTracking(CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> ReadWithoutTracking(int[] ids, CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> ReadWithoutTracking(Func<E, bool> predicate, CancellationToken cancellationToken = default);
    }
}
