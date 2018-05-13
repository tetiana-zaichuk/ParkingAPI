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
    //[Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private ParkingService service { get; set; }

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
        [Route("api/[Controller]/TransactionsForTheLastMinuteOnCar/{id}")]
        [HttpGet("{number}", Name = "Get")]
        public IEnumerable<Transaction> GetTransactionsForTheLastMinuteOnCar(int number)
        {
            return service.GetTransactionsForTheLastMinuteOnCar(number);
        }

        // PUT: api/Transactions/TopUp/5
        [Route("api/[Controller]/TopUp/{number}")]
        [HttpPut("{number}")]
        public void PutTopUp(int number, decimal money)
        {
            service.PutTopUp(number, money);
        }
      }
}
