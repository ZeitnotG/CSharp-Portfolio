using FarmSim.Models.Products;
using System;

namespace FarmSim
{
    internal class Cow : Animal
    {
        public override Product Produce()
        {
            if (!isAlive)
                return null;
            if (Hunger > 60 || Health < 30)
                return null;
            if (LastProducedDay >= ProduceIntevalDays)
            {
                LastProducedDay = 0;
                Console.WriteLine("Cow produces milk");
                return ProductCatalog.Get(ProductType.Milk);
            }
            return null;
        }

        public override void Eat(Product feed)
        {
            base.Eat(feed);
            Console.WriteLine("Cow eats hay");
        }
        public Cow(string name, int health, int hunger) : base(name, health, hunger)
        {
            Name = name;
            Health = health;
            Hunger = hunger;
            ProduceIntevalDays = 2;
        }
    }
}
