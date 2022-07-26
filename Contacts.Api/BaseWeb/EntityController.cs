using Contacts.Data.Repositories;
using Contacts.Domain.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.BaseWeb;

[Authorize]
public abstract class EntityController<E> : ControllerBase, ICrudController<E>
    where E : BaseEntity, new()
{
    private readonly ILogger _logger;
    private readonly IRepository<E> _repository;

    public EntityController(ILogger<EntityController<E>> logger, IRepository<E> repository)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<E>> Create(E entity)
    {
        _logger.LogInformation("Create: User: {user} Entity: {entity}", User?.Identity?.Name, entity);
        try
        {
            if (ModelState.IsValid && entity is not null)
            {
                var newEntity = await _repository.SetUser(User?.Identity?.Name).Create(entity);
                return CreatedAtAction(nameof(Read), newEntity.Id, newEntity);
            }
            else return UnprocessableEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in Create: User: {user} Entity: {entity}", User?.Identity?.Name, entity);
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<E>> Read(int id)
    {
        _logger.LogInformation("Read: User: {user} Id: {id}", User?.Identity?.Name, id);
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Read(id);
                if (result is not null)
                    return Ok(result);
                else
                    return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in Read: User: {user} Id: {id}", User?.Identity?.Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<E>>> Read()
    {
        _logger.LogInformation("Read: User: {user}", User?.Identity?.Name);
        try
        {
            var result = await _repository.Read();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in Read: User: {user}", User?.Identity?.Name);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<E>> Update(int id, E entity)
    {
        _logger.LogInformation("Update User: {user} Id: {id} Entity: {entity}", User?.Identity?.Name, id, entity);
        try
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = await _repository.SetUser(User?.Identity?.Name).Update(id, entity);
                return Ok(entityToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in Update User: {user} Id: {id} Entity: {entity}", User?.Identity?.Name, id, entity);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<E>> Delete(int id)
    {
        _logger.LogInformation("Delete: User: {user} Id: {id}", User?.Identity?.Name, id);
        try
        {
            if (ModelState.IsValid && id > 0)
            {
                if (await _repository.SetUser(User?.Identity?.Name).Delete(id))
                    return StatusCode(StatusCodes.Status204NoContent);
                else throw new Exception("Delete failed");
            }
            else return UnprocessableEntity();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in Delete: User: {user} Id: {id}", User?.Identity?.Name, id);
            return BadRequest(ex.ToString());
        }
    }
}