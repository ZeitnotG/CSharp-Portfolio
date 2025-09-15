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
        }

        public override Product Produce()
        {
            Console.WriteLine("Chicken produces eggs");
            return ProductCatalog.Get(ProductType.Egg);
        }
        public override void Eat(Product feed)
        {
            base.Eat(feed);
            Console.WriteLine("Chicken eats corn");
        }
    }
}
