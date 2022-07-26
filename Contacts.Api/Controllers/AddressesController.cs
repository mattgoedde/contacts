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
public class AddressesController : EntityController<AddressEntity>
{
    public AddressesController(ILogger<EntityController<AddressEntity>> logger, IRepository<AddressEntity> repository)
    : base(logger, repository)
    {
    }
}