using System;

namespace Parking
{
    public class Car
    {
        public Guid Id { get; }
        public decimal Balance { get; set; }
        public CarType CarType { get; }

        public Car(CarType type, decimal balance)
        {
            Id = Guid.NewGuid(); 
            CarType = type;
            Balance = balance;
        }

    }
}
