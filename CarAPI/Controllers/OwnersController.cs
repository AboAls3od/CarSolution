using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnersService _ownersService;

        public OwnersController(IOwnersService ownersService)
        {
            _ownersService = ownersService;
        }

        [HttpGet]
        public ActionResult<List<OwnerDto>> Get()
        {
            var owners = _ownersService.GetOwners();
            var ownerDtos = owners.Select(o => new OwnerDto
            {
                Id = o.Id,
                Name = o.Name,
                CarId = o.Car?.Id
            }).ToList();

            return Ok(ownerDtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<OwnerDto> Get(int id)
        {
            var owner = _ownersService.GetById(id);
            if (owner == null)
            {
                return NotFound();
            }

            return Ok(new OwnerDto
            {
                Id = owner.Id,
                Name = owner.Name,
                CarId = owner.Car?.Id
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] Owner owner)
        {
            var success = _ownersService.AddOwner(owner);
            if (!success)
            {
                return BadRequest("Could not add owner.");
            }
            return CreatedAtAction(nameof(Get), new { id = owner.Id }, owner);
        }

        [HttpPost("buy")]
        public IActionResult BuyCar([FromBody] BuyCarInput input)
        {
            var result = _ownersService.BuyCar(input);
            if (result.Contains("Successfull"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
