using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parking;

namespace ParkingAPI.Controllers
{
    [Produces("application/json")]
    public class TransactionsController : Controller
    {
        private ParkingService service { get; }

        public TransactionsController(ParkingService service)
        {
            this.service = service;
        }

        // GET: api/Transactions/TransactionsFile
        [Route("api/[Controller]/TransactionsFile")]
        [HttpGet]
        public string GetTransactionsFile()
        {
            return service.GetTransactionsFile();
        }

        // GET: api/Transactions/TransactionsForTheLastMinute
        [Route("api/[Controller]/TransactionsForTheLastMinute")]
        [HttpGet]
        public List<Transaction> GetTransactionsForTheLastMinute()
        {
            return service.GetTransactionsForTheLastMinute();
        }

        // GET: api/Transactions/TransactionsForTheLastMinuteOnCar/5
        [Route("api/[Controller]/TransactionsForTheLastMinuteOnCar/{numberStr}")]
        [HttpGet("{numberStr}", Name = "Get")]
        public ObjectResult GetTransactionsForTheLastMinuteOnCar(string numberStr)
        {
            if (!int.TryParse(numberStr, out var number)) return BadRequest("It must be numbers");
            if (number > service.GetNumberOfBusyPlaces() || number == 0) return NotFound("The place with this number is empty.");
            return Ok(service.GetTransactionsForTheLastMinuteOnCar(number));
        }

        // PUT: api/Transactions/TopUp/?numberStr=1&moneyStr=250
        [Route("api/[Controller]/TopUp")]
        [HttpPut]
        public IActionResult PutTopUpBalanceCar(string numberStr, string moneyStr)
        {
            if (!decimal.TryParse(moneyStr, out var money) || !int.TryParse(numberStr, out var number)) return BadRequest();
            if (number > service.GetNumberOfBusyPlaces() || number == 0) return NotFound("The place with this number is empty.");
            service.PutTopUp(number, money);
            return Ok();
        }
    }
}
