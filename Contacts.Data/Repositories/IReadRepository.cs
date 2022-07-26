﻿using Contacts.Domain.Base;

namespace Contacts.Data.Repositories
{
    public interface IReadRepository<E>
    where E : BaseEntity, new()
    {
        public Task<E> Read(int id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> Read(CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> Read(Func<E, bool> predicate, CancellationToken cancellationToken = default);
        public Task<E> ReadWithoutTracking(int id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> ReadWithoutTracking(CancellationToken cancellationToken = default);
        public Task<IEnumerable<E>> ReadWithoutTracking(Func<E, bool> predicate, CancellationToken cancellationToken = default);
    }
}