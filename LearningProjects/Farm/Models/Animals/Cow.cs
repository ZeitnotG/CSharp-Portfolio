using Farm.Models.Products;
using System;

namespace Farm
{
    internal class Cow : Animal
    {
        public override void Produce()
        {
            Console.WriteLine("Cow produces milk");
        }

        public override void Eat(Product feed)
        {
            Console.WriteLine("Cow eats hay");
        }
    }
}
