using RPGSystem.Interfaces;
using System;

namespace RPGSystem
{

    public class Enemy : IDamageable, IAttacker
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public int Level { get; private set; }
        public int XPReward { get; private set; }
        public bool IsAlive { get; private set; }

        public Enemy(string name, int health, int attackPower, int defense, int level, int xPReward)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Defense = defense;
            Level = level;
            XPReward = xPReward;
            IsAlive = true;
        }

        public int TakeDamage(int amount)
        {
            int damage = amount > Defense ? amount - Defense : 1;
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} was injured to {damage} health point. Current health: {Health}");

            if(Health == 0)
            {
                Console.WriteLine($"{Name} is dead");
                IsAlive = false;
            }
            return damage;
        }

        public void Attack(IDamageable target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} ");
            int dealt = target.TakeDamage(AttackPower);
            Console.WriteLine($"{Name} dealt {dealt} damage.");
        }
    }
}