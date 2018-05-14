using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parking;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TransactionsController : Controller
    {
        private ParkingService Service { get; }

        public TransactionsController(ParkingService service) => Service = service;

        // GET: api/Transactions/TransactionsFile
        [Route("TransactionsFile")]
        [HttpGet]
        public string GetTransactionsFile() => Service.GetTransactionsFile();

        // GET: api/Transactions/TransactionsForTheLastMinute
        [Route("TransactionsForTheLastMinute")]
        [HttpGet]
        public List<Transaction> GetTransactionsForTheLastMinute() => Service.GetTransactionsForTheLastMinute();

        // GET: api/Transactions/TransactionsForTheLastMinuteOnCar/5
        [Route("TransactionsForTheLastMinuteOnCar/{carNumber}")]
        [HttpGet("{carNumber}", Name = "Get")]
        public ObjectResult GetTransactionsForTheLastMinuteOnCar(string carNumber)
        {
            if (!int.TryParse(carNumber, out var number))
            {
                return BadRequest("It must be numbers");
            }

            if (number > Service.GetNumberOfBusyPlaces() || number == 0)
            {
                return NotFound("The place with this number is empty.");
            }

            return Ok(Service.GetTransactionsForTheLastMinuteOnCar(number));
        }

        // PUT: api/Transactions/TopUp/?carNumber=1&moneyStr=250
        [Route("TopUp")]
        [HttpPut]
        public IActionResult PutTopUpBalanceCar(string carNumber, string moneyStr)
        {
            if (!decimal.TryParse(moneyStr, out var money) || !int.TryParse(carNumber, out var number))
            {
                return BadRequest();
            }

            if (number > Service.GetNumberOfBusyPlaces() || number == 0)
            {
                return NotFound("The place with this number is empty.");
            }

            Service.PutTopUp(number, money);
            return Ok();
        }
    }
}
