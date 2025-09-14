using Farm.Models.Products;
using System;

namespace Farm
{
    internal class Chicken:Animal
    {
        public override void Produce()
        {
            Console.WriteLine("Chicken produces eggs");
        }
        public override void Eat(Product feed)
        {
            
            Console.WriteLine("Chicken eats corn");
        }
    }
}
