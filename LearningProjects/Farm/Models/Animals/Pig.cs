using System;

namespace Farm
{
    internal class Pig : Animal
    {
        public override void Produce()
        {
            Console.WriteLine("Pigs produces meal");
        }
        public override void Eat()
        {
            Console.WriteLine("Pig eats all");
        }
    }
}
