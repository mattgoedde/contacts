using Contacts.Api.BaseWeb;
using Contacts.Data.Repositories;
using Contacts.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : EntityController<Address>
{
    public AddressesController(ILogger<EntityController<Address>> logger, IRepository<Address> repository)
    : base(logger, repository)
    {
    }
}