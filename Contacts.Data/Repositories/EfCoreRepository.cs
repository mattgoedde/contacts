using Contacts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Contacts.Data.Repositories;

public class EfCoreRepository<E> : IRepository<E>
    where E : class, IEntity, new()
{
    private readonly ContextOptions _options;
    private readonly ContactsDbContext _context;

    public EfCoreRepository(IOptionsSnapshot<ContextOptions> contextOptions, ContactsDbContext context)
    {
        _options = contextOptions.Get("ContextOptions");
        _context = context;
    }

    public async Task<E> Create(E entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Read(id);

        if (entity is null)
            return false;

        _context.Remove<E>(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<E?> Read(int id)
    {
        return await _context.Set<E>()
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<E>> Read()
    {
        return await _context.Set<E>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<E>> Read(Func<E, bool> predicate)
    {
        return await _context.Set<E>()
            .AsNoTracking()
            .Where(predicate)
            .AsQueryable()
            .ToListAsync();
    }

    public IRepository<E> SetUser(string? user)
    {
        _context.SetUser(user);
        return this;
    }

    public async Task<E?> Update(int id, E entity)
    {
        var entityToUpdate = await Read(id);
        if (entityToUpdate is null)
            return null;

        entityToUpdate.Update(entity);
        _context.Set<E>().Update(entityToUpdate);

        await _context.SaveChangesAsync();

        return entityToUpdate;
    }
}