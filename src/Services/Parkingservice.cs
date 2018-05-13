using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Parking;

using Parking = Parking.Parking;

namespace ParkingAPI
{
    public class ParkingService
    {
        private readonly global::Parking.Parking _parking;

        public ParkingService()
        {
            _parking = global::Parking.Parking.GetParking();
        }

        public List<Car> GetCars()
        {
            List<Car> cars = global::Parking.Parking.GetCars();
            return cars;
        }

        public string GetDetailsOnOneCar(int id)
        {
            var car = _parking.DetailsOnOneCar(id);
            return "" + car.Id + car.CarType + car.Balance;
        }

        public void PostCar(CarType type, decimal balance)
        {
            _parking.AddCar(type, balance);
        }

        public decimal DeleteCar(int id)
        {
            _parking.RemoveCar(id, out var balance);
            return balance;
        }

        public int GetNumberOfFreePlaces()
        {
            return _parking.GetNumberOfFreePlaces();
        }

        public decimal GetTotalRevenue()
        {
            return _parking.GetTotalRevenue();
        }

        public int GetNumberOfBusyPlaces()
        {
            return global::Parking.Parking.GetNumberOfBusyPlaces();
        }

        public string GetTransactionsFile()
        {
            return _parking.GetTransactionsFile();
        }

        public List<Transaction> GetTransactionsForTheLastMinute()
        {
            return _parking.GetTransactionsForTheLastMinute();
        }

        public IEnumerable<Transaction> GetTransactionsForTheLastMinuteOnCar(int number)
        {
            var car = _parking.DetailsOnOneCar(number);
            return _parking.GetTransactionsForTheLastMinute().Where(n => n.CarId == car.Id);
        }

        public decimal PutTopUp(int number, decimal money)
        {
            return _parking.TopUp(number, money);
        }
    }
}
