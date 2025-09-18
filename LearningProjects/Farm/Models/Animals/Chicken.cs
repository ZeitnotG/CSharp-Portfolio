using FarmSim.Models.Products;
using System;

namespace FarmSim
{
    internal class Chicken : Animal
    {
        public Chicken(string name) : base(name)
        {
            Name = name;
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

        public override Animal Clone(string name)
        {
            return new Chicken(name);
        }
    }
}
