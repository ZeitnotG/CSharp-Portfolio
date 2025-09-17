using FarmSim.Models.Products;
using System;

namespace FarmSim
{
    internal class Chicken : Animal
    {
        public Chicken(string name, int health, int hunger) : base(name, health, hunger)
        {
            Name = name;
            Health = health;
            Hunger = hunger;
            ProduceIntevalDays = 1;
        }

        public override Product Produce()
        { 
            if (!isAlive)
                return null;
            if (Hunger > 60 || Health < 30)
                return null;
            if (LastProducedDay >= ProduceIntevalDays)
            {
                LastProducedDay = 0;
                Console.WriteLine("Chicken produces eggs");
                return ProductCatalog.Get(ProductType.Egg);
            }
            return null;
        }
        
        public override void Eat(Product feed)
        {
            base.Eat(feed);
            Console.WriteLine("Chicken eats corn");
        }
    }
}
