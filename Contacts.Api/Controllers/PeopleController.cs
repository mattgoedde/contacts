using Contacts.Api.BaseWeb;
using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : EntityController<Person>
{
    public PeopleController(ILogger<EntityController<Person>> logger, IRepository<Person> repository)
    : base(logger, repository)
    {
    }
}