using System;

namespace FarmSim.Models.Products
{
    public class Product
    {
        public ProductType Type { get;}
       public string Name { get; }
       public int Value { get; }
       public int Nutrition {  get; }
       public Product(ProductType type, string name, int value, int nutrition)
        {
            Type = type;
            Name = name;
            Value = value;
            Nutrition = nutrition;
        }
    }
}
