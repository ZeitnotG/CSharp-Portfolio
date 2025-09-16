using FarmSim.Core;
using System;
namespace FarmSim
{
    internal class Game
    {
        private int day;
        private FarmSim.Core.Farm farm;
        static void Main(string[] args)
        {
            Animal chicken = new Chicken("Chick", 30, 40);
            Animal cow = new Cow("Cow", 30, 40);
        }
      
        public Game()
        {
            day = 1;
            farm = new FarmSim.Core.Farm();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine($"\n=== Day {day} ===");

                farm.FeedAnimals();

                farm.CollectProducts();

                Console.WriteLine("Do you want to sell or buy? sell/buy/skip");
                string action = Console.ReadLine();
                if (action == "sell")
                    farm.Sell();
                else if (action == "buy")
                {
                    Console.WriteLine("What do you want to buy? Hay/Corn");
                    string typeInput = Console.ReadLine();
                    if (Enum.TryParse<ProductType>(typeInput, out ProductType productType))
                    {
                        Console.WriteLine("How many?");
                        int quantity = int.Parse(Console.ReadLine());
                        farm.Buy(productType, quantity);
                    }
                }
                else 
                    Console.WriteLine("Unknown command");

               day++;
            }
        }

    }
}
