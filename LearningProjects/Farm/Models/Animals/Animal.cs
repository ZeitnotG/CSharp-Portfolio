using FarmSim.Models.Products;
using System;
namespace FarmSim
{
    public enum ProductType
    {
        Milk,
        Egg,
        Meat,
        Corn,
        Hay
    }
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Health {  get; set; }
        public int Hunger {  get; set; }
        public int Happiness {  get; set; }
        public int LastProducedDay {  get; protected set; }
        public int ProduceIntevalDays { get; protected set; }

        public bool isAlive => Health> 0;
        public abstract Product Produce();
        public virtual void Eat(Product feed)
        {
            Hunger = Math.Max(0, Hunger - feed.Nutrition);
        }
        public Animal(string name)
        {
            Name = name;
           ResetStats();
        }

        protected void ResetStats()
        {
            Hunger = 0;
            Health = 100;
            Happiness = 100;
            LastProducedDay = 0;
        }

        public virtual void TickDay()
        {
            Hunger += Math.Min(0, Hunger+10);

            if (Hunger > 70)
            {
                Health = Math.Max(0, Health - (Hunger - 70) / 5);
            }

            LastProducedDay++;

            if (Health < 30)
            {
                Happiness = Math.Max(0, Happiness - 5);
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}, Health: {Health}, Hunger: {Hunger}");
        }

        public abstract Animal Clone(string name);
    }
}
