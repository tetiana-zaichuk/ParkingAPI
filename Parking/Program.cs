using System;

namespace Parking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                menu.ShowMenu();
                flag = menu.Action();
            }
        }
    }
}
