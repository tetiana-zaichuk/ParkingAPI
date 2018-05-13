using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking;

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
            return service.GetCars();
        }

        // GET: api/Cars/DetailsOnCar 
        [Route("api/[Controller]/DetailsOnCar/{number}")]
        [HttpGet("{number}")]
        public string GetDetailsOnOneCar(int number)
        {
            return service.GetDetailsOnOneCar(number);
        }

        // POST: api/Cars/AddCar/?type=1&balance=2 
        [Route("api/[Controller]/AddCar")]
        [HttpPost]
        public void PostCar(CarType type, decimal balance)
        {
            service.PostCar(type, balance);
        }

        // DELETE: api/Cars/DeleteCar/5
        [Route("api/[Controller]/DeleteCar/{number}")]
        [HttpDelete("{number}")]
        public decimal DeleteCar(int number)
        {
            return service.DeleteCar(number);
        }
    }
}
