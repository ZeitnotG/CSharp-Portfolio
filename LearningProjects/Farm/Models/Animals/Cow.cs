using FarmSim.Models.Products;
using System;

namespace FarmSim
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
        public Cow(string name, int health, int hunger) : base(name, health, hunger)
        {
            Name = name;
            Health = health;
            Hunger = hunger;
        }
    }
}
