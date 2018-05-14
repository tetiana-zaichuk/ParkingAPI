using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ParkingAPI.Models;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class CarsController : Controller
    {
        private ParkingService Service { get; }

        public CarsController(ParkingService service) => Service = service;

        // GET: api/Cars/GetCars 
        [Route("GetCars")]
        [HttpGet]
        public List<Car> GetCars() => Mapper.Map<List<Parking.Car>, List<Car>>(Service.GetCars());

        // GET: api/Cars/DetailsOnCar/1
        [Route("DetailsOnCar/{numberStr}")]
        [HttpGet("{numberStr}")]
        public ObjectResult GetDetailsOnOneCar(string numberStr)
        {

            if (!int.TryParse(numberStr, out var number)) return BadRequest("It must be numbers");
            if (number > Service.GetNumberOfBusyPlaces() || number==0) return NotFound("The place with this number is empty.");
            return Ok(Service.GetDetailsOnOneCar(number));
        }

        // POST: api/Cars/AddCar/?type=1&balanceStr=2.2 
        [Route("AddCar")]
        [HttpPost]
        public IActionResult PostCar(string type, string balanceStr)
        {
            var isValid = Int32.TryParse(type, out var i) ? Enum.IsDefined(typeof(CarType), i) : Enum.IsDefined(typeof(CarType), type);
            if (!isValid || !decimal.TryParse(balanceStr, out var balance)) return BadRequest();
            Service.PostCar(Mapper.Map<CarType, Parking.CarType>(Enum.Parse<CarType>(type)), balance);
            return Ok();
        }

        // DELETE: api/Cars/DeleteCar/1
        [Route("DeleteCar/{numberStr}")]
        [HttpDelete("{numberStr}")]
        public ObjectResult DeleteCar(string numberStr)
        {
            if (!int.TryParse(numberStr, out var number)) return BadRequest("It must be numbers");
            if (number > Service.GetNumberOfBusyPlaces() || number == 0) return NotFound("The place with this number is empty.");
            return Ok(Service.DeleteCarAsync(number).Result);
        }
    }
}
