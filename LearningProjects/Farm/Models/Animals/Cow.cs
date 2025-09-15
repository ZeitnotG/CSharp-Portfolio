using FarmSim.Models.Products;
using System;

namespace FarmSim
{
    internal class Cow : Animal
    {
        public override Product Produce()
        {
            Console.WriteLine("Cow produces milk");
            return ProductCatalog.Get(ProductType.Milk);
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
        }
    }
}
