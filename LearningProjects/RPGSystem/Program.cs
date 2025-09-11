using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RPGSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item knife = new Item("Knife", 3, 10, ItemType.Weapon);
            Item dumbbell = new Item("Dumbbell", 20, 0, ItemType.Other);

            Player player = new Player("Ivan", 30, 50, 10, 3);
            Player player1 = new Player("Lisa", 20, 40, 5, 6);


            player.AddItemToInventory(knife);
            player.Inventory.ShowItems();

            player1.AddItemToInventory(knife);
            player1.AddItemToInventory(dumbbell);
            player1.Inventory.ShowItems();
            Battle(player1, player);
        }
        public static void Battle(Player p1, Player p2)
        {
            Console.WriteLine($"Battle {p1.Name} vs {p2.Name} started!");
            int r = 1;
            while (p1.IsAlive == true && p2.IsAlive == true)
            {
                Console.WriteLine($"Round {r}:" );
                p1.Attack(p2);
                if(p2.IsAlive == true)
                p2.Attack(p1);
                r++;
            }

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

    public class Player : ICarryingEntity, IDamageable
    {
        public string Name { get; private set; }
        public int Health {  get; private set; }
        public int MaxHealth {  get; private set; }
        public int AttackPower {  get; private set; }
        public int Defense {  get; private set; }
        public int Experience {  get; private set; }
        public int Level { get; private set; }
        public double MaxWeight { get; private set; }
        public Inventory Inventory { get; private set; }
        public bool IsAlive { get; private set;}

        public Player(string name, double maxWeight, int maxHealth, int attackPower, int defense)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            AttackPower = attackPower;
            Defense = defense;
            Experience = 0;
            Level = 1;
            MaxWeight = maxWeight;
            Inventory = new Inventory(this);
            IsAlive = true;
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.AddItem(item);
        }

        public void TakeDamage(int amount)
        {
            if (amount > Defense)
            {
                Health -= amount - Defense;
                Health = Math.Max(0, Health);
                Console.WriteLine($"{Name} was injured to {amount - Defense} health point. Current health: {Health}/{MaxHealth}");
                CheckDeath();
            }
            else
            {
                Health = Math.Max(0, Health - 1);
                Console.WriteLine($"{Name} was injured to 1 health point. Current health: {Health}/{MaxHealth}");
                CheckDeath();
            }
        }

        public void Attack(IDamageable target)
        {
            target.TakeDamage(AttackPower);
        }

        public void Heal(int amount)
        {
            if (Health + amount <= MaxHealth)
            {
                Health += amount;
                Console.WriteLine($"{amount} health points restored. Health: {Health}/{MaxHealth}");
            }
            else
            {
                Health = MaxHealth;
                Console.WriteLine($"Your health is fully restored. Health: {Health}/{MaxHealth}");
            }
        }

        public void GainExperience(int xp)
        {
            Experience += xp;
            while(Experience >= 100 * Level)
            {
                Console.WriteLine($"{Name} has leveled up. " +
                    $"Max health, attack power and defense have been increased, your health is fully restored");
                LevelUp();
                Experience -= 100 * Level;
            }
        }

        public void LevelUp()
        {
            Level++;
            MaxHealth += 10;
            AttackPower += 2;
            Defense += 2;
            Health = MaxHealth;
        }

        public void CheckDeath()
        {
            if (Health == 0)
            {
                Console.WriteLine($"{Name} is dead");
                IsAlive = false;
            }
        }
    }
    public interface ICarryingEntity
    {
        string Name { get; }
        double MaxWeight { get; }
    }
    public interface IDamageable
    {
        string Name { get; }
        int Health {  get; }
        int Defense {  get; }
        void TakeDamage(int amount);
        bool IsAlive {  get; }
    }
}

