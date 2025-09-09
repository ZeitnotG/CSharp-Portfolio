using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get;  set; }
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public enum OrderStatus
    {
        New,
        Processing,
        Shipped,
        Delivered
    }

    public class Order
    {
        public List<Product> Products { get; private set; }
        public OrderStatus Status { get; private set; }
        public Order(List<Product> products)
        {
            Products = products;
            Status = OrderStatus.New;
        }
        public void AddProduct(Product product) 
        {
            Products.Add(product);
        }
        public decimal CalculateAmount()
        {
            decimal amount = 0;
            foreach (Product product in Products) 
            {
                amount+= product.Price;
            }
            return amount;
        }
        public void ChangeStatus(OrderStatus status) 
        {
            Status = status;
        }


    }

    public class Customer
    {
        public string Name { get; set; }
        public List<Order> orders { get; set; }
        public Customer(string name, List<Order> orders)
        {
            Name = name;
            this.orders = orders;
        }
    }
}
