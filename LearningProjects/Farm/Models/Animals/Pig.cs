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
        public Pig(string name, int health, int hunger) : base(name, health, hunger)
        {
            Name = name;
            Health = health;
            Hunger = hunger;
        }
    }
}
