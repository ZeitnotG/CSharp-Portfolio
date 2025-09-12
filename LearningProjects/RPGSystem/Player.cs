using System;
using System.Collections.Generic;

namespace RPGSystem
{
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
            Console.WriteLine($"{Name} gains {xp} xp");
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
}

