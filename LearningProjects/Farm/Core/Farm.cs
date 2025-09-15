using FarmSim.Models.Products;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace FarmSim.Core
{
    internal class Farm
    {
        public List<Animal> Animals;
        public Dictionary<ProductType, int> Storage { get; }
        public Farm()
        {
            Animals = new List<Animal>();
            Storage = new Dictionary<ProductType, int>();
            foreach (ProductType pt in Enum.GetValues(typeof(ProductType)))
                Storage[pt] = 0;
        }

        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
            Console.WriteLine($"{animal.Name} added to your farm");
        }
        public void FeedAnimals()
        {
            foreach (Animal animal in Animals)
            {
                switch (animal)
                {
                    case Cow cow:
                        if (ConsumeFromStorage(ProductType.Hay))
                            cow.Eat(ProductCatalog.Get(ProductType.Hay));
                        else
                            Console.WriteLine($"{cow.Name} has nothing to eat");
                        break;
                    case Chicken chicken:
                        if (ConsumeFromStorage(ProductType.Corn))
                        chicken.Eat(ProductCatalog.Get(ProductType.Corn));
                        else
                            Console.WriteLine($"{chicken.Name} has nothing to eat");
                        break;
                    case Pig pig:
                        if (ConsumeFromStorage(ProductType.Corn))
                            pig.Eat(ProductCatalog.Get(ProductType.Corn));
                        else if (ConsumeFromStorage(ProductType.Hay))
                            pig.Eat(ProductCatalog.Get(ProductType.Hay));
                        else
                            Console.WriteLine($"{pig.Name} has nothing to eat");
                            break;
                    default:
                        Console.WriteLine($"{animal.Name} could not be fed");
                        break;
                }
            }
        }
        public void CollectProducts()
        {
            foreach(Animal animal in Animals)
            {
                Product product = animal.Produce();
                if (product != null)
                {
                    if (Storage.ContainsKey(product.Type))
                        Storage[product.Type]++;
                    else
                        Storage[product.Type] = 1;
                }
            }
            Console.WriteLine($"Your storage: Eggs = {Storage[ProductType.Egg]}, Milk = {Storage[ProductType.Milk]}, Meat = {Storage[ProductType.Meat]}," +
    $"Corn = {Storage[ProductType.Corn]}, Hay = {Storage[ProductType.Hay]}");
        }
        public void CheckPrices()
        {

        }
        public void Trade()
        {

        }

        public void AddToStorage(ProductType type, int quantity = 1)
        {
            if (Storage.ContainsKey(type))
                Storage[type] += quantity;
            else
                Storage[type] = quantity;
        }

        public bool ConsumeFromStorage(ProductType type, int quantity = 1)
        {
            if (Storage.TryGetValue(type, out int available) && available >= quantity)
            {
                Storage[type] -= quantity;
                return true;
            }
            return false;
        }
    }
}
