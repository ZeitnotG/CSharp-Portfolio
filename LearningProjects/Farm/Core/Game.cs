using FarmSim.Core;
using System;
namespace FarmSim
{
    internal class Game
    {
        private FarmSim.Core.Farm farm;
        static void Main(string[] args)
        {

            Game game = new Game();
            game.Run();
        }
      
        public Game()
        {
            farm = new FarmSim.Core.Farm();
        }

        public void Run()
        {
            Animal chicken = new Chicken("Chick");
            Animal chicken1 = new Chicken("Honey");
            Animal cow = new Cow("Cow");

            farm.AddAnimal(cow);
            farm.AddAnimal(chicken);
            farm.AddAnimal(chicken1);

            while (true)
            {
                Console.WriteLine($"\n=== Day {farm.Day} ===");

                farm.FeedAnimals();
                farm.HandleReproduct();
                farm.Animals.ForEach(animal => animal.TickDay());
                farm.CollectProducts();

                Console.WriteLine("Do you want to sell or buy? sell/buy/skip/save/load");
                string action = Console.ReadLine();
                if (action == "sell")
                {
                    farm.Sell();
                    farm.Day++;
                }
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
                    farm.Day++;
                }
                else if (action == "save")
                    farm.SaveGame(farm);
                else if (action == "load")
                    farm = farm.LoadGame(farm);
                else
                    Console.WriteLine("Unknown command");
            }
        }

    }
}
