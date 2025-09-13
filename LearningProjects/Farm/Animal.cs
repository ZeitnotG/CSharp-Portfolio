using System;
namespace Farm
{
    public enum ProductType
    {
        Milk,
        Egg,
        Meal
    }
    public class Animal
    {
        public string Name { get; set; }
        public int Health {  get; set; }
        public int Hunger {  get; set; }
        public ProductType productType { get; set; }
        public int Produce(ProductType productType)
        {
            int amount = 0;
            return amount;
        }
    }
}
