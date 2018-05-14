using Microsoft.AspNetCore.Mvc;

namespace ParkingAPI.Controllers
{
    [Produces("application/json")]
    // [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private ParkingService service { get; }

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
    }
}
