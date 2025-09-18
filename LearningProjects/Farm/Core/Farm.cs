using Farm.Models.Products;
using FarmSim.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace FarmSim.Core
{
    internal class Farm
    {
        public int Money { get; private set; }
        public List<Animal> Animals;
        public Dictionary<ProductType, int> Storage { get; }
        public Farm()
        {
            Money = 100;
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
            foreach (Animal animal in Animals)
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
        public void Buy(ProductType type, int quantity)
        {
            if (!PriceList.BuyPrices.ContainsKey(type))
            {
                Console.WriteLine($"{type} cannot be bought");
                return;
            }

            int cost = PriceList.BuyPrices[type] * quantity;
            if (Money >= cost)
            {
                Money -= cost;
                Storage[type] += quantity;
                Console.WriteLine($"Bought {quantity} {type} for {cost} coins. Money left: {Money}");
            }
            else
                Console.WriteLine("Not enough money!");
        }
        public void Sell()
        {
            foreach (KeyValuePair<ProductType, int> kvp in PriceList.SellPrices)
            {
                ProductType type = kvp.Key;
                int price = kvp.Value;

                if (Storage[type] > 0)
                {
                    Money += Storage[type] * price;
                    Console.WriteLine($"Sold {Storage[type]} for {price} coins. Money: {Money}");
                    Storage[type] = 0;
                }
            }
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

        public void HandleReproduct()
        {
            var groups = Animals.GroupBy(a => a.GetType());

            foreach (var group in groups)
            {
                int count = group.Count();
                if (count < 2)
                    continue;

                double baseChance = 0.05;

                double reproductionChance = baseChance * (count - 1);

                Random rnd = new Random();

                if (rnd.NextDouble() < reproductionChance)
                {
                    Animal parent = group.First();
                    string childName = GenerateUniqueName(parent);
                    Animal child = parent.Clone(childName);
                    Animals.Add(child);

                    Console.WriteLine($"{parent.GetType().Name} reproduced! New baby {child.Name} was born.");
                }
            }
        }

        private int _animalCounter = 1;

        private string GenerateUniqueName(Animal parent)
        {
            return $"{parent.GetType().Name}_{_animalCounter++}";
        }
    }
}
