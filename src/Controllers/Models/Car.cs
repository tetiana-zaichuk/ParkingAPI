using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI
{
    public class Car
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        [EnumDataType(typeof(CarType))]
        public CarType CarType { get; set; }

        //public Car(CarType type, decimal balance)
        //{
        //    Id = Guid.NewGuid();
        //    CarType = type;
        //    Balance = balance;
        //}
    }
}
