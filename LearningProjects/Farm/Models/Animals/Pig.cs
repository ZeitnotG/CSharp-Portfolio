using FarmSim.Models.Products;
using System;

namespace FarmSim
{
    internal class Pig : Animal
    {
        public override Product Produce()
        {
            Console.WriteLine("Pigs produces meal");
            return null;
        }
        /* public override void Eat()
         {
             Console.WriteLine("Pig eats all");
         }*/
        public Pig(string name) : base(name)
        {

        }

        public override Animal Clone(string name)
        {
            return new Pig(name);
        }
    }
}
