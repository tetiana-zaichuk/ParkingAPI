using System;
using System.Linq;

namespace Parking
{
    public class Menu
    {
        private readonly string[] _menu = {
            "Please, enter the number of an action.\n",
            "1 - Current balance",
            "2 - Revenue for the last minute",
            "3 - Show number of free and busy places",
            "4 - Add the car",
            "5 - Remove the car",
            "6 - Top up car balance",
            "7 - Display transactions for the last minute",
            "8 - Display Transactions.log",
            "0 - Exit"
        };

        private readonly Parking _parking = Parking.GetParking();

        public void ShowMenu()
        {
            foreach (var el in _menu)
            {
                Console.WriteLine(el);
            }
        }

        public bool Action()
        {
            var flag = true;            
            var value = GetAndValidateInputInt(0, 8);
            Console.Clear();
            switch (value)
            {
                case 1:
                    Console.WriteLine($"Total revenue: {_parking.GetTotalRevenue()}");
                    break;
                case 2:
                    Console.WriteLine($"Revenue for the last minute:{Parking.AmountForTheLastMinute()}");
                    break;
                case 3:
                    Console.WriteLine($"Free spaces: {_parking.GetNumberOfFreePlaces()}.\nBusy spaces: {Parking.GetNumberOfBusyPlaces()}.");
                    break;
                case 4:
                    AddCarMenuItem();
                    break;
                case 5:
                    RemoveCarMenuItem();
                    break;
                case 6:
                    TopUpCarBalanceMenuItem();
                    break;
                case 7:
                    DisplayTransactionsForTheLastMinuteMenuItem();
                    break;
                case 8:
                    DisplayTransactionsFileMenuItem();
                    break;
                default:
                    flag = false;
                    break;
            }

            if (flag)
            {
                EndOfParagraph();
            }
            return flag;
        }

        private void EndOfParagraph()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to go to menu.");
            Console.ReadKey();
        }

        public decimal GetAndValidateInputDecimal()
        {
            decimal input = 0;
            var str = Console.ReadLine();
            while (str.Contains(',') || !decimal.TryParse(str, out input))
            {
                Console.WriteLine("You need to type digits. Please use the following format: 25.25");
                str = Console.ReadLine();
            }
            return input;
        }

        public int GetAndValidateInputInt(int firstCondition, int secondCondition)
        {
            int input;
            while (!(int.TryParse(Console.ReadLine(), out input) && (input >= firstCondition && input <= secondCondition)))
            {
                Console.WriteLine($"You need to enter a number from {firstCondition} to {secondCondition}.");
            }
            return input;
        }

        private void AddCarMenuItem()
        {
            Console.WriteLine("To add the car, please, enter: car type and balance.\nEnter car type (Motorcycle = 1, Bus = 2, Passenger = 3, Truck = 4):");
            var carType = GetAndValidateInputInt(1, 4);
            Console.WriteLine("Enter the balance:");
            var carBalance = GetAndValidateInputDecimal();
            _parking.AddCar((CarType)carType, carBalance);
            Console.WriteLine("The car was added.");
        }

        private void RemoveCarMenuItem()
        {
            var busyPlaces = Parking.GetNumberOfBusyPlaces();
            if (busyPlaces == 0)
            {
                Console.WriteLine("There are no cars in the parking.");
            }
            else
            {
                Console.WriteLine($"To remove the car, please, enter the number of this car from 1 to {busyPlaces}:");
                var numberOfCar = GetAndValidateInputInt(1, busyPlaces);
                if (_parking.HasFine(numberOfCar))
                {
                    Console.WriteLine("The car has a fine. Would you like to top up balance (press any key) or no (press 0)?");
                    var i = int.Parse(Console.ReadLine());
                    if (i == 0)
                    {
                        Console.WriteLine("The car was not removed.");
                        return;
                    }
                }
                _parking.RemoveCar(numberOfCar, out var balance);
                Console.WriteLine($"Balance was {balance}. The car removed.");
            }
        }

        private void TopUpCarBalanceMenuItem()
        {
            var busyPlaces = Parking.GetNumberOfBusyPlaces();
            if (busyPlaces == 0)
            {
                Console.WriteLine("There are no cars in the parking.");
            }
            else
            {
                Console.WriteLine(
                    $"To top up car balance, please, enter the number of this car from 1 to {busyPlaces}:");
                var numberOfCar = GetAndValidateInputInt(1, busyPlaces);
                Console.WriteLine("Enter the amount of money:");
                var money = GetAndValidateInputDecimal();
                Console.WriteLine($"The balance is topped up. The new balance: {_parking.TopUp(numberOfCar, money)}");
            }
        }

        private void DisplayTransactionsForTheLastMinuteMenuItem()
        {
            var list = _parking.GetTransactionsForTheLastMinute();
            if (list.Any())
            {
                Console.WriteLine("Display transactions for the last minute:");
                list.ForEach(n =>
                    Console.WriteLine($"Transaction date: {n.CreatedOn} Car Id:{n.CarId} Amount: {n.Amount}"));
            }
            else
            {
                Console.WriteLine("There are no transactions for the last minute.");
            }
        }

        private void DisplayTransactionsFileMenuItem()
        {
            Console.WriteLine("Display Transactions.log");
            try
            {
                var transactions = _parking.GetTransactionsFile().Split(' ');
                for (var i = 0; i < transactions.Length - 3; i++)
                {
                    Console.WriteLine(
                        $"Date: {transactions[i]} Time: {transactions[++i]} Amount for the last minute: {transactions[++i]}. The total number of transactions for the last minute: {transactions[++i]}.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! We are not able to show you the file. Please try again later.");
            }
        }       
    }
}
