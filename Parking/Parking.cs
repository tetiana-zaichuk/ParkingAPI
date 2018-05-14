using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Parking
    {
        private static readonly Lazy<Parking> Lazy = new Lazy<Parking>(() => new Parking());
        private static List<Car> cars = new List<Car>(Settings.ParkingSpace);
        private static List<Transaction> transactions = new List<Transaction>();
        public static decimal Balance { get; private set; }

        private Parking()
        {
            Balance = 0;
            Timer timer = new Timer();
        }

        public static Parking GetParking() => Lazy.Value;

        public decimal GetTotalRevenue() => Balance;

        public static List<Car> GetCars() => cars;

        public static async Task CollectPaymentAsync(Car car)
        {
            Settings.Prices.TryGetValue(car.CarType, out var price);
            if (car.Balance < price)
            {
                price = price * Settings.CoefficientFine;
            }
            car.Balance -= price;
            Balance += price;
            transactions.Add(new Transaction(DateTime.Now, car.Id, price));
        }

        public Car DetailsOnOneCar(int number) => cars[number-1];

        public void AddCar(CarType type, decimal balance) => cars.Add(new Car(type, balance));

        public bool HasFine(int number) => cars[number - 1].Balance < 0;

        public async Task<decimal> RemoveCarAsync(int number)
        {
            var fine = cars[number - 1].Balance;
            if (HasFine(number))
            {
                TopUp(number, Math.Abs(cars[number - 1].Balance));
                await CollectPaymentAsync(cars[number - 1]);
            }
            cars.Remove(cars[number - 1]);
            return fine;
        }
        public decimal TopUp(int value, decimal money) => cars[value - 1].Balance += money;

        public int GetNumberOfFreePlaces() => cars == null ? Settings.ParkingSpace : Settings.ParkingSpace - cars.Count;

        public static int GetNumberOfBusyPlaces() => cars?.Count ?? 0;

        public static decimal AmountForTheLastMinute() => transactions.Sum(n => n.Amount);

        public List<Transaction> GetTransactionsForTheLastMinute() => transactions;

        public static async Task WriteToTransactionsFileAsync()
        {
            try
            {
                byte[] array = Encoding.Default.GetBytes("" + DateTime.Now + " " + AmountForTheLastMinute() + " " + transactions.Count + " ");
                transactions.Clear();
                using (var fstream = new FileStream(Settings.Path, FileMode.OpenOrCreate))
                {
                    fstream.Seek(0, SeekOrigin.End);
                    await fstream.WriteAsync(array, 0, array.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception. " + e.Message);
            }
        }

        public string GetTransactionsFile()
        {
            try
            {
                using (FileStream fstream = File.OpenRead(Settings.Path))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    var textFromFile = Encoding.Default.GetString(array);
                    return textFromFile;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
