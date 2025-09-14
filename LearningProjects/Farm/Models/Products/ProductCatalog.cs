using Farm;
using Farm.Models.Products;
using System;
using System.Collections.Generic;

namespace Farm.Models.Products
{
    public static class ProductCatalog
    {
        public static readonly Dictionary<ProductType, Product> Templates = new Dictionary<ProductType, Product>
    {
        { ProductType.Hay,  new Product(ProductType.Hay,  "Hay",  1, 15) },
        { ProductType.Corn, new Product(ProductType.Corn, "Corn", 1, 8)  },
        { ProductType.Milk, new Product(ProductType.Milk, "Milk", 5, 6)  },
        { ProductType.Egg,  new Product(ProductType.Egg,  "Egg",  2, 2)  },
        { ProductType.Meat, new Product(ProductType.Meat, "Meat", 8, 20) },
    };

        public static Product Get(ProductType type) => Templates[type];
    }
}
