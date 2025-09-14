using System;
namespace Farm
{
    public enum ProductType
    {
        Milk,
        Egg,
        Meal
    }
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Health {  get; set; }
        public int Hunger {  get; set; }
        public abstract void Produce();
        public abstract void Eat();

    }
}
