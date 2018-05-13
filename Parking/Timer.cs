using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Parking
{
    public class Timer
    {
        private static System.Timers.Timer _paymentCollectionTimer, _transactionsLoggingTimer;
        
        public Timer()
        {
            using (_paymentCollectionTimer)
            {
                SetPaymentCollectionTimer();
            }

            using (_transactionsLoggingTimer)
            {
                SetTransactionsLoggingTimer();
            }
        }

        public static void SetPaymentCollectionTimer()
        {
            _paymentCollectionTimer = new System.Timers.Timer(Settings.Timeout);
            _paymentCollectionTimer.Elapsed += OnTimedEventForPaymentCollection;
            _paymentCollectionTimer.AutoReset = true;
            _paymentCollectionTimer.Enabled = true;
        }

        private static async void OnTimedEventForPaymentCollection(Object source, ElapsedEventArgs e)
        {
            var cars = Parking.GetCars();
            foreach (var car in cars)
            {
                await Parking.CollectPaymentAsync(car);
            }
        }

        private static void SetTransactionsLoggingTimer()
        {
            _transactionsLoggingTimer = new System.Timers.Timer(Settings.TransactionLoggingTimeout);
            _transactionsLoggingTimer.Elapsed += OnTimedEventForTransactionsLogging;
            _transactionsLoggingTimer.AutoReset = true;
            _transactionsLoggingTimer.Enabled = true;
        }

        private static async void OnTimedEventForTransactionsLogging(Object source, ElapsedEventArgs e) => await Parking.WriteToTransactionsFileAsync();

    }
}
