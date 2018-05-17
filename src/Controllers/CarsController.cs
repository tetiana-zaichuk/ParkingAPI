using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        // GET: api/Cars
        [HttpGet]
        public List<Car> GetCars() => Mapper.Map<List<Parking.Car>, List<Car>>(Service.GetCars());

        // GET: api/Cars/1
        [HttpGet("{carNumber}")]
        public ObjectResult GetCarDetails(string carNumber)
        {
            if (!int.TryParse(carNumber, out var number))
            {
                return BadRequest("It must be numbers");
            }

            if (number > Service.GetNumberOfBusyPlaces() || number == 0)
            {
                return NotFound("The place with this number is empty.");
            }

            return Ok(Service.GetCarDetails(number));
        }

        // POST: api/Cars
        [HttpPost]
        public IActionResult AddCar([FromBody]Car car)
        {
            var type = car.CarType.ToString();
            var isValid = Int32.TryParse(type, out var i) ? Enum.IsDefined(typeof(CarType), i) : Enum.IsDefined(typeof(CarType), car.CarType);
            if (!isValid || !decimal.TryParse(car.Balance.ToString(), out var balance))
            {
                return BadRequest(car.Balance);
            }
            Service.PostCar(Mapper.Map<CarType, Parking.CarType>(Enum.Parse<CarType>(type)), balance);
            return Ok();
        }

        // DELETE: api/Cars/1
        [HttpDelete("{carNumber}")]
        public async Task<HttpResponseMessage> DeleteCar(string carNumber)
        {
            if (!int.TryParse(carNumber, out var number))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (number > Service.GetNumberOfBusyPlaces() || number == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            await Service.DeleteCarAsync(number);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
