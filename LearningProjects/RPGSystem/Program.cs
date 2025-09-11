using System;
using System.Collections.Generic;

namespace RPGSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item knife = new Item("Knife", 3, 10, ItemType.Weapon);
            Item dumbbell = new Item("Dumbbell", 20, 0, ItemType.Other);

            Player player = new Player("Ivan", 1, 30);
            Player player1 = new Player("Lisa", 1, 20);


            player.AddItemToInventory(knife);
            player.Inventory.ShowItems();

            player1.AddItemToInventory(knife);
            player1.AddItemToInventory(dumbbell);
            player1.Inventory.ShowItems();
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
        public List<Item> Items { get; private set; }
        public double TotalWeight { get; private set; }
        public ICarryingEntity Owner { get; private set; }

        public Inventory(ICarryingEntity owner)
        {
            Owner = owner;
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            if (TotalWeight + item.Weight <= Owner.MaxWeight)
            {
                Items.Add(item);
                TotalWeight += item.Weight;
                Console.WriteLine($"Item {item.Name} added to {Owner.Name}'s inventory.");
            }
            else
            {
                Console.WriteLine($"{Owner.Name} cannot carry {item.Name}, too heavy!");
            }
        }

        public void RemoveItem(Item item)
        {
            if (Items.Remove(item))
            {
                TotalWeight -= item.Weight;
                Console.WriteLine($"Item {item.Name} removed from {Owner.Name}'s inventory.");
            }
        }

        public void ShowItems()
        {
            Console.WriteLine($"\n{Owner.Name}'s inventory:");
            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                Console.WriteLine($"{i + 1}. {item.Name} - Weight: {item.Weight}, Value: {item.Value}, Type: {item.Type}");
            }
            Console.WriteLine($"Total weight: {TotalWeight}\n");
        }
    }

    public class Player : ICarryingEntity
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public double MaxWeight { get; private set; }
        public Inventory Inventory { get; private set; }

        public Player(string name, int level, double maxWeight)
        {
            Name = name;
            Level = level;
            MaxWeight = maxWeight;
            Inventory = new Inventory(this);
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.AddItem(item);
        }
    }
    public interface ICarryingEntity
    {
        string Name { get; }
        double MaxWeight { get; }
    }
}

