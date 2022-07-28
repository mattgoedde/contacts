using Contacts.Data.Database;
using Contacts.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data.Repositories;

public class EfCoreRepository<E> : IRepository<E>
    where E : EntityBase, new()
{
    private readonly ContactsDbContext _context;

    public EfCoreRepository(ContactsDbContext context)
    {
        _context = context;
    }

    public async Task<E> Create(E entity, CancellationToken cancellationToken = default)
    {
        _context.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Read(id, cancellationToken);

        if (entity is null)
            return false;

        _context.Remove<E>(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<E> Read(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<E>> Read(CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<E>> Read(Func<E, bool> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .Where(predicate)
            .AsQueryable()
            .ToListAsync(cancellationToken);
    }

    public async Task<E> ReadWithoutTracking(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .AsNoTrackingWithIdentityResolution()
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<E>> ReadWithoutTracking(CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<E>> ReadWithoutTracking(int[] ids, CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<E>> ReadWithoutTracking(Func<E, bool> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<E>()
            .AsNoTrackingWithIdentityResolution()
            .Where(predicate)
            .AsQueryable()
            .ToListAsync(cancellationToken);
    }

    public IRepository<E> SetUser(string user, CancellationToken cancellationToken = default)
    {
        _context.SetUser(user);
        return this;
    }

    public async Task<E> Update(int id, E entity, CancellationToken cancellationToken = default)
    {
        var entityToUpdate = await Read(id, cancellationToken);
        if (entityToUpdate is null)
            return null;

        _context.Set<E>().Update(entityToUpdate);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}