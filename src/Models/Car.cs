using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        [EnumDataType(typeof(CarType))]
        public CarType CarType { get; set; }
    }
}
