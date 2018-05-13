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
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private ParkingService service { get; set; }

        public CarsController(ParkingService service)
        {
            this.service = service;
        }

        // GET: api/Cars/GetCars //[Route("api/[Controller]/GetCars")]
        // GET: api/Cars
        [HttpGet]
        public List<Car> GetCars()
        {

            return service.GetCars();
        }

        // GET: api/Cars/DetailsOnCar //[Route("api/[Controller]/DetailsOnCar")] //[HttpGet]
        // GET: api/Cars/5
        [HttpGet("{number}", Name = "Get")]
        public string GetDetailsOnOneCar(int number)
        {
            return service.GetDetailsOnOneCar(number);
        }

        // POST: api/Cars/AddCar //[Route("api/[Controller]/AddCar")]
        // POST: api/Cars
        [HttpPost]
        public void PostCar(CarType type, decimal balance)
        {
            service.PostCar(type, balance);
        }

        // DELETE: api/Cars/DeleteCar //[Route("api/[Controller]/DeleteCar")] [HttpDelete]
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{number}")]
        public decimal DeleteCar(int number)
        {
            return service.DeleteCar(number);
        }
    }
}
