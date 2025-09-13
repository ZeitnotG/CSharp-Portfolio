using System;

namespace RPGSystem
{
    public class BattleService
    {
        public static void Battle(Player p1, Player p2)
        {
            Console.WriteLine($"Battle {p1.Name} vs {p2.Name} started!");
            int r = 1;
            while (p1.IsAlive == true && p2.IsAlive == true)
            {
                Console.WriteLine($"Round {r}:");
                p1.Attack(p2);
                if (p2.IsAlive == true)
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
    }
}

