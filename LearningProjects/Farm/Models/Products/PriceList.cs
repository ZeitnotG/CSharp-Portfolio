using FarmSim;
using FarmSim.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Models.Products
{
    internal class PriceList
    {
        public static readonly Dictionary<ProductType, int> SellPrices = new Dictionary<ProductType, int>
        {
            { ProductType.Egg, 5 },
            {ProductType.Milk, 10 },
            {ProductType.Meat, 20 }
        };
        public static readonly Dictionary<ProductType, int> BuyPrices = new Dictionary<ProductType, int>
    {
        { ProductType.Hay, 2 },
        { ProductType.Corn, 3 }
    };
    }
}
