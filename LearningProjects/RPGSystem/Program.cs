using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RPGSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item knife = new Item("Knife", ItemType.Weapon, 3, attackBonus:10);
            Item dumbbell = new Item("Dumbbell", ItemType.Other, 20);

            Player player = new Player("Ivan", 30, 50, 10, 3);
            Player player1 = new Player("Lisa", 20, 40, 5, 6);


            player.AddItemToInventory(knife);
            player.Inventory.ShowItems();

            player1.AddItemToInventory(knife);
            player1.AddItemToInventory(dumbbell);
            player1.Inventory.ShowItems();
            Battle(player1, player);
            Console.WriteLine(player);
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
        public ItemType Type { get; set; }

        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; } 
        public int HealAmount { get; set; }

        public Item(string name, ItemType type, double weight, int attackBonus = 0, int defenseBonus = 0, int healAmount = 0)
        {
            Name = name;
            Weight = weight;
            Type = type;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
            HealAmount = healAmount;
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

    public class Player : ICarryingEntity, IDamageable
    {
        public string Name { get; private set; }
        public int Health {  get; private set; }
        public int MaxHealth {  get; private set; }
        public int AttackPower {  get; private set; }
        public int BaseAttack {  get; private set; }
        public int Defense {  get; private set; }
        public int BaseDefense {  get; private set; }
        public int Experience {  get; private set; }
        public int Level { get; private set; }
        public double MaxWeight { get; private set; }

        public Inventory Inventory { get; private set; }
        public Item EquippedWeapon {  get; private set; }
        public Item EquippedArmor {  get; private set; }

        public Dictionary<ItemType, Item> EquippedItems { get; private set; }

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
            BaseAttack = AttackPower;
            BaseDefense = Defense;

            EquippedItems = new Dictionary<ItemType, Item>();
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.AddItem(item);
        }

        public int TakeDamage(int amount)
        {
            if (amount > Defense)
            {
                Health -= amount - Defense;
                Health = Math.Max(0, Health);
                Console.WriteLine($"{Name} was injured to {amount - Defense} health point. Current health: {Health}/{MaxHealth}");
                CheckDeath();
                return amount - Defense;
            }
            else
            {
                Health = Math.Max(0, Health - 1);
                Console.WriteLine($"{Name} was injured to 1 health point. Current health: {Health}/{MaxHealth}");
                CheckDeath();
                return 1;
            }
        }

        public void Attack(IDamageable target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} ");
            int dealt = target.TakeDamage(AttackPower);
            Console.WriteLine($"{Name} нанес {dealt} урона.");
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

        public override string ToString()
        {
            return $"{Name} (Lvl {Level}) | HP: {Health}/{MaxHealth} | Atk: {AttackPower} | Def: {Defense} | XP: {Experience}";
        }

        public void EquipItem(Item item)
        {
            if (item.Type == ItemType.Potion)
            {
                UseItem(item);
                return;
            }

            else
                if (EquippedItems.ContainsKey(item.Type))
                {
                    UnequipItem(EquippedItems[item.Type]);
                    EquippedItems[item.Type] = item;
                    Console.WriteLine($"{item.Type} {item.Name} equipped");
                    RecalculateStats();
                }
            
        }

        public void UnequipItem(Item item)
        {
            if (EquippedItems.ContainsKey(item.Type))
            {
                EquippedItems.Remove(item.Type);
                Console.WriteLine($"{item.Type} {item.Name} unequipped");
                RecalculateStats();
            }
        }
        public void UseItem(Item item)
        {
            if (item.Type != ItemType.Potion)
            {
                Console.WriteLine($"{item.Name} not a potion");
                return;
            }
            else
            {
                int heal = item.HealAmount;
                int oldHealth = Health;
                Health = Math.Min(heal + Health, MaxHealth);
                Console.WriteLine($"{Name} used {item.Name} and restored {Health - oldHealth} HP. Current health: {Health}/{MaxHealth}");
                Inventory.RemoveItem(item);
            }
        }
        public void RecalculateStats()
        {
            AttackPower = BaseAttack;
            Defense = BaseDefense;
            foreach (var equipped in EquippedItems.Values)
            {
                AttackPower += equipped.AttackBonus;
                Defense += equipped.DefenseBonus;
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
        int TakeDamage(int amount);
        bool IsAlive {  get; }
    }
}

