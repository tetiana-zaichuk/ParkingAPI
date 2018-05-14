using System.Collections.Generic;

namespace Parking
{
    public class Settings
    {
        public static int Timeout { get; }
        public static int TransactionLoggingTimeout { get; }
        public static Dictionary<CarType, int> Prices = new Dictionary<CarType, int>
        {
            [CarType.Motorcycle] = 1,
            [CarType.Bus] = 2,
            [CarType.Passenger] = 4,
            [CarType.Truck] = 5
        };
        public static int ParkingSpace { get; }
        public static int CoefficientFine { get; }

        static Settings()
        {
            //Milliseconds
            Timeout = 3000;
            TransactionLoggingTimeout = 60000;
            ParkingSpace = 50;
            CoefficientFine = 3;
        }
    }
}
