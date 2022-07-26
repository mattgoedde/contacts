using Contacts.Api.BaseWeb;
using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhoneNumbersController : EntityController<PhoneNumber>
{
    public PhoneNumbersController(ILogger<EntityController<PhoneNumber>> logger, IRepository<PhoneNumber> repository)
    : base(logger, repository)
    {
    }
}