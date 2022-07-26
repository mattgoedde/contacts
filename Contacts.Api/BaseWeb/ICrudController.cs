using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.BaseWeb;

public interface ICrudController<TEntity>
{
    public Task<ActionResult<TEntity>> Create(TEntity model);
    public Task<ActionResult<TEntity>> Read(int id);
    public Task<ActionResult<IEnumerable<TEntity>>> Read();
    public Task<ActionResult<TEntity>> Update(int id, TEntity model);
    public Task<ActionResult<TEntity>> Delete(int id);
}