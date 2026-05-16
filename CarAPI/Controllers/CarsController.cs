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
    public class CarsController : ControllerBase
    {
        private readonly ICarsService _carService;

        public CarsController(ICarsService carsService)
        {
            _carService = carsService;
        }

        [HttpGet]
        public ActionResult<List<CarDto>> Get()
        {
            var cars = _carService.GetAll();
            var carDtos = cars.Select(c => new CarDto
            {
                Id = c.Id,
                Type = c.Type,
                Velocity = c.Velocity,
                Price = c.Price,
                OwnerName = c.Owner?.Name
            }).ToList();

            return Ok(carDtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CarDto> Get(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(new CarDto
            {
                Id = car.Id,
                Type = car.Type,
                Velocity = car.Velocity,
                Price = car.Price,
                OwnerName = car.Owner?.Name
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] Car car)
        {
            var success = _carService.AddCar(car);
            if (!success)
            {
                return BadRequest("Could not add car.");
            }
            return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
        }

        [HttpDelete("{carId:int}")]
        public IActionResult Delete(int carId)
        {
            var success = _carService.Remove(carId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
