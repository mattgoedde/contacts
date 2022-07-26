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
public class ContactController : EntityController<ContactEntity>
{
    public ContactController(ILogger<EntityController<ContactEntity>> logger, IRepository<ContactEntity> repository)
    : base(logger, repository)
    {
    }
}