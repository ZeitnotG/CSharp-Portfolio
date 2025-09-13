using System;

namespace RPGSystem
{

    public class Enemy : IDamageable
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Defense { get; private set; }
        public int Level { get; private set; }
        public int XPReward { get; private set; }
        public bool IsAlive { get; private set; }

        public Enemy(string name, int health, int defense, int level, int xPReward)
        {
            Name = name;
            Health = health;
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
    }
}