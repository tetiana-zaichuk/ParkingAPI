using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parking;

namespace ParkingAPI.Services
{
    public class ParkingService
    {
        private readonly Parking.Parking _parking;

        public ParkingService() => _parking = Parking.Parking.GetParking();

        public List<Parking.Car> GetCars() => Parking.Parking.GetCars();

        public string GetDetailsOnOneCar(int id)
        {
            var car = _parking.DetailsOnOneCar(id);
            return "" + car.Id + " " + car.CarType + " " + car.Balance;
        }

        public void PostCar(Parking.CarType type, decimal balance) => _parking.AddCar(type, balance);

        public async Task<decimal> DeleteCarAsync(int id) => await _parking.RemoveCarAsync(id);

        public int GetNumberOfFreePlaces() => _parking.GetNumberOfFreePlaces();

        public decimal GetTotalRevenue() => _parking.GetTotalRevenue();

        public int GetNumberOfBusyPlaces() => Parking.Parking.GetNumberOfBusyPlaces();

        public string GetTransactionsFile() => _parking.GetTransactionsFile();

        public List<Transaction> GetTransactionsForTheLastMinute() => _parking.GetTransactionsForTheLastMinute();

        public IEnumerable<Transaction> GetTransactionsForTheLastMinuteOnCar(int number)
        {
            var car = _parking.DetailsOnOneCar(number);
            return _parking.GetTransactionsForTheLastMinute().Where(n => n.CarId == car.Id);
        }

        public decimal PutTopUp(int number, decimal money) => _parking.TopUp(number, money);
    }
}
