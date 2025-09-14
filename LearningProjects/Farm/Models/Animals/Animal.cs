using Farm.Models.Products;
using System;
namespace Farm
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
        public abstract void Produce();
        public virtual void Eat(Product feed)
        {
            Hunger -= Math.Max(0, Hunger - feed.Nutrition);
        }

    }
}
