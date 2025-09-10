using System;
using System.Collections.Generic;

namespace OnlineStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product onion = new Product("Onion", 30);
            Product potato = new Product("Potato", 40);
            Product meal = new Product("Meal", 100);
            Order order = new Order();
            order.AddProduct(onion);
            order.AddProduct(potato);
            order.AddProduct(meal);
            Customer customer = new Customer("Ivan");
            Customer customer1 = new Customer("Lisa");
            customer.AddOrder(order);
            order.ChangeStatus(OrderStatus.Processing);
            customer.DisplayOrders();
            Order order1 = new Order();
            Product tea = new Product("Tea", 50);
            order1.AddProduct(tea);
            order1.AddProduct(meal);
            customer1.AddOrder(order1);
            customer1.DisplayOrders();
            order1.RemoveProduct(tea);
            customer1.DisplayOrders();
            Order order2 = new Order();
            customer.AddOrder(order2);
            order2.AddProduct(tea);
            order2.AddProduct(meal);
            customer.DisplayOrders();
            customer.GetTotalSpent();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
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
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }
        public Order()
        {
            Products = new List<Product>();
            Status = OrderStatus.New;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }
        public void AddProduct(Product product)
        {
            Products.Add(product);
            Console.WriteLine($"Product {product.Name} added to your order");
        }
        public decimal CalculateAmount()
        {
            decimal amount = 0;
            foreach (Product product in Products)
            {
                amount += product.Price;
            }
            return amount;
        }
        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.Now;
            Console.WriteLine($"Your order status has been changed in {UpdatedAt}");
        }

        public void RemoveProduct(Product product)
        {        
                Products.Remove(product);
                Console.WriteLine($"Product {product.Name} has been removed from your order");   
        }

    }

    public class Customer
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public Customer(string name)
        {
            Name = name;
            Orders = new List<Order>();
        }
        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }
        public void DisplayOrders()
        {
            Console.WriteLine($"Dear {Name}, your orders:");
            int number = 1;
            foreach (Order order in Orders)
            {
                Console.WriteLine($"The order number {number}  has total amount {order.CalculateAmount()}" +$"" +
                    $" $. Status: {order.Status}. Order creation time: {order.CreatedAt}." +
                    $" Last updated: {order.UpdatedAt}." +
                    $" Details: ");
                number++;
                foreach (Product product in order.Products)
                {
                    Console.WriteLine($"{product.Name} - {product.Price} $");
                }
            }
        }
        public void GetTotalSpent()
        {
          decimal amount = 0;
            foreach(Order order in Orders)
            {
               amount += order.CalculateAmount();
            }
            Console.WriteLine($"Your orders total amount: {amount} $");
        }
    }
}
