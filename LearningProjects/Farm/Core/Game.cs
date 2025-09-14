using FarmSim.Core;
using System;
namespace FarmSim
{
    internal class Game
    {
        static void Main(string[] args)
        {
            Farm farm = new Farm();

            farm.AddToStorage(ProductType.Hay, 10);
            farm.AddToStorage(ProductType.Corn, 10);

            Animal chicken = new Chicken("Chick", 30, 40);
            farm.AddAnimal(chicken);
            farm.FeedAnimals();
            chicken.ShowInfo();

        }
    }
}
