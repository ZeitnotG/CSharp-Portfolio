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
        public abstract Product Produce();
        public virtual void Eat(Product feed)
        {
            Hunger = Math.Max(0, Hunger - feed.Nutrition);
        }
        public Animal(string name, int health, int hunger)
        {
            Name = name;
            Health = health;
            Hunger = hunger;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}, Health: {Health}, Hunger: {Hunger}");
        }
    }
}
