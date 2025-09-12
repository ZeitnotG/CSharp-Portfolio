using System;
using System.Collections.Generic;

namespace RPGSystem
{
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
                Console.WriteLine($"{i + 1}. {item.Name} - Weight: {item.Weight}, Type: {item.Type}");
            }
            Console.WriteLine($"Total weight: {TotalWeight}\n");
        }

        public Item FindItemByName(string name)
        {
            foreach(Item item in Items)
            {
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return null;
        }
    }
}

