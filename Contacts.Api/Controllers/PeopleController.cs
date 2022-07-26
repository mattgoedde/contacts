using Contacts.Api.BaseWeb;
using Contacts.Data;
using Contacts.Data.Entities;
using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : EntityController<PersonEntity>
{
    public PeopleController(ILogger<EntityController<PersonEntity>> logger, IRepository<PersonEntity> repository)
    : base(logger, repository)
    {
    }
}