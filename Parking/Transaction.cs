using System;

namespace Parking
{
    public class Transaction
    {
        public DateTime CreatedOn { get; }
        public Guid CarId { get; }
        public decimal Amount { get; }

        public Transaction(DateTime time, Guid carId, decimal amount)
        {
            CreatedOn = time;
            CarId = carId;
            Amount = amount;
        }
    }
}
