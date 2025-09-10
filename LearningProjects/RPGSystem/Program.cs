using System;
using System.Collections.Generic;

namespace RPGSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item knife = new Item("Knife", 3, 10, ItemType.Weapon);
            Inventory inventory = new Inventory();
            inventory.AddItem(knife);
            inventory.ShowItems();
        }
    }

    public enum ItemType
    {
        Weapon,
        Potion,
        Equipment,
        Other
    }
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public ItemType Type { get; set; }
        public Item(string name, double weight, int value, ItemType type)
        {
            Name = name;
            Weight = weight;
            Value = value;
            Type = type;
        }
    }

    public class Inventory
    {
        public List<Item> Items { get; set; }
        public double TotalWeight { get; private set; }
        public Inventory()
        {
            Items = new List<Item>();
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
            Console.WriteLine($"Item {item.Name} has been added to inventory");
        }
        public void RemoveItem(Item item) 
        { 
            Items.Remove(item);
            Console.WriteLine($"Item {item.Name} has been removed from inventory");
        }
        public void ShowItems()
        {
            int i = 1;
            Console.WriteLine("List of items:");
            foreach (Item item in Items)
            {
                Console.WriteLine($"{i}. Name - {item.Name};   Weight - {item.Weight}; " +
                    $"  Value - {item.Value};   Item type - {item.Type}");
            }
        }
    }

    public class Player 
    { 
        //in progress
    }

}                    
