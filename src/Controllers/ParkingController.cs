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
    // [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private ParkingService service { get; set; }

        public ParkingController(ParkingService service)
        {
            this.service = service;
        }

        // GET: api/Parking/NumberOfFreePlaces
        [Route("api/[Controller]/NumberOfFreePlaces")]
        [HttpGet]
        public int GetNumberOfFreePlaces()
        {
            return service.GetNumberOfFreePlaces();
        }

        // GET: api/Parking/NumberOfBusyPlaces
        [Route("api/[Controller]/NumberOfBusyPlaces")]
        [HttpGet]
        public int GetNumberOfBusyPlaces()
        {
            return service.GetNumberOfBusyPlaces();
        }

        //GET: api/Parking/TotalRevenue
        [Route("api/[Controller]/TotalRevenue")]
        [HttpGet]
        public decimal GetTotalRevenue()
        {
            return service.GetTotalRevenue();
        }

        /*// GET: api/Parking
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Parking/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Parking
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Parking/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
