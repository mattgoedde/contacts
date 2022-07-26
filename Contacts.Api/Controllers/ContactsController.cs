using Contacts.Api.BaseWeb;
using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : EntityController<Contact>
{
    public ContactController(ILogger<EntityController<Contact>> logger, IRepository<Contact> repository)
    : base(logger, repository)
    {
    }
}