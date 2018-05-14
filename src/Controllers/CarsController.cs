using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking;
using AutoMapper;

namespace ParkingAPI.Controllers
{
    [Produces("application/json")]
    public class CarsController : Controller
    {
        private ParkingService service { get; set; }

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
        [Route("api/[Controller]/DetailsOnCar/{number}")]
        [HttpGet("{number}")]
        public string GetDetailsOnOneCar(int number)
        {
            return service.GetDetailsOnOneCar(number);
        }
        
        // POST: api/Cars/AddCar/?type=1&balance=2.2 
        [Route("api/[Controller]/AddCar")]
        [HttpPost]
        public IActionResult PostCar(string type, decimal balance)
        {
            bool isValid = Int32.TryParse(type, out var i) ? Enum.IsDefined(typeof(CarType), i) : Enum.IsDefined(typeof(CarType), type);
            if (isValid)
            {
                CarType parsedType = Enum.Parse<CarType>(type);
                service.PostCar(Mapper.Map<CarType, Parking.CarType>(parsedType), balance);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Cars/DeleteCar/1
        [Route("api/[Controller]/DeleteCar/{number}")]
        [HttpDelete("{number}")]
        public decimal DeleteCar(int number)
        {
            return service.DeleteCar(number);
        }
    }
}
