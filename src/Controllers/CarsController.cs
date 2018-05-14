using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ParkingAPI.Controllers
{
    [Produces("application/json")]
    public class CarsController : Controller
    {
        private ParkingService service { get; }

        public CarsController(ParkingService service)
        {
            this.service = service;
        }

        // GET: api/Cars/GetCars 
        [Route("api/[Controller]/GetCars")]
        [HttpGet]
        public List<Car> GetCars()
        {
            return Mapper.Map<List<Parking.Car>, List<Car>>(service.GetCars());
        }

        // GET: api/Cars/DetailsOnCar/1
        [Route("api/[Controller]/DetailsOnCar/{numberStr}")]
        [HttpGet("{numberStr}")]
        public ObjectResult GetDetailsOnOneCar(string numberStr)
        {

            if (!int.TryParse(numberStr, out var number)) return BadRequest("It must be numbers");
            if (number > service.GetNumberOfBusyPlaces() || number==0) return NotFound("The place with this number is empty.");
            return Ok(service.GetDetailsOnOneCar(number));
        }

        // POST: api/Cars/AddCar/?type=1&balanceStr=2.2 
        [Route("api/[Controller]/AddCar")]
        [HttpPost]
        public IActionResult PostCar(string type, string balanceStr)
        {
            bool isValid = Int32.TryParse(type, out var i) ? Enum.IsDefined(typeof(CarType), i) : Enum.IsDefined(typeof(CarType), type);
            if (!isValid || !decimal.TryParse(balanceStr, out var balance)) return BadRequest();
            service.PostCar(Mapper.Map<CarType, Parking.CarType>(Enum.Parse<CarType>(type)), balance);
            return Ok();
        }

        // DELETE: api/Cars/DeleteCar/1
        [Route("api/[Controller]/DeleteCar/{numberStr}")]
        [HttpDelete("{numberStr}")]
        public ObjectResult DeleteCar(string numberStr)
        {
            if (!int.TryParse(numberStr, out var number)) return BadRequest("It must be numbers");
            if (number > service.GetNumberOfBusyPlaces() || number == 0) return NotFound("The place with this number is empty.");
            return Ok(service.DeleteCar(number));
        }
    }
}
