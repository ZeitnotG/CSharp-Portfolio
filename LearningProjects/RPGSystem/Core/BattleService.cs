using RPGSystem.Interfaces;
using System;

namespace RPGSystem
{
    public class BattleService
    {
        public static void Battle(Player p1, Player p2)
        {
            Console.WriteLine($"Battle {p1.Name} vs {p2.Name} started!");
            int r = 1;
            while (p1.IsAlive && p2.IsAlive)
            {
                Console.WriteLine($"Round {r}:");
                p1.Attack(p2);
                if (p2.IsAlive)
                    p2.Attack(p1);
                r++;
            }
            if (p1.IsAlive)
            {
                Console.WriteLine($"{p1.Name} wins!");
                p1.GainExperience(p2.XPReward);
            }
            else if (p2.IsAlive)
            {
                Console.WriteLine($"{p2.Name} wins!");
                p2.GainExperience(p1.XPReward);
            }

        }

        public static void Battle(Player player, IDamageable target)
        {
           if(target is IAttacker attacker)
            {
                int r = 1;
                Console.WriteLine($"Battle {player.Name} vs {target.Name} started!");
                while (player.IsAlive && target.IsAlive)
                {
                    Console.WriteLine($"Round {r}:");
                    player.Attack(target);
                    if(target.IsAlive)
                        attacker.Attack(player);
                    r++;
                }
                if (player.IsAlive)
                {
                    Console.WriteLine($"{player.Name} kills {target.Name}");
                    player.GainExperience(target.XPReward);
                }
                else
                {
                    Console.WriteLine($"{target.Name} kills {player.Name}. Game over");
                }
            }
            else
            {
                while (target.IsAlive)
                {
                    player.Attack(target);
                }
            }
        }
    }
}

